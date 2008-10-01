using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace TOPwh
{
	public partial class MainFrm : Form
	{
		#region Variables

		Graphics gfxScreen;
		
		Bitmap bmpScreenBuffer, bmpStatic;
		Bitmap bmpPolygons, bmpPolygonCurrent, bmpPolygonSelected, bmpGrid;
		Graphics gfxScreenBuffer, gfxStatic;
		Graphics gfxPolygons, gfxPolygonCurrent, gfxPolygonSelected, gfxGrid;

		Point ptLast = new Point();
		Point ptCurrent = new Point();

		int iUserDraw = 0;

		long lTimerFreq = 0;

		String szApplicationPath;

		
		struct CurrentPolygon
		{
			public bool bIsOpen;
			public List<Point> listVertices;
			public Rectangle rcBoundingBoxLineStringWithoutCurrentVertex;
			public Rectangle rcBoundingBoxLineStringComplete;
			public int iContainerIndex;
		}

		CurrentPolygon cpCurrentDraw;


		struct CurrentDocument
		{
			public bool bChanged;
			public String szName;
			public String szFile;
			public List<CPolygon> listPolygons;
		}

		CurrentDocument cdCurrentDoc;


		private delegate void ToolboxOptionsDelegate();

		[Serializable]
		class ToolboxOptions : ISerializable
		{
			public ToolboxOptions()
			{
			}

			[NonSerialized]
			public ToolboxOptionsDelegate dgUpdateGrid;
			[NonSerialized]
			public ToolboxOptionsDelegate dgUpdatePolygonsAll;
			[NonSerialized]
			public ToolboxOptionsDelegate dgUpdatePolygonsFinished;
			[NonSerialized]
			public ToolboxOptionsDelegate dgUpdatePolygonSelected;
			[NonSerialized]
			public ToolboxOptionsDelegate dgUpdateScreen;
			[NonSerialized]
			public ToolboxOptionsDelegate dgUpdateTimerStepFrequency;


			const String szPolygonSettings = "1. Polygon Settings";
			const String szAlgorithmSettings = "2. Algorithm Settings";
			const String szGridSettings = "3. Grid Settings";
			const String szPolygonGraphics = "4. Polygon Graphics";
			const String szAlgorithmGraphics = "5. Algorithm Graphics";
			const String szLogSettings = "6. Log Settings";
			const String szGridGraphics = "7. Grid Graphics";


			#region Grid Settings

			bool bGridShowInternal = true;
			bool bGridSnapInternal = false;
			int iGridWidthInternal = 15;

			[CategoryAttribute(szGridSettings), DescriptionAttribute("Display or hide the grid.")]
			public bool ShowGrid
			{
				get
				{
					return bGridShowInternal;
				}

				set
				{
					bGridShowInternal = value;

					if (dgUpdateGrid != null)
						dgUpdateGrid.Invoke();
				}
			}

			[CategoryAttribute(szGridSettings), DescriptionAttribute("Density of grid lines.")]
			public int GridWidth
			{
				get
				{
					return iGridWidthInternal;
				}

				set
				{
					if (value > 0)
					{
						iGridWidthInternal = value;

						if (dgUpdateGrid != null)
							dgUpdateGrid.Invoke();
					}
				}
			}

			[CategoryAttribute(szGridSettings), DescriptionAttribute("Only allow drawing on grid points.")]
			public bool SnapToGrid
			{
				get
				{
					return bGridSnapInternal;
				}

				set
				{
					bGridSnapInternal = value;
				}
			}

			#endregion

			#region Grid Graphics

			public readonly Pen penGrid = new Pen(Color.Silver, 1);
			public readonly Pen penGridClip = new Pen(Color.MediumBlue, 2);

			[CategoryAttribute(szGridGraphics), DescriptionAttribute("Line thickness for drawing the grid.")]
			public float GridLineStrength
			{
				get
				{
					return penGrid.Width;
				}
				set
				{
					penGrid.Width = value;

					if (dgUpdateGrid != null)
						dgUpdateGrid.Invoke();
				}
			}

			[CategoryAttribute(szGridGraphics), DescriptionAttribute("Color to draw the grid lines.")]
			public Color GridLineColor
			{
				get
				{
					return penGrid.Color;
				}
				set
				{
					penGrid.Color = value;

					if (dgUpdateGrid != null)
						dgUpdateGrid.Invoke();
				}
			}
			
			#endregion


			#region Polygon Settings

			bool bPolygonDrawInternal = true;
			bool bPolygonShadeInternal = true;
			bool bPolygonHighlightInternal = true;

			[CategoryAttribute(szPolygonSettings), DescriptionAttribute("Draw polygons on the drawing area.")]
			public bool DrawPolygons
			{
				get
				{
					return bPolygonDrawInternal;
				}

				set
				{
					bPolygonDrawInternal = value;

					if (dgUpdatePolygonsAll != null)
						dgUpdatePolygonsAll.Invoke();
				}
			}

			[CategoryAttribute(szPolygonSettings), DescriptionAttribute("Translucent shading for polygons.")]
			public bool ShadePolygons
			{
				get
				{
					return bPolygonShadeInternal;
				}

				set
				{
					bPolygonShadeInternal = value;

					if (dgUpdatePolygonsAll != null)
						dgUpdatePolygonsAll.Invoke();
				}
			}

			[CategoryAttribute(szPolygonSettings), DescriptionAttribute("Draw selected polygon in different color.")]
			public bool HighlightSelection
			{
				get
				{
					return bPolygonHighlightInternal;
				}

				set
				{
					bPolygonHighlightInternal = value;

					if (dgUpdatePolygonSelected != null)
						dgUpdatePolygonSelected.Invoke();
				}
			}

			#endregion

			#region Polygon Graphics

			public readonly Pen penCurrentPolygon = new Pen(Color.Black, 2);
			
			public readonly Pen penFinishedPolygons = new Pen(Color.Gray, 2);
			public readonly SolidBrush brushFinischedPolygon = new SolidBrush(Color.FromArgb(60, Color.Gray));
			
			public readonly Pen penSelectedPolygon = new Pen(Color.Red, 2);
			public readonly SolidBrush brushSelectedPolygon = new SolidBrush(Color.FromArgb(30, Color.Red));


			[CategoryAttribute(szPolygonGraphics), DescriptionAttribute("Line thickness for drawing polygon vertices.")]
			public float PolygonLineStrength
			{
				get
				{
					return penFinishedPolygons.Width;
				}
				set
				{
					penFinishedPolygons.Width = value;

					if (dgUpdatePolygonsFinished != null)
						dgUpdatePolygonsFinished.Invoke();
				}
			}

			[CategoryAttribute(szPolygonGraphics), DescriptionAttribute("Color for polygon drawing.")]
			public Color PolygonColor
			{
				get
				{
					return penFinishedPolygons.Color;
				}
				set
				{
					penFinishedPolygons.Color = value;
					brushFinischedPolygon.Color = Color.FromArgb(brushFinischedPolygon.Color.A, penFinishedPolygons.Color);

					if (dgUpdatePolygonsFinished != null)
						dgUpdatePolygonsFinished.Invoke();
				}
			}

			[CategoryAttribute(szPolygonGraphics), DescriptionAttribute("Opacity for shading polygons.")]
			public byte PolygonShadeOpacity
			{
				get
				{
					return brushFinischedPolygon.Color.A;
				}
				set
				{
					brushFinischedPolygon.Color = Color.FromArgb(value, brushFinischedPolygon.Color);

					if (dgUpdatePolygonsFinished != null)
						dgUpdatePolygonsFinished.Invoke();
				}
			}


			[CategoryAttribute(szPolygonGraphics), DescriptionAttribute("Line thickness for drawing the selected polygon's vertices.")]
			public float SelectionLineStrength
			{
				get
				{
					return penSelectedPolygon.Width;
				}
				set
				{
					penSelectedPolygon.Width = value;

					if (dgUpdatePolygonSelected != null)
						dgUpdatePolygonSelected.Invoke();
				}
			}

			[CategoryAttribute(szPolygonGraphics), DescriptionAttribute("Color of the selected polygon's vertices.")]
			public Color SelectionColor
			{
				get
				{
					return penSelectedPolygon.Color;
				}
				set
				{
					penSelectedPolygon.Color = value;
					brushSelectedPolygon.Color = Color.FromArgb(brushSelectedPolygon.Color.A, penSelectedPolygon.Color);

					if (dgUpdatePolygonSelected != null)
						dgUpdatePolygonSelected.Invoke();
				}
			}

			[CategoryAttribute(szPolygonGraphics), DescriptionAttribute("Opacity for shading the selected polygon.")]
			public byte SelectionShadeOpacity
			{
				get
				{
					return brushSelectedPolygon.Color.A;
				}
				set
				{
					brushSelectedPolygon.Color = Color.FromArgb(value, brushSelectedPolygon.Color);

					if (dgUpdatePolygonSelected != null)
						dgUpdatePolygonSelected.Invoke();
				}
			}
			
			#endregion


			#region Algorithm Settings

			int iAlgorithmStepsInternal = 1;
			int iAlgorithmrequencyInternal = 200;
			bool bAlgorithmDrawInternal = true;
			bool bAlgorithmStepBeep = true;
			bool bAlgorithmAddLogEntry = true;
			bool bAlgorithmClearLogBeforeRun = true;
			bool bAlgorithmShowConnectedPolygons = false;
			bool bAlgorithmShadeConnectedPolygons = false;
			bool bAlgorithmShowConnections = true;
			bool bAlgorithmShowDiagonals = true;
			bool bAlgorithmShowStateObjects = true;
			int iAlgorithmLevel = -1;

			[CategoryAttribute(szAlgorithmSettings), DescriptionAttribute("Show the algorithm's result on the drawing area.")]
			public bool DrawAlgorithmData
			{
				get
				{
					return bAlgorithmDrawInternal;
				}

				set
				{
					bAlgorithmDrawInternal = value;
					
					if (dgUpdateScreen != null)
						dgUpdateScreen.Invoke();
				}
			}

			[CategoryAttribute(szAlgorithmSettings), DescriptionAttribute("Play an audio signal everytime time a step is performed.")]
			public bool BeepOnStep
			{
				get
				{
					return bAlgorithmStepBeep;
				}

				set
				{
					bAlgorithmStepBeep = value;
				}
			}

			[CategoryAttribute(szAlgorithmSettings), DescriptionAttribute("Add an explanatory log entry for each step.")]
			public bool AddLogEntries
			{
				get
				{
					return bAlgorithmAddLogEntry;
				}

				set
				{
					bAlgorithmAddLogEntry = value;
				}
			}

			[CategoryAttribute(szAlgorithmSettings), DescriptionAttribute("Clear the log window before running an algorithm.")]
			public bool ClearLogBeforeRun
			{
				get
				{
					return bAlgorithmClearLogBeforeRun;
				}

				set
				{
					bAlgorithmClearLogBeforeRun = value;
				}
			}

			[CategoryAttribute(szAlgorithmSettings), DescriptionAttribute("Number of steps to advance on each lick on the step button.")]
			public int StepsPerClick
			{
				get
				{
					return iAlgorithmStepsInternal;
				}

				set
				{
					if(value >= 1)
						iAlgorithmStepsInternal = value;
				}
			}

			[CategoryAttribute(szAlgorithmSettings), DescriptionAttribute("The frequency of steps in slow motion mode (in milliseconds).")]
			public int StepFrequency
			{
				get
				{
					return iAlgorithmrequencyInternal;
				}

				set
				{
					if (value > 0)
					{
						iAlgorithmrequencyInternal = value;

						if (dgUpdateTimerStepFrequency != null)
							dgUpdateTimerStepFrequency.Invoke();
					}
				}
			}

			[CategoryAttribute(szAlgorithmSettings), DescriptionAttribute("Draw the connected polygons on the screen.")]
			public bool DrawConnectedPolygons
			{
				get
				{
					return bAlgorithmShowConnectedPolygons;
				}

				set
				{
					bAlgorithmShowConnectedPolygons = value;

					if (dgUpdateScreen != null)
						dgUpdateScreen.Invoke();
				}
			}

			[CategoryAttribute(szAlgorithmSettings), DescriptionAttribute("Shade the connected polygons.")]
			public bool ShadeConnectedPolygons
			{
				get
				{
					return bAlgorithmShadeConnectedPolygons;
				}

				set
				{
					bAlgorithmShadeConnectedPolygons = value;

					if (dgUpdateScreen != null)
						dgUpdateScreen.Invoke();
				}
			}

			[CategoryAttribute(szAlgorithmSettings), DescriptionAttribute("Draw the connections between polygons.")]
			public bool DrawConnections
			{
				get
				{
					return bAlgorithmShowConnections;
				}

				set
				{
					bAlgorithmShowConnections = value;

					if (dgUpdateScreen != null)
						dgUpdateScreen.Invoke();
				}
			}

			[CategoryAttribute(szAlgorithmSettings), DescriptionAttribute("Draw the diagonals for triangulation.")]
			public bool DrawDiagonals
			{
				get
				{
					return bAlgorithmShowDiagonals;
				}

				set
				{
					bAlgorithmShowDiagonals = value;

					if (dgUpdateScreen != null)
						dgUpdateScreen.Invoke();
				}
			}

			[CategoryAttribute(szAlgorithmSettings), DescriptionAttribute("Draw state objects (i.e. vertices and points currently investigated by the algorithm).")]
			public bool DrawCurrentState
			{
				get
				{
					return bAlgorithmShowStateObjects;
				}

				set
				{
					bAlgorithmShowStateObjects = value;

					if (dgUpdateScreen != null)
						dgUpdateScreen.Invoke();
				}
			}

			[CategoryAttribute(szAlgorithmSettings), DescriptionAttribute("Level of polygons up to which the algorithm will proceed. Set to -1 for infinite.")]
			public int TriangulationLevel
			{
				get
				{
					return iAlgorithmLevel;
				}

				set
				{
					if(value > 0 || value == -1)
						iAlgorithmLevel = value;
				}
			}

			#endregion

			#region Algorithm Graphics

			public readonly Pen penAlgorithmConnectedPolygons = new Pen(Color.Brown, 4);
			public readonly SolidBrush brushAlgorithmConnectedPolygons = new SolidBrush(Color.FromArgb(30, Color.Brown));

			public readonly Pen penAlgorithmStack = new Pen(Color.DarkKhaki, 2);
			public readonly SolidBrush brushAlgorithmStack = new SolidBrush(Color.FromArgb(150, Color.DarkKhaki));

			public readonly Pen penAlgorithmConnections = new Pen(Color.Brown, 3);
			public readonly Pen penAlgorithmDiagonals = new Pen(Color.DarkCyan, 1);

			public readonly Pen penAlgorithmCurrentVertex = new Pen(Color.Magenta, 2);
			public readonly Pen penAlgorithmCurrentPoint = new Pen(Color.Magenta, 8);
			
			public readonly Pen penAlgorithmCurrentPointLock = new Pen(Color.Black, 8);
			public readonly Pen penAlgorithmCurrentVertexLock = new Pen(Color.Black, 2);

			public readonly Pen penAlgorithmCurrentPolygon = new Pen(Color.Orange, 2);
			public readonly SolidBrush brushAlgorithmCurrentPolygon = new SolidBrush(Color.FromArgb(80, Color.Orange));


			[CategoryAttribute(szAlgorithmGraphics), DescriptionAttribute("Line thickness for drawing diagonals.")]
			public float DiagonalsLineStrength
			{
				get
				{
					return penAlgorithmDiagonals.Width;
				}
				set
				{
					penAlgorithmDiagonals.Width = value;
				
					if (dgUpdateScreen != null)
						dgUpdateScreen.Invoke();
				}
			}

			[CategoryAttribute(szAlgorithmGraphics), DescriptionAttribute("Color to draw diagonals in.")]
			public Color DiagonalsLineColor
			{
				get
				{
					return penAlgorithmDiagonals.Color;
				}
				set
				{
					penAlgorithmDiagonals.Color = value;

					if (dgUpdateScreen != null)
						dgUpdateScreen.Invoke();
				}
			}


			[CategoryAttribute(szAlgorithmGraphics), DescriptionAttribute("Line thickness for drawing connected polygons' vertices.")]
			public float ConnectedPolygonsLineStrength
			{
				get
				{
					return penAlgorithmConnectedPolygons.Width;
				}
				set
				{
					penAlgorithmConnectedPolygons.Width = value;

					if (dgUpdateScreen != null)
						dgUpdateScreen.Invoke();
				}
			}

			[CategoryAttribute(szAlgorithmGraphics), DescriptionAttribute("Color for drawing connected polygons.")]
			public Color ConnectedPolygonsColor
			{
				get
				{
					return penAlgorithmConnectedPolygons.Color;
				}
				set
				{
					penAlgorithmConnectedPolygons.Color = value;
					brushAlgorithmConnectedPolygons.Color = Color.FromArgb(brushAlgorithmConnectedPolygons.Color.A, penAlgorithmConnectedPolygons.Color);

					if (dgUpdateScreen != null)
						dgUpdateScreen.Invoke();
				}
			}

			[CategoryAttribute(szAlgorithmGraphics), DescriptionAttribute("Opacity for shading connected polygons.")]
			public byte ConnectedPolygonsOpacity
			{
				get
				{
					return brushAlgorithmConnectedPolygons.Color.A;
				}
				set
				{
					brushAlgorithmConnectedPolygons.Color = Color.FromArgb(value, brushAlgorithmConnectedPolygons.Color);

					if (dgUpdateScreen != null)
						dgUpdateScreen.Invoke();
				}
			}


			[CategoryAttribute(szAlgorithmGraphics), DescriptionAttribute("Line thickness for drawing investigated polygons' vertices.")]
			public float InvestigatedPolygonsLineStrength
			{
				get
				{
					return penAlgorithmCurrentPolygon.Width;
				}
				set
				{
					penAlgorithmCurrentPolygon.Width = value;

					if (dgUpdateScreen != null)
						dgUpdateScreen.Invoke();
				}
			}

			[CategoryAttribute(szAlgorithmGraphics), DescriptionAttribute("Color for drawing investigated polygons.")]
			public Color InvestigatedPolygonsColor
			{
				get
				{
					return penAlgorithmCurrentPolygon.Color;
				}
				set
				{
					penAlgorithmCurrentPolygon.Color = value;
					brushAlgorithmCurrentPolygon.Color = Color.FromArgb(brushAlgorithmCurrentPolygon.Color.A, penAlgorithmCurrentPolygon.Color);

					if (dgUpdateScreen != null)
						dgUpdateScreen.Invoke();
				}
			}

			[CategoryAttribute(szAlgorithmGraphics), DescriptionAttribute("Opacity for shading investigated polygons.")]
			public byte InvestigatedPolygonsOpacity
			{
				get
				{
					return brushAlgorithmCurrentPolygon.Color.A;
				}
				set
				{
					brushAlgorithmCurrentPolygon.Color = Color.FromArgb(value, brushAlgorithmCurrentPolygon.Color);

					if (dgUpdateScreen != null)
						dgUpdateScreen.Invoke();
				}
			}


			[CategoryAttribute(szAlgorithmGraphics), DescriptionAttribute("Line thickness for drawing polygon stack.")]
			public float StackedPolygonsLineStrength
			{
				get
				{
					return penAlgorithmStack.Width;
				}
				set
				{
					penAlgorithmStack.Width = value;

					if (dgUpdateScreen != null)
						dgUpdateScreen.Invoke();
				}
			}

			[CategoryAttribute(szAlgorithmGraphics), DescriptionAttribute("Color for drawing polygon stack.")]
			public Color StackedPolygonsColor
			{
				get
				{
					return penAlgorithmStack.Color;
				}
				set
				{
					penAlgorithmStack.Color = value;
					brushAlgorithmStack.Color = Color.FromArgb(brushAlgorithmStack.Color.A, penAlgorithmStack.Color);

					if (dgUpdateScreen != null)
						dgUpdateScreen.Invoke();
				}
			}

			[CategoryAttribute(szAlgorithmGraphics), DescriptionAttribute("Opacity for shading polygon stack.")]
			public byte StackedPolygonsOpacity
			{
				get
				{
					return brushAlgorithmStack.Color.A;
				}
				set
				{
					brushAlgorithmStack.Color = Color.FromArgb(value, brushAlgorithmStack.Color);

					if (dgUpdateScreen != null)
						dgUpdateScreen.Invoke();
				}
			}


			[CategoryAttribute(szAlgorithmGraphics), DescriptionAttribute("Line thickness for drawing polygon connections.")]
			public float ConnectionsLineStrength
			{
				get
				{
					return penAlgorithmConnections.Width;
				}
				set
				{
					penAlgorithmConnections.Width = value;

					if (dgUpdateScreen != null)
						dgUpdateScreen.Invoke();
				}
			}

			[CategoryAttribute(szAlgorithmGraphics), DescriptionAttribute("Color for drawing polygon connections.")]
			public Color ConnectionsColor
			{
				get
				{
					return penAlgorithmConnections.Color;
				}
				set
				{
					penAlgorithmConnections.Color = value;

					if (dgUpdateScreen != null)
						dgUpdateScreen.Invoke();
				}
			}


			[CategoryAttribute(szAlgorithmGraphics), DescriptionAttribute("Line thickness for drawing vertices currently under investigation.")]
			public float CurrentVerticesLineStrength
			{
				get
				{
					return penAlgorithmCurrentVertex.Width;
				}
				set
				{
					penAlgorithmCurrentVertex.Width = value;

					if (dgUpdateScreen != null)
						dgUpdateScreen.Invoke();
				}
			}

			[CategoryAttribute(szAlgorithmGraphics), DescriptionAttribute("Color for drawing vertices currently under investigation.")]
			public Color CurrentVerticesColor
			{
				get
				{
					return penAlgorithmCurrentVertex.Color;
				}
				set
				{
					penAlgorithmCurrentVertex.Color = value;

					if (dgUpdateScreen != null)
						dgUpdateScreen.Invoke();
				}
			}


			[CategoryAttribute(szAlgorithmGraphics), DescriptionAttribute("Line thickness for drawing points currently under investigation.")]
			public float CurrentPointLineStrength
			{
				get
				{
					return penAlgorithmCurrentPoint.Width;
				}
				set
				{
					penAlgorithmCurrentPoint.Width = value;

					if (dgUpdateScreen != null)
						dgUpdateScreen.Invoke();
				}
			}

			[CategoryAttribute(szAlgorithmGraphics), DescriptionAttribute("Color for drawing points currently under investigation.")]
			public Color CurrentPointColor
			{
				get
				{
					return penAlgorithmCurrentPoint.Color;
				}
				set
				{
					penAlgorithmCurrentPoint.Color = value;

					if (dgUpdateScreen != null)
						dgUpdateScreen.Invoke();
				}
			}


			[CategoryAttribute(szAlgorithmGraphics), DescriptionAttribute("Line thickness for drawing points currently locked.")]
			public float LockedPointLineStrength
			{
				get
				{
					return penAlgorithmCurrentPointLock.Width;
				}
				set
				{
					penAlgorithmCurrentPointLock.Width = value;

					if (dgUpdateScreen != null)
						dgUpdateScreen.Invoke();
				}
			}

			[CategoryAttribute(szAlgorithmGraphics), DescriptionAttribute("Color for drawing points currently locked.")]
			public Color LockedPointColor
			{
				get
				{
					return penAlgorithmCurrentPointLock.Color;
				}
				set
				{
					penAlgorithmCurrentPointLock.Color = value;

					if (dgUpdateScreen != null)
						dgUpdateScreen.Invoke();
				}
			}


			[CategoryAttribute(szAlgorithmGraphics), DescriptionAttribute("Color for drawing segments currently locked.")]
			public Color LockedSegmentColor
			{
				get
				{
					return penAlgorithmCurrentVertexLock.Color;
				}
				set
				{
					penAlgorithmCurrentVertexLock.Color = value;

					if (dgUpdateScreen != null)
						dgUpdateScreen.Invoke();
				}
			}

			[CategoryAttribute(szAlgorithmGraphics), DescriptionAttribute("Line thickness for drawing segments currently locked.")]
			public float LockedSegmentLineStrength
			{
				get
				{
					return penAlgorithmCurrentVertexLock.Width;
				}
				set
				{
					penAlgorithmCurrentVertexLock.Width = value;

					if (dgUpdateScreen != null)
						dgUpdateScreen.Invoke();
				}
			}

			#endregion


			#region Log Settings

			bool bLogEnabled = true;
			bool bLogBeepDisabled = false;

			[CategoryAttribute(szLogSettings), DescriptionAttribute("Enable information output in the log window.")]
			public bool LogEnabled
			{
				get
				{
					return bLogEnabled;
				}

				set
				{
					bLogEnabled = value;
				}
			}

			[CategoryAttribute(szLogSettings), DescriptionAttribute("Enable or disable audio signals for log entries.")]
			public bool LogBeepDisabled
			{
				get
				{
					return bLogBeepDisabled;
				}

				set
				{
					bLogBeepDisabled = value;
				}
			}

			#endregion


			#region Serialization
				
			void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
			{
				info.AddValue("bGridShowInternal", bGridShowInternal);
				info.AddValue("bGridSnapInternal", bGridSnapInternal);
				info.AddValue("iGridWidthInternal", iGridWidthInternal);

				SerializeHelperSerializePen(info, penGrid, "penGrid");
				SerializeHelperSerializePen(info, penGridClip, "penGridClip");

				info.AddValue("bPolygonDrawInternal", bPolygonDrawInternal);
				info.AddValue("bPolygonShadeInternal", bPolygonShadeInternal);
				info.AddValue("bPolygonHighlightInternal", bPolygonHighlightInternal);

				SerializeHelperSerializePen(info, penCurrentPolygon, "penCurrentPolygon");
				SerializeHelperSerializePen(info, penFinishedPolygons, "penFinishedPolygons");
				SerializeHelperSerializeBrush(info, brushFinischedPolygon, "brushFinischedPolygon");
				SerializeHelperSerializePen(info, penSelectedPolygon, "penSelectedPolygon");
				SerializeHelperSerializeBrush(info, brushSelectedPolygon, "brushSelectedPolygon");

				info.AddValue("iAlgorithmStepsInternal", iAlgorithmStepsInternal);
				info.AddValue("iAlgorithmrequencyInternal", iAlgorithmrequencyInternal);
				info.AddValue("bAlgorithmDrawInternal", bAlgorithmDrawInternal);
				info.AddValue("bAlgorithmStepBeep", bAlgorithmStepBeep);
				info.AddValue("bAlgorithmAddLogEntry", bAlgorithmAddLogEntry);
				info.AddValue("bAlgorithmClearLogBeforeRun", bAlgorithmClearLogBeforeRun);
				info.AddValue("bAlgorithmShowConnectedPolygons", bAlgorithmShowConnectedPolygons);
				info.AddValue("bAlgorithmShadeConnectedPolygons", bAlgorithmShadeConnectedPolygons);
				info.AddValue("bAlgorithmShowConnections", bAlgorithmShowConnections);
				info.AddValue("bAlgorithmShowDiagonals", bAlgorithmShowDiagonals);
				info.AddValue("bAlgorithmShowStateObjects", bAlgorithmShowStateObjects);
				info.AddValue("iAlgorithmLevel", iAlgorithmLevel);
			
				SerializeHelperSerializePen(info, penAlgorithmConnectedPolygons, "penAlgorithmConnectedPolygons");
				SerializeHelperSerializeBrush(info, brushAlgorithmConnectedPolygons, "brushAlgorithmConnectedPolygons");
				SerializeHelperSerializePen(info, penAlgorithmStack, "penAlgorithmStack");
				SerializeHelperSerializeBrush(info, brushAlgorithmStack, "brushAlgorithmStack");
				SerializeHelperSerializePen(info, penAlgorithmConnections, "penAlgorithmConnections");
				SerializeHelperSerializePen(info, penAlgorithmDiagonals, "penAlgorithmDiagonals");
				SerializeHelperSerializePen(info, penAlgorithmCurrentVertex, "penAlgorithmCurrentVertex");
				SerializeHelperSerializePen(info, penAlgorithmCurrentPoint, "penAlgorithmCurrentPoint");
				SerializeHelperSerializePen(info, penAlgorithmCurrentPointLock, "penAlgorithmCurrentPointLock");
				SerializeHelperSerializePen(info, penAlgorithmCurrentVertexLock, "penAlgorithmCurrentVertexLock");
				SerializeHelperSerializePen(info, penAlgorithmCurrentPolygon, "penAlgorithmCurrentPolygon");
				SerializeHelperSerializeBrush(info, brushAlgorithmCurrentPolygon, "brushAlgorithmCurrentPolygon");

				info.AddValue("bLogEnabled", bLogEnabled);
				info.AddValue("bLogBeepDisabled", bLogBeepDisabled);
			}

			protected ToolboxOptions(SerializationInfo info, StreamingContext context)
			{
				bGridShowInternal = info.GetBoolean("bGridShowInternal");
				bGridSnapInternal = info.GetBoolean("bGridSnapInternal");
				iGridWidthInternal = info.GetInt32("iGridWidthInternal");

				SerializeHelperDeserializePen(info, penGridClip, "penGridClip");
				SerializeHelperDeserializePen(info, penGrid, "penGrid");

				bPolygonDrawInternal = info.GetBoolean("bPolygonDrawInternal");
				bPolygonShadeInternal = info.GetBoolean("bPolygonShadeInternal");
				bPolygonHighlightInternal = info.GetBoolean("bPolygonHighlightInternal");

				SerializeHelperDeserializePen(info, penCurrentPolygon, "penCurrentPolygon");
				SerializeHelperDeserializePen(info, penFinishedPolygons, "penFinishedPolygons");
				SerializeHelperDeserializeBrush(info, brushFinischedPolygon, "brushFinischedPolygon");
				SerializeHelperDeserializePen(info, penSelectedPolygon, "penSelectedPolygon");
				SerializeHelperDeserializeBrush(info, brushSelectedPolygon, "brushSelectedPolygon");

				iAlgorithmStepsInternal = info.GetInt32("iAlgorithmStepsInternal");
				iAlgorithmrequencyInternal = info.GetInt32("iAlgorithmrequencyInternal");
				bAlgorithmDrawInternal = info.GetBoolean("bAlgorithmDrawInternal");
				bAlgorithmStepBeep = info.GetBoolean("bAlgorithmStepBeep");
				bAlgorithmAddLogEntry = info.GetBoolean("bAlgorithmAddLogEntry");
				bAlgorithmClearLogBeforeRun = info.GetBoolean("bAlgorithmClearLogBeforeRun");
				bAlgorithmShowConnectedPolygons = info.GetBoolean("bAlgorithmShowConnectedPolygons");
				bAlgorithmShadeConnectedPolygons = info.GetBoolean("bAlgorithmShadeConnectedPolygons");
				bAlgorithmShowConnections = info.GetBoolean("bAlgorithmShowConnections");
				bAlgorithmShowDiagonals = info.GetBoolean("bAlgorithmShowDiagonals");
				bAlgorithmShowStateObjects = info.GetBoolean("bAlgorithmShowStateObjects");
				iAlgorithmLevel = info.GetInt32("iAlgorithmLevel");

				SerializeHelperDeserializePen(info, penAlgorithmConnectedPolygons, "penAlgorithmConnectedPolygons");
				SerializeHelperDeserializeBrush(info, brushAlgorithmConnectedPolygons, "brushAlgorithmConnectedPolygons");
				SerializeHelperDeserializePen(info, penAlgorithmStack, "penAlgorithmStack");
				SerializeHelperDeserializeBrush(info, brushAlgorithmStack, "brushAlgorithmStack");
				SerializeHelperDeserializePen(info, penAlgorithmConnections, "penAlgorithmConnections");
				SerializeHelperDeserializePen(info, penAlgorithmDiagonals, "penAlgorithmDiagonals");
				SerializeHelperDeserializePen(info, penAlgorithmCurrentVertex, "penAlgorithmCurrentVertex");
				SerializeHelperDeserializePen(info, penAlgorithmCurrentPoint, "penAlgorithmCurrentPoint");
				SerializeHelperDeserializePen(info, penAlgorithmCurrentPointLock, "penAlgorithmCurrentPointLock");
				SerializeHelperDeserializePen(info, penAlgorithmCurrentVertexLock, "penAlgorithmCurrentVertexLock");
				SerializeHelperDeserializePen(info, penAlgorithmCurrentPolygon, "penAlgorithmCurrentPolygon");
				SerializeHelperDeserializeBrush(info, brushAlgorithmCurrentPolygon, "brushAlgorithmCurrentPolygon");

				bLogEnabled = info.GetBoolean("bLogEnabled");
				bLogBeepDisabled = info.GetBoolean("bLogBeepDisabled");

				dgUpdateGrid = null;
				dgUpdatePolygonsAll = null;
				dgUpdatePolygonsFinished = null;
				dgUpdatePolygonSelected = null;
				dgUpdateScreen = null;
				dgUpdateTimerStepFrequency = null;
			}
			

			void SerializeHelperSerializePen(SerializationInfo info, Pen pen, String name)
			{
				info.AddValue(name + "Color", pen.Color);
				info.AddValue(name + "Width", pen.Width);
			}

			void SerializeHelperDeserializePen(SerializationInfo info, Pen pen, String name)
			{
				pen.Color = (Color)info.GetValue(name + "Color", pen.Color.GetType());
				pen.Width = info.GetSingle(name + "Width");
			}

			void SerializeHelperSerializeBrush(SerializationInfo info, SolidBrush brush, String name)
			{
				info.AddValue(name, brush.Color);
			}

			void SerializeHelperDeserializeBrush(SerializationInfo info, SolidBrush brush, String name)
			{
				brush.Color = (Color)info.GetValue(name, brush.Color.GetType());
			}


			#endregion
		}

		ToolboxOptions tboCurrentApp;
		

		#endregion



		#region Win32
		[DllImport("user32.dll")]
		static extern void MessageBeep(uint uType);

		const uint MB_OK = 0x00000000;

		const uint MB_ICONHAND = 0x00000010;
		const uint MB_ICONQUESTION = 0x00000020;
		const uint MB_ICONEXCLAMATION = 0x00000030;
		const uint MB_ICONASTERISK = 0x00000040;

		[DllImport("Kernel32.dll")]
		private static extern bool QueryPerformanceCounter(out long lpPerformanceCount);

		[DllImport("Kernel32.dll")]
		private static extern bool QueryPerformanceFrequency(out long lpFrequency);

		#endregion


		#region Initialization & Exit
		
		bool bInitialized = false;
		
		/// <summary>
		/// Constructor for the main window class
		/// </summary>
		/// <remarks>This method performs all necessary initialization regarding the document managment, user interface
		/// and graphics setup.</remarks>
		public MainFrm()
		{
			InitializeComponent();

			szApplicationPath = Environment.CurrentDirectory;


			// Current document and draw structure initialization
			initCurrentDocStruct(ref cdCurrentDoc);
			initCurrentDrawStruct(ref cpCurrentDraw);
			initCurrentAlgorithmStruct(ref caCurrentAlg);


			// Initialize properties that can be changed with the property grid
			SettingsLoad();


			// High performance timer initialization
			if (QueryPerformanceFrequency(out lTimerFreq) == false)
			{
				// High-performance counter not supported
				throw new Win32Exception();
			}


			// Graphics initialization
			GraphicsInit();


			// Create a new document
			DocumentNew();

			// Update screen
			RenderObjectsAll();
			DrawScreen();


			// Finally set initial user interface states
			splitContainerToolbox.Panel2Collapsed = !showToolboxToolStripMenuItem.Checked;
			splitContainerLogWindow.Panel2Collapsed = !showLogWindowToolStripMenuItem.Checked;
			statusStrip1.Visible = showStatusBarToolStripMenuItem.Checked;

			toolStripFile.Visible = standardToolStripMenuItem.Checked;
			toolStripView.Visible = viewToolStripMenuItem2.Checked;
			toolStripDrawing.Visible = drawToolStripMenuItem.Checked;
			toolStripControl.Visible = controlToolStripMenuItem1.Checked;

			toolStripControl.Enabled = false;

			// Done
			bInitialized = true;
		}

		private void Form1_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (!DocumentClose())
				e.Cancel = true;
			else
			{
				SettingsSave();
			}
		}

		#endregion



		#region Drawing Area

		private void DrawModeBegin()
		{
			if (cpCurrentDraw.bIsOpen)
				return;

			iUserDraw = 1;
			toolStripButtonDrawMode.Checked = true;
			toolStripControl.Enabled = false;
		}

		private bool DrawModeEnd()
		{
			if (cpCurrentDraw.bIsOpen)
				if (!closeCurrentPolygon())
					return false;

			DrawModeReset();

			return true;
		}

		private void DrawModeReset()
		{
			iUserDraw = 0;
			resetCurrentPolygon();
			toolStripButtonDrawMode.Checked = false;

			if(cdCurrentDoc.listPolygons.Count > 0)
				toolStripControl.Enabled = true;
			else
				toolStripControl.Enabled = false;
		}


		private void initCurrentDrawStruct(ref CurrentPolygon cpInit)
		{
			cpInit.bIsOpen = false;
			cpInit.iContainerIndex = -1;
			cpInit.rcBoundingBoxLineStringComplete = new Rectangle(0, 0, 0, 0);
			cpInit.rcBoundingBoxLineStringWithoutCurrentVertex = new Rectangle(0, 0, 0, 0);

			if (cpCurrentDraw.listVertices == null)
				cpCurrentDraw.listVertices = new List<Point>(20);
			else
				cpCurrentDraw.listVertices.Clear();
		}


		private void startCurrentPolygon()
		{
			initCurrentDrawStruct(ref cpCurrentDraw);
			cpCurrentDraw.bIsOpen = true;
		}

		private bool closeCurrentPolygon()
		{
			// TODO: This function contains redundant code with the OnClick() handler for the drawing area (eg. intersection tests). Maybe isolate this code to seperate function.
			// TODO: This function uses cpCurrentDraw.listVertices[0] and cpCurrentDraw.listVertices[cpCurrentDraw.listVertices.Count - 1] in lots of loops. Performance could be improved by creating a reference to this points once and then working with the reference in the loops.


			// Make sure all but the first and the last vertex don't intersect with the closing vertex
			for (int i = 1; i < cpCurrentDraw.listVertices.Count - 2; i++)
			{
				if (LineStringIntersection(cpCurrentDraw.listVertices[0], cpCurrentDraw.listVertices[cpCurrentDraw.listVertices.Count - 1], cpCurrentDraw.listVertices[i], cpCurrentDraw.listVertices[i + 1]) != LINESTRINGINTERSECTIONRESULT.INTERSECTION_NONE)
				{
					LogAddEntry("Cannot close polygon. Closing vertex would intersect with current line string. Only simple polygons allowed!", "IconError", true);
					return false;
				}
			}


			// TODO: Not sure if the following test is really necessary. Think about it again.
			
			// Make sure the first as well as the last vertex intersect with the closing vertex only in one point each (namely there end points)
			if (LineStringIntersection(cpCurrentDraw.listVertices[0], cpCurrentDraw.listVertices[cpCurrentDraw.listVertices.Count - 1], cpCurrentDraw.listVertices[cpCurrentDraw.listVertices.Count - 1], cpCurrentDraw.listVertices[cpCurrentDraw.listVertices.Count - 2]) != LINESTRINGINTERSECTIONRESULT.INTERSECTION_POINT ||
				LineStringIntersection(cpCurrentDraw.listVertices[0], cpCurrentDraw.listVertices[cpCurrentDraw.listVertices.Count - 1], cpCurrentDraw.listVertices[0], cpCurrentDraw.listVertices[1]) != LINESTRINGINTERSECTIONRESULT.INTERSECTION_POINT)
			{
				LogAddEntry("Cannot close polygon. Closing vertex would intersect with current line string. Only simple polygons allowed!", "IconError", true);
				return false;
			}


			// TODO: Replace the intersectsWith function, using a function that also receives the bounding box and performs a quicktest before looping throught all vertices
			
			// Make sure that the closing vertex will not intersect with containing polygon (if there is any) or any other polygons on the same tree level as the current one
			TreeNode[] nodesContainer = treeViewPolygons.Nodes.Find(cpCurrentDraw.iContainerIndex.ToString(), true);
			if (nodesContainer.GetLength(0) != 1)
				throw new Exception("Polygon list sync exception!");

			if (cpCurrentDraw.iContainerIndex != -1)
			{
				if (cdCurrentDoc.listPolygons[cpCurrentDraw.iContainerIndex].intersectsWith(new CSegment(new CPoint(cpCurrentDraw.listVertices[0]), new CPoint(cpCurrentDraw.listVertices[cpCurrentDraw.listVertices.Count - 1]))))
				{
					LogAddEntry("Cannot close polygon. Closing vertex would intersect with containing polygon!", "IconError", true);
					return false;
				}
			}

			foreach (TreeNode nodeLoop in nodesContainer[0].Nodes)
			{
				int iKeyTemp = int.Parse(nodeLoop.Name);
				if (cdCurrentDoc.listPolygons[iKeyTemp].intersectsWith(new CSegment(new CPoint(cpCurrentDraw.listVertices[0]), new CPoint(cpCurrentDraw.listVertices[cpCurrentDraw.listVertices.Count - 1]))))
				{
					LogAddEntry("Cannot close polygon. Closing vertex would intersect with other polygons!", "IconError", true);
					return false;
				}
			}


			// Add a copy of the first point of the polygon as it's last point
			cpCurrentDraw.listVertices.Add(cpCurrentDraw.listVertices[0]);

			// Add the current polygon to the polygon list
			CPolygon pNew = new CPolygon(cpCurrentDraw.listVertices);
			cdCurrentDoc.listPolygons.Add(pNew);
			int iIndexNew = cdCurrentDoc.listPolygons.Count - 1;


			// Find a place for the new polygon in the tree
			TreeNode[] nodeList = treeViewPolygons.Nodes.Find(cpCurrentDraw.iContainerIndex.ToString(), true);
			if(nodeList.GetLength(0) != 1)
				throw new Exception("Tree view sync exception!");

			// Create a new tree node for the polygon
			TreeNode nodeTemp = new TreeNode("Polygon " + ((int)(iIndexNew + 1)).ToString());

			// If the current polygons contains some of the polygons on the same tree level, make those nodes childs of the new polygon's node
			//List<int> listDeletions = new List<int>(nodeList[0].Nodes.Count);
			int[] listDeletions = new int[nodeList[0].Nodes.Count];
			int iCountDeletions = 0;
			foreach (TreeNode nodeLoop in nodeList[0].Nodes)
			{
				int iKeyTemp = int.Parse(nodeLoop.Name);
				CPolygon pTemp = cdCurrentDoc.listPolygons[iKeyTemp];
				if (pNew.containsNonIntersectingStrict(ref pTemp))
				{
					iCountDeletions++;

					nodeTemp.Nodes.Add((TreeNode)nodeLoop.Clone());
					listDeletions[iCountDeletions - 1] = iKeyTemp;
					//nodeLoop.Remove();
					//listDeletions.Add(iKeyTemp);
				}
			}

			for (int i = 0; i < iCountDeletions; i++)
			{
				TreeNode[] nodesFind = nodeList[0].Nodes.Find(listDeletions[i].ToString(), false);
				if (nodesFind.GetLength(0) != 1)
					throw new Exception("Polygon tree view sync exception!");

				nodesFind[0].Remove();
			}

			// Set the new node's information
			nodeTemp.Name = iIndexNew.ToString();
			nodeTemp.ImageKey = "ImagePolygon";
			nodeTemp.SelectedImageKey = "ImagePolygon";

			// Finally add the new polygon's node to the tree
			nodeList[0].Nodes.Add(nodeTemp);

			nodeTemp.EnsureVisible();
			nodeTemp.Expand();


			cpCurrentDraw.bIsOpen = false;


			// Update document status
			if (cdCurrentDoc.bChanged == false)
			{
				cdCurrentDoc.bChanged = true;
				UpdateWindowTitle();
			}

			// Now update the screen
			RenderBegin();
			RenderObjectsPolygons();
			RenderObjectsPolygonCurrent();
			RenderEnd();

			// Return true, as we really closed the current polygon (without any errors)
			return true;
		}

		private void cancelCurrentPolygon()
		{
			cpCurrentDraw.bIsOpen = false;

			RenderBegin();
			RenderObjectsPolygonCurrent();
			RenderEnd();
		}

		private void resetCurrentPolygon()
		{
			cpCurrentDraw.bIsOpen = false;
		}


		private void toolStripButtonDrawMode_Click(object sender, EventArgs e)
		{
			if (toolStripButtonDrawMode.Checked)
				DrawModeBegin();
			else
			{
				if(!DrawModeEnd())
				{
					toolStripButtonDrawMode.Checked = true;
					MessageBox.Show("Drawing mode cannot be ended because a currently open polygon can't be closed due to intersections.\n\nPlease close or cancel the current polygon before ending drawing mode.", AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return;
				}
			}
		}


		private void splitContainer1_Panel1_MouseMove(object sender, MouseEventArgs e)
		{
			ptCurrent.X = e.X;
			ptCurrent.Y = e.Y;

			toolStripStatusLabelPos.Text = "(" + e.X.ToString() + ", " + e.Y.ToString() + ")";

			DrawScreen();
		}

		private void splitContainer1_Panel1_MouseClick(object sender, MouseEventArgs e)
		{
			if (e.Button != MouseButtons.Left)
				return;

			// If not in DrawingMode, do nothing
			if (iUserDraw == 0)
			{
				int iClicked = findMostInnerContainer(new Point(e.X, e.Y));
				
				TreeNode[] nodesFind = treeViewPolygons.Nodes.Find(iClicked.ToString(), true);
				
				if (nodesFind.GetLength(0) != 1)
					throw new Exception("Polygon tree view sync exception!");

				nodesFind[0].EnsureVisible();
				treeViewPolygons.SelectedNode = nodesFind[0];

				treeViewPolygons.Focus();

				return;
			}


			// From here on, work with grid-clipped coordinates if enabled
			Point ptEventPoint = new Point();
			if (!tboCurrentApp.SnapToGrid)
			{
				ptEventPoint.X = e.X;
				ptEventPoint.Y = e.Y;
			}
			else
			{
				if (!(clipToGrid(new Point(e.X, e.Y), 5, true, ref ptEventPoint)))
					return;
			}


			// If in DrawingMode and it's the first vertex of the new polygon, do init stuff
			if(!cpCurrentDraw.bIsOpen)
			{
				startCurrentPolygon();

				cpCurrentDraw.rcBoundingBoxLineStringWithoutCurrentVertex.X = ptEventPoint.X;
				cpCurrentDraw.rcBoundingBoxLineStringWithoutCurrentVertex.Y = ptEventPoint.Y;

				// Set to '0' so that next check for intersection can exit after quick check
				cpCurrentDraw.rcBoundingBoxLineStringWithoutCurrentVertex.Width = 0;
				cpCurrentDraw.rcBoundingBoxLineStringWithoutCurrentVertex.Height = 0;

				cpCurrentDraw.rcBoundingBoxLineStringComplete = cpCurrentDraw.rcBoundingBoxLineStringWithoutCurrentVertex;

				ptLast.X = ptEventPoint.X;
				ptLast.Y = ptEventPoint.Y;

				cpCurrentDraw.listVertices.Add(ptLast);

				cpCurrentDraw.iContainerIndex = findMostInnerContainer(ptLast);

				return;
			}


			// If in DrawingMode and it's NOT the first vertex of the new polygon, just add a new vertex and perform necessary checks

			// Make sure the current vertex is not a point
			if (ptEventPoint.X == ptLast.X && ptEventPoint.Y == ptLast.Y)
			{
				LogAddEntry("Cannot set current vertex. Vertex intersects with current line string. Only simple polygons allowed!", "IconError", true);
				return;
			}

			// Make sure the current vertex doesn't intersect with the rest of the line string
			Point ptCurrent = new Point(ptEventPoint.X, ptEventPoint.Y);

			int iMinX = Math.Min(ptCurrent.X, ptLast.X);
			int iMaxX = Math.Max(ptCurrent.X, ptLast.X);
			int iMinY = Math.Min(ptCurrent.Y, ptLast.Y);
			int iMaxY = Math.Max(ptCurrent.Y, ptLast.Y);
			Rectangle rcCurrentVertex = new Rectangle(iMinX, iMinY, iMaxX - iMinX + 1, iMaxY - iMinY + 1);

			if (cpCurrentDraw.rcBoundingBoxLineStringWithoutCurrentVertex.IntersectsWith(rcCurrentVertex))
			{
				for (int i = 0; i < cpCurrentDraw.listVertices.Count - 2; i++)
				{
					if (LineStringIntersection(ptLast, ptCurrent, cpCurrentDraw.listVertices[i], cpCurrentDraw.listVertices[i + 1]) != LINESTRINGINTERSECTIONRESULT.INTERSECTION_NONE)
					{
						LogAddEntry("Cannot set current vertex. Vertex intersects with current line string. Only simple polygons allowed!", "IconError", true);
						return;
					}
				}
			}

			if (cpCurrentDraw.listVertices.Count > 1)
			{
				if (LineStringIntersection(ptLast, ptCurrent, cpCurrentDraw.listVertices[cpCurrentDraw.listVertices.Count - 2], ptLast) != LINESTRINGINTERSECTIONRESULT.INTERSECTION_POINT)
				{
					LogAddEntry("Cannot set current vertex. Vertex overlaps with last vertex. Only simple polygons allowed!", "IconError", true);
					return;
				}
			}


			// Create temporary bounding box which includes ALL vertices (including the one currently beeing checked)
			Rectangle rcNewTemp = cpCurrentDraw.rcBoundingBoxLineStringComplete;
			if (ptEventPoint.X < rcNewTemp.X)
			{
				int iX2 = rcNewTemp.X + rcNewTemp.Width - 1;
				rcNewTemp.X = ptEventPoint.X;
				rcNewTemp.Width = iX2 - rcNewTemp.X + 1;
			}
			if (ptEventPoint.X > rcNewTemp.X + rcNewTemp.Width - 1) rcNewTemp.Width = ptEventPoint.X - rcNewTemp.X + 1;
			if (ptEventPoint.Y < rcNewTemp.Y)
			{
				int iY2 = rcNewTemp.Y + rcNewTemp.Height - 1;
				rcNewTemp.Y = ptEventPoint.Y;
				rcNewTemp.Height = iY2 - rcNewTemp.Y + 1;
			}
			if (ptEventPoint.Y > rcNewTemp.Y + rcNewTemp.Height - 1) rcNewTemp.Height = ptEventPoint.Y - rcNewTemp.Y + 1;


			// TODO: Replace the intersectsWith function, using a function that also receives the bounding box and performs a quicktest before looping throught all vertices

			// Check for intersections with containing polygon (if there is one) and all polygons on the same tree level
			TreeNode[] nodesContainer = treeViewPolygons.Nodes.Find(cpCurrentDraw.iContainerIndex.ToString(), true);
			if (nodesContainer.GetLength(0) != 1)
				throw new Exception("Polygon list sync exception!");

			if (cpCurrentDraw.iContainerIndex != -1)
			{
				if(cdCurrentDoc.listPolygons[cpCurrentDraw.iContainerIndex].intersectsWith(new CSegment(new CPoint(ptLast), new CPoint(ptCurrent))))
				{
					LogAddEntry("Cannot set current vertex. Vertex intersects with the containing polygon!", "IconError", true);
					return;
				}
			}

			foreach (TreeNode nodeLoop in nodesContainer[0].Nodes)
			{
				int iKeyTemp = int.Parse(nodeLoop.Name);
				if(cdCurrentDoc.listPolygons[iKeyTemp].intersectsWith(new CSegment(new CPoint(ptLast), new CPoint(ptCurrent))))
				{
					LogAddEntry("Cannot set current vertex. Vertex intersects with other polygons!", "IconError", true);
					return;
				}
			}


			// Update bounding box 1 to include all but the current vertex
			Rectangle rcTemp = cpCurrentDraw.rcBoundingBoxLineStringWithoutCurrentVertex;
			if (ptLast.X < rcTemp.X)
			{
				int iX2 = rcTemp.X + rcTemp.Width - 1;
				rcTemp.X = ptLast.X;
				rcTemp.Width = iX2 - rcTemp.X + 1;
			}
			if (ptLast.X > rcTemp.X + rcTemp.Width - 1) rcTemp.Width = ptLast.X - rcTemp.X + 1;
			if (ptLast.Y < rcTemp.Y)
			{
				int iY2 = rcTemp.Y + rcTemp.Height - 1;
				rcTemp.Y = ptLast.Y;
				rcTemp.Height = iY2 - rcTemp.Y + 1;
			}
			if (ptLast.Y > rcTemp.Y + rcTemp.Height - 1) rcTemp.Height = ptLast.Y - rcTemp.Y + 1;
			cpCurrentDraw.rcBoundingBoxLineStringWithoutCurrentVertex = rcTemp;

			ptLast.X = ptEventPoint.X;
			ptLast.Y = ptEventPoint.Y;

			cpCurrentDraw.listVertices.Add(ptLast);

			// Update bouding box 2 to include ALL vertices
			cpCurrentDraw.rcBoundingBoxLineStringComplete = rcNewTemp;

			RenderBegin();
			RenderObjectsPolygonCurrent();
			RenderEnd();

			DrawScreen();
		}


		private void contextMenuStripDraw_Opening(object sender, CancelEventArgs e)
		{
			if (!cpCurrentDraw.bIsOpen)
				e.Cancel = true;

			if (cpCurrentDraw.listVertices.Count < 3)
				closePolygonToolStripMenuItem.Enabled = false;
			else
				closePolygonToolStripMenuItem.Enabled = true;
		}

		private void closePolygonToolStripMenuItem_Click_1(object sender, EventArgs e)
		{
			closeCurrentPolygon();
		}

		private void cancelToolStripMenuItem_Click(object sender, EventArgs e)
		{
			cancelCurrentPolygon();
			iUserDraw = 1;
		}

		#endregion

		#region Painting Functions

		void DrawScreen()
		{
			if (!bGraphicsInitialized)
				return;

			// Clear screen
			gfxScreenBuffer.Clear(Color.Transparent);


			// Draw static objects
			RenderEnsureEnded();
			gfxScreenBuffer.DrawImage(bmpStatic, 0, 0);


			// If enabled (and in draw mode), clip cursor to grid

			Point ptCurrentTemp = new Point();
			bool bDrawCurrent = true;
			if (tboCurrentApp.SnapToGrid && iUserDraw != 0)
			{
				if (clipToGrid(ptCurrent, 5, true, ref ptCurrentTemp))
				{
					int iCursorSize = 5;
					gfxScreenBuffer.DrawLine(tboCurrentApp.penGridClip, ptCurrentTemp.X - iCursorSize, ptCurrentTemp.Y, ptCurrentTemp.X + iCursorSize, ptCurrentTemp.Y);
					gfxScreenBuffer.DrawLine(tboCurrentApp.penGridClip, ptCurrentTemp.X, ptCurrentTemp.Y - iCursorSize, ptCurrentTemp.X, ptCurrentTemp.Y + iCursorSize);
				}
				else
					bDrawCurrent = false;
			}
			else
				ptCurrentTemp = ptCurrent;

			
			// Draw algorithm results
			if (caCurrentAlg.amCurrent != CurrentAlgorithm.AlgorithmMode.NONE && tboCurrentApp.DrawAlgorithmData)
			{
				// Connections so far
				if (tboCurrentApp.DrawConnections)
				{
					foreach (CSegment segTemp in caCurrentAlg.listConnections)
						gfxScreenBuffer.DrawLine(tboCurrentApp.penAlgorithmConnections, segTemp.ptStart.X, segTemp.ptStart.Y, segTemp.ptEnd.X, segTemp.ptEnd.Y);
				}

				if (tboCurrentApp.DrawConnectedPolygons)
				{
					switch (caCurrentAlg.asCurrent)
					{
						case CurrentAlgorithm.AlgorithmState.JOIN:
							// Current Connected Polygons
							for (int i = 0; i < Math.Min(caCurrentAlg.listPolygons.Count, caCurrentAlg.cajData.iCurrentPolygon); i++)
							{
								if (caCurrentAlg.listTriangulate[i])
								{
									Point[] listPointsTemp = caCurrentAlg.listPolygons[i].vertices;
									gfxScreenBuffer.DrawPolygon(tboCurrentApp.penAlgorithmConnectedPolygons, listPointsTemp);
									if (tboCurrentApp.ShadeConnectedPolygons)
										gfxScreenBuffer.FillPolygon(tboCurrentApp.brushAlgorithmConnectedPolygons, listPointsTemp);
								}
							}
							break;
						
						case CurrentAlgorithm.AlgorithmState.TRIANGULATE:
						case CurrentAlgorithm.AlgorithmState.DONE:
							// Connected Polygons
							for (int i = 0; i < caCurrentAlg.listPolygons.Count; i++)
							{
								if (caCurrentAlg.listTriangulate[i])
								{
									Point[] listPointsTemp = caCurrentAlg.listPolygons[i].vertices;
									gfxScreenBuffer.DrawPolygon(tboCurrentApp.penAlgorithmConnectedPolygons, listPointsTemp);
									if (tboCurrentApp.ShadeConnectedPolygons)
										gfxScreenBuffer.FillPolygon(tboCurrentApp.brushAlgorithmConnectedPolygons, listPointsTemp);
								}
							}
							break;
					}
				}

				if (tboCurrentApp.DrawCurrentState)
				{
					if (caCurrentAlg.amCurrent != CurrentAlgorithm.AlgorithmMode.FULLSPEED)
					{
						switch (caCurrentAlg.asCurrent)
						{
							case CurrentAlgorithm.AlgorithmState.JOIN:

								break;

							case CurrentAlgorithm.AlgorithmState.TRIANGULATE:
							case CurrentAlgorithm.AlgorithmState.DONE:

								break;
						}
						switch (caCurrentAlg.asCurrent)
						{
							case CurrentAlgorithm.AlgorithmState.JOIN:
								// Current Polygon
								foreach (CPolygon pTemp in caCurrentAlg.cajData.listCurrentPolygon)
								{
									Point[] listPointsTemp = pTemp.vertices;
									gfxScreenBuffer.DrawPolygon(tboCurrentApp.penAlgorithmCurrentPolygon, listPointsTemp);
									gfxScreenBuffer.FillPolygon(tboCurrentApp.brushAlgorithmCurrentPolygon, listPointsTemp);
								}

								// Current Sub-Polygon
								foreach (CPolygon pTemp in caCurrentAlg.cajData.listCurrentSubPolygon)
								{
									Point[] listPointsTemp = pTemp.vertices;
									gfxScreenBuffer.DrawPolygon(tboCurrentApp.penAlgorithmCurrentPolygon, listPointsTemp);
									gfxScreenBuffer.FillPolygon(tboCurrentApp.brushAlgorithmCurrentPolygon, listPointsTemp);
								}

								// Extreme Points
								foreach (CPoint ptTemp in caCurrentAlg.cajData.jssnCurrent.listPointsExtreme)
									gfxScreenBuffer.DrawEllipse(tboCurrentApp.penAlgorithmCurrentPointLock, ptTemp.X, ptTemp.Y, 1, 1);

								// Maximum / Locked Points
								foreach (CPoint ptTemp in caCurrentAlg.cajData.jssnCurrent.listPointsMax)
									gfxScreenBuffer.DrawEllipse(tboCurrentApp.penAlgorithmCurrentPointLock, ptTemp.X, ptTemp.Y, 1, 1);

								// Investigation Points
								foreach (CPoint ptTemp in caCurrentAlg.cajData.jssnCurrent.listPointsInvestigated)
									gfxScreenBuffer.DrawEllipse(tboCurrentApp.penAlgorithmCurrentPoint, ptTemp.X, ptTemp.Y, 1, 1);

								// Extreme Segments
								foreach (CSegment segTemp in caCurrentAlg.cajData.jssnCurrent.listSegmentsExtreme)
									gfxScreenBuffer.DrawLine(tboCurrentApp.penAlgorithmCurrentVertexLock, segTemp.ptStart.X, segTemp.ptStart.Y, segTemp.ptEnd.X, segTemp.ptEnd.Y);

								// Maximum / Locked Segments
								foreach (CSegment segTemp in caCurrentAlg.cajData.jssnCurrent.listSegmentsMax)
									gfxScreenBuffer.DrawLine(tboCurrentApp.penAlgorithmCurrentVertexLock, segTemp.ptStart.X, segTemp.ptStart.Y, segTemp.ptEnd.X, segTemp.ptEnd.Y);

								// Investigation Segments
								foreach (CSegment segTemp in caCurrentAlg.cajData.jssnCurrent.listSegmentsInvestigated)
									gfxScreenBuffer.DrawLine(tboCurrentApp.penAlgorithmCurrentVertex, segTemp.ptStart.X, segTemp.ptStart.Y, segTemp.ptEnd.X, segTemp.ptEnd.Y);

								// Diagonals (Connection Line)
								foreach (CSegment segTemp in caCurrentAlg.cajData.jssnCurrent.listDiagonals)
									gfxScreenBuffer.DrawLine(tboCurrentApp.penAlgorithmConnections, segTemp.ptStart.X, segTemp.ptStart.Y, segTemp.ptEnd.X, segTemp.ptEnd.Y);

								break;

							case CurrentAlgorithm.AlgorithmState.TRIANGULATE:
								// Current stack of polygons
								foreach (List<CPoint> ptTemp in caCurrentAlg.catData.listTriCurrentStack)
								{
									Point[] listPointsConvert = new Point[ptTemp.Count];
									
									for (int i = 0; i < ptTemp.Count; i++)
										listPointsConvert[i] = new Point(ptTemp[i].X, ptTemp[i].Y);

									gfxScreenBuffer.DrawPolygon(tboCurrentApp.penAlgorithmStack, listPointsConvert);
									gfxScreenBuffer.FillPolygon(tboCurrentApp.brushAlgorithmStack, listPointsConvert);
								}

								// Currently analyzed polygon
								List<CPoint> listShort = caCurrentAlg.catData.listTriCurrentPolygon;
								if (caCurrentAlg.catData.listTriCurrentPolygon.Count > 0)
								{
									List<CPoint> listTemp = caCurrentAlg.catData.listTriCurrentPolygon;
									Point[] listPointsConvert = new Point[listTemp.Count];

									for (int i = 0; i < listTemp.Count; i++)
										listPointsConvert[i] = new Point(listTemp[i].X, listTemp[i].Y);

									gfxScreenBuffer.DrawPolygon(tboCurrentApp.penAlgorithmCurrentPolygon, listPointsConvert);
									gfxScreenBuffer.FillPolygon(tboCurrentApp.brushAlgorithmCurrentPolygon, listPointsConvert);									
								}

								// Currently analyzed vertices
								if (caCurrentAlg.catData.listTriVerticesLook.Count == 3)
								{
									List<CPoint> listTemp = caCurrentAlg.catData.listTriVerticesLook;
									
									tboCurrentApp.penAlgorithmCurrentVertex.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
									gfxScreenBuffer.DrawLine(tboCurrentApp.penAlgorithmCurrentVertex, listTemp[0].X, listTemp[0].Y, listTemp[1].X, listTemp[1].Y);
									gfxScreenBuffer.DrawLine(tboCurrentApp.penAlgorithmCurrentVertex, listTemp[1].X, listTemp[1].Y, listTemp[2].X, listTemp[2].Y);

									tboCurrentApp.penAlgorithmCurrentVertex.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
									gfxScreenBuffer.DrawLine(tboCurrentApp.penAlgorithmCurrentVertex, listTemp[2].X, listTemp[2].Y, listTemp[0].X, listTemp[0].Y);
								}

								// Currently analyzed point
								if (caCurrentAlg.catData.listTriPointLook.Count == 1)
									gfxScreenBuffer.DrawEllipse(tboCurrentApp.penAlgorithmCurrentPoint, caCurrentAlg.catData.listTriPointLook[0].X, caCurrentAlg.catData.listTriPointLook[0].Y, 1, 1);

								// Current maximum point
								if (caCurrentAlg.catData.listTriPointTop.Count == 1)
									gfxScreenBuffer.DrawEllipse(tboCurrentApp.penAlgorithmCurrentPointLock, caCurrentAlg.catData.listTriPointTop[0].X, caCurrentAlg.catData.listTriPointTop[0].Y, 1, 1);
								break;
						}
					}
				}

				// Diagonals so far
				if (tboCurrentApp.DrawDiagonals)
				{
					foreach (List<CDiagonal> listTemp in caCurrentAlg.listTriangulations)
					{
						for (int i = 0; i < listTemp.Count; i++)
						{
							gfxScreenBuffer.DrawLine(tboCurrentApp.penAlgorithmDiagonals, listTemp[i].ptStart.X, listTemp[i].ptStart.Y, listTemp[i].ptEnd.X, listTemp[i].ptEnd.Y);
						}
					}
				}
			}


			// Draw current vertex
			if (iUserDraw == 1 && cpCurrentDraw.bIsOpen && bDrawCurrent)
				gfxScreenBuffer.DrawLine(tboCurrentApp.penCurrentPolygon, ptLast, ptCurrentTemp);


			// Now show it on the screen
			gfxScreen.DrawImage(bmpScreenBuffer, 0, 0);
		}


		bool bRenderChangedGrid = true;
		bool bRenderChangedPolygons = true;
		bool bRenderChangedPolygonCurrent = true;
		bool bRenderChangedPolygonSelected = true;

		/// <summary>
		/// Function to initialize a rendering process.
		/// </summary>
		/// <remarks>
		/// The rendering process must be ended by calling RenderEnd.</remarks>
		/// <seealso cref="RenderEnd"/>
		void RenderBegin()
		{
		}

		/// <summary>
		/// Function to end a rendering process in a clean and defined way.
		/// </summary>
		/// <seealso cref="RenderBegin"/>
		void RenderEnd()
		{
			if (!(bRenderChangedGrid || bRenderChangedPolygons || bRenderChangedPolygonCurrent || bRenderChangedPolygonSelected))
				return;

			gfxStatic.Clear(Color.White);

			gfxStatic.DrawImage(bmpGrid, 0, 0);
			gfxStatic.DrawImage(bmpPolygons, 0, 0);
			gfxStatic.DrawImage(bmpPolygonSelected, 0, 0);
			gfxStatic.DrawImage(bmpPolygonCurrent, 0, 0);

			bRenderChangedGrid = false;
			bRenderChangedPolygons = false;
			bRenderChangedPolygonCurrent = false;
			bRenderChangedPolygonSelected = false;
		}

		/// <summary>
		/// Makes sure all changes to static object are applied to the final image before painting it on the screen.
		/// </summary>
		/// <remarks>
		/// This function is called in the screen painting function to make sure that all changes even if you missed to call RenderEnd
		/// somewhere will be applied to the static image before it is painted on the screen.</remarks>
		/// <seealso cref="RenderEnd"/>
		/// <seealso cref="RenderBegin"/>
		void RenderEnsureEnded()
		{
			RenderEnd();
		}


		/// <summary>
		/// Wrapper functions that simplifies the process of updating all static objects.
		/// </summary>
		/// <remarks>This function effectively just calls all RenderObjects* functions.</remarks>
		/// <seealso cref="RenderObjectsGrid"/>
		/// <seealso cref="RenderObjectsPolygons"/>
		/// <seealso cref="RenderObjectsPolygonCurrent"/>
		/// <seealso cref="RenderObjectsPolygonSelected"/>
		void RenderObjectsAll()
		{
			RenderObjectsGrid();
			RenderObjectsPolygons();
			RenderObjectsPolygonCurrent();
			RenderObjectsPolygonSelected();
		}


		void RenderObjectsGrid()
		{
			gfxGrid.Clear(Color.Transparent);			

			// Draw grid if necessary
			if (tboCurrentApp.ShowGrid)
			{
				int iGrid = (int)(bmpGrid.Width / tboCurrentApp.GridWidth);
				for (int i = 0; i <= iGrid; i++)
					gfxGrid.DrawLine(tboCurrentApp.penGrid, (float)tboCurrentApp.GridWidth * i, 0.0F, (float)tboCurrentApp.GridWidth * i, bmpGrid.Height);

				iGrid = (int)(bmpGrid.Height / tboCurrentApp.GridWidth);
				for (uint i = 0; i <= iGrid; i++)
					gfxGrid.DrawLine(tboCurrentApp.penGrid, 0.0F, (float)tboCurrentApp.GridWidth * i, bmpGrid.Width, (float)tboCurrentApp.GridWidth * i);
			}

			bRenderChangedGrid = true;
		}

		void RenderObjectsPolygons()
		{
			gfxPolygons.Clear(Color.Transparent);

			// Draw finished polygons
			int iCount = 0;
			foreach (CPolygon pTemp in cdCurrentDoc.listPolygons)
			{
				Point[] points = pTemp.vertices;
				if (tboCurrentApp.DrawPolygons)
				{
					for (int i = 0; i < points.GetLength(0) - 1; i++)
					{
						gfxPolygons.DrawLine(tboCurrentApp.penFinishedPolygons, points[i], points[i + 1]);
					}
				}

				if (tboCurrentApp.ShadePolygons)
					gfxPolygons.FillPolygon(tboCurrentApp.brushFinischedPolygon, points);

				iCount++;
			}

			bRenderChangedPolygons = true;
		}

		void RenderObjectsPolygonCurrent()
		{
			gfxPolygonCurrent.Clear(Color.Transparent);

			// Draw the current polygon so far
			if (cpCurrentDraw.bIsOpen)
			{
				for (int i = 0; i < cpCurrentDraw.listVertices.Count - 1; i++)
				{
					gfxPolygonCurrent.DrawLine(tboCurrentApp.penCurrentPolygon, cpCurrentDraw.listVertices[i], cpCurrentDraw.listVertices[i + 1]);

					//gfxPolygonCurrent.DrawRectangle(penCurrentPolygon, cpCurrentDraw.rcBoundingBoxLineStringWithoutCurrentVertex);
					//gfxPolygonCurrent.DrawRectangle(penCurrentPolygon, cpCurrentDraw.rcBoundingBoxLineStringComplete);
				}
			}

			bRenderChangedPolygonCurrent = true;
		}

		void RenderObjectsPolygonSelected()
		{
			gfxPolygonSelected.Clear(Color.Transparent);

			if (tboCurrentApp.HighlightSelection)
			{
				TreeNode nTemp = treeViewPolygons.SelectedNode;
				if (nTemp != null)
				{
					int iSelected = int.Parse(nTemp.Name);
					if (iSelected >= 0)
					{
						if (!(iSelected < cdCurrentDoc.listPolygons.Count))
							throw new Exception("TreeView sync exception!");

						CPolygon pTemp = cdCurrentDoc.listPolygons[iSelected];
						Point[] points = pTemp.vertices;
						if (tboCurrentApp.DrawPolygons)
						{
							for (int i = 0; i < points.GetLength(0) - 1; i++)
							{
								gfxPolygonSelected.DrawLine(tboCurrentApp.penSelectedPolygon, points[i], points[i + 1]);
							}
						}

						if (tboCurrentApp.ShadePolygons)
							gfxPolygonSelected.FillPolygon(tboCurrentApp.brushSelectedPolygon, points);

					}
				}
			}

			bRenderChangedPolygonSelected = true;
		}


		bool bGraphicsInitialized = false;
		
		void GraphicsInit()
		{
			int iWidth = splitContainerToolbox.Panel1.ClientSize.Width;
			int iHeight = splitContainerToolbox.Panel1.ClientSize.Height;
			

			bmpScreenBuffer = new Bitmap(iWidth, iHeight);
			bmpGrid = new Bitmap(iWidth, iHeight);
			bmpPolygons = new Bitmap(iWidth, iHeight);
			bmpPolygonCurrent = new Bitmap(iWidth, iHeight);
			bmpPolygonSelected = new Bitmap(iWidth, iHeight);
			
			bmpStatic = new Bitmap(iWidth, iHeight);


			gfxScreenBuffer = Graphics.FromImage(bmpScreenBuffer);
			gfxGrid = Graphics.FromImage(bmpGrid);
			gfxPolygons = Graphics.FromImage(bmpPolygons);
			gfxPolygonCurrent = Graphics.FromImage(bmpPolygonCurrent);
			gfxPolygonSelected = Graphics.FromImage(bmpPolygonSelected);
			
			gfxStatic = Graphics.FromImage(bmpStatic);

			gfxScreen = splitContainerToolbox.Panel1.CreateGraphics();


			bGraphicsInitialized = true;
		}

		void GraphicsResize()
		{
			if (!bGraphicsInitialized)
			{
				GraphicsInit();
				return;
			}

			bmpScreenBuffer.Dispose();
			bmpGrid.Dispose();
			bmpPolygons.Dispose();
			bmpPolygonCurrent.Dispose();
			bmpPolygonSelected.Dispose();
			bmpStatic.Dispose();

			bmpScreenBuffer = null;
			bmpGrid = null;
			bmpPolygons = null;
			bmpPolygonCurrent = null;
			bmpPolygonSelected = null;
			bmpStatic = null;

			gfxScreenBuffer = null;
			gfxGrid = null;
			gfxPolygons = null;
			gfxPolygonCurrent = null;
			gfxPolygonSelected = null;
			gfxStatic = null;
			gfxScreen = null;

			GC.Collect();

			GraphicsInit();
		}


		private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
		{
			DrawScreen();
		}

		private void splitContainer1_Panel1_ClientSizeChanged(object sender, EventArgs e)
		{
			if (!bInitialized)
				return;

			GraphicsResize();
			RenderObjectsAll();
			DrawScreen();
		}
		
		#endregion 



		#region Polygon List

		private void removePolygonToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if(MessageBox.Show("Do you really want to delete the selected polygon?", AssemblyTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
				removePolygon(false);
		}

		private void removePolygonAndAllchildsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (MessageBox.Show("Do you really want to delete the selected polygon and all polygons inside it?", AssemblyTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
				removePolygon(true);
		}


		void removePolygon(bool bRemoveChilds)
		{
			TreeNode nodeTemp = treeViewPolygons.SelectedNode;

			if (nodeTemp == null)
				return;

			// This is the index to be removed from the polygon list
			int iIndex = int.Parse(nodeTemp.Name);

			if (!bRemoveChilds)
			{
				// Copy child nodes to parent node
				TreeNode nodeParent = nodeTemp.Parent;
				foreach (TreeNode nodeLoop in nodeTemp.Nodes)
				{
					nodeParent.Nodes.Add((TreeNode)nodeLoop.Clone());
				}

				// Adjust tree view indices
				lowerNodeKeyByOne(treeViewPolygons.Nodes[0], iIndex);

				// Remove the according polygon from the polygon list
				cdCurrentDoc.listPolygons.RemoveAt(iIndex);
			}
			else
			{
				// Adjust tree view indices and remove childs from polygon list
				lowerNodeKeysForChilds(nodeTemp);
			}

			// Now remove the node from the tree view
			nodeTemp.Remove();

			cdCurrentDoc.bChanged = true;
			UpdateWindowTitle();

			// Disbale Control toolbar if no polygons present
			if (cdCurrentDoc.listPolygons.Count == 0)
			{
				AlgorithmModeReset();
				toolStripControl.Enabled = false;
				toolStripDrawing.Enabled = true;
			}

			// Update screen
			RenderBegin();
			RenderObjectsPolygons();
			RenderObjectsPolygonSelected();
			RenderEnd();

			DrawScreen();
		}

		void lowerNodeKeyByOne(TreeNode nodeStart, int iStartingKey)
		{
			int iKeyTemp = int.Parse(nodeStart.Name);
			if (iKeyTemp >= iStartingKey)
			{
				// As indices are zero based and text is one based, update text before lowering index by one
				nodeStart.Text = "Polygon " + iKeyTemp.ToString();

				// Now lower index and update node's key / name
				iKeyTemp -= 1;
				nodeStart.Name = iKeyTemp.ToString();
			}

			foreach (TreeNode nodeTemp in nodeStart.Nodes)
				lowerNodeKeyByOne(nodeTemp, iStartingKey);
		}

		void lowerNodeKeysForChilds(TreeNode nodeStartChild)
		{
			int iKeyTemp = int.Parse(nodeStartChild.Name);
			
			// Update container index of the current drawing polygon
			if (iKeyTemp == cpCurrentDraw.iContainerIndex)
				cpCurrentDraw.iContainerIndex = int.Parse(nodeStartChild.Parent.Name);

			lowerNodeKeyByOne(treeViewPolygons.Nodes[0], iKeyTemp);
			cdCurrentDoc.listPolygons.RemoveAt(iKeyTemp);

			foreach (TreeNode nodeTemp in nodeStartChild.Nodes)
				lowerNodeKeysForChilds(nodeTemp);
		}


		private void treeViewPolygons_MouseDown(object sender, MouseEventArgs e)
		{
			TreeNode nodeTemp = treeViewPolygons.HitTest(e.Location).Node;

			if (e.Button == MouseButtons.Left)
			{
				if (nodeTemp == null)
				{
					if (treeViewPolygons.SelectedNode != null)
					{
						treeViewPolygons.SelectedNode = null;
						RenderBegin();
						RenderObjectsPolygonSelected();
						RenderEnd();
						DrawScreen();
					}
				}
			}
			
			if (e.Button == MouseButtons.Right)
			{
				if (nodeTemp != null)
				{
					treeViewPolygons.SelectedNode = nodeTemp;
					treeViewPolygons.ContextMenuStrip = contextMenuStripPolygonList;
				}
				else
					treeViewPolygons.ContextMenuStrip = null;
			}
		}

		private void contextMenuStripPolygonList_Opening(object sender, CancelEventArgs e)
		{
			if (!(treeViewPolygons.SelectedNode != null && int.Parse(treeViewPolygons.SelectedNode.Name) >= 0))
			{
				e.Cancel = true;
				return;
			}

			// If the program's currently in algorithm mode, prevent user from deleting polygons
			if (caCurrentAlg.amCurrent != CurrentAlgorithm.AlgorithmMode.NONE)
			{
				e.Cancel = true;
				return;
			}

			if (treeViewPolygons.SelectedNode.Nodes.Count == 0)
				removePolygonAndAllchildsToolStripMenuItem.Enabled = false;
			else
				removePolygonAndAllchildsToolStripMenuItem.Enabled = true;
		}


		private void treeViewPolygons_AfterSelect(object sender, TreeViewEventArgs e)
		{
			RenderBegin();
			RenderObjectsPolygonSelected();
			RenderEnd();

			DrawScreen();
		}

		
		private void toolStripButtonShowPolygonList_Click(object sender, EventArgs e)
		{
			splitContainerPolygonList.Panel1Collapsed = !toolStripButton1.Checked;
			showPolygonListToolStripMenuItem.Checked = toolStripButton1.Checked;
			Invalidate();
		}

		private void showPolygonListToolStripMenuItem_Click(object sender, EventArgs e)
		{
			splitContainerPolygonList.Panel1Collapsed = !showPolygonListToolStripMenuItem.Checked;
			toolStripButton1.Checked = showPolygonListToolStripMenuItem.Checked;
			Invalidate();
		}

		#endregion

		#region Toolbox

		private void toolboxChangedGrid()
		{
			RenderBegin();
			RenderObjectsGrid();
			RenderEnd();

			DrawScreen();
		}

		private void toolboxChangedPolygonAll()
		{
			RenderBegin();
			RenderObjectsPolygons();
			RenderObjectsPolygonSelected();
			RenderEnd();

			DrawScreen();
		}

		private void toolboxChangedPolygonsFinished()
		{
			RenderBegin();
			RenderObjectsPolygons();
			RenderEnd();

			DrawScreen();
		}

		private void toolboxChangedPolygonSelected()
		{
			RenderBegin();
			RenderObjectsPolygonSelected();
			RenderEnd();

			DrawScreen();
		}

		private void toolboxChangedAlgorithmStepFrequency()
		{
			timerLowSpeedMode.Interval = tboCurrentApp.StepFrequency;
		}

		private void toolboxChangedRedraw()
		{
			DrawScreen();
		}


		private void showToolboxToolStripMenuItem_Click(object sender, EventArgs e)
		{
			splitContainerToolbox.Panel2Collapsed = !showToolboxToolStripMenuItem.Checked;
			toolStripButtonShowToolbox.Checked = showToolboxToolStripMenuItem.Checked;
			Invalidate();
		}

		private void toolStripButtonShowToolbox_Click(object sender, EventArgs e)
		{
			splitContainerToolbox.Panel2Collapsed = !toolStripButtonShowToolbox.Checked;
			showToolboxToolStripMenuItem.Checked = toolStripButtonShowToolbox.Checked;
			Invalidate();
		}

		#endregion

		#region Log Window

		private void clearLogToolStripMenuItem_Click(object sender, EventArgs e)
		{
			LogClear();
		}


		private void showLogWindowToolStripMenuItem_Click(object sender, EventArgs e)
		{
			splitContainerLogWindow.Panel2Collapsed = !showLogWindowToolStripMenuItem.Checked;
			toolStripButtonShowLog.Checked = showLogWindowToolStripMenuItem.Checked;
			Invalidate();
		}

		private void toolStripButtonShowLog_Click(object sender, EventArgs e)
		{
			splitContainerLogWindow.Panel2Collapsed = !toolStripButtonShowLog.Checked;
			showLogWindowToolStripMenuItem.Checked = toolStripButtonShowLog.Checked;
			Invalidate();
		}


		void LogAddEntry(String szText, String szIcon, bool bBeep)
		{
			LogAddEntry(szText, szIcon);

			if (bBeep && !tboCurrentApp.LogBeepDisabled && tboCurrentApp.LogEnabled)
				MessageBeep(MB_OK);
		}

		void LogAddEntry(String szText, String szIcon)
		{
			if (!tboCurrentApp.LogEnabled)
				return;

			int iTemp = listViewLog.Items.Count + 1;

			ListViewItem lviTemp = listViewLog.Items.Add("");
			lviTemp.ImageIndex = imageListLog.Images.IndexOfKey(szIcon);

			lviTemp.SubItems.Add(iTemp.ToString());
			lviTemp.SubItems.Add(szText);

			listViewLog.TopItem = lviTemp;
		}


		void LogClear()
		{
			listViewLog.Items.Clear();
		}

		#endregion
		

		#region Algorithm

		struct CurrentAlgorithm
		{
			public enum AlgorithmMode
			{
				NONE,
				FULLSPEED,
				SLOW,
				STEP
			}
			public AlgorithmMode amCurrent;

			public enum AlgorithmState
			{
				NONE,
				JOIN,
				TRIANGULATE,
				DONE
			}
			public AlgorithmState asCurrent;

			public CurrentAlgorithmJoin cajData;
			public CurrentAlgorithmTriangulation catData;

			public List<CPolygon> listPolygons;
			public List<List<CDiagonal>> listTriangulations;
			public List<CSegment> listConnections;

			public List<bool> listTriangulate;
		}
		CurrentAlgorithm caCurrentAlg;

		struct CurrentAlgorithmJoin
		{
			public enum PolygonState
			{
				INITIAL,
				SELECT_SUB,
				IN_PROGRESS,
				DONE
			}
			public PolygonState psCurrentPolygonState;
			
			public int iCurrentPolygon;
			public List<CPolygon> listCurrentPolygon;

			public List<CPolygon> listSubPolygons;
			
			public int iCurrentSubPolygon;
			public List<CPolygon> listCurrentSubPolygon;

			public CPolygon.JoinStepwiseSnapshot jssnCurrent;
		}

		struct CurrentAlgorithmTriangulation
		{
			public int iCurrentPolygon;
			
			public enum PolygonState
			{
				INITIAL,
				IN_PROGRESS,
				DONE
			}
			public PolygonState psCurrentPolygonState;

			public List<CDiagonal> listDiagonalsBackUp;

			// Contains the current sub-polygon's points
			public List<CPoint> listTriCurrentPolygon;

			// Contains the sub-polygons on the stack
			public List<List<CPoint>> listTriCurrentStack;

			// Contains the THREE vertices currently being testes whether they are convex
			public List<CPoint> listTriVerticesLook;

			// Contains the current ONE point beeing tested for containment
			public List<CPoint> listTriPointLook;

			// Contains the current ONE point beeing closest to the triangle top
			public List<CPoint> listTriPointTop;
		}


		private void initCurrentAlgorithmStruct(ref CurrentAlgorithm caInit)
		{
			caInit.amCurrent = CurrentAlgorithm.AlgorithmMode.NONE;
			caInit.asCurrent = CurrentAlgorithm.AlgorithmState.NONE;
			

			if (caInit.listTriangulations == null)
				caInit.listTriangulations = new List<List<CDiagonal>>(cdCurrentDoc.listPolygons.Count);
			else
			{
				caInit.listTriangulations.Clear();
				caInit.listTriangulations.Capacity = cdCurrentDoc.listPolygons.Count;
			}


			if (caInit.listPolygons == null)
				caInit.listPolygons = new List<CPolygon>(cdCurrentDoc.listPolygons.Count);
			else
			{
				caInit.listPolygons.Clear();
				caInit.listPolygons.Capacity = cdCurrentDoc.listPolygons.Count;
			}


			if (caInit.listConnections == null)
				caInit.listConnections = new List<CSegment>(Math.Max(cdCurrentDoc.listPolygons.Count - 1, 0));
			else
			{
				caInit.listConnections.Clear();
				caInit.listConnections.Capacity = cdCurrentDoc.listPolygons.Count - 1;
			}


			if (caInit.listTriangulate == null)
				caInit.listTriangulate = new List<bool>(cdCurrentDoc.listPolygons.Count);
			else
			{
				caInit.listTriangulate.Clear();
				caInit.listTriangulate.Capacity = cdCurrentDoc.listPolygons.Count;
			}


			initCurrentAlgorithmTriangulationStruct(ref caInit.catData);
			initCurrentAlgorithmJoinStruct(ref caInit.cajData);
		}

		private void initCurrentAlgorithmTriangulationStruct(ref CurrentAlgorithmTriangulation catInit)
		{
			catInit.iCurrentPolygon = 0;
			catInit.psCurrentPolygonState = CurrentAlgorithmTriangulation.PolygonState.INITIAL;


			if (catInit.listDiagonalsBackUp == null)
				catInit.listDiagonalsBackUp = new List<CDiagonal>();
			else
				catInit.listDiagonalsBackUp.Clear();


			if (catInit.listTriCurrentPolygon == null)
				catInit.listTriCurrentPolygon = new List<CPoint>();
			else
				catInit.listTriCurrentPolygon.Clear();


			if (catInit.listTriCurrentStack == null)
				catInit.listTriCurrentStack = new List<List<CPoint>>();
			else
				catInit.listTriCurrentStack.Clear();


			if (catInit.listTriVerticesLook == null)
				catInit.listTriVerticesLook = new List<CPoint>(3);
			else
				catInit.listTriVerticesLook.Clear();


			if (catInit.listTriPointLook == null)
				catInit.listTriPointLook = new List<CPoint>(1);
			else
				catInit.listTriPointLook.Clear();


			if (catInit.listTriPointTop == null)
				catInit.listTriPointTop = new List<CPoint>(1);
			else
				catInit.listTriPointTop.Clear();
		}

		private void initCurrentAlgorithmJoinStruct(ref CurrentAlgorithmJoin cajInit)
		{
			cajInit.iCurrentPolygon = 0;
			cajInit.psCurrentPolygonState = CurrentAlgorithmJoin.PolygonState.INITIAL;

			if (cajInit.listCurrentPolygon == null)
				cajInit.listCurrentPolygon = new List<CPolygon>(1);
			else
				cajInit.listCurrentPolygon.Clear();

			
			if (cajInit.listSubPolygons == null)
				cajInit.listSubPolygons = new List<CPolygon>();
			else
				cajInit.listSubPolygons.Clear();


			if (cajInit.listCurrentSubPolygon == null)
				cajInit.listCurrentSubPolygon = new List<CPolygon>(1);
			else
				cajInit.listCurrentSubPolygon.Clear();
			
			cajInit.iCurrentSubPolygon = 0;

			cajInit.jssnCurrent.jssNext = CPolygon.JoinStepwiseState.SELECT_EXTREME_POINTS;
			cajInit.jssnCurrent.listDiagonals = new List<CSegment>();
			cajInit.jssnCurrent.listPointsExtreme = new List<CPoint>();
			cajInit.jssnCurrent.listPointsInvestigated = new List<CPoint>();
			cajInit.jssnCurrent.listPointsMax = new List<CPoint>();
			cajInit.jssnCurrent.listSegmentsExtreme = new List<CSegment>();
			cajInit.jssnCurrent.listSegmentsInvestigated = new List<CSegment>();
			cajInit.jssnCurrent.listSegmentsMax = new List<CSegment>();
		}


		private void AlgorithmModeBegin(CurrentAlgorithm.AlgorithmMode amCurrent)
		{
			toolStripDrawing.Enabled = false;

			toolStripButtonAlgorithmModeFull.Enabled = false;
			toolStripButtonAlgorithmModeSlow.Enabled = false;
			toolStripButtonAlgorithmModeStep.Enabled = false;

			toolStripButtonAlgorithmStop.Enabled = false;
			toolStripButtonAlgorithmPlay.Enabled = false;
			toolStripButtonAlgorithmPause.Enabled = false;

			if (tboCurrentApp.ClearLogBeforeRun)
				LogClear();

			initCurrentAlgorithmStruct(ref caCurrentAlg);

			// Make a copy of the polygon list to work with and reserve space for the triangulation results
			for (int i = 0; i < cdCurrentDoc.listPolygons.Count; i++)
			{
				// TODO: This code to copy a polygon is quite clumsy. Add an additional constructor to CPolygon which takes a CPolygon type as parameter to copy a polygon
				caCurrentAlg.listPolygons.Add(new CPolygon(new List<Point>(cdCurrentDoc.listPolygons[i].vertices)));

				TreeNode[] listNodes = treeViewPolygons.Nodes[0].Nodes.Find(i.ToString(), true);

				if (listNodes.GetLength(0) != 1)
					throw new Exception("Polygon tree view sync exception!");

				// Skip if node is not within triangulation depth level
				if (tboCurrentApp.TriangulationLevel != -1 && listNodes[0].Level > tboCurrentApp.TriangulationLevel)
					caCurrentAlg.listTriangulate.Add(false);
				else
					caCurrentAlg.listTriangulate.Add(true);

				caCurrentAlg.listTriangulations.Add(new List<CDiagonal>());
			}

			caCurrentAlg.amCurrent = amCurrent;
			caCurrentAlg.asCurrent = CurrentAlgorithm.AlgorithmState.NONE;
		}

		private void AlgorithmModeEnd()
		{
			LogAddEntry("Triangulation mode ended.", "IconAlgorithmStop", true);

			caCurrentAlg.amCurrent = CurrentAlgorithm.AlgorithmMode.NONE;
			initCurrentAlgorithmStruct(ref caCurrentAlg);

			toolStripButtonAlgorithmModeFull.Enabled = true;
			toolStripButtonAlgorithmModeSlow.Enabled = true;
			toolStripButtonAlgorithmModeStep.Enabled = true;

			toolStripButtonAlgorithmStop.Enabled = false;
			toolStripButtonAlgorithmPlay.Enabled = false;
			toolStripButtonAlgorithmPause.Enabled = false;

			toolStripDrawing.Enabled = true;

			DrawScreen();
		}

		private void AlgorithmModeReset()
		{
			if (caCurrentAlg.amCurrent != CurrentAlgorithm.AlgorithmMode.NONE)
				AlgorithmModeEnd();
		}


		private void AlgorithmModeStepBegin()
		{
			AlgorithmModeBegin(CurrentAlgorithm.AlgorithmMode.STEP);
			
			LogAddEntry("Triangulating polygons in single step mode started.", "IconAlgorithmModeStep", true);

			toolStripButtonAlgorithmPlay.Enabled = true;
			toolStripButtonAlgorithmStop.Enabled = true;

			AlgorithmModeStepInit();
		}

		private void AlgorithmModeStepInit()
		{
			caCurrentAlg.asCurrent = CurrentAlgorithm.AlgorithmState.JOIN;
		}

		private bool AlgorithmModeStepForward()
		{
			bool bReturn = true;

			switch (caCurrentAlg.asCurrent)
			{
				case CurrentAlgorithm.AlgorithmState.JOIN:
					// Skip polygons that don't have to be triangulated due to their level
					while (caCurrentAlg.cajData.iCurrentPolygon < caCurrentAlg.listPolygons.Count && !caCurrentAlg.listTriangulate[caCurrentAlg.cajData.iCurrentPolygon])
						caCurrentAlg.cajData.iCurrentPolygon++;

					if (caCurrentAlg.cajData.iCurrentPolygon < caCurrentAlg.listPolygons.Count)
					{
						int iPolygonNumberPrint = caCurrentAlg.cajData.iCurrentPolygon + 1;

						CPolygon pCurrent = caCurrentAlg.listPolygons[caCurrentAlg.cajData.iCurrentPolygon];
						
						caCurrentAlg.cajData.listCurrentPolygon.Clear();
						caCurrentAlg.cajData.listCurrentPolygon.Add(pCurrent);

						if (caCurrentAlg.cajData.psCurrentPolygonState == CurrentAlgorithmJoin.PolygonState.INITIAL)
						{
							// Create right to left ordered list of sub-polygons

							TreeNode[] listNodes = treeViewPolygons.Nodes[0].Nodes.Find(caCurrentAlg.cajData.iCurrentPolygon.ToString(), true);

							if (listNodes.GetLength(0) != 1)
								throw new Exception("Polygon tree view sync exception!");

							// Clear the list of sub-polygons
							caCurrentAlg.cajData.listSubPolygons.Clear();

							// Loop through child nodes of current polygon's tree node
							int iCount = 0;
							int iVertices = 0;
							foreach (TreeNode nodeLoop in listNodes[0].Nodes)
							{
								int iIndexTemp = int.Parse(nodeLoop.Name);

								CPolygon pSub = cdCurrentDoc.listPolygons[iIndexTemp];

								// Counting information to calculate the amout of space needed for diagonals in the current polygons triangulation list
								iCount++;
								iVertices += pSub.SizeLogical;

								// Now add it to the sub-polygon list according to it's x-coordinate
								if (caCurrentAlg.cajData.listSubPolygons.Count == 0)
									caCurrentAlg.cajData.listSubPolygons.Add(pSub);
								else if (caCurrentAlg.cajData.listSubPolygons.Count == 1)
								{
									if (pSub.RightMostPoint.X > caCurrentAlg.cajData.listSubPolygons[0].RightMostPoint.X)
										caCurrentAlg.cajData.listSubPolygons.Insert(0, pSub);
									else
										caCurrentAlg.cajData.listSubPolygons.Add(pSub);
								}
								else
								{
									for (int j = 0; j < caCurrentAlg.cajData.listSubPolygons.Count; j++)
									{
										if (j == 0)
										{
											if (pSub.RightMostPoint.X > caCurrentAlg.cajData.listSubPolygons[j].RightMostPoint.X)
											{
												caCurrentAlg.cajData.listSubPolygons.Insert(j, pSub);
												break;
											}
										}
										else if (j == caCurrentAlg.cajData.listSubPolygons.Count - 1)
										{
											if (pSub.RightMostPoint.X > caCurrentAlg.cajData.listSubPolygons[j].RightMostPoint.X)
											{
												caCurrentAlg.cajData.listSubPolygons.Insert(j, pSub);
												break;
											}
											else
											{
												caCurrentAlg.cajData.listSubPolygons.Add(pSub);
												break;
											}
										}
										else
										{
											if (caCurrentAlg.cajData.listSubPolygons[j - 1].RightMostPoint.X >= pSub.RightMostPoint.X && caCurrentAlg.cajData.listSubPolygons[j].RightMostPoint.X <= pSub.RightMostPoint.X)
											{
												caCurrentAlg.cajData.listSubPolygons.Insert(j, pSub);
												break;
											}
										}
									}
								}
							}

							caCurrentAlg.listTriangulations[caCurrentAlg.cajData.iCurrentPolygon].Capacity = pCurrent.SizeLogical + iVertices + iCount * 2 - 3;
							
							caCurrentAlg.cajData.iCurrentSubPolygon = 0;
							caCurrentAlg.cajData.psCurrentPolygonState = CurrentAlgorithmJoin.PolygonState.SELECT_SUB;

							LogAddEntry("Selected polygon " + iPolygonNumberPrint.ToString() + ".", "IconAlgorithmStep", false);
							LogAddEntry("Polygon " + iPolygonNumberPrint.ToString() + " has " + caCurrentAlg.cajData.listSubPolygons.Count.ToString() + " sub-polygons and will be connected with those iteratively from right to left.", "IconInfo", true);
						}

						if (caCurrentAlg.cajData.psCurrentPolygonState == CurrentAlgorithmJoin.PolygonState.SELECT_SUB)
						{
							if (caCurrentAlg.cajData.iCurrentSubPolygon < caCurrentAlg.cajData.listSubPolygons.Count)
							{
								int iSubPolygonNumberPrint = caCurrentAlg.cajData.iCurrentSubPolygon + 1;

								LogAddEntry("Selecting sub-polygon " + iSubPolygonNumberPrint.ToString() + ".", "IconAlgorithmStep", false);

								caCurrentAlg.cajData.listCurrentSubPolygon.Clear();
								caCurrentAlg.cajData.listCurrentSubPolygon.Add(caCurrentAlg.cajData.listSubPolygons[caCurrentAlg.cajData.iCurrentSubPolygon]);

								pCurrent.joinWithChildStepwiseBegin(caCurrentAlg.cajData.listSubPolygons[caCurrentAlg.cajData.iCurrentSubPolygon], out caCurrentAlg.cajData.jssnCurrent);
								
								caCurrentAlg.cajData.psCurrentPolygonState = CurrentAlgorithmJoin.PolygonState.IN_PROGRESS;
							}
							else
							{
								// We're done with this polygon. All subpolygons joined.

								LogAddEntry("Polygon " + iPolygonNumberPrint.ToString() + " has been connected with all sub-polygons. Selecting next polygon.", "IconInfo", true);

								caCurrentAlg.cajData.listCurrentSubPolygon.Clear();

								caCurrentAlg.cajData.iCurrentPolygon++;
								caCurrentAlg.cajData.psCurrentPolygonState = CurrentAlgorithmJoin.PolygonState.INITIAL;
							}
						}

						if (caCurrentAlg.cajData.psCurrentPolygonState == CurrentAlgorithmJoin.PolygonState.IN_PROGRESS)
						{
							CPolygon.JoinStepwiseState jssTemp = caCurrentAlg.cajData.jssnCurrent.jssNext;
							
							bool bResult = pCurrent.joinWithChildStepwiseStep(out caCurrentAlg.cajData.jssnCurrent);

							switch (jssTemp)
							{
								case CPolygon.JoinStepwiseState.SELECT_EXTREME_POINTS:
									LogAddEntry("Selecting the right most vertex of the sub-polygon and the left most of the outer polygon which is still right of the sub-polygon.", "IconAlgorithmStep", false);
									break;

								case CPolygon.JoinStepwiseState.FIND_INTERSECTION:
									LogAddEntry("Searching for left most intersection with segment formed by the extreme points.", "IconAlgorithmStep", false);
									if(caCurrentAlg.cajData.jssnCurrent.jssNext == CPolygon.JoinStepwiseState.GET_TRIANGLE)
										LogAddEntry("Left most intersection found.", "IconInfo", true);
									else if(caCurrentAlg.cajData.jssnCurrent.jssNext == CPolygon.JoinStepwiseState.JOIN)
										LogAddEntry("There is no intersection. Right point will be connection point.", "IconInfo", true);
									break;

								case CPolygon.JoinStepwiseState.GET_TRIANGLE:
									LogAddEntry("Selecting the triagle formed by right most point and the intersecting segments' end points.", "IconAlgorithmStep", false);
									break;

								case CPolygon.JoinStepwiseState.FIND_MAX_INSIDE:
									LogAddEntry("Test if point is inside the triangle and closest to the sub-polygon's right most point.", "IconAlgorithmStep", false);
									if(caCurrentAlg.cajData.jssnCurrent.jssNext == CPolygon.JoinStepwiseState.JOIN)
										LogAddEntry("Left most intersection found. This point will be the connection point.", "IconInfo", true);
									break;

								case CPolygon.JoinStepwiseState.JOIN:
									LogAddEntry("Combining the two polygons using the found connection point and adding the connection to the list of diagonals for triangulation.", "IconAlgorithmStep", false);
									break;
							}

							if (!bResult)
							{
								// We're done with the current sub-polygon. Move on to the next sub polygon.

								LogAddEntry("Sub-polygon has been connected with outer polygon. Selecting next sub-polygon.", "IconInfo", true);

								CPolygon pCopy;
								CSegment segCopy;
								if (pCurrent.joinWithChildStepwiseEnd(out pCopy, out segCopy))
								{
									caCurrentAlg.listPolygons[caCurrentAlg.cajData.iCurrentPolygon] = pCopy;
									caCurrentAlg.listTriangulations[caCurrentAlg.cajData.iCurrentPolygon].Add(new CDiagonal(segCopy.ptStart, segCopy.ptEnd));
									caCurrentAlg.listConnections.Add(segCopy);

									caCurrentAlg.cajData.iCurrentSubPolygon++;
									caCurrentAlg.cajData.psCurrentPolygonState = CurrentAlgorithmJoin.PolygonState.SELECT_SUB;
								}
								else
									throw new Exception("Stepwise joining out of sync exception!");
							}
						}
					}
					else
					{
						// We're done joining polygons. Go on with triangulation

						LogAddEntry("All polygons have been connected with their sub-polygons. Now triangulate the connected polygons.", "IconInfo", true);

						caCurrentAlg.cajData.listCurrentPolygon.Clear();

						caCurrentAlg.catData.iCurrentPolygon = 0;
						caCurrentAlg.asCurrent = CurrentAlgorithm.AlgorithmState.TRIANGULATE;
					}
					break;

				case CurrentAlgorithm.AlgorithmState.TRIANGULATE:
					// Skip polygons that don't have to be triangulated due to their level
					while (caCurrentAlg.catData.iCurrentPolygon < caCurrentAlg.listPolygons.Count && !caCurrentAlg.listTriangulate[caCurrentAlg.catData.iCurrentPolygon])
						caCurrentAlg.catData.iCurrentPolygon++;

					if (caCurrentAlg.catData.iCurrentPolygon < caCurrentAlg.listPolygons.Count)
					{
						CPolygon pTemp = caCurrentAlg.listPolygons[caCurrentAlg.catData.iCurrentPolygon];
						List<CDiagonal> listDiagonalsTemp = caCurrentAlg.listTriangulations[caCurrentAlg.catData.iCurrentPolygon];

						if (caCurrentAlg.catData.psCurrentPolygonState == CurrentAlgorithmTriangulation.PolygonState.INITIAL)
						{
							int iPrint = caCurrentAlg.catData.iCurrentPolygon + 1;
							LogAddEntry("Triangulating polygon " + iPrint.ToString() + ".", "IconInfo", true);

							pTemp.triangulateStepwiseBegin();

							// BackUp the diagonals we have from connecting the polygon with sub-polygons before
							caCurrentAlg.catData.listDiagonalsBackUp.Clear();
							caCurrentAlg.catData.listDiagonalsBackUp.AddRange(listDiagonalsTemp);

							// To avoid permanent reallcoations
							caCurrentAlg.catData.listTriCurrentPolygon.Clear();
							caCurrentAlg.catData.listTriCurrentPolygon.Capacity = pTemp.Size;

							caCurrentAlg.catData.listTriCurrentStack.Clear();
							caCurrentAlg.catData.listTriCurrentStack.Capacity = pTemp.SizeLogical - 2;

							caCurrentAlg.catData.psCurrentPolygonState = CurrentAlgorithmTriangulation.PolygonState.IN_PROGRESS;
						}

						// Get the kind of action the algorithm is going to perform
						CPolygon.TriangulateStepwiseState tssTemp = pTemp.tsdThis.tssNext;

						// Execute the next step
						bool bResult = pTemp.triangulateStepwiseStep();

						// Now deal with the resulting data
						switch (tssTemp)
						{
							case CPolygon.TriangulateStepwiseState.GET_NEXT_POLYGON_ON_STACK:
								caCurrentAlg.catData.listTriCurrentPolygon.Clear();
								caCurrentAlg.catData.listTriCurrentPolygon.AddRange(pTemp.tsdThis.listCurrent);

								caCurrentAlg.catData.listTriCurrentStack.Clear();
								foreach (List<CPoint> listTemp in pTemp.tsdThis.stackPolygons)
									caCurrentAlg.catData.listTriCurrentStack.Add(new List<CPoint>(listTemp));

								caCurrentAlg.catData.listTriVerticesLook.Clear();
								caCurrentAlg.catData.listTriPointLook.Clear();
								caCurrentAlg.catData.listTriPointTop.Clear();
								if (pTemp.tsdThis.tssNext != CPolygon.TriangulateStepwiseState.DONE)
									LogAddEntry("Pop a (sub-)polygon from the stack.", "IconAlgorithmStep", false);
								if (pTemp.tsdThis.tssNext == CPolygon.TriangulateStepwiseState.GET_NEXT_POLYGON_ON_STACK)
									LogAddEntry("The current (sub-)polygon is a triangle. No need for further triangulation.", "IconInfo", true);
								break;

							case CPolygon.TriangulateStepwiseState.FIND_NEXT_CONVEX_VERTEX:
								caCurrentAlg.catData.listTriVerticesLook.Clear();
								caCurrentAlg.catData.listTriVerticesLook.Add(pTemp.tsdThis.listCurrent[pTemp.tsdThis.iReal]);
								caCurrentAlg.catData.listTriVerticesLook.Add(pTemp.tsdThis.listCurrent[pTemp.tsdThis.iReal + 1]);
								caCurrentAlg.catData.listTriVerticesLook.Add(pTemp.tsdThis.listCurrent[pTemp.tsdThis.iReal + 2]);

								caCurrentAlg.catData.listTriPointLook.Clear();
								caCurrentAlg.catData.listTriPointTop.Clear();

								if (pTemp.tsdThis.tssNext == CPolygon.TriangulateStepwiseState.CHECK_POINT_FOR_CONTAINMENT)
									LogAddEntry("Trying to find a convex vertex. Current vertex IS convex.", "IconAlgorithmStep", false);
								else
									LogAddEntry("Trying to find a convex vertex. Current vertex is NOT.", "IconAlgorithmStep", false);
								break;

							case CPolygon.TriangulateStepwiseState.CHECK_POINT_FOR_CONTAINMENT:
								caCurrentAlg.catData.listTriPointLook.Clear();
								caCurrentAlg.catData.listTriPointLook.Add(pTemp.tsdThis.listCurrent[pTemp.tsdThis.jReal]);
								if (pTemp.tsdThis.bCurrentInside)
								{
									if (pTemp.tsdThis.bCurrentMax)
									{
										LogAddEntry("Test whether remaining vertices are inside the triangle. Current vertex IS inside and is closest to triangle top so far.", "IconAlgorithmStep", false);
										caCurrentAlg.catData.listTriPointTop.Clear();
										caCurrentAlg.catData.listTriPointTop.Add(pTemp.tsdThis.listCurrent[pTemp.tsdThis.iIndexClosest]);
									}
									else
										LogAddEntry("Test whether remaining vertices are inside the triangle. Current vertex IS inside but NOT the closest to the top so far.", "IconAlgorithmStep", false);
								}
								else
									LogAddEntry("Test whether remaining vertices are inside the triangle. Current vertex is NOT.", "IconAlgorithmStep", false);


								break;

							case CPolygon.TriangulateStepwiseState.REMOVE_CURRENT_EAR:
								caCurrentAlg.catData.listTriCurrentPolygon.Clear();
								caCurrentAlg.catData.listTriCurrentPolygon.AddRange(pTemp.tsdThis.listCurrent);

								caCurrentAlg.catData.listTriVerticesLook.Clear();
								caCurrentAlg.catData.listTriPointLook.Clear();
								caCurrentAlg.catData.listTriPointTop.Clear();

								listDiagonalsTemp.Clear();
								listDiagonalsTemp.AddRange(caCurrentAlg.catData.listDiagonalsBackUp);
								listDiagonalsTemp.AddRange(pTemp.tsdThis.listDiagonals);

								LogAddEntry("There are no points inside. This was an \"ear\" and has been \"cut\" off.", "IconAlgorithmStep", false);
								if (pTemp.tsdThis.tssNext == CPolygon.TriangulateStepwiseState.GET_NEXT_POLYGON_ON_STACK)
									LogAddEntry("The remaining polygon is a triangle. No need for further triangulation.", "IconInfo", true);
								break;

							case CPolygon.TriangulateStepwiseState.SPLIT_POLYGON:
								caCurrentAlg.catData.listTriCurrentStack.Clear();
								foreach (List<CPoint> listTemp in pTemp.tsdThis.stackPolygons)
									caCurrentAlg.catData.listTriCurrentStack.Add(new List<CPoint>(listTemp));

								caCurrentAlg.catData.listTriVerticesLook.Clear();
								caCurrentAlg.catData.listTriPointLook.Clear();
								caCurrentAlg.catData.listTriPointTop.Clear();

								listDiagonalsTemp.Clear();
								listDiagonalsTemp.AddRange(caCurrentAlg.catData.listDiagonalsBackUp);
								listDiagonalsTemp.AddRange(pTemp.tsdThis.listDiagonals);

								LogAddEntry("All vertices tested. Split up polygon connecting triangle top and closest point and push the two polygons on the stack.", "IconAlgorithmStep", false);
								break;
						}

						if (!bResult)
						{
							pTemp.triangulateStepwiseEnd();

							int iPrint = caCurrentAlg.catData.iCurrentPolygon + 1;
							LogAddEntry("Triangulation of polygon " + iPrint.ToString() + " is done.", "IconInfo", true);

							caCurrentAlg.catData.iCurrentPolygon++;
							caCurrentAlg.catData.psCurrentPolygonState = CurrentAlgorithmTriangulation.PolygonState.INITIAL;
							caCurrentAlg.catData.listTriCurrentPolygon.Clear();
							caCurrentAlg.catData.listTriPointLook.Clear();
							caCurrentAlg.catData.listTriPointTop.Clear();
							caCurrentAlg.catData.listTriVerticesLook.Clear();
							caCurrentAlg.catData.listTriCurrentStack.Clear();
						}
					}
					else
					{
						LogAddEntry("Triangulation of all polygons complete!.", "IconAlgorithmDone", true);
						toolStripButtonAlgorithmPlay.Enabled = false;

						bReturn = false;
						
						caCurrentAlg.asCurrent = CurrentAlgorithm.AlgorithmState.DONE;
					}
					break;
			}

			DrawScreen();

			if(tboCurrentApp.BeepOnStep)
				MessageBeep(MB_OK);

			return bReturn;
		}

		private void AlgorithmModeStepEnd()
		{
			AlgorithmModeEnd();
		}



		private void AlgorithmModeFullBegin()
		{
			AlgorithmModeBegin(CurrentAlgorithm.AlgorithmMode.FULLSPEED);
			
			LogAddEntry("Triangulating polygons in full speed started.", "IconAlgorithmModeFullSpeed", true);

			LogAddEntry("Triangulating all polygons up to level " + tboCurrentApp.TriangulationLevel.ToString() + ".", "IconInfo", true);


			int iCountTotalJoin = 0;
			float fTimeTotalJoin = 0.0F;
			int iCountTotalTriangulation = 0;
			float fTimeTotalTriangulation = 0.0F;


			// Joining the polygons
			
			caCurrentAlg.asCurrent = CurrentAlgorithm.AlgorithmState.JOIN;

			for(int i = 0; i < caCurrentAlg.listPolygons.Count; i++)
			{
				if (!caCurrentAlg.listTriangulate[i])
					continue;

				int iPolygonNumberPrint = i + 1;

				TreeNode[] listNodes = treeViewPolygons.Nodes[0].Nodes.Find(i.ToString(), true);

				if (listNodes.GetLength(0) != 1)
					throw new Exception("Polygon tree view sync exception!");

				CPolygon pTemp = caCurrentAlg.listPolygons[i];

				caCurrentAlg.cajData.listSubPolygons.Clear();

				int iCount = 0;
				int iVertices = 0;
				foreach(TreeNode nodeLoop in listNodes[0].Nodes)
				{
					int iIndexTemp = int.Parse(nodeLoop.Name);
					
					CPolygon pTemp2 = cdCurrentDoc.listPolygons[iIndexTemp];

					// Counting information to calculate the amout of space needed for diagonals in the current polygons triangulation list
					iCount++;
					iVertices += pTemp2.SizeLogical;

					// Add to right to left sorted list in the right position
					if (caCurrentAlg.cajData.listSubPolygons.Count == 0)
						caCurrentAlg.cajData.listSubPolygons.Add(pTemp2);
					else if (caCurrentAlg.cajData.listSubPolygons.Count == 1)
					{
						if (pTemp2.RightMostPoint.X > caCurrentAlg.cajData.listSubPolygons[0].RightMostPoint.X)
							caCurrentAlg.cajData.listSubPolygons.Insert(0, pTemp2);
						else
							caCurrentAlg.cajData.listSubPolygons.Add(pTemp2);
					}
					else
					{
						for (int j = 0; j < caCurrentAlg.cajData.listSubPolygons.Count; j++)
						{
							if (j == 0)
							{
								if (pTemp2.RightMostPoint.X > caCurrentAlg.cajData.listSubPolygons[j].RightMostPoint.X)
								{
									caCurrentAlg.cajData.listSubPolygons.Insert(j, pTemp2);
									break;
								}
							}
							else if (j == caCurrentAlg.cajData.listSubPolygons.Count - 1)
							{
								if (pTemp2.RightMostPoint.X > caCurrentAlg.cajData.listSubPolygons[j].RightMostPoint.X)
								{
									caCurrentAlg.cajData.listSubPolygons.Insert(j, pTemp2);
									break;
								}
								else
								{
									caCurrentAlg.cajData.listSubPolygons.Add(pTemp2);
									break;
								}
							}
							else
							{
								if (caCurrentAlg.cajData.listSubPolygons[j - 1].RightMostPoint.X >= pTemp2.RightMostPoint.X && caCurrentAlg.cajData.listSubPolygons[j].RightMostPoint.X <= pTemp2.RightMostPoint.X)
								{
									caCurrentAlg.cajData.listSubPolygons.Insert(j, pTemp2);
									break;
								}
							}
						}
					}

				}

				caCurrentAlg.listTriangulations[i].Capacity = pTemp.SizeLogical + iVertices + iCount * 2 - 3;

				long lTimeTemp1 = 0;
				long lTimeTemp2 = 0;
				QueryPerformanceCounter(out lTimeTemp1);

				for (int j = 0; j < caCurrentAlg.cajData.listSubPolygons.Count; j++)
				{
					CSegment segConnection;
					pTemp = pTemp.joinWithChild(caCurrentAlg.cajData.listSubPolygons[j], out segConnection);

					caCurrentAlg.listConnections.Add(segConnection);
					caCurrentAlg.listTriangulations[i].Add(new CDiagonal(segConnection.ptStart, segConnection.ptEnd));

					iCountTotalJoin++;
				}

				caCurrentAlg.listPolygons[i] = pTemp;

				QueryPerformanceCounter(out lTimeTemp2);
				float fTimeCurrent = (float)(lTimeTemp2 - lTimeTemp1) / (float)lTimerFreq;
				fTimeTotalJoin += fTimeCurrent;

				LogAddEntry("Connected polygon " + iPolygonNumberPrint.ToString() + " with " + caCurrentAlg.cajData.listSubPolygons.Count.ToString() + " sub-polygons in " + Math.Round(fTimeCurrent, 4).ToString() + "s.", "IconInfo", true);
			}

			
			// Now do the triangulations
			
			caCurrentAlg.asCurrent = CurrentAlgorithm.AlgorithmState.TRIANGULATE;

			for (int i = 0; i < caCurrentAlg.listPolygons.Count; i++)
			{
				int iPolygonNumberPrint = i + 1;

				if (caCurrentAlg.listTriangulate[i] == false)
					continue;

				CPolygon pTemp = caCurrentAlg.listPolygons[i];
				
				long lTimeTemp1 = 0;
				long lTimeTemp2 = 0;
				QueryPerformanceCounter(out lTimeTemp1);

				caCurrentAlg.listTriangulations[i].AddRange(pTemp.Triangulate());

				QueryPerformanceCounter(out lTimeTemp2);
				float fTimeCurrent = (float)(lTimeTemp2 - lTimeTemp1) / (float)lTimerFreq;
				fTimeTotalTriangulation += fTimeCurrent;

				iCountTotalTriangulation++;

				LogAddEntry("Triangulated polygon " + iPolygonNumberPrint.ToString() + " in " + Math.Round(fTimeCurrent, 4).ToString() + "s.", "IconInfo", true);
			}


			LogAddEntry("All connected polygons have been triangulated.", "IconAlgorithmDone", true);

			LogAddEntry("A total number of " + iCountTotalJoin.ToString() + " polygon connections has been done in " + Math.Round(fTimeTotalJoin, 4).ToString() + "s.", "IconInfo", true);
			LogAddEntry("A total number of " + iCountTotalTriangulation.ToString() + " polygon triangulations has been done in " + Math.Round(fTimeTotalTriangulation, 4).ToString() + "s.", "IconInfo", true);

			LogAddEntry("All together, the operation took " + Math.Round(fTimeTotalTriangulation + fTimeTotalJoin, 4).ToString() + "s.", "IconInfo", true);

			LogAddEntry("Done triangulating polygons in full speed mode.", "IconAlgorithmDone", true);

			toolStripButtonAlgorithmStop.Enabled = true;

			caCurrentAlg.asCurrent = CurrentAlgorithm.AlgorithmState.DONE;

			DrawScreen();
		}

		private void AlgorithmModeFullEnd()
		{
			AlgorithmModeEnd();
		}


		private void AlgorithmModeSlowBegin()
		{
			AlgorithmModeBegin(CurrentAlgorithm.AlgorithmMode.SLOW);

			LogAddEntry("Triangulating polygons in slowmotion mode started.", "IconAlgorithmModeSlow", true);

			toolStripButtonAlgorithmPlay.Enabled = true;
			toolStripButtonAlgorithmStop.Enabled = true;
			toolStripButtonAlgorithmPause.Enabled = true;

			AlgorithmModeStepInit();

			timerLowSpeedMode.Interval = tboCurrentApp.StepFrequency;
			timerLowSpeedMode.Start();
		}

		private void AlgorithmModeSlowEnd()
		{
			timerLowSpeedMode.Stop();
			toolStripButtonAlgorithmPause.Checked = false;

			AlgorithmModeStepEnd();
		}



		private void toolStripButtonModeFullSpeed_Click(object sender, EventArgs e)
		{
			AlgorithmModeFullBegin();
		}

		private void toolStripButtonAlgorithmModeSlow_Click(object sender, EventArgs e)
		{
			AlgorithmModeSlowBegin();
		}

		private void toolStripButtonModeStep_Click(object sender, EventArgs e)
		{
			AlgorithmModeStepBegin();
		}


		private void toolStripButtonAlgorithmForward_Click(object sender, EventArgs e)
		{
			for (int i = 0; i < tboCurrentApp.StepsPerClick; i++)
			{
				if (!AlgorithmModeStepForward())
					break;
			}
		}
		
		private void toolStripButtonAlgorithmPause_Click(object sender, EventArgs e)
		{
			if (toolStripButtonAlgorithmPause.Checked)
				timerLowSpeedMode.Stop();
			else
				timerLowSpeedMode.Start();
		}

		private void toolStripButtonAlgorithmStop_Click(object sender, EventArgs e)
		{
			switch (caCurrentAlg.amCurrent)
			{
				case CurrentAlgorithm.AlgorithmMode.STEP:
					AlgorithmModeStepEnd();
					break;

				case CurrentAlgorithm.AlgorithmMode.FULLSPEED:
					AlgorithmModeFullEnd();
					break;

				case CurrentAlgorithm.AlgorithmMode.SLOW:
					AlgorithmModeSlowEnd();
					break;
			}
		}


		private void timerLowSpeedMode_Tick(object sender, EventArgs e)
		{
			if (!AlgorithmModeStepForward())
			{
				timerLowSpeedMode.Stop();
				toolStripButtonAlgorithmPause.Enabled = false;
			}
		}

		#endregion



		#region Settings

		private void SettingsLoad()
		{
			IFormatter formatter = new BinaryFormatter();
			try
			{
				Stream stream = new FileStream(szApplicationPath + "\\TOPwh.settings", FileMode.Open, FileAccess.Read, FileShare.Read);
				tboCurrentApp = (ToolboxOptions)formatter.Deserialize(stream);
				stream.Close();
				
				SettingsHelperSetDelegates();
				propertyGrid1.SelectedObject = tboCurrentApp;
			}
			catch
			{
				SettingsReset();
			}
		}

		private void SettingsSave()
		{
			IFormatter formatter = new BinaryFormatter();
			try
			{
				Stream stream = new FileStream(szApplicationPath + "\\TOPwh.settings", FileMode.Create, FileAccess.Write, FileShare.None);
				formatter.Serialize(stream, tboCurrentApp);
				stream.Close();
			}
			catch { }
		}


		private void SettingsReset()
		{
			tboCurrentApp = new ToolboxOptions();
			SettingsHelperSetDelegates();

			propertyGrid1.SelectedObject = tboCurrentApp;
		}


		private void SettingsHelperSetDelegates()
		{
			tboCurrentApp.dgUpdatePolygonsAll = new ToolboxOptionsDelegate(toolboxChangedPolygonAll);
			tboCurrentApp.dgUpdatePolygonsFinished = new ToolboxOptionsDelegate(toolboxChangedPolygonsFinished);
			tboCurrentApp.dgUpdatePolygonSelected = new ToolboxOptionsDelegate(toolboxChangedPolygonSelected);
			tboCurrentApp.dgUpdateGrid = new ToolboxOptionsDelegate(toolboxChangedGrid);
			tboCurrentApp.dgUpdateTimerStepFrequency = new ToolboxOptionsDelegate(toolboxChangedAlgorithmStepFrequency);
			tboCurrentApp.dgUpdateScreen = new ToolboxOptionsDelegate(toolboxChangedRedraw);
		}


		private void toolStripButton2_Click(object sender, EventArgs e)
		{
			if (MessageBox.Show("Are you sure you want to reset all toolbox settings?", AssemblyTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
				SettingsReset();
		}

		#endregion

		#region Document Managment

		bool DocumentClose()
		{
			if (cdCurrentDoc.bChanged)
			{
				DialogResult dlgrResult = MessageBox.Show("Do you want to save changes in the current document before closing it?", AssemblyTitle, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

				if (dlgrResult == DialogResult.Cancel)
					return false;
				
				if (dlgrResult == DialogResult.Yes)
					DocumentSave();
			}

			DrawModeReset();
			AlgorithmModeReset();
			LogClear();

			toolStripControl.Enabled = false;
			toolStripDrawing.Enabled = false;

			return true;			
		}

		void DocumentSave()
		{
			if (cdCurrentDoc.szFile == "")
				DocumentSaveAs();
			else
				DocumentSaveCommon(cdCurrentDoc.szFile);
		}

		void DocumentSaveAs()
		{
			if (cdCurrentDoc.szFile != "")
				saveFileDialogMain.FileName = cdCurrentDoc.szFile;
			else
				saveFileDialogMain.FileName = cdCurrentDoc.szName;

			if (saveFileDialogMain.ShowDialog() == DialogResult.OK)
				DocumentSaveCommon(saveFileDialogMain.FileName);
		}

		void DocumentSaveCommon(String szFile)
		{
			IFormatter formatter = new BinaryFormatter();
			try
			{
				Stream stream = new FileStream(szFile, FileMode.Create, FileAccess.Write, FileShare.None);
				formatter.Serialize(stream, cdCurrentDoc.listPolygons);
				formatter.Serialize(stream, treeViewPolygons.Nodes[0]);
				stream.Close();
			}
			catch
			{
				MessageBox.Show("The file could not be saved.\nMake sure it is not write protected!", AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			cdCurrentDoc.bChanged = false;
			cdCurrentDoc.szFile = szFile;
			cdCurrentDoc.szName = szFile.Substring(szFile.LastIndexOf('\\') + 1);

			UpdateWindowTitle();
		}

		void DocumentNew()
		{
			if (!DocumentClose())
				return;


			cdCurrentDoc.bChanged = false;
			cdCurrentDoc.szName = "Untitled";
			cdCurrentDoc.szFile = "";

			if (cdCurrentDoc.listPolygons == null)
				cdCurrentDoc.listPolygons = new List<CPolygon>(5);
			else
				cdCurrentDoc.listPolygons.Clear();

			treeViewPolygons.Nodes.Clear();
			treeViewPolygons.Nodes.Add("-1", "Polygons", "ImageDocument");
			treeViewPolygons.Nodes[0].SelectedImageKey = "ImageDocument";
			treeViewPolygons.Nodes[0].Expand();

			UpdateWindowTitle();

			toolStripDrawing.Enabled = true;

			RenderBegin();
			RenderObjectsAll();
			RenderEnd();

			DrawScreen();
		}

		void DocumentOpen()
		{
			if (!DocumentClose())
				return;


			if (openFileDialogMain.ShowDialog() != DialogResult.OK)
				return;

			String szFileName = openFileDialogMain.FileName;

			List<CPolygon> listTemp;
			TreeNode nodeTemp;

			IFormatter formatter = new BinaryFormatter();
			try
			{
				Stream stream = new FileStream(szFileName, FileMode.Open, FileAccess.Read, FileShare.Read);
				listTemp = (List<CPolygon>)formatter.Deserialize(stream);
				nodeTemp = (TreeNode)formatter.Deserialize(stream);
				stream.Close();
			}
			catch
			{
				MessageBox.Show("The file could not be opened.\nMake sure it is not used by another application!", AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			// Fill polygon list with file data
			cdCurrentDoc.listPolygons = listTemp;

			if(cdCurrentDoc.listPolygons.Count > 0)
				toolStripControl.Enabled = true;

			// Fill polygon tree view with data from file
			treeViewPolygons.Nodes[0].Nodes.Clear();

			foreach(TreeNode nodeLoop in nodeTemp.Nodes)
			{
				treeViewPolygons.Nodes[0].Nodes.Add((TreeNode)nodeLoop.Clone());
			}

			treeViewPolygons.Nodes[0].Expand();

			// Update document structure
			cdCurrentDoc.bChanged = false;
			cdCurrentDoc.szFile = szFileName;
			cdCurrentDoc.szName = szFileName.Substring(szFileName.LastIndexOf('\\') + 1);

			UpdateWindowTitle();

			// Enable Control toolbar if at least one polygon is present
			if(cdCurrentDoc.listPolygons.Count > 0)
				toolStripControl.Enabled = true;

			toolStripDrawing.Enabled = true;

			RenderBegin();
			RenderObjectsAll();
			RenderEnd();

			DrawScreen();
		}


		private void initCurrentDocStruct(ref CurrentDocument cdInit)
		{
			cdInit.bChanged = false;
			cdInit.szFile = "";
			cdInit.szName = "Untitled";

			if (cdInit.listPolygons == null)
				cdInit.listPolygons = new List<CPolygon>(10);
			else
				cdInit.listPolygons.Clear();
		}


		private void toolStripButtonOpen_Click(object sender, EventArgs e)
		{
			DocumentOpen();
		}

		private void toolStripButtonNewDoc_Click(object sender, EventArgs e)
		{
			DocumentNew();
		}

		private void toolStripButtonSave_Click(object sender, EventArgs e)
		{
			DocumentSave();
		}

		private void toolStripButtonExit_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}

		private void newToolStripMenuItem_Click(object sender, EventArgs e)
		{
			DocumentNew();
		}

		private void saveToolStripMenuItem_Click(object sender, EventArgs e)
		{
			DocumentSave();
		}

		private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			DocumentSaveAs();
		}

		private void openToolStripMenuItem_Click(object sender, EventArgs e)
		{
			DocumentOpen();
		}

		#endregion



		#region General Helper Functions

		private void UpdateWindowTitle()
		{
			String szTemp = AssemblyTitle;

			if (cdCurrentDoc.szName == "")
				szTemp += " - Untitled";
			else
				szTemp += " - " + cdCurrentDoc.szName;

			if (cdCurrentDoc.bChanged)
				szTemp += " *";

			Text = szTemp;
		}

		private string AssemblyTitle
		{
			get
			{
				// Get all Title attributes on this assembly
				object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
				// If there is at least one Title attribute
				if (attributes.Length > 0)
				{
					// Select the first one
					AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
					// If it is not an empty string, return it
					if (titleAttribute.Title != "")
						return titleAttribute.Title;
				}
				// If there was no Title attribute, or if the Title attribute was the empty string, return the .exe name
				return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
			}
		}


		private int findMostInnerContainer(Point ptSearch)
		{
			int iContainer = -1;
			
			TreeNode nodeTemp = treeViewPolygons.Nodes[0];
			if (nodeTemp.Nodes.Count > 0)
			{
				nodeTemp = nodeTemp.Nodes[0];
				while (nodeTemp != null)
				{
					int iTemp = int.Parse(nodeTemp.Name);
					if (cdCurrentDoc.listPolygons[iTemp].contains(new CPoint(ptSearch)))
					{
						iContainer = iTemp;
						if (nodeTemp.Nodes.Count == 0)
							break;
						else
							nodeTemp = nodeTemp.Nodes[0];
					}
					else
						nodeTemp = nodeTemp.NextNode;
				}
			}

			return iContainer;
		}

		private bool clipToGrid(Point ptReal, int iSmoothness, bool bSmoothnessAbsolute, ref Point ptResult)
		{
			Point ptTemp = new Point();

			ptTemp.X = ptReal.X / (int)tboCurrentApp.GridWidth * (int)tboCurrentApp.GridWidth;
			ptTemp.Y = ptReal.Y / (int)tboCurrentApp.GridWidth * (int)tboCurrentApp.GridWidth;

			ptTemp.X = ptReal.X <= ptTemp.X + (int)tboCurrentApp.GridWidth / 2 ? ptTemp.X : ptTemp.X + (int)tboCurrentApp.GridWidth;
			ptTemp.Y = ptReal.Y <= ptTemp.Y + (int)tboCurrentApp.GridWidth / 2 ? ptTemp.Y : ptTemp.Y + (int)tboCurrentApp.GridWidth;
			
			int iDiff = bSmoothnessAbsolute ? iSmoothness : (int)Math.Round(((double)iSmoothness * (double)tboCurrentApp.GridWidth), 0);

			if(Math.Abs(ptTemp.X - ptReal.X) <= iDiff && Math.Abs(ptTemp.Y - ptReal.Y) <= iDiff)
			{
				ptResult = ptTemp;
				return true;
			}
			else
			{
				ptResult = new Point();
				return false;
			}
		}

		#region Intersection Test

		// TODO: Review this code and check if this is really necessary. It's somewhat redundant with the code in the CPolygon class.

		struct Vector
		{
			public int X;
			public int Y;

			public Vector(Point ptInit)
			{
				this.X = ptInit.X;
				this.Y = ptInit.Y;
			}

			public static Vector operator -(Vector vSub1, Vector vSub2)
			{
				Vector vTemp;
				vTemp.X = vSub1.X - vSub2.X;
				vTemp.Y = vSub1.Y - vSub2.Y;
				return vTemp;
			}
		}

		static float perp(Vector u, Vector v)
		{
			return (float)(u.X * v.Y - u.Y * v.X);
		}

		static float dot(Vector u, Vector v)
		{
			return (float)(u.X * v.X + u.Y * v.Y);
		}

		static bool inSegment(Point ptPoint, Point ptSegmentStart, Point ptSegmentEnd)
		{
			if (ptSegmentStart.X != ptSegmentEnd.X)
			{    // S is not vertical
				if (ptSegmentStart.X <= ptPoint.X && ptPoint.X <= ptSegmentEnd.X)
					return true;
				if (ptSegmentStart.X >= ptPoint.X && ptPoint.X >= ptSegmentEnd.X)
					return true;
			}
			else
			{    // S is vertical, so test y coordinate
				if (ptSegmentStart.Y <= ptPoint.Y && ptPoint.Y <= ptSegmentEnd.Y)
					return true;
				if (ptSegmentStart.Y >= ptPoint.Y && ptPoint.Y >= ptSegmentEnd.Y)
					return true;
			}

			return false;
		}

		enum LINESTRINGINTERSECTIONRESULT
		{
			INTERSECTION_NONE,
			INTERSECTION_POINT,
			INTERSECTION_SEGMENT
		}

		private LINESTRINGINTERSECTIONRESULT LineStringIntersection(Point ptLine1Start, Point ptLine1End, Point ptLine2Start, Point ptLine2End)
		{
			const float SMALL_NUM = 0.00000001F;


			Vector u = new Vector(ptLine1End) - new Vector(ptLine1Start);
			Vector v = new Vector(ptLine2End) - new Vector(ptLine2Start);
			Vector w = new Vector(ptLine1Start) - new Vector(ptLine2Start);
			float D = perp(u, v);

			// test if they are parallel (includes either being a point)
			if (Math.Abs(D) < SMALL_NUM)
			{          // S1 and S2 are parallel
				if (perp(u, w) != 0 || perp(v, w) != 0)
				{
					return LINESTRINGINTERSECTIONRESULT.INTERSECTION_NONE;                   // they are NOT collinear
				}
				// they are collinear or degenerate
				// check if they are degenerate points
				float du = dot(u, u);
				float dv = dot(v, v);
				if (du == 0 && dv == 0)
				{           // both segments are points
					if (ptLine1Start != ptLine2Start)         // they are distinct points
						return LINESTRINGINTERSECTIONRESULT.INTERSECTION_NONE;
					//*I0 = S1.P0;                // they are the same point
					return LINESTRINGINTERSECTIONRESULT.INTERSECTION_POINT;
				}
				if (du == 0)
				{                    // S1 is a single point
					if (!inSegment(ptLine1Start, ptLine2Start, ptLine2End))  // but is not in S2
						return LINESTRINGINTERSECTIONRESULT.INTERSECTION_NONE;
					//*I0 = S1.P0;
					return LINESTRINGINTERSECTIONRESULT.INTERSECTION_POINT;
				}
				if (dv == 0)
				{                    // S2 a single point
					if (!inSegment(ptLine2Start, ptLine1Start, ptLine1End))  // but is not in S1
						return 0;
					//*I0 = S2.P0;
					return LINESTRINGINTERSECTIONRESULT.INTERSECTION_POINT;
				}
				// they are collinear segments - get overlap (or not)
				float t0, t1;                   // endpoints of S1 in eqn for S2
				Vector w2 = new Vector(ptLine1End) - new Vector(ptLine2Start);
				if (v.X != 0)
				{
					t0 = (float)w.X / (float)v.X;
					t1 = (float)w2.X / (float)v.X;
				}
				else
				{
					t0 = (float)w.Y / (float)v.Y;
					t1 = (float)w2.Y / (float)v.Y;
				}
				if (t0 > t1)
				{                  // must have t0 smaller than t1
					float t = t0; t0 = t1; t1 = t;    // swap if not
				}
				if (t0 > 1 || t1 < 0)
				{
					return LINESTRINGINTERSECTIONRESULT.INTERSECTION_NONE;     // NO overlap
				}
				t0 = t0 < 0 ? 0 : t0;              // clip to min 0
				t1 = t1 > 1 ? 1 : t1;              // clip to max 1
				if (t0 == t1)
				{                 // intersect is a point
					//*I0 = S2.P0 + t0 * v;
					return LINESTRINGINTERSECTIONRESULT.INTERSECTION_POINT;
				}

				// they overlap in a valid subsegment
				//*I0 = S2.P0 + t0 * v;
				//*I1 = S2.P0 + t1 * v;
				return LINESTRINGINTERSECTIONRESULT.INTERSECTION_SEGMENT;
			}

			// the segments are skew and may intersect in a point
			// get the intersect parameter for S1
			float sI = perp(v, w) / D;
			if (sI < 0 || sI > 1)               // no intersect with S1
				return LINESTRINGINTERSECTIONRESULT.INTERSECTION_NONE;

			// get the intersect parameter for S2
			float tI = perp(u, w) / D;
			if (tI < 0 || tI > 1)               // no intersect with S2
				return LINESTRINGINTERSECTIONRESULT.INTERSECTION_NONE;

			//*I0 = S1.P0 + sI * u;               // compute S1 intersect point
			return LINESTRINGINTERSECTIONRESULT.INTERSECTION_POINT;
		}

		#endregion

		#endregion

		#region Misc Menu & Toolbar Commands

		private void exitToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}


		private void showStatusBarToolStripMenuItem_Click(object sender, EventArgs e)
		{
			statusStrip1.Visible = showStatusBarToolStripMenuItem.Checked;
		}


		private void standardToolStripMenuItem_Click(object sender, EventArgs e)
		{
			toolStripFile.Visible = standardToolStripMenuItem.Checked;
		}

		private void viewToolStripMenuItem2_Click(object sender, EventArgs e)
		{
			toolStripView.Visible = viewToolStripMenuItem2.Checked;
		}

		private void drawToolStripMenuItem_Click(object sender, EventArgs e)
		{
			toolStripDrawing.Visible = drawToolStripMenuItem.Checked;
		}

		private void controlToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			toolStripControl.Visible = controlToolStripMenuItem1.Checked;
		}
		

		private void helpToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			try
			{
				Help.ShowHelp(this, szApplicationPath + "\\TOPwh Help.chm");
			}
			catch
			{
				MessageBox.Show("Cannot display help file!", AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
		}

		private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
		{
			using (AboutBox abTemp = new AboutBox())
			{
				abTemp.ShowDialog();
			}
		}

		#endregion
	}
}