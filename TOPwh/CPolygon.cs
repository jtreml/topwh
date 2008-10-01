using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace TOPwh
{
	/// <summary>
	/// Struct to provide a basic point type variable including operators.
	/// </summary>
	[Serializable]
	public struct CPoint
	{
		/// <summary>
		/// X coordinate of the point.
		/// </summary>
		public int X;
		/// <summary>
		/// Y coordinate of the point.
		/// </summary>
		public int Y;

		/// <summary>
		/// Initializes the structure with the given coordinates.
		/// </summary>
		/// <param name="X">X value to set.</param>
		/// <param name="Y">Y value to set.</param>
		public CPoint(int X, int Y)
		{
			this.X = X;
			this.Y = Y;
		}

		/// <summary>
		/// Initializes the structure with the values of a existing <see cref="Point"/>.
		/// </summary>
		/// <param name="ptInit"><see cref="Point"/> to use for initialization.</param>
		public CPoint(Point ptInit)
		{
			X = ptInit.X;
			Y = ptInit.Y;
		}

		/// <summary>
		/// Overloaded operator to test two <see cref="CPoint"/> variables for equality.
		/// </summary>
		/// <param name="ptPoint1">Point 1</param>
		/// <param name="ptPoint2">Point 2</param>
		/// <returns>true if the both points are the same, false otherwise.</returns>
		public static bool operator ==(CPoint ptPoint1, CPoint ptPoint2)
		{
			return (ptPoint1.X == ptPoint2.X && ptPoint1.Y == ptPoint2.Y);
		}

		/// <summary>
		/// Overloaded operator to test two <see cref="CPoint"/> variables for inequality.
		/// </summary>
		/// <param name="ptPoint1">Point 1</param>
		/// <param name="ptPoint2">Point 2</param>
		/// <returns>true if NOT euqal, false otherwise</returns>
		public static bool operator !=(CPoint ptPoint1, CPoint ptPoint2)
		{
			return (ptPoint1.X != ptPoint2.X || ptPoint1.Y != ptPoint2.Y);
		}

		/// <summary>
		/// Compares any <see cref="object"/> to the <see cref="CPoint"/> structure and tests for equality.
		/// </summary>
		/// <param name="obj"><see cref="object"/> to compare to.</param>
		/// <returns>true if the point equals the given object (converted to a <see cref="CPoint"/>), false 
		/// otherwise. If a null reference has been passed or the <see cref="object"/> is not convertable to <see cref="CPoint"/>, false is returned, too.</returns>
		/// <remarks>This method is overwritten to follow C# guidelines, as <see cref="operator =="/> is overwritten.</remarks>
		public override bool Equals(object obj)
		{
			// C# guideline requirement
			if (obj == null)
				return false;

			// C# guideline requirement
			CPoint ptTemp = (CPoint)obj;
			if ((object)ptTemp == null)
				return false;

			return (X == ptTemp.X && Y == ptTemp.Y);
		}

		/// <summary>
		/// Compares any <see cref="CPoint"/> to the <see cref="CPoint"/> structure and tests for equality.
		/// </summary>
		/// <param name="ptPoint"><see cref="CPoint"/> to compare to.</param>
		/// <returns>true if the point equals the given <see cref="CPoint"/>, false 
		/// otherwise. If a null reference has been passed, false is returned, too.</returns>
		/// <remarks>This method is overwritten to follow C# guidelines, as <see cref="operator =="/> is overwritten.</remarks>
		public bool Equals(CPoint ptPoint)
		{
			// C# guideline requirement
			if ((object)ptPoint == null)
				return false;

			return (X == ptPoint.X && Y == ptPoint.Y);
		}

		/// <summary>
		/// Returns the bitwise, exclusive OR of the points coordinates.
		/// </summary>
		/// <returns>An <see cref="int"/> value to be interpreted as bitwise.</returns>
		/// <remarks>This method is overwritten to follow C# guidelines, as <see cref="operator =="/> and therefore <see cref="Equals(CPoint)"/> are overwritten.</remarks>
		public override int GetHashCode()
		{
			return X ^ Y;
		}

		/// <summary>
		/// Converts the current point to a string.
		/// </summary>
		/// <returns>The same string as returned by <see cref="Point.ToString()"/> for a <see cref="Point"/> holding the same values as the current <see cref="CPoint"/>.</returns>
		/// <remarks>This function also makes it possible to get a well formatted view of a <see cref="CPoint"/> variable's content when debugging in Microsoft Visual Studio.</remarks>
		public override String ToString()
		{
			return new Point(X, Y).ToString();
		}
	}

	/// <summary>
	/// A basic struct for respresting points.
	/// </summary>
	/// <remarks>Compared to <see cref="CPoint"/>, <see cref="CPointF"/> is more accurate as it uses float values instead if int for the point's coordinates.</remarks>
	[Serializable]
	public struct CPointF
	{
		/// <summary>
		/// The points x coordinate.
		/// </summary>
		public float X;
		/// <summary>
		/// The points y coordinate.
		/// </summary>
		public float Y;

		/// <summary>
		/// Initializes the point with the given coordinates.
		/// </summary>
		/// <param name="X">x coordinate.</param>
		/// <param name="Y">y coordinate.</param>
		public CPointF(float X, float Y)
		{
			this.X = X;
			this.Y = Y;
		}
	}

	/// <summary>
	/// Struct to provide a basic rectangle type variable including operators and basic algorithms.
	/// </summary>
	[Serializable]
	public struct CRectangle
	{
		/// <summary>
		/// Top left corner of the rectangle represented by the structure.
		/// </summary>
		public CPoint ptTopLeft;
		/// <summary>
		/// Bottom right corner of the rectangle represented by the structure.
		/// </summary>
		public CPoint ptBottomRight;

		/// <summary>
		/// Initialize the structure from two given <see cref="CPoint"/>s.
		/// </summary>
		/// <param name="ptTopLeft">Point to set as the top left corner of the rectangle.</param>
		/// <param name="ptBottomRight">Point to set as the bottom right corner of the rectangle.</param>
		public CRectangle(CPoint ptTopLeft, CPoint ptBottomRight)
		{
			this.ptTopLeft = ptTopLeft;
			this.ptBottomRight = ptBottomRight;
		}
	}

	/// <summary>
	/// Provides a basic vector class and according operations.
	/// </summary>
	public struct CVector
	{
		/// <summary>
		/// X value of the vector.
		/// </summary>
		public int X;
		/// <summary>
		/// Y value of the vector.
		/// </summary>
		public int Y;

		/// <summary>
		/// Initialize the vector with a given <see cref="Point"/> interpreted as a vector.
		/// </summary>
		/// <param name="ptInit"><see cref="Point"/> to use for initialization.</param>
		public CVector(Point ptInit)
		{
			this.X = ptInit.X;
			this.Y = ptInit.Y;
		}

		/// <summary>
		/// Initialize the vector with a given <see cref="CPoint"/> interpreted as a vector.
		/// </summary>
		/// <param name="ptInit"><see cref="CPoint"/> to use for initialization.</param>
		public CVector(CPoint ptInit)
		{
			this.X = ptInit.X;
			this.Y = ptInit.Y;
		}

		/// <summary>
		/// Overloaded <see cref="operator -"/> to provide substraction of vectors.
		/// </summary>
		/// <param name="vSub1">Vector 1</param>
		/// <param name="vSub2">Vector 2</param>
		/// <returns>Returns the difference vector of the two given vectors.</returns>
		public static CVector operator -(CVector vSub1, CVector vSub2)
		{
			CVector vTemp;
			vTemp.X = vSub1.X - vSub2.X;
			vTemp.Y = vSub1.Y - vSub2.Y;
			return vTemp;
		}
	}

	/// <summary>
	/// Struct to provide a basic line type.
	/// </summary>
	public struct CLine
	{
		/// <summary>
		/// Starting point of the line represented by the structure.
		/// </summary>
		public CPoint ptStart;
		/// <summary>
		/// End point of the line represented by the structure.
		/// </summary>
		public CPoint ptEnd;


		/// <summary>
		/// Initializes the line with the given points used as staring point / end point of the line.
		/// </summary>
		/// <param name="ptStart">Staring point to set.</param>
		/// <param name="ptEnd">End point to set.</param>
		public CLine(CPoint ptStart, CPoint ptEnd)
		{
			this.ptStart = ptStart;
			this.ptEnd = ptEnd;
		}

		/// <summary>
		/// Initializes the line with a given segment, using the segment's starting and end point to define the line.
		/// </summary>
		/// <param name="segConvert">Segment to use for initialization.</param>
		public CLine(CSegment segConvert)
		{
			ptStart = segConvert.ptStart;
			ptEnd = segConvert.ptEnd;
		}

		/// <summary>
		/// Initializes the line with a given ray, using the ray's starting point and direction point to define the line.
		/// </summary>
		/// <param name="rayConvert">Ray to use for initialization.</param>
		public CLine(CRay rayConvert)
		{
			ptStart = rayConvert.ptStart;
			ptEnd = rayConvert.ptDir;
		}


		/// <summary>
		/// Cases which may result of the <see cref="intersectsWith(CLine, ref float, ref float)"/> method.
		/// </summary>
		public enum INTERSECTIONCASE
		{
			/// <summary>
			/// There is definitly NO intersection between the given lines, no matter if it's real lines, segments or rays.
			/// </summary>
			/// <remarks>This is the case when both lines are degenerated (points) and distinct.</remarks>
			INTERSECTION_SURE_NONE,
			/// <summary>
			/// There is definitly an intersection between the two lines and the intersection is a single point.
			/// </summary>
			/// <remarks>This is the case when both lines are degenerated (points) and the same.</remarks>
			INTERSECTION_SURE_POINT,
			/// <summary>
			/// It's possible that there is an intersection in a single point, depending on whether the given lines are segments, rays or real lines (or a mixture of those).
			/// Using the <see cref="intersectsWith(CLine, ref float, ref float)"/> method, the calling method can test for sure intersection by evaluating values sI and tI returned from <see cref="intersectsWith(CLine, ref float, ref float)"/>.
			/// </summary>
			INTERSECTION_POSSIBLE_POINT_SKEW,
			/// <summary>
			/// It's possible that there is an intersection in the form of an overlapping segment, depending on whether the given lines are segments, rays or real lines (or a mixture of those).
			/// Using the <see cref="intersectsWith(CLine, ref float, ref float)"/> method, the calling method can test for sure intersection by evaluating values sI and tI returned from <see cref="intersectsWith(CLine, ref float, ref float)"/>.
			/// </summary>
			INTERSECTION_POSSIBLE_POINT_OR_SEGMENT_COLLINEAR,
			/// <summary>
			/// It's possible that there is an intersection in the point given by the degenerated line L1, depending on whether the second line is a segment, ray or real line.
			/// Using the <see cref="intersectsWith(CLine, ref float, ref float)"/> method, the calling method can test for sure intersection by evaluating values sI and tI returned from <see cref="intersectsWith(CLine, ref float, ref float)"/>.
			/// </summary>
			INTERSECTION_POSSIBLE_POINT_COLLINEAR_DEGENERATE_L1,
			/// <summary>
			/// It's possible that there is an intersection in the point given by the degenerated line L2, depending on whether the first line is a segment, ray or real line.
			/// Using the <see cref="intersectsWith(CLine, ref float, ref float)"/> method, the calling method can test for sure intersection by evaluating values sI and tI returned from <see cref="intersectsWith(CLine, ref float, ref float)"/>.
			/// </summary>
			INTERSECTION_POSSIBLE_POINT_COLLINEAR_DEGENERATE_L2
		}

		// TODO: Review / rewrite the remarks in the documentational comment
		/// <summary>
		/// General test for intersection of lines (be it segments, rays or real lines) which returns detailed information about the intersection.
		/// </summary>
		/// <param name="lnLineOther">Line to test intersection with.</param>
		/// <param name="isp1">Reference to a variable in which to store the intersection parameter one.</param>
		/// <param name="isp2">Reference to a variable in which to store the intersection parameter one.</param>
		/// <returns>A <see cref="INTERSECTIONCASE"/> value indicating the type of intersection and wether the result is definite or needs further evaluation by the calling method.</returns>
		/// <remarks>
		/// <para>The two intersection parameters returned can be used to decide wether there really is an intersection or not in case the method is used for segments or rays.</para>
		/// <para>If one of the two lines is a segment, an intersection only exists if 0 <![CDATA[<]]>= s1 <![CDATA[<]]>= 1.</para>
		/// <para>If one of the two lines is a ray, an intersection only exists if s1 <![CDATA[>]]>= 0.</para>
		/// <para>If both lines are rays or segments, the same conditions must be true for BOTH parameters s1 AND t1.</para>
		///  </remarks>
		public INTERSECTIONCASE intersectsWith(CLine lnLineOther, ref float isp1, ref float isp2)
		{
			const float SMALL_NUM = 0.00000001F;

			isp1 = 0.0F;
			isp2 = 0.0F;

			CVector u = new CVector(ptEnd) - new CVector(ptStart);
			CVector v = new CVector(lnLineOther.ptEnd) - new CVector(lnLineOther.ptStart);
			CVector w = new CVector(ptStart) - new CVector(lnLineOther.ptStart);
			float D = perp(u, v);

			// test if they are parallel (includes either being a point)
			if (Math.Abs(D) < SMALL_NUM)
			{          // S1 and S2 are parallel
			    if (perp(u, w) != 0 || perp(v, w) != 0)
			    {
			        return INTERSECTIONCASE.INTERSECTION_SURE_NONE;                   // they are NOT collinear
			    }
			    // they are collinear or degenerate
			    // check if they are degenerate points
			    float du = dot(u, u);
			    float dv = dot(v, v);
			    if (du == 0 && dv == 0)
			    {           // both segments are points
			        if (ptStart != lnLineOther.ptStart)         // they are distinct points
			            return INTERSECTIONCASE.INTERSECTION_SURE_NONE;
			        //*I0 = S1.P0;                // they are the same point
			        return INTERSECTIONCASE.INTERSECTION_SURE_POINT;
			    }
			    if (du == 0)
			    {                    // S1 is a single point
					//if (!inSegment(ptLine1Start, ptLine2Start, ptLine2End))  // but is not in S2
					//    return INTERSECTIONCASE.INTERSECTION_SURE_NONE;
					////*I0 = S1.P0;
					//return INTERSECTIONCASE.INTERSECTION_SURE_POINT;
					return INTERSECTIONCASE.INTERSECTION_POSSIBLE_POINT_COLLINEAR_DEGENERATE_L1;
			    }
			    if (dv == 0)
			    {                    // S2 a single point
					//if (!inSegment(ptLine2Start, ptLine1Start, ptLine1End))  // but is not in S1
					//    return 0;
					////*I0 = S2.P0;
					//return INTERSECTIONCASE.INTERSECTION_SURE_POINT;
					return INTERSECTIONCASE.INTERSECTION_POSSIBLE_POINT_COLLINEAR_DEGENERATE_L2;
			    }
			    // they are collinear segments - get overlap (or not)
			    float t0, t1;                   // endpoints of S1 in eqn for S2
				CVector w2 = new CVector(ptEnd) - new CVector(lnLineOther.ptStart);
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

				//if (t0 > t1)
				//{                  // must have t0 smaller than t1
				//    float t = t0; t0 = t1; t1 = t;    // swap if not
				//}
				//if (t0 > 1 || t1 < 0)
				//{
				//    return INTERSECTIONCASE.INTERSECTION_SURE_NONE;     // NO overlap
				//}
				//t0 = t0 < 0 ? 0 : t0;              // clip to min 0
				//t1 = t1 > 1 ? 1 : t1;              // clip to max 1
				//if (t0 == t1)
				//{                 // intersect is a point
				//    //*I0 = S2.P0 + t0 * v;
				//    return INTERSECTIONCASE.INTERSECTION_SURE_POINT;
				//}

				//// they overlap in a valid subsegment
				////*I0 = S2.P0 + t0 * v;
				////*I1 = S2.P0 + t1 * v;

				isp1 = t0;
				isp2 = t1;
			    return INTERSECTIONCASE.INTERSECTION_POSSIBLE_POINT_OR_SEGMENT_COLLINEAR;
			}

			// the segments are skew and may intersect in a point
			// get the intersect parameter for S1
			float sI = perp(v, w) / D;
			//if (sI < 0 || sI > 1)               // no intersect with S1
			//    return INTERSECTIONCASE.INTERSECTION_SURE_NONE;

			// get the intersect parameter for S2
			float tI = perp(u, w) / D;
			//if (tI < 0 || tI > 1)               // no intersect with S2
			//    return INTERSECTIONCASE.INTERSECTION_SURE_NONE;

			//*I0 = S1.P0 + sI * u;               // compute S1 intersect point

			isp1 = sI;
			isp2 = tI;
			return INTERSECTIONCASE.INTERSECTION_POSSIBLE_POINT_SKEW;
		}

		/// <summary>
		/// Returns the perp product of two vectors.
		/// </summary>
		/// <param name="u">Vector 1</param>
		/// <param name="v">Vector 2</param>
		/// <returns>The perp product of vector 1 and vector 2.</returns>
		static private float perp(CVector u, CVector v)
		{
			return (float)(u.X * v.Y - u.Y * v.X);
		}

		/// <summary>
		/// Returns the perp product of two vectors.
		/// </summary>
		/// <param name="u">Vector 1</param>
		/// <param name="v">Vector 2</param>
		/// <returns>The perp product of vector 1 and vector 2.</returns>
		static private float dot(CVector u, CVector v)
		{
			return (float)(u.X * v.X + u.Y * v.Y);
		}

		/// <summary>
		/// Test if the line intersects with a given line.
		/// </summary>
		/// <param name="lnOther">Line to test for intersection with.</param>
		/// <returns>true if the two lines intersect, false otherwise.</returns>
		public bool intersectsWith(CLine lnOther)
		{
			float isp1 = 0.0F;
			float isp2 = 0.0F;
			switch (intersectsWith(lnOther, ref isp1, ref isp2))
			{
				case INTERSECTIONCASE.INTERSECTION_SURE_NONE:
					return false;

				case INTERSECTIONCASE.INTERSECTION_POSSIBLE_POINT_COLLINEAR_DEGENERATE_L1:
				case INTERSECTIONCASE.INTERSECTION_POSSIBLE_POINT_COLLINEAR_DEGENERATE_L2:
				case INTERSECTIONCASE.INTERSECTION_POSSIBLE_POINT_OR_SEGMENT_COLLINEAR:
				case INTERSECTIONCASE.INTERSECTION_POSSIBLE_POINT_SKEW:
				case INTERSECTIONCASE.INTERSECTION_SURE_POINT:
					return true;
			}

			// Should never reach this code, just to prevent the compiler from complaining about code paths not returning a value
			throw new Exception("Unhandled line intersection case!");
			
		}


		/// <summary>
		/// Returns the lines gradient / inclination.
		/// </summary>
		/// <remarks>If the line is vertical, positive / negative infinity is returned. If the line is degenerated (i.e. a single point), 0 is returned.</remarks>
		public float Inclination
		{
			get
			{
				if(ptStart.X == ptEnd.X){
					if (ptEnd.Y > ptStart.Y)
						return float.PositiveInfinity;
					else if (ptEnd.Y < ptStart.Y)
						return float.NegativeInfinity;
					else
						return 0.0F;
				}
				
				return ((float)ptEnd.Y - (float)ptStart.Y) / ((float)ptEnd.X - (float)ptStart.X);
			}
		}
	}

	/// <summary>
	/// Structure to represent a line segment and provide basic operations on it.
	/// </summary>
	[Serializable]
	public struct CSegment
	{
		/// <summary>
		/// Starting point of the segment.
		/// </summary>
		public CPoint ptStart;
		/// <summary>
		/// End point of the segment.
		/// </summary>
		public CPoint ptEnd;


		/// <summary>
		/// Initializes the segment with a given starting and end point of type <see cref="CPoint"/>.
		/// </summary>
		/// <param name="ptStart">The starting point.</param>
		/// <param name="ptEnd">The end point.</param>
		public CSegment(CPoint ptStart, CPoint ptEnd)
		{
			this.ptStart = ptStart;
			this.ptEnd = ptEnd;
		}


		/// <summary>
		/// Conversion operator to provide conversion of a <see cref="CSegment"/> to a <see cref="CLine"/>.
		/// </summary>
		/// <param name="segConvert"><see cref="CSegment"/> to convert.</param>
		/// <returns>A <see cref="CLine"/> object defined by the segments starting and end point.</returns>
		public static explicit operator CLine(CSegment segConvert)
		{
			return new CLine(segConvert);
		}

		/// <summary>
		/// Test if a point which is known to be "collinear" with the segment is inside (i.e. intersects with) the segment.
		/// </summary>
		/// <param name="ptTest">Point to test.</param>
		/// <returns>true if inside (or on the end points), false otherwise.</returns>
		public bool containsCollinear(CPoint ptTest)
		{
			if (ptStart.X != ptEnd.X)
			{
				if (ptStart.X <= ptTest.X && ptTest.X <= ptEnd.X)
					return true;
				if (ptStart.X >= ptTest.X && ptTest.X >= ptEnd.X)
					return true;
			}
			else
			{
				if (ptStart.Y <= ptTest.Y && ptTest.Y <= ptEnd.Y)
					return true;
				if (ptStart.Y >= ptTest.Y && ptTest.Y >= ptEnd.Y)
					return true;
			}

			return false;
		}

		/// <summary>
		/// Test is the current segment intersects with a given segment.
		/// </summary>
		/// <param name="segTest">Segment to test for intersection with.</param>
		/// <returns>true if there is an intersection (either a single point or an overlapping segment), false otherwise.</returns>
		public bool intersectsWith(CSegment segTest)
		{
			float isp1 = 0.0F;
			float isp2 = 0.0F;

			switch (((CLine)segTest).intersectsWith((CLine)this, ref isp1, ref isp2))
			{
				case CLine.INTERSECTIONCASE.INTERSECTION_SURE_NONE:
					return false;

				case CLine.INTERSECTIONCASE.INTERSECTION_SURE_POINT:
					return true;

				case CLine.INTERSECTIONCASE.INTERSECTION_POSSIBLE_POINT_COLLINEAR_DEGENERATE_L1:
					return segTest.containsCollinear(ptStart);

				case CLine.INTERSECTIONCASE.INTERSECTION_POSSIBLE_POINT_COLLINEAR_DEGENERATE_L2:
					return containsCollinear(segTest.ptStart);

				case CLine.INTERSECTIONCASE.INTERSECTION_POSSIBLE_POINT_OR_SEGMENT_COLLINEAR:
					if (Math.Min(isp1, isp2) > 1 || Math.Max(isp1, isp2) < 0)
						return false;
					else
						return true;
				
				case CLine.INTERSECTIONCASE.INTERSECTION_POSSIBLE_POINT_SKEW:
					return (isp1 >= 0 && isp1 <= 1 && isp2 >= 0 && isp2 <= 1);
			}

			// Should never reach this code, just to prevent the compiler from complaining about code paths not returning a value
			throw new Exception("Unhandled line intersection case!");
		}

		/// <summary>
		/// Tests if current segment intersects with a given segment.
		/// </summary>
		/// <param name="segTest">Segment to test for intersection with.</param>
		/// <param name="ptIntersectionStart">Variable to store the resulting intersection starting point in.</param>
		/// <param name="ptIntersectionEnd">Variable to store the resulting intersection end point in.</param>
		/// <returns>true if segments intersect, false otherwise.</returns>
		/// <remarks>
		/// <para>This function contains untested parts of code. Please have a look at the code to ensure it's correctness.</para>
		/// <para>There is also a more accurate version of this function taking a <see cref="CPointF"/> for storing the results. 
		/// Thus, results are not trimmed to int values. See <see cref="intersectsWith(CSegment, out CPointF, out CPointF)"/> for more information.</para>
		/// <para>If intersection is a single point, the two <see cref="CPoint"/> out variables will hold the same point, otherwise
		/// they will hold different values.</para>
		/// <para>If the function returns false, i.e. there is no intersection, the out variables are not guaranteed to hold a certain value.</para>
		/// </remarks>
		public bool intersectsWith(CSegment segTest, out CPoint ptIntersectionStart, out CPoint ptIntersectionEnd)
		{
			// TODO: This functions contains untested code which need review

			ptIntersectionStart = new CPoint();
			ptIntersectionEnd = new CPoint();

			float isp1 = 0.0F;
			float isp2 = 0.0F;

			switch (((CLine)segTest).intersectsWith((CLine)this, ref isp1, ref isp2))
			{
				case CLine.INTERSECTIONCASE.INTERSECTION_SURE_NONE:
					return false;

				case CLine.INTERSECTIONCASE.INTERSECTION_SURE_POINT:
					return true;

				case CLine.INTERSECTIONCASE.INTERSECTION_POSSIBLE_POINT_COLLINEAR_DEGENERATE_L1:
					if (segTest.containsCollinear(ptStart))
					{
						ptIntersectionStart = new CPoint(ptStart.X, ptStart.Y);
						ptIntersectionEnd = new CPoint(ptStart.X, ptStart.Y);

						return true;
					}
					else
						return false;

				case CLine.INTERSECTIONCASE.INTERSECTION_POSSIBLE_POINT_COLLINEAR_DEGENERATE_L2:
					if(containsCollinear(segTest.ptStart))
					{
						ptIntersectionStart = new CPoint(segTest.ptStart.X, segTest.ptStart.Y);
						ptIntersectionEnd = new CPoint(segTest.ptStart.X, segTest.ptStart.Y);

						return true;
					}
					else
						return false;

				case CLine.INTERSECTIONCASE.INTERSECTION_POSSIBLE_POINT_OR_SEGMENT_COLLINEAR:
					if (Math.Min(isp1, isp2) > 1 || Math.Max(isp1, isp2) < 0)
						return false;
					else
					{
						// TODO: Check this part for correctness of the following calculation

						int x1 = (int)((float)segTest.ptStart.X + (float)(segTest.ptEnd.X - segTest.ptStart.X) * isp1);
						int y1 = (int)((float)segTest.ptStart.Y + (float)(segTest.ptEnd.Y - segTest.ptStart.Y) * isp1);

						int x2 = (int)((float)segTest.ptStart.X + (float)(segTest.ptEnd.X - segTest.ptStart.X) * isp2);
						int y2 = (int)((float)segTest.ptStart.Y + (float)(segTest.ptEnd.Y - segTest.ptStart.Y) * isp2);

						ptIntersectionStart = new CPoint(x1, y1);
						ptIntersectionEnd = new CPoint(x2, y2);

						return true;
					}
				
				case CLine.INTERSECTIONCASE.INTERSECTION_POSSIBLE_POINT_SKEW:
					// TODO: Check this part for correctness of the calculation (Also the CLine.intersectsWith suggest using isp1, isp2 seems to be the correct solution)
					// TODO: Replace this by implementing *, -, + operators for CPoint class
					
					int x = (int)((float)ptStart.X + (float)(ptEnd.X - ptStart.X) * isp2);
					int y = (int)((float)ptStart.Y + (float)(ptEnd.Y - ptStart.Y) * isp2);

					ptIntersectionStart = new CPoint(x, y);
					ptIntersectionEnd = new CPoint(x, y);
					
					return (isp1 >= 0 && isp1 <= 1 && isp2 >= 0 && isp2 <= 1);
			}

			// Should never reach this code, just to prevent the compiler from complaining about code paths not returning a value
			throw new Exception("Unhandled line intersection case!");
		}

		/// <summary>
		/// Tests if current segment intersects with a given segment.
		/// </summary>
		/// <param name="segTest">Segment to test for intersection with.</param>
		/// <param name="ptIntersectionStart">Variable to store the resulting intersection starting point in.</param>
		/// <param name="ptIntersectionEnd">Variable to store the resulting intersection end point in.</param>
		/// <returns>true if segments intersect, false otherwise.</returns>
		/// <remarks>
		/// <para>This function contains untested parts of code. Please have a look at the code to ensure it's correctness.</para>
		/// <para>If intersection is a single point, the two <see cref="CPointF"/> out variables will hold the same point, otherwise
		/// they will hold different values.</para>
		/// <para>If the function returns false, i.e. there is no intersection, the out variables are not guaranteed to hold a certain value.</para>
		/// </remarks>
		public bool intersectsWith(CSegment segTest, out CPointF ptIntersectionStart, out CPointF ptIntersectionEnd)
		{
			// TODO: This functions contains untested code which need review

			ptIntersectionStart = new CPointF();
			ptIntersectionEnd = new CPointF();

			float isp1 = 0.0F;
			float isp2 = 0.0F;

			switch (((CLine)segTest).intersectsWith((CLine)this, ref isp1, ref isp2))
			{
				case CLine.INTERSECTIONCASE.INTERSECTION_SURE_NONE:
					return false;

				case CLine.INTERSECTIONCASE.INTERSECTION_SURE_POINT:
					return true;

				case CLine.INTERSECTIONCASE.INTERSECTION_POSSIBLE_POINT_COLLINEAR_DEGENERATE_L1:
					if (segTest.containsCollinear(ptStart))
					{
						ptIntersectionStart = new CPointF((float)ptStart.X, (float)ptStart.Y);
						ptIntersectionEnd = new CPointF((float)ptStart.X, (float)ptStart.Y);

						return true;
					}
					else
						return false;

				case CLine.INTERSECTIONCASE.INTERSECTION_POSSIBLE_POINT_COLLINEAR_DEGENERATE_L2:
					if (containsCollinear(segTest.ptStart))
					{
						ptIntersectionStart = new CPointF((float)segTest.ptStart.X, (float)segTest.ptStart.Y);
						ptIntersectionEnd = new CPointF((float)segTest.ptStart.X, (float)segTest.ptStart.Y);

						return true;
					}
					else
						return false;

				case CLine.INTERSECTIONCASE.INTERSECTION_POSSIBLE_POINT_OR_SEGMENT_COLLINEAR:
					if (Math.Min(isp1, isp2) > 1 || Math.Max(isp1, isp2) < 0)
						return false;
					else
					{
						// TODO: Check this part for correctness of the following calculation

						float x1 = ((float)segTest.ptStart.X + (float)(segTest.ptEnd.X - segTest.ptStart.X) * isp1);
						float y1 = ((float)segTest.ptStart.Y + (float)(segTest.ptEnd.Y - segTest.ptStart.Y) * isp1);

						float x2 = ((float)segTest.ptStart.X + (float)(segTest.ptEnd.X - segTest.ptStart.X) * isp2);
						float y2 = ((float)segTest.ptStart.Y + (float)(segTest.ptEnd.Y - segTest.ptStart.Y) * isp2);

						ptIntersectionStart = new CPointF(x1, y1);
						ptIntersectionEnd = new CPointF(x2, y2);

						return true;
					}

				case CLine.INTERSECTIONCASE.INTERSECTION_POSSIBLE_POINT_SKEW:
					// TODO: Check this part for correctness of the calculation (Also the CLine.intersectsWith suggest using isp1, isp2 seems to be the correct solution)
					// TODO: Replace this by implementing *, -, + operators for CPoint class
					
					float x = ((float)ptStart.X + (float)(ptEnd.X - ptStart.X) * isp2);
					float y = ((float)ptStart.Y + (float)(ptEnd.Y - ptStart.Y) * isp2);

					ptIntersectionStart = new CPointF(x, y);
					ptIntersectionEnd = new CPointF(x, y);

					return (isp1 >= 0 && isp1 <= 1 && isp2 >= 0 && isp2 <= 1);
			}

			// Should never reach this code, just to prevent the compiler from complaining about code paths not returning a value
			throw new Exception("Unhandled line intersection case!");
		}
	}

	/// <summary>
	/// Structure to represent a ray and provide basic operations on it.
	/// </summary>
	public struct CRay
	{
		/// <summary>
		/// Starting point of the ray.
		/// </summary>
		public CPoint ptStart;
		/// <summary>
		/// Point indication the ray's direction.
		/// </summary>
		public CPoint ptDir;

		/// <summary>
		/// Initialize a ray with two given <see cref="CPoint"/>s.
		/// </summary>
		/// <param name="ptStart">Point beeing the ray's starting point.</param>
		/// <param name="ptDir">Point indicating the ray's direction.</param>
		public CRay(CPoint ptStart, CPoint ptDir)
		{
			this.ptStart = ptStart;
			this.ptDir = ptDir;
		}

		/// <summary>
		/// Test for intersection of the ray with a given segment.
		/// </summary>
		/// <param name="segTest">Segement to test for intersection with.</param>
		/// <returns>true if the ray intersects with the segment, false otherwise.</returns>
		public bool intersectsWith(CSegment segTest)
		{
			float isp1 = 0.0F;
			float isp2 = 0.0F;

			switch (((CLine)segTest).intersectsWith((CLine)this, ref isp1, ref isp2))
			{
				case CLine.INTERSECTIONCASE.INTERSECTION_SURE_NONE:
					return false;

				case CLine.INTERSECTIONCASE.INTERSECTION_SURE_POINT:
					return true;

				case CLine.INTERSECTIONCASE.INTERSECTION_POSSIBLE_POINT_COLLINEAR_DEGENERATE_L1:
					return segTest.containsCollinear(ptStart);

				case CLine.INTERSECTIONCASE.INTERSECTION_POSSIBLE_POINT_COLLINEAR_DEGENERATE_L2:
					return containsCollinear(segTest.ptStart);

				case CLine.INTERSECTIONCASE.INTERSECTION_POSSIBLE_POINT_OR_SEGMENT_COLLINEAR:
					return !(Math.Max(isp1, isp2) < 0);
				
				case CLine.INTERSECTIONCASE.INTERSECTION_POSSIBLE_POINT_SKEW:
					return (isp1 >= 0 && isp1 <= 1 && isp2 >= 0);
			}
			
			// Should never reach this code, just to prevent the compiler from complaining about code paths not returning a value
			throw new Exception("Unhandled line intersection case!");
		}

		/// <summary>
		/// Conversion operator provided to convert a given <see cref="CRay"/> object to type <see cref="CLine"/>.
		/// </summary>
		/// <param name="rayConvert">Ray to convert.</param>
		/// <returns>A <see cref="CLine"/> object defined by the ray's starting point and direction indicator.</returns>
		public static explicit operator CLine(CRay rayConvert)
		{
			return new CLine(rayConvert);
		}

		/// <summary>
		/// Tests if a given point which is known to be collinear with the ray is contained in (i.e intersects with) the ray.
		/// </summary>
		/// <param name="ptTest">Point to test.</param>
		/// <returns>true if contained in the ray, false otherwise.</returns>
		public bool containsCollinear(CPoint ptTest)
		{
			if (ptStart.X != ptDir.X)
			{    // S is not vertical
				if (ptStart.X <= ptTest.X && ptStart.X <= ptDir.X)
					return true;
				if (ptStart.X >= ptTest.X && ptStart.X >= ptDir.X)
					return true;
			}
			else
			{    // S is vertical, so test y coordinate
				if (ptStart.Y <= ptTest.Y && ptStart.Y <= ptDir.Y)
					return true;
				if (ptStart.Y >= ptTest.Y && ptStart.Y >= ptDir.Y)
					return true;
			}

			return false;
		}
	}

	/// <summary>
	/// Simple triangle type which provides basic operations like <see cref="Area"/> , <see cref="signedArea"/> or <see cref="contains(CPoint)"/>.
	/// </summary>
	[Serializable]
	public struct CTriangle
	{
		/// <summary>
		/// Point A
		/// </summary>
		public CPoint ptA;
		/// <summary>
		/// Point B
		/// </summary>
		public CPoint ptB;
		/// <summary>
		/// Point C
		/// </summary>
		public CPoint ptC;

		/// <summary>
		/// Initializes the strcuture with three given points of type <see cref="CPoint"/>.
		/// </summary>
		/// <param name="ptA">Point 1</param>
		/// <param name="ptB">Point 2</param>
		/// <param name="ptC">Point 3</param>
		public CTriangle(CPoint ptA, CPoint ptB, CPoint ptC)
		{
			this.ptA = ptA;
			this.ptB = ptB;
			this.ptC = ptC;
		}

		/// <summary>
		/// Tests if a given point lays inside the triangle or on the outside.
		/// </summary>
		/// <param name="ptTest">Point to test.</param>
		/// <returns>true if inside (or on the border), false otherwise.</returns>
		/// <remarks>To test whether a point is STRICTLY inside the triangle or not (i.e. not outside but 
		/// neither on the border), use <see cref="containsStrict"/>.</remarks>
		/// <seealso cref="containsStrict"/>
		public bool contains(CPoint ptTest)
		{
			int iSign1 = Math.Sign(new CTriangle(ptA, ptTest, ptC).signedArea);
			int iSign2 = Math.Sign(new CTriangle(ptC, ptTest, ptB).signedArea);
			int iSign3 = Math.Sign(new CTriangle(ptB, ptTest, ptA).signedArea);

			if ((iSign1 == 0 && iSign2 == 0) || (iSign2 == 0 && iSign3 == 0) || (iSign3 == 0 && iSign1 == 0))
				return true;

			if (iSign1 == 0)
				return (iSign2 == iSign3);
			if (iSign2 == 0)
				return (iSign1 == iSign3);
			if (iSign3 == 0)
				return (iSign1 == iSign2);

			return (iSign1 == iSign2 && iSign2 == iSign3);
		}

		/// <summary>
		/// Test if a given point is inside the triangle. Furthermore returns the distances to the triangles edges.
		/// </summary>
		/// <param name="ptTest">Point to test.</param>
		/// <param name="fSA1">Distance of ptTest from side [AC].</param>
		/// <param name="fSA2">Distance of ptTest from side [CB].</param>
		/// <param name="fSA3">Distance of ptTest from side [BA].</param>
		/// <returns>true if point is inside, false otherwise.</returns>
		/// <remarks><para>This function is identically to <see cref="contains(CPoint)"/> except that it returns more information.</para>
		/// <para>Note that this method shares the same difference with <see cref="containsStrict"/> as <see cref="contains(CPoint)"/> does!</para></remarks>
		public bool contains(CPoint ptTest, ref float fSA1, ref float fSA2, ref float fSA3)
		{
			fSA1 = new CTriangle(ptA, ptTest, ptC).signedArea;
			fSA2 = new CTriangle(ptC, ptTest, ptB).signedArea;
			fSA3 = new CTriangle(ptB, ptTest, ptA).signedArea;

			int iSign1 = Math.Sign(fSA1);
			int iSign2 = Math.Sign(fSA2);
			int iSign3 = Math.Sign(fSA3);

			if ((iSign1 == 0 && iSign2 == 0) || (iSign2 == 0 && iSign3 == 0) || (iSign3 == 0 && iSign1 == 0))
				return true;

			if (iSign1 == 0)
				return (iSign2 == iSign3);
			if (iSign2 == 0)
				return (iSign1 == iSign3);
			if (iSign3 == 0)
				return (iSign1 == iSign2);

			return (iSign1 == iSign2 && iSign2 == iSign3);
		}

		/// <summary>
		/// Tests if a given point lays STRICTLY inside the triangle or on the outside.
		/// </summary>
		/// <param name="ptTest">Point to test.</param>
		/// <returns>true if inside, false if outside or on the border.</returns>
		/// <remarks>To test for containment with points on the border considered inside, 
		/// use <see cref="contains(CPoint)"/>.</remarks>
		/// <seealso cref="contains(CPoint)"/>
		public bool containsStrict(CPoint ptTest)
		{
			int iSign1 = Math.Sign(new CTriangle(ptA, ptTest, ptC).signedArea);
			int iSign2 = Math.Sign(new CTriangle(ptC, ptTest, ptB).signedArea);
			int iSign3 = Math.Sign(new CTriangle(ptB, ptTest, ptA).signedArea);

			return (iSign1 == iSign2 && iSign2 == iSign3);
		}

		/// <summary>
		/// Returns the signed area of the triangle.
		/// </summary>
		/// <remarks>This method is usually used for distance or containment tests. To get a 
		/// positive area value use <see cref="Area"/>.</remarks>
		/// <seealso cref="Area"/>
		public float signedArea
		{
			get
			{
				return ptB.X * ptC.Y + ptA.X * ptB.Y + ptC.X * ptA.Y - ptB.X * ptA.Y - ptC.X * ptB.Y - ptA.X * ptC.Y;
			}
		}

		/// <summary>
		/// Returns the (positive) area of the triangle.
		/// </summary>
		public double Area
		{
			get
			{
				return Math.Abs(signedArea);
			}
		}
	}

	/// <summary>
	/// A very simple structure to represent a diagonal in a polygon.
	/// </summary>
	/// <remarks><para>This strucutre is mainly used for storing list of diagonals without too much memory overhead. 
	/// Besides, it improves the code's readability.</para>
	/// <para>This structure doesn't provide much functionality. Any methods needed should find a better place in 
	/// the <see cref="CLine"/>, <see cref="CSegment"/> or <see cref="CRay"/> structures.</para></remarks>
	/// <seealso cref="CLine"/>
	/// <seealso cref="CSegment"/>
	/// <seealso cref="CRay"/>
	public struct CDiagonal
	{
		/// <summary>
		/// The starting point of the diagonal.
		/// </summary>
		public CPoint ptStart;
		/// <summary>
		/// The end point of the diagonal.
		/// </summary>
		public CPoint ptEnd;

		/// <summary>
		/// Initializes the structures from two given points of type <see cref="CPoint"/>.
		/// </summary>
		/// <param name="ptStart">Starting point.</param>
		/// <param name="ptEnd">End point.</param>
		public CDiagonal(CPoint ptStart, CPoint ptEnd)
		{
			this.ptStart = ptStart;
			this.ptEnd = ptEnd;
		}
	}


	/// <summary>
	/// Class to provide basic algorithms on poligons.
	/// </summary>
	[Serializable]
	public class CPolygon
	{
		List<CPoint> listVertices;
		CRectangle rcBoundingBox;
		int iIndexLeftMost;
		int iIndexRightMost;


		/// <summary>
		/// Property to access the list of vertices the polygon consists of.
		/// </summary>
		public Point[] vertices
		{
			get
			{
				Point[] vertices = new Point[listVertices.Count];
				for (int i = 0; i < listVertices.Count; i++)
					vertices[i] = new Point(listVertices[i].X, listVertices[i].Y);

				return vertices;
			}
		}


		/// <summary>
		/// Returns the logical size of the current polygon.
		/// </summary>
		/// <remarks>
		/// <para>"Size" refers to the number of vertices the polygon consists of.</para>
		/// <para>Due to optimization purposes, the internal list of the polygons vertices ends with a copy of the first vertex. 
		/// Thus, the number of vertices the polygon consists of is the size of the vertex list minus one. This is exactly what this function
		/// returns.</para>
		/// <para>To get the actual list size, use <see cref="Size"/>.</para></remarks>
		public int SizeLogical
		{
			get
			{
				return listVertices.Count - 1;
			}
		}

		/// <summary>
		/// Returns the actual size of the polygon's vertex list.
		/// </summary>
		/// <remarks>This value differs from the real number of vertices the polygon consists of. See <see cref="SizeLogical"/> for more information.</remarks>
		public int Size
		{
			get
			{
				return listVertices.Count;
			}
		}


		/// <summary>
		/// Returns the list index of the polygon's left most point.
		/// </summary>
		/// <remarks>To get a list of the polygons vertices, use <see cref="vertices"/>.</remarks>
		public int LeftMostPointIndex
		{
			get
			{
				return iIndexLeftMost;
			}
		}

		/// <summary>
		/// Returns the polygon's left most point.
		/// </summary>
		public CPoint LeftMostPoint
		{
			get
			{
				return listVertices[iIndexLeftMost];
			}
		}

		/// <summary>
		/// Returns the list index of the polygon's right most point.
		/// </summary>
		/// <remarks>To get a list of the polygons vertices, use <see cref="vertices"/>.</remarks>
		public int RightMostPointIndex
		{
			get
			{
				return iIndexRightMost;
			}
		}

		/// <summary>
		/// Returns the polygon's right most point.
		/// </summary>
		public CPoint RightMostPoint
		{
			get
			{
				return listVertices[iIndexRightMost];
			}
		}


		/// <summary>
		/// Initializes the polygon object with a given list of points / vertices.
		/// </summary>
		/// <param name="listPoints">List of vertices.</param>
		public CPolygon(List<Point> listPoints)
		{
			// TODO: One might introduce some security check here to make sure we're dealing with a simple polygon

			List<CPoint> listPointsTemp = new List<CPoint>(listPoints.Count);
			foreach (Point ptTemp in listPoints)
				listPointsTemp.Add(new CPoint(ptTemp));

			CPolygonHelper(listPointsTemp);
		}

		/// <summary>
		/// Initializes the polygon object with a given list of points / vertices.
		/// </summary>
		/// <param name="listPoints">List of vertices.</param>
		public CPolygon(List<CPoint> listPoints)
		{
			CPolygonHelper(listPoints);
		}

		/// <summary>
		/// Internal helper function to avoide code redundancy for common initialization code.
		/// </summary>
		/// <param name="listPoints">List of vertices to initialize the polygon with.</param>
		private void CPolygonHelper(List<CPoint> listPoints)
		{
			// TODO: One might introduce some security check here to make sure we're dealing with a simple polygon


			if (listPoints.Count < 3)
				throw new Exception("Polygon must consist of at least three vertices!");


			listVertices = new List<CPoint>(listPoints.Count);
			tsdThis.bIsStarted = false;


			CPoint ptMin = new CPoint();
			CPoint ptMax = new CPoint();

			ptMin.X = listPoints[0].X;
			ptMin.Y = listPoints[0].Y;
			ptMax = ptMin;

			iIndexLeftMost = 0;

			foreach (CPoint ptTemp in listPoints)
			{
				listVertices.Add(ptTemp);

				if (ptTemp.X < ptMin.X)
				{
					ptMin.X = ptTemp.X;

					// Remember the left most point's index
					iIndexLeftMost = listVertices.Count - 1;
				}
				if (ptTemp.Y < ptMin.X) ptMin.Y = ptTemp.Y;
				if (ptTemp.X > ptMax.X)
				{
					ptMax.X = ptTemp.X;

					// Remember the right most point's index
					iIndexRightMost = listVertices.Count - 1;
				}
				if (ptTemp.Y > ptMax.Y) ptMax.Y = ptTemp.Y;
			}

			rcBoundingBox = new CRectangle(ptMin, ptMax);
		}


		/// <summary>
		/// Test wether a given point is STRICTLY inside the polygon. Points on the border are considered out.
		/// </summary>
		/// <param name="ptTest">Point to test.</param>
		/// <returns>true if inside, false otherwise (outside or on the border).</returns>
		public bool containsStrict(CPoint ptTest)
		{
			int iCount = 0;
			for (int i = 0; i < listVertices.Count - 1; i++)
			{
				if ((listVertices[i].Y <= ptTest.Y && listVertices[i + 1].Y > ptTest.Y) ||		//upward crossing or...
					(listVertices[i].Y > ptTest.Y && listVertices[i + 1].Y <= ptTest.Y))		//downward crossing
				{
					// Compute the intersection x-coordinate
					float vt = ((float)(ptTest.Y - listVertices[i].Y)) / (float)((listVertices[i + 1].Y - listVertices[i].Y));
					float xt = (float)listVertices[i].X + vt * (float)(listVertices[i + 1].X - listVertices[i].X);

					if (ptTest.X == xt)
						return false;
					else if (ptTest.X < xt)
						iCount++;
				}
			}

			return (iCount & 1) == 1;
		}

		/// <summary>
		/// Tests whether a given point is inside the polygon. Points on the border are considered in.
		/// </summary>
		/// <param name="ptTest">Point to test.</param>
		/// <returns>true if inside (or on border), false otherwise.</returns>
		public bool contains(CPoint ptTest)
		{
			int iCount = 0;
			for (int i = 0; i < listVertices.Count - 1; i++)
			{
				if ((listVertices[i].Y <= ptTest.Y && listVertices[i + 1].Y > ptTest.Y) ||		//upward crossing or...
					(listVertices[i].Y > ptTest.Y && listVertices[i + 1].Y <= ptTest.Y))		//downward crossing
				{
					// Compute the intersection x-coordinate
					float vt = (float)(ptTest.Y - listVertices[i].Y) / (listVertices[i + 1].Y - listVertices[i].Y);
					float xt = listVertices[i].X + vt * (listVertices[i + 1].X - listVertices[i].X);

					if (ptTest.X == xt)
						return false;
					else if (ptTest.X < xt)
						iCount++;
				}
			}

			return (iCount & 1) == 1;
		}

		/// <summary>
		/// Tests if the current polygon striclty contains a given polygon which is known to be non-intersecting (either completely 
		/// inside or outside) the current polygon.
		/// </summary>
		/// <param name="pTest">Polygon to test.</param>
		/// <returns>
		/// <para>This method ONLY returns a correct result if the passed polygon IS CERTAIN not to intersect with 
		/// the current polygon. Do not use the method with polygons that might intersect.</para>
		/// <para>The strict version of this function checks for strict containment of the passed polygon, i.e. points
		/// of this polygon mustn't lay on the outside of the current polygon, neither on it's border. Therefore, if this
		/// method is used, it should be used on a polygon whose vetices have been checked with <see cref="containsStrict(CPoint)"/>.</para>
		/// </returns>
		public bool containsNonIntersectingStrict(ref CPolygon pTest)
		{
			if (containsStrict(new CPoint(pTest.vertices[0])))
				return true;
			else
				return false;
		}

		/// <summary>
		/// Tests whether a given segment intersects with the current polygon.
		/// </summary>
		/// <param name="segTest">Segment to test for intersection.</param>
		/// <returns>true if there is an intersection, false otherwise.</returns>
		public bool intersectsWith(CSegment segTest)
		{
			for (int i = 0; i < listVertices.Count - 1; i++)
			{
				if(segTest.intersectsWith(new CSegment(listVertices[i], listVertices[i + 1])))
					return true;
			}

			return false;
		}


		#region Triangulation

		/// <summary>
		/// Triagnulates the current polygon (in O(n²) by finding and cutting "ears").
		/// </summary>
		/// <returns>The resulting list of diagonals which triangulate the polygon.</returns>
		/// <remarks>The algorithm uses the triangulation method of finding an cutting "ears" combined with
		/// a divide an conquer mechanism.</remarks>
		public List<CDiagonal> Triangulate()
		{
			// Initialization:

			// Temporary copy of the vertex list which we're going to use (as we're going to drop vertices in this algorithm)
			List<CPoint> listVerticesWork = new List<CPoint>(listVertices);

			// Check whether vertices are given in CCW or CW in the list (if CCW, reverse)
			CPoint ptLeftMost = listVerticesWork[iIndexLeftMost];
			CPoint ptBefore = listVerticesWork[iIndexLeftMost == 0 ? listVerticesWork.Count - 2 : iIndexLeftMost - 1];
			CPoint ptAfter = listVerticesWork[(iIndexLeftMost + 1) % (listVerticesWork.Count - 1)];

			if (new CLine(ptLeftMost, ptAfter).Inclination > new CLine(ptLeftMost, ptBefore).Inclination)
				listVerticesWork.Reverse();
			
			int iVertices = listVerticesWork.Count - 1;

			// Create list to hold diagonals (and allocate it with a defined length, as we already know the exact number of diagonals)
			List<CDiagonal> listDiagonals = new List<CDiagonal>(iVertices - 3);
			

			// Create a stack (and allocate with a defined number, as we can't divide the polygon in more than n - 2 smaller polygons)
			Stack<List<CPoint>> stackPolygons = new Stack<List<CPoint>>((iVertices - 2));

			// Push the current polygon onto the stack
			stackPolygons.Push(listVerticesWork);


			// Run:

			while (stackPolygons.Count != 0)
			{
				List<CPoint> listCurrent = stackPolygons.Pop();
				int n = listCurrent.Count - 1;
				
				int i = 0;
				while (n > 3)
				{
					if (i > (n - 2))
						i = 0;

					while (!(new CTriangle(listCurrent[i], listCurrent[i + 1], listCurrent[i + 2]).signedArea > 0))
					{
						i++;

						if (i > (n - 2))
							i = 0;

						// TODO: Should / could be removed after enough testing (when sure the algorithm does it's job in all cases)
						if (!(i < (n - 1)))
							throw new Exception("Algorithm logic exception!");
					}

					int v1 = i;
					int v2 = i + 1;
					int v3 = i + 2;

					bool bInside = false;
					float fSignedArea = 0.0F;
					int iIndexClosest = 0;

					for (int j = 1; j < n + 1; j++)
					{
						if (j == v1 || j == v2 || j == v3)
							continue;

						if (v1 == 0 || v3 == n)
							if (j == n)
								continue;

						if (listCurrent[j] == listCurrent[v1] || listCurrent[j] == listCurrent[v2] || listCurrent[j] == listCurrent[v3])
							continue;


						float fSA1 = 0.0F;
						float fSA2 = 0.0F;
						float fSA3 = 0.0F;

						// TODO: Think about how to avoid at least once or completely the using Math.Abs in the following lines
						// TODO: Calculation "(v3 + i + 1) % c" is used two times, calculate it once, save it in varibale and then use the variable instead of recalculating it each time
						// {

						if (new CTriangle(listCurrent[v1], listCurrent[v2], listCurrent[v3]).contains(listCurrent[j], ref fSA1, ref fSA2, ref fSA3) && listCurrent[j] != listCurrent[v1] && listCurrent[j] != listCurrent[v2] && listCurrent[j] != listCurrent[v3])
						{
							if (!bInside)
							{
								bInside = true;
								fSignedArea = Math.Abs(fSA1);
								iIndexClosest = j;
							}
							else
							{
								if (Math.Abs(fSA1) > fSignedArea)
								{
									fSignedArea = Math.Abs(fSA1);
									iIndexClosest = j;
								}
							}
						}

						// }

					}

					if (bInside)
					{
						if (iIndexClosest > v2)
						{
							// Part 1 contains vertices [v2,...,iIndexClosest,v2]
							List<CPoint> listPart1 = new List<CPoint>((iIndexClosest - v2 + 1) + 1);
							listPart1.AddRange(listCurrent.GetRange(v2, iIndexClosest - v2 + 1));
							listPart1.Add(listCurrent[v2]);

							// Part 2 contains vertices [0,...,v2,iIndexClosest,...,n-1,0]
							List<CPoint> listPart2 = new List<CPoint>((n - (iIndexClosest - v2 - 1)) + 1);
							listPart2.AddRange(listCurrent.GetRange(0, v2 + 1));
							listPart2.AddRange(listCurrent.GetRange(iIndexClosest, n - iIndexClosest));
							listPart2.Add(listCurrent[0]);

							stackPolygons.Push(listPart1);
							stackPolygons.Push(listPart2);
						}
						else
						{
							// Part 1 contains vertices [v2,...,iIndexClosest,v2]
							List<CPoint> listPart1 = new List<CPoint>((v2 - iIndexClosest + 1) + 1);
							listPart1.AddRange(listCurrent.GetRange(iIndexClosest, v2 - iIndexClosest + 1));
							listPart1.Add(listCurrent[iIndexClosest]);

							// Part 2 contains vertices [0,...,v2,iIndexClosest,...,n-1,0]
							List<CPoint> listPart2 = new List<CPoint>((n - (v2 - iIndexClosest - 1)) + 1);
							listPart2.AddRange(listCurrent.GetRange(0, iIndexClosest + 1));
							listPart2.AddRange(listCurrent.GetRange(v2, n - v2));
							listPart2.Add(listCurrent[0]);

							stackPolygons.Push(listPart1);
							stackPolygons.Push(listPart2);
						}

						listDiagonals.Add(new CDiagonal(listCurrent[v2], listCurrent[iIndexClosest]));
						break;
					}
					else
					{
						listDiagonals.Add(new CDiagonal(listCurrent[v1], listCurrent[v3]));
						listCurrent.RemoveAt(v2);
						n--;
					}
				}
			}

			return listDiagonals;
		}

		#region Stepwise

		/// <summary>
		/// State indication for a stepwise triangulation process.
		/// </summary>
		public enum TriangulateStepwiseState
		{
			/// <summary>
			/// Current step is to get the nex polygon from the stack.
			/// </summary>
			GET_NEXT_POLYGON_ON_STACK,
			/// <summary>
			/// Current step is to find a convex vertex in the current polygon.
			/// </summary>
			FIND_NEXT_CONVEX_VERTEX,
			/// <summary>
			/// Current step is to check all other vertices if they are contained within the triangle formed by the current convex vertex and its neighbours.
			/// </summary>
			CHECK_POINT_FOR_CONTAINMENT,
			/// <summary>
			/// Current step is to remove the current vertex from the polygon if in the step before no vertices have been found to be contained in the triangle mentioned above.
			/// </summary>
			REMOVE_CURRENT_EAR,
			/// <summary>
			/// Current step is to remove split up the current polygon along the current convex vertex and it's closest vertex contained in the triangle mentioned above.
			/// </summary>
			SPLIT_POLYGON,
			/// <summary>
			/// The current polygon has been completely triangulated.
			/// </summary>
			DONE
		}

		/// <summary>
		/// Holds the current state of a stepwise triangulation of the current polygon.
		/// </summary>
		/// <remarks>
		/// <para>This struct holds all values necessary for a linearized version of the <see cref="Triangulate"/> algorithm, named triangulateStepwise*.</para>
		/// <para>See the source code of <see cref="Triangulate"/> to understand the meaning of its members.</para>
		/// <para>The code for stepwise triangulation should be rewritten to implement a pattern similar to the code for stepwise joining of polygons, i.e. 
		/// a public struct which is returned by the individual methode of the algorithms and provides all necessary information about the current triangulation state, 
		/// and a private, internal struc containing all variables necessary for linearization of the triangulation code.</para></remarks>
		[Serializable]
		public struct TriangulateStepwiseDataset
		{
			/// <summary>
			/// Variable necessary for linearization of <see cref="Triangulate"/>.
			/// </summary>
			public bool bIsStarted;
			/// <summary>
			/// Variable necessary for linearization of <see cref="Triangulate"/>.
			/// </summary>
			public TriangulateStepwiseState tssNext;

			/// <summary>
			/// Variable necessary for linearization of <see cref="Triangulate"/>.
			/// </summary>
			public List<CPoint> listVerticesWork;
			/// <summary>
			/// Variable necessary for linearization of <see cref="Triangulate"/>.
			/// </summary>
			public List<CDiagonal> listDiagonals;
			/// <summary>
			/// Variable necessary for linearization of <see cref="Triangulate"/>.
			/// </summary>
			public Stack<List<CPoint>> stackPolygons;

			/// <summary>
			/// Variable necessary for linearization of <see cref="Triangulate"/>.
			/// </summary>
			public List<CPoint> listCurrent;
			/// <summary>
			/// Variable necessary for linearization of <see cref="Triangulate"/>.
			/// </summary>
			public int n;

			/// <summary>
			/// Variable necessary for linearization of <see cref="Triangulate"/>.
			/// </summary>
			public int i;

			/// <summary>
			/// Variable necessary for linearization of <see cref="Triangulate"/>.
			/// </summary>
			public int v1;
			/// <summary>
			/// Variable necessary for linearization of <see cref="Triangulate"/>.
			/// </summary>
			public int v2;
			/// <summary>
			/// Variable necessary for linearization of <see cref="Triangulate"/>.
			/// </summary>
			public int v3;

			/// <summary>
			/// Variable necessary for linearization of <see cref="Triangulate"/>.
			/// </summary>
			public bool bInside;
			/// <summary>
			/// Variable necessary for linearization of <see cref="Triangulate"/>.
			/// </summary>
			public float fSignedArea;
			/// <summary>
			/// Variable necessary for linearization of <see cref="Triangulate"/>.
			/// </summary>
			public int iIndexClosest;

			/// <summary>
			/// Variable necessary for linearization of <see cref="Triangulate"/>.
			/// </summary>
			public int j;

			/// <summary>
			/// Variable necessary for linearization of <see cref="Triangulate"/>.
			/// </summary>
			public int iReal;
			/// <summary>
			/// Variable necessary for linearization of <see cref="Triangulate"/>.
			/// </summary>
			public int jReal;
			/// <summary>
			/// Variable necessary for linearization of <see cref="Triangulate"/>.
			/// </summary>
			public bool bCurrentInside;
			/// <summary>
			/// Variable necessary for linearization of <see cref="Triangulate"/>.
			/// </summary>
			public bool bCurrentMax;
		}

		/// <summary>
		/// The current state of a stepwise triangulation.
		/// </summary>
		/// <remarks>Holds the state of the one and only stepwise triangulation which an instance of this class can perform at a time.</remarks>
		public TriangulateStepwiseDataset tsdThis;

		/// <summary>
		/// Start a stepwise triangulation.
		/// </summary>
		/// <returns>true if successful, false otherwise (especially if a stepwise triangulation is already in progress).</returns>
		/// <remarks>Call this function before performing the actual stepwise triangulation with <see cref="triangulateStepwiseStep"/> to do all 
		/// necessary initialization.</remarks>
		public bool triangulateStepwiseBegin()
		{
			if (tsdThis.bIsStarted)
				return false;

			// Temporary copy of the vertex list which we're going to use (as we're going to drop vertices in this algorithm)
			tsdThis.listVerticesWork = new List<CPoint>(listVertices);

			// Check whether vertices are given in CCW or CW in the list (if CCW, reverse)
			CPoint ptLeftMost = listVertices[iIndexLeftMost];
			CPoint ptBefore = listVertices[iIndexLeftMost == 0 ? listVertices.Count - 2 : iIndexLeftMost - 1];
			CPoint ptAfter = listVertices[(iIndexLeftMost + 1) % (listVertices.Count - 1)];

			if (new CLine(ptLeftMost, ptAfter).Inclination > new CLine(ptLeftMost, ptBefore).Inclination)
				tsdThis.listVerticesWork.Reverse();

			int iVertices = tsdThis.listVerticesWork.Count - 1;

			// Create list to hold diagonals (and allocate it with a defined length, as we already know the exact number of diagonals)
			tsdThis.listDiagonals = new List<CDiagonal>(iVertices - 3);


			// Create a stack (and allocate with a defined number, as we can't divide the polygon in more than n - 2 smaller polygons)
			tsdThis.stackPolygons = new Stack<List<CPoint>>((iVertices - 2));

			// Push the current polygon onto the stack
			tsdThis.stackPolygons.Push(tsdThis.listVerticesWork);


			tsdThis.bIsStarted = true;
			tsdThis.tssNext = TriangulateStepwiseState.GET_NEXT_POLYGON_ON_STACK;


			return true;
		}

		/// <summary>
		/// Perform the next step of a stepwise triangulation.
		/// </summary>
		/// <returns>true if further steps are necessary, false if this has been the last step.</returns>
		/// <remarks>Do not forget to call <see cref="triangulateStepwiseEnd"/> when this function returns false, i.e. the polygon is 
		/// completely triangulated, to do necessary clean up.</remarks>
		public bool triangulateStepwiseStep()
		{
			switch (tsdThis.tssNext)
			{
				case TriangulateStepwiseState.GET_NEXT_POLYGON_ON_STACK:
					if (tsdThis.stackPolygons.Count == 0)
					{
						tsdThis.tssNext = TriangulateStepwiseState.DONE;
						return false;
					}
					else
					{
						tsdThis.listCurrent = tsdThis.stackPolygons.Pop();
						tsdThis.n = tsdThis.listCurrent.Count - 1;

						tsdThis.i = 0;
						triangulateStepwiseHelper1();
					}
					break;

				case TriangulateStepwiseState.FIND_NEXT_CONVEX_VERTEX:
					if (!(new CTriangle(tsdThis.listCurrent[tsdThis.i], tsdThis.listCurrent[tsdThis.i + 1], tsdThis.listCurrent[tsdThis.i + 2]).signedArea > 0))
					{
						tsdThis.i++;
						tsdThis.iReal = tsdThis.i - 1;

						// TODO: Should / could be removed after enough testing (when sure the algorithm does it's job in all cases)
						if (!(tsdThis.i < (tsdThis.n - 1)))
							throw new Exception("Algorithm logic exception!");
					}
					else
					{
						tsdThis.iReal = tsdThis.i;

						tsdThis.v1 = tsdThis.i;
						tsdThis.v2 = tsdThis.i + 1;
						tsdThis.v3 = tsdThis.i + 2;

						tsdThis.bInside = false;
						tsdThis.fSignedArea = 0.0F;
						tsdThis.iIndexClosest = 0;

						tsdThis.j = 1;

						tsdThis.tssNext = TriangulateStepwiseState.CHECK_POINT_FOR_CONTAINMENT;
					}
					break;

				case TriangulateStepwiseState.CHECK_POINT_FOR_CONTAINMENT:
					// This part has been changed from the original algorithm to internally step forward in unimportant cases
					triangulateStepwiseHelper2();

					tsdThis.jReal = tsdThis.j;

					float fSA1 = 0.0F;
					float fSA2 = 0.0F;
					float fSA3 = 0.0F;

					// TODO: Think about how to avoid at least once or completely the using Math.Abs in the following lines
					// TODO: Calculation "(v3 + i + 1) % c" is used two times, calculate it once, save it in varibale and then use the variable instead of recalculating it each time
					// {

					if (new CTriangle(tsdThis.listCurrent[tsdThis.v1], tsdThis.listCurrent[tsdThis.v2], tsdThis.listCurrent[tsdThis.v3]).contains(tsdThis.listCurrent[tsdThis.j], ref fSA1, ref fSA2, ref fSA3))
					{
						tsdThis.bCurrentInside = true;

						if (!tsdThis.bInside)
						{
							tsdThis.bInside = true;
							tsdThis.fSignedArea = Math.Abs(fSA1);
							tsdThis.iIndexClosest = tsdThis.j;
							tsdThis.bCurrentMax = true;
						}
						else
						{
							if (Math.Abs(fSA1) > tsdThis.fSignedArea)
							{
								tsdThis.fSignedArea = Math.Abs(fSA1);
								tsdThis.iIndexClosest = tsdThis.j;
								tsdThis.bCurrentMax = true;
							}
							else
								tsdThis.bCurrentMax = false;
						}
					}
					else
						tsdThis.bCurrentInside = false;

					// }
					
					tsdThis.j++;

					triangulateStepwiseHelper2();
					break;

				case TriangulateStepwiseState.SPLIT_POLYGON:
					if (tsdThis.iIndexClosest > tsdThis.v2)
					{
						// Part 1 contains vertices [v2,...,iIndexClosest,v2]
						List<CPoint> listPart1 = new List<CPoint>((tsdThis.iIndexClosest - tsdThis.v2 + 1) + 1);
						listPart1.AddRange(tsdThis.listCurrent.GetRange(tsdThis.v2, tsdThis.iIndexClosest - tsdThis.v2 + 1));
						listPart1.Add(tsdThis.listCurrent[tsdThis.v2]);

						// Part 2 contains vertices [0,...,v2,iIndexClosest,...,n-1,0]
						List<CPoint> listPart2 = new List<CPoint>((tsdThis.n - (tsdThis.iIndexClosest - tsdThis.v2 - 1)) + 1);
						listPart2.AddRange(tsdThis.listCurrent.GetRange(0, tsdThis.v2 + 1));
						listPart2.AddRange(tsdThis.listCurrent.GetRange(tsdThis.iIndexClosest, tsdThis.n - tsdThis.iIndexClosest));
						listPart2.Add(tsdThis.listCurrent[0]);

						tsdThis.stackPolygons.Push(listPart1);
						tsdThis.stackPolygons.Push(listPart2);
					}
					else
					{
						// Part 1 contains vertices [v2,...,iIndexClosest,v2]
						List<CPoint> listPart1 = new List<CPoint>((tsdThis.v2 - tsdThis.iIndexClosest + 1) + 1);
						listPart1.AddRange(tsdThis.listCurrent.GetRange(tsdThis.iIndexClosest, tsdThis.v2 - tsdThis.iIndexClosest + 1));
						listPart1.Add(tsdThis.listCurrent[tsdThis.iIndexClosest]);

						// Part 2 contains vertices [0,...,v2,iIndexClosest,...,n-1,0]
						List<CPoint> listPart2 = new List<CPoint>((tsdThis.n - (tsdThis.v2 - tsdThis.iIndexClosest - 1)) + 1);
						listPart2.AddRange(tsdThis.listCurrent.GetRange(0, tsdThis.iIndexClosest + 1));
						listPart2.AddRange(tsdThis.listCurrent.GetRange(tsdThis.v2, tsdThis.n - tsdThis.v2));
						listPart2.Add(tsdThis.listCurrent[0]);

						tsdThis.stackPolygons.Push(listPart1);
						tsdThis.stackPolygons.Push(listPart2);
					}

					tsdThis.listDiagonals.Add(new CDiagonal(tsdThis.listCurrent[tsdThis.v2], tsdThis.listCurrent[tsdThis.iIndexClosest]));

					tsdThis.tssNext = TriangulateStepwiseState.GET_NEXT_POLYGON_ON_STACK;
					break;

				case TriangulateStepwiseState.REMOVE_CURRENT_EAR:
					tsdThis.listDiagonals.Add(new CDiagonal(tsdThis.listCurrent[tsdThis.v1], tsdThis.listCurrent[tsdThis.v3]));
					tsdThis.listCurrent.RemoveAt(tsdThis.v2);
					
					tsdThis.n--;

					triangulateStepwiseHelper1();
					break;
			}

			return true;
		}

		/// <summary>
		/// Ends a stepwise triangulation.
		/// </summary>
		/// <remarks>Performs all necessary clean up after a finished or canceled stepwise triangulation.</remarks>
		public void triangulateStepwiseEnd()
		{
			tsdThis.bIsStarted = false;

			tsdThis.listDiagonals = null;
			tsdThis.listVerticesWork = null;
			tsdThis.stackPolygons = null;
		}


		/// <summary>
		/// Helper function to avoid code redundancy in <see cref="triangulateStepwiseStep"/>.
		/// </summary>
		protected void triangulateStepwiseHelper1()
		{
			if (tsdThis.n > 3)
			{
				if (tsdThis.i > (tsdThis.n - 2))
					tsdThis.i = 0;

				tsdThis.tssNext = TriangulateStepwiseState.FIND_NEXT_CONVEX_VERTEX;
			}
			else
			{
				tsdThis.tssNext = TriangulateStepwiseState.GET_NEXT_POLYGON_ON_STACK;
			}
		}

		/// <summary>
		/// Helper function to avoid code redundancy in <see cref="triangulateStepwiseStep"/>.
		/// </summary>
		protected void triangulateStepwiseHelper2()
		{
			if (tsdThis.j == tsdThis.v1 || tsdThis.j == tsdThis.v2 || tsdThis.j == tsdThis.v3)
			{
				tsdThis.j++;
				triangulateStepwiseHelper2();
			}
			else
			{
				if (tsdThis.v1 == 0 || tsdThis.v3 == tsdThis.n)
				{
					if (tsdThis.j == tsdThis.n)
					{
						tsdThis.j++;
						triangulateStepwiseHelper2();
					}
				}
			}

			if (!(tsdThis.j < tsdThis.n + 1))
			{
				tsdThis.j--;
				if (tsdThis.bInside)
				{
					tsdThis.tssNext = TriangulateStepwiseState.SPLIT_POLYGON;
				}
				else
				{
					tsdThis.tssNext = TriangulateStepwiseState.REMOVE_CURRENT_EAR;
				}
			}
			else
			{
				if (tsdThis.listCurrent[tsdThis.j] == tsdThis.listCurrent[tsdThis.v1] || tsdThis.listCurrent[tsdThis.j] == tsdThis.listCurrent[tsdThis.v2] || tsdThis.listCurrent[tsdThis.j] == tsdThis.listCurrent[tsdThis.v3])
				{
					tsdThis.j++;
					triangulateStepwiseHelper2();
				}
			}
		}

		#endregion

		#endregion

		#region Join

		/// <summary>
		/// Unites the current polygon with a given child polygon (i.e. a polygon that lies inside)
		/// </summary>
		/// <param name="cpChild">Child polygon to unite with the current polygon.</param>
		/// <param name="segConnection">Variable to receive the segment connection the two polygons.</param>
		/// <returns>A new polygon consisting of the vertices of the parent and the child polygon and 
		/// is simple (i.e. does not self-intersect).</returns>
		public CPolygon joinWithChild(CPolygon cpChild, out CSegment segConnection)
		{
			segConnection = new CSegment() ;


			// The new polygon (additional TWO vertices for the endpoints of the common line, additional ONE vertex which is a copy of the first vertex)
			List<CPoint> listVerticesNew = new List<CPoint>(this.SizeLogical + cpChild.SizeLogical + 2 + 1);

			// Copies of the vertex lists with which we're going to work
			List<CPoint> listVerticesOuter = new List<CPoint>(listVertices);
			List<CPoint> listVerticesInner = new List<CPoint>(cpChild.listVertices);
			

			// Get the right most vertex of the inner polygon and find the left most vertex of the outer which is still right of the right most of the inner
			int iInnerRightMostIndex = cpChild.RightMostPointIndex;
			CPoint ptInnerRightMost = cpChild.RightMostPoint; 
			
			int iOuterLeftMostRightMostIndex = this.RightMostPointIndex;
			CPoint ptOuterLeftMostRightMost = this.RightMostPoint;
			
			for(int i = 0; i < listVerticesOuter.Count - 1; i++)
			{
				if (listVerticesOuter[i].X > ptInnerRightMost.X && listVerticesOuter[i].X < ptOuterLeftMostRightMost.X)
				{
					iOuterLeftMostRightMostIndex = i;
					ptOuterLeftMostRightMost = listVerticesOuter[i];
				}
			}


			// Get the rotation sense of the outer polygon
			CPoint ptLeftMost = listVertices[iIndexLeftMost];
			CPoint ptBefore = listVertices[iIndexLeftMost == 0 ? listVertices.Count - 2 : iIndexLeftMost - 1];
			CPoint ptAfter = listVertices[(iIndexLeftMost + 1) % (listVertices.Count - 1)];

			bool bSenseCCW = (new CLine(ptLeftMost, ptAfter).Inclination > new CLine(ptLeftMost, ptBefore).Inclination);

			// Get the rotation sense of the inner polygon
			ptLeftMost = cpChild.listVertices[cpChild.iIndexLeftMost];
			ptBefore = cpChild.listVertices[cpChild.iIndexLeftMost == 0 ? cpChild.listVertices.Count - 2 : cpChild.iIndexLeftMost - 1];
			ptAfter = cpChild.listVertices[(cpChild.iIndexLeftMost + 1) % (cpChild.listVertices.Count - 1)];
			
			// If they have the same sense, invert inner polygon
			if ((new CLine(ptLeftMost, ptAfter).Inclination > new CLine(ptLeftMost, ptBefore).Inclination) != bSenseCCW)
			{
				listVerticesInner.Reverse();
				iInnerRightMostIndex = listVerticesInner.Count - 1 - iInnerRightMostIndex;
			}



			// Now we have a "stripe" / region between these two points in which there are no other points of either polygon

			// For each other vertex of the outer polygon test for intersection with the connection between these two extreme points
			bool bFound = false;
			float fIntersectionX = (float)ptOuterLeftMostRightMost.X;
			int iFoundIndex = 0;
			int iFoundDir = 0;
			for (int i = 0; i < listVerticesOuter.Count - 1; i++)
			{
				if (i == iOuterLeftMostRightMostIndex || i + 1 == iOuterLeftMostRightMostIndex)
					continue;

				CPointF ptIntersect1, ptIntersect2;
				if (new CSegment(ptOuterLeftMostRightMost, ptInnerRightMost).intersectsWith(new CSegment(listVerticesOuter[i], listVerticesOuter[i + 1]), out ptIntersect1, out ptIntersect2))
				{
					if (Math.Min(ptIntersect1.X, ptIntersect2.X) < fIntersectionX)
					{
						fIntersectionX = Math.Min(ptIntersect1.X, ptIntersect2.X);
						bFound = true;

						if (listVerticesOuter[i].X >= ptOuterLeftMostRightMost.X)
						{
							iFoundIndex = i;
							iFoundDir = 1;
						}
						else
						{
							iFoundIndex = i + 1;
							iFoundDir = -1;
						}
					}
				}
			}

			// If we have an intersection, test for other points in the triangle
			if (bFound)
			{
				CTriangle triTemp = new CTriangle(ptInnerRightMost, listVerticesOuter[iFoundIndex], listVerticesOuter[iFoundIndex + iFoundDir]);

				int iNearestIndex = iFoundIndex;
				for (int i = 0; i < listVerticesOuter.Count - 1; i++)
				{
					if (i == iFoundIndex || i == iFoundIndex + iFoundDir)
						continue;

					if (triTemp.contains(listVerticesOuter[i]) && listVerticesOuter[i] != triTemp.ptA && listVerticesOuter[i] != triTemp.ptB && listVerticesOuter[i] != triTemp.ptC)
						if (listVerticesOuter[i].X < listVerticesOuter[iNearestIndex].X && listVerticesOuter[i].X > listVerticesInner[iInnerRightMostIndex].X)
							iNearestIndex = i;
				}

				iFoundIndex = iNearestIndex;
			}
			else
				iFoundIndex = iOuterLeftMostRightMostIndex;

			// Now join the two polygons
			int iJoinRightIndex = iFoundIndex;
			int iJoinLeftIndex = iInnerRightMostIndex;

			listVerticesNew.AddRange(listVerticesOuter.GetRange(0, iJoinRightIndex + 1));
			listVerticesInner.Reverse();
			iJoinLeftIndex = (listVerticesInner.Count - 1) - iJoinLeftIndex;
			listVerticesNew.AddRange(listVerticesInner.GetRange(iJoinLeftIndex, listVerticesInner.Count - 1 - iJoinLeftIndex));
			listVerticesNew.AddRange(listVerticesInner.GetRange(0, iJoinLeftIndex + 1));
			listVerticesNew.AddRange(listVerticesOuter.GetRange(iJoinRightIndex, listVerticesOuter.Count - iJoinRightIndex));

			segConnection = new CSegment(listVerticesInner[iJoinLeftIndex], listVerticesOuter[iJoinRightIndex]);
			return new CPolygon(listVerticesNew);;
		}


		#region Stepwise

		// State indication

		/// <summary>
		/// State indication value to indicate the current state during a stepwise join of the polygon with a given subpolygon.
		/// </summary>
		public enum JoinStepwiseState
		{
			/// <summary>
			/// Current step is searching for the inner polygon's right most point and the outer polygon's left most point which is still right of the inner polygon.
			/// </summary>
			SELECT_EXTREME_POINTS,
			/// <summary>
			/// Current step is findig an intersection of the outer polygon's segments with the segment formed by the two extreme points of inner and outer polygon.
			/// </summary>
			FIND_INTERSECTION,
			/// <summary>
			/// Current step is retrieving the triangle formed by the intersection segments starting and end point and the inner polygon's extreme point.
			/// </summary>
			GET_TRIANGLE,
			/// <summary>
			/// Current step is finding the vertex of the outer polygon which is closest to the inner polygon's extreme point and also inside the above mentioned triagnle.
			/// </summary>
			FIND_MAX_INSIDE,
			/// <summary>
			/// Current step is connecting the two polygons with the points found in the steps before.
			/// </summary>
			JOIN,
			/// <summary>
			/// Joining the two polygons has finished.
			/// </summary>
			DONE
		}


		// State data

		/// <summary>
		/// Holds the current state of a stepwise joining of polygons.
		/// </summary>
		/// <remarks>
		/// <para>This struct holds all values necessary for a linearized version of the <see cref="joinWithChild"/> algorithm, named joinStepwise*.</para>
		/// <para>See the source code of <see cref="joinWithChild"/> to understand the meaning of its members.</para></remarks>
		[Serializable]
		protected struct JoinStepwiseData
		{
			/// <summary>
			/// Variable necessary for linearization of <see cref="joinWithChild"/>.
			/// </summary>
			public bool bIsStarted;
			/// <summary>
			/// Variable necessary for linearization of <see cref="joinWithChild"/>.
			/// </summary>
			public JoinStepwiseState jssNext;

			/// <summary>
			/// Variable necessary for linearization of <see cref="joinWithChild"/>.
			/// </summary>
			public CPolygon cpChild;

			/// <summary>
			/// Variable necessary for linearization of <see cref="joinWithChild"/>.
			/// </summary>
			public List<CPoint> listVerticesNew;

			/// <summary>
			/// Variable necessary for linearization of <see cref="joinWithChild"/>.
			/// </summary>
			public List<CPoint> listVerticesOuter;
			/// <summary>
			/// Variable necessary for linearization of <see cref="joinWithChild"/>.
			/// </summary>
			public List<CPoint> listVerticesInner;

			/// <summary>
			/// Variable necessary for linearization of <see cref="joinWithChild"/>.
			/// </summary>
			public int iInnerRightMostIndex;
			/// <summary>
			/// Variable necessary for linearization of <see cref="joinWithChild"/>.
			/// </summary>
			public CPoint ptInnerRightMost;

			/// <summary>
			/// Variable necessary for linearization of <see cref="joinWithChild"/>.
			/// </summary>
			public int iOuterLeftMostRightMostIndex;
			/// <summary>
			/// Variable necessary for linearization of <see cref="joinWithChild"/>.
			/// </summary>
			public CPoint ptOuterLeftMostRightMost;

			/// <summary>
			/// Variable necessary for linearization of <see cref="joinWithChild"/>.
			/// </summary>
			public bool bFound;
			/// <summary>
			/// Variable necessary for linearization of <see cref="joinWithChild"/>.
			/// </summary>
			public float fIntersectionX;
			/// <summary>
			/// Variable necessary for linearization of <see cref="joinWithChild"/>.
			/// </summary>
			public int iFoundIndex;
			/// <summary>
			/// Variable necessary for linearization of <see cref="joinWithChild"/>.
			/// </summary>
			public int iFoundDir;

			/// <summary>
			/// Variable necessary for linearization of <see cref="joinWithChild"/>.
			/// </summary>
			public int i;

			/// <summary>
			/// Variable necessary for linearization of <see cref="joinWithChild"/>.
			/// </summary>
			public CTriangle triTemp;
			/// <summary>
			/// Variable necessary for linearization of <see cref="joinWithChild"/>.
			/// </summary>
			public int iNearestIndex;

			/// <summary>
			/// Variable necessary for linearization of <see cref="joinWithChild"/>.
			/// </summary>
			public int j;

			/// <summary>
			/// Variable necessary for linearization of <see cref="joinWithChild"/>.
			/// </summary>
			public int iJoinRightIndex;
			/// <summary>
			/// Variable necessary for linearization of <see cref="joinWithChild"/>.
			/// </summary>
			public int iJoinLeftIndex;

			/// <summary>
			/// Variable necessary for linearization of <see cref="joinWithChild"/>.
			/// </summary>
			public CSegment segConnection;
		}

		/// <summary>
		/// Holds the state of a stepwise join of this polygon with another.
		/// </summary>
		/// <remarks>This variable holds the values for the one and only join that can be active at a certain point of time. Other 
		/// joins cannot be performed before the current one has enden. Take this into account when using multi thrading.</remarks>
		JoinStepwiseData jsdThis;


		// Snapshot and according methods

		/// <summary>
		/// Public snapshot of important state information of a stepwise join currently beeing performed.
		/// </summary>
		/// <remarks>This method is intended for external callers wanting to display the current join state, e.g. on a GUI.</remarks>
		[Serializable]
		public struct JoinStepwiseSnapshot
		{
			/// <summary>
			/// Contructor which can be used to create a DEEP copy of a given <see cref="JoinStepwiseSnapshot"/> struct.
			/// </summary>
			/// <param name="jssnInit">Struct to copy values from.</param>
			public JoinStepwiseSnapshot(ref JoinStepwiseSnapshot jssnInit)
			{
				jssNext = jssnInit.jssNext;

				listPointsExtreme = new List<CPoint>(jssnInit.listPointsExtreme);
				listPointsMax = new List<CPoint>(jssnInit.listPointsMax);
				listPointsInvestigated = new List<CPoint>(jssnInit.listPointsInvestigated);
				listSegmentsExtreme = new List<CSegment>(jssnInit.listSegmentsExtreme);
				listSegmentsMax = new List<CSegment>(jssnInit.listSegmentsMax);
				listSegmentsInvestigated = new List<CSegment>(jssnInit.listSegmentsInvestigated);

				listDiagonals = new List<CSegment>(jssnInit.listDiagonals);
			}

			/// <summary>
			/// Step that will be performed on the next call to <see cref="joinWithChildStepwiseStep"/>.
			/// </summary>
			public JoinStepwiseState jssNext;

			/// <summary>
			/// Holds the current extreme points.
			/// </summary>
			public List<CPoint> listPointsExtreme;
			/// <summary>
			/// Holds the points closest to the inner polygon's extreme point.
			/// </summary>
			public List<CPoint> listPointsMax;
			/// <summary>
			/// Holds the points which are currently tested by the algorithm.
			/// </summary>
			public List<CPoint> listPointsInvestigated;
			/// <summary>
			/// Holds the segments connecting the current extreme points.
			/// </summary>
			public List<CSegment> listSegmentsExtreme;
			/// <summary>
			/// Holds the segment with the intersection closest to the inner polygon's extreme point.
			/// </summary>
			public List<CSegment> listSegmentsMax;
			/// <summary>
			/// Holds the segments currently tested by the algorithm.
			/// </summary>
			public List<CSegment> listSegmentsInvestigated;

			/// <summary>
			/// Holds the diagonal(s) for triangulation resulting from the algorithm when connecting the outer polygon with the inner.
			/// </summary>
			public List<CSegment> listDiagonals;
		}

		/// <summary>
		/// A public snapshot of the one an only stepwise joing that can be performed at a time.
		/// </summary>
		protected JoinStepwiseSnapshot jssnThis;


		/// <summary>
		/// Used to initialize a <see cref="JoinStepwiseSnapshot"/> struct.
		/// </summary>
		/// <param name="jssnInit">Reference to the struct to be initialized.</param>
		/// <remarks>The most important function of this method is to initialize the given structs' <see cref="List&lt;T&gt;"/>s with a 
		/// reasonable initial capacity that avoids resizing them while the algorithm runs.</remarks>
		protected void joinWithChildStepwiseSnapshotInit(ref JoinStepwiseSnapshot jssnInit)
		{
			// Will hold the two extreme points AND finally also the maximum point inside if there is one
			if (jssnInit.listPointsExtreme == null)
				jssnInit.listPointsExtreme = new List<CPoint>(3);
			else
				jssnInit.listPointsExtreme.Clear();

			// Will hold the current maxmimum point inside while searching for others
			if (jssnInit.listPointsMax == null)
				jssnInit.listPointsMax = new List<CPoint>(1);
			else
				jssnInit.listPointsMax.Clear();
			
			// Will hold the currently tested point
			if (jssnInit.listPointsInvestigated == null)
				jssnInit.listPointsInvestigated = new List<CPoint>(1);
			else
				jssnInit.listPointsInvestigated.Clear();

			// Will hold the segment given by the extreme points AND finally the triangle (three segments) by the cutting segments starting and end point and one of the extreme points
			if (jssnInit.listSegmentsExtreme == null)
				jssnInit.listSegmentsExtreme = new List<CSegment>(3);
			else
				jssnInit.listSegmentsExtreme.Clear();

			// Will hold the current cutting segments end point while searching for others
			if (jssnInit.listSegmentsMax == null)
				jssnInit.listSegmentsMax = new List<CSegment>(1);
			else
				jssnInit.listSegmentsMax.Clear();

			// Will hold the currenly testes segment
			if (jssnInit.listSegmentsInvestigated == null)
				jssnInit.listSegmentsInvestigated = new List<CSegment>(1);
			else
				jssnInit.listSegmentsInvestigated.Clear();

			// Will hold the diagonal used to connect the polygons
			if (jssnInit.listDiagonals == null)
				jssnInit.listDiagonals = new List<CSegment>(1);
			else
				jssnInit.listDiagonals.Clear();
		}

		/// <summary>
		/// Completely clears a <see cref="JoinStepwiseSnapshot"/> struct.
		/// </summary>
		/// <param name="jssnClear">Reference to the struct to clear.</param>
		protected void joinWithChildStepwiseSnapshotClear(ref JoinStepwiseSnapshot jssnClear)
		{
			jssnClear.listPointsExtreme.Clear();
			jssnClear.listPointsMax.Clear();
			jssnClear.listPointsInvestigated.Clear();
			jssnClear.listSegmentsExtreme.Clear();
			jssnClear.listSegmentsMax.Clear();
			jssnClear.listSegmentsInvestigated.Clear();
			jssnClear.listDiagonals.Clear();
		}


		// Actual algorithm

		/// <summary>
		/// Begins a new stepwise join.
		/// </summary>
		/// <param name="cpChild">Child polygon to connect with the current polygon.</param>
		/// <param name="jssnResult">Variable to receive the state of the current stepwise join.</param>
		/// <returns>true if operation successful, false otherwise (especially if a stepwise join is already in progress).</returns>
		/// <remarks>Use to prepare the current polgyon for joing with a sub-polygon before performing the stepwise join with <see cref="joinWithChildStepwiseStep"/>.</remarks>
		public bool joinWithChildStepwiseBegin(CPolygon cpChild, out JoinStepwiseSnapshot jssnResult)
		{
			if (jsdThis.bIsStarted)
			{
				jssnResult = new JoinStepwiseSnapshot();
				return false;
			}

			joinWithChildStepwiseSnapshotInit(ref jssnThis);


			// The new polygon (additional TWO vertices for the endpoints of the common line, additional ONE vertex which is a copy of the first vertex)
			jsdThis.listVerticesNew = new List<CPoint>(this.SizeLogical + cpChild.SizeLogical + 2 + 1);

			// Copies of the vertex lists with which we're going to work
			jsdThis.listVerticesOuter = new List<CPoint>(listVertices);
			jsdThis.listVerticesInner = new List<CPoint>(cpChild.listVertices);

			jsdThis.cpChild = cpChild;


			jsdThis.bIsStarted = true;
			jsdThis.jssNext = JoinStepwiseState.SELECT_EXTREME_POINTS;

			jssnResult = new JoinStepwiseSnapshot(ref jssnThis);
			return true;
		}

		/// <summary>
		/// Perform the next step in a stepwise join.
		/// </summary>
		/// <param name="jssnResult">Variable to receive the state of the current stepwise join.</param>
		/// <returns>true if further steps are necessary to complete the join, false if this has been the last step.</returns>
		/// <remarks>If this method returns false (i.e. the stepwise join is complete), do not forget to call <see cref="joinWithChildStepwiseEnd"/> to 
		/// finish the stepwise join.</remarks>
		public bool joinWithChildStepwiseStep(out JoinStepwiseSnapshot jssnResult)
		{
			switch (jsdThis.jssNext)
			{
				case JoinStepwiseState.SELECT_EXTREME_POINTS:
					// Get the right most vertex of the inner polygon and find the left most vertex of the outer which is still right of the right most of the inner
					jsdThis.iInnerRightMostIndex = jsdThis.cpChild.RightMostPointIndex;
					jsdThis.ptInnerRightMost = jsdThis.cpChild.RightMostPoint;

					jsdThis.iOuterLeftMostRightMostIndex = this.RightMostPointIndex;
					jsdThis.ptOuterLeftMostRightMost = this.RightMostPoint;

					for (int i = 0; i < jsdThis.listVerticesOuter.Count - 1; i++)
					{
						if (jsdThis.listVerticesOuter[i].X > jsdThis.ptInnerRightMost.X && jsdThis.listVerticesOuter[i].X < jsdThis.ptOuterLeftMostRightMost.X)
						{
							jsdThis.iOuterLeftMostRightMostIndex = i;
							jsdThis.ptOuterLeftMostRightMost = jsdThis.listVerticesOuter[i];
						}
					}

					// Get the rotation sense of the outer polygon
					CPoint ptLeftMost = listVertices[iIndexLeftMost];
					CPoint ptBefore = listVertices[iIndexLeftMost == 0 ? listVertices.Count - 2 : iIndexLeftMost - 1];
					CPoint ptAfter = listVertices[(iIndexLeftMost + 1) % (listVertices.Count - 1)];

					bool bSenseCCW = (new CLine(ptLeftMost, ptAfter).Inclination > new CLine(ptLeftMost, ptBefore).Inclination);

					// Get the rotation sense of the inner polygon
					ptLeftMost = jsdThis.cpChild.listVertices[jsdThis.cpChild.iIndexLeftMost];
					ptBefore = jsdThis.cpChild.listVertices[jsdThis.cpChild.iIndexLeftMost == 0 ? jsdThis.cpChild.listVertices.Count - 2 : jsdThis.cpChild.iIndexLeftMost - 1];
					ptAfter = jsdThis.cpChild.listVertices[(jsdThis.cpChild.iIndexLeftMost + 1) % (jsdThis.cpChild.listVertices.Count - 1)];

					// If they have the same sense, invert inner polygon
					if ((new CLine(ptLeftMost, ptAfter).Inclination > new CLine(ptLeftMost, ptBefore).Inclination) != bSenseCCW)
					{
						jsdThis.listVerticesInner.Reverse();
						jsdThis.iInnerRightMostIndex = jsdThis.listVerticesInner.Count - 1 - jsdThis.iInnerRightMostIndex;
					}

					// Now we have a "stripe" / region between these two points in which there are no other points of either polygon

					jsdThis.bFound = false;
					jsdThis.fIntersectionX = jsdThis.ptOuterLeftMostRightMost.X;
					jsdThis.iFoundIndex = 0;
					jsdThis.iFoundDir = 0;

					jsdThis.i = 0;

					jssnThis.listPointsExtreme.Clear();
					jssnThis.listPointsExtreme.Add(jsdThis.ptInnerRightMost);
					jssnThis.listPointsExtreme.Add(jsdThis.ptOuterLeftMostRightMost);

					jsdThis.jssNext = JoinStepwiseState.FIND_INTERSECTION;
					
					jssnThis.jssNext = jsdThis.jssNext;

					jssnThis.listPointsExtreme.Clear();
					jssnThis.listPointsExtreme.Add(jsdThis.ptInnerRightMost);
					jssnThis.listPointsExtreme.Add(jsdThis.ptOuterLeftMostRightMost);

					jssnThis.listSegmentsExtreme.Clear();
					jssnThis.listSegmentsExtreme.Add(new CSegment(jsdThis.ptInnerRightMost, jsdThis.ptOuterLeftMostRightMost));
					break;

				case JoinStepwiseState.FIND_INTERSECTION:
					// For each other vertex of the outer polygon test for intersection with the connection between these two extreme points
					
					joinWithChildStepwiseHelper1();

					jssnThis.listSegmentsInvestigated.Clear();
					jssnThis.listSegmentsInvestigated.Add(new CSegment(jsdThis.listVerticesOuter[jsdThis.i], jsdThis.listVerticesOuter[jsdThis.i + 1]));

					CPointF ptIntersect1, ptIntersect2;
					if (new CSegment(jsdThis.ptOuterLeftMostRightMost, jsdThis.ptInnerRightMost).intersectsWith(new CSegment(jsdThis.listVerticesOuter[jsdThis.i], jsdThis.listVerticesOuter[jsdThis.i + 1]), out ptIntersect1, out ptIntersect2))
					{
						if (Math.Min(ptIntersect1.X, ptIntersect2.X) < jsdThis.fIntersectionX)
						{
							jsdThis.fIntersectionX = Math.Min(ptIntersect1.X, ptIntersect2.X);
							jsdThis.bFound = true;

							jssnThis.listSegmentsMax.Clear();
							jssnThis.listSegmentsMax.Add(new CSegment(jsdThis.listVerticesOuter[jsdThis.i], jsdThis.listVerticesOuter[jsdThis.i + 1]));

							if (jsdThis.listVerticesOuter[jsdThis.i].X >= jsdThis.ptOuterLeftMostRightMost.X)
							{
								jsdThis.iFoundIndex = jsdThis.i;
								jsdThis.iFoundDir = 1;
							}
							else
							{
								jsdThis.iFoundIndex = jsdThis.i + 1;
								jsdThis.iFoundDir = -1;
							}
						}
					}

					jsdThis.i++;

					joinWithChildStepwiseHelper1();
					break;

				case JoinStepwiseState.GET_TRIANGLE:
					jsdThis.triTemp = new CTriangle(jsdThis.ptInnerRightMost, jsdThis.listVerticesOuter[jsdThis.iFoundIndex], jsdThis.listVerticesOuter[jsdThis.iFoundIndex + jsdThis.iFoundDir]);
					jsdThis.iNearestIndex = jsdThis.iFoundIndex;

					jsdThis.j = 0;


					jsdThis.jssNext = JoinStepwiseState.FIND_MAX_INSIDE;
					
					jssnThis.jssNext = jsdThis.jssNext;

					jssnThis.listSegmentsMax.Clear();
					jssnThis.listSegmentsInvestigated.Clear();

					jssnThis.listPointsExtreme.Clear();
					jssnThis.listSegmentsExtreme.Clear();

					jssnThis.listPointsExtreme.Add(jsdThis.ptInnerRightMost);
					jssnThis.listPointsExtreme.Add(jsdThis.listVerticesOuter[jsdThis.iFoundIndex]);
					jssnThis.listPointsExtreme.Add(jsdThis.listVerticesOuter[jsdThis.iFoundIndex + jsdThis.iFoundDir]);

					jssnThis.listSegmentsExtreme.Add(new CSegment(jsdThis.ptInnerRightMost, jsdThis.listVerticesOuter[jsdThis.iFoundIndex]));
					jssnThis.listSegmentsExtreme.Add(new CSegment(jsdThis.listVerticesOuter[jsdThis.iFoundIndex], jsdThis.listVerticesOuter[jsdThis.iFoundIndex + jsdThis.iFoundDir]));
					jssnThis.listSegmentsExtreme.Add(new CSegment(jsdThis.listVerticesOuter[jsdThis.iFoundIndex + jsdThis.iFoundDir], jsdThis.ptInnerRightMost));
					break;

				case JoinStepwiseState.FIND_MAX_INSIDE:
					joinWithChildStepwiseHelper2();

					jssnThis.listPointsInvestigated.Clear();
					jssnThis.listPointsInvestigated.Add(jsdThis.listVerticesOuter[jsdThis.j]);

					if (jsdThis.triTemp.contains(jsdThis.listVerticesOuter[jsdThis.j]))
						if (jsdThis.listVerticesOuter[jsdThis.j].X < jsdThis.listVerticesOuter[jsdThis.iNearestIndex].X && jsdThis.listVerticesOuter[jsdThis.j].X > jsdThis.listVerticesInner[jsdThis.iInnerRightMostIndex].X)
						{
							jsdThis.iNearestIndex = jsdThis.j;

							jssnThis.listPointsMax.Clear();
							jssnThis.listPointsMax.Add(jsdThis.listVerticesOuter[jsdThis.j]);
						}

					jsdThis.j++;

					joinWithChildStepwiseHelper2();
					break;

				case JoinStepwiseState.JOIN:
					// Now join the two polygons
					jsdThis.iJoinRightIndex = jsdThis.iFoundIndex;
					jsdThis.iJoinLeftIndex = jsdThis.iInnerRightMostIndex;

					jsdThis.listVerticesNew.AddRange(jsdThis.listVerticesOuter.GetRange(0, jsdThis.iJoinRightIndex + 1));
					jsdThis.listVerticesInner.Reverse();
					jsdThis.iJoinLeftIndex = (jsdThis.listVerticesInner.Count - 1) - jsdThis.iJoinLeftIndex;
					jsdThis.listVerticesNew.AddRange(jsdThis.listVerticesInner.GetRange(jsdThis.iJoinLeftIndex, jsdThis.listVerticesInner.Count - 1 - jsdThis.iJoinLeftIndex));
					jsdThis.listVerticesNew.AddRange(jsdThis.listVerticesInner.GetRange(0, jsdThis.iJoinLeftIndex + 1));
					jsdThis.listVerticesNew.AddRange(jsdThis.listVerticesOuter.GetRange(jsdThis.iJoinRightIndex, jsdThis.listVerticesOuter.Count - jsdThis.iJoinRightIndex));

					jsdThis.segConnection = new CSegment(jsdThis.listVerticesInner[jsdThis.iJoinLeftIndex], jsdThis.listVerticesOuter[jsdThis.iJoinRightIndex]);

					jsdThis.jssNext = JoinStepwiseState.DONE;
					
					jssnThis.jssNext = jsdThis.jssNext;
					
					jssnThis.listPointsInvestigated.Clear();
					jssnThis.listPointsMax.Clear();

					jssnThis.listPointsExtreme.Clear();
					jssnThis.listSegmentsExtreme.Clear();

					jssnThis.listDiagonals.Clear();
					jssnThis.listDiagonals.Add(jsdThis.segConnection);

					jssnResult = new JoinStepwiseSnapshot(ref jssnThis);
					return false;
			}

			jssnResult = new JoinStepwiseSnapshot(ref jssnThis);
			return true;
		}

		/// <summary>
		/// Ends a stepwise join.
		/// </summary>
		/// <param name="pResult">Variable to receive the combined / connected polygon.</param>
		/// <param name="segConnection">Variable to receive the segment used to connect the original polygons.</param>
		/// <returns>true if successful, false otherwise (especially if a stepwise join has not been initialized with <see cref="joinWithChildStepwiseBegin"/> before.</returns>
		/// <remarks>Do not forget to call this function if you're either done with a stepwise join or want to cancel one, as this method performs all 
		/// necessary clean up.</remarks>
		public bool joinWithChildStepwiseEnd(out CPolygon pResult, out CSegment segConnection)
		{
			if (!jsdThis.bIsStarted)
			{
				pResult = null;
				segConnection = new CSegment();
				return false;
			}

			joinWithChildStepwiseSnapshotClear(ref jssnThis);

			segConnection = jsdThis.segConnection;
			pResult = new CPolygon(jsdThis.listVerticesNew);

			jsdThis.bIsStarted = false;
			return true;
		}


		// Helper functions

		/// <summary>
		/// Helper function used in <see cref="joinWithChildStepwiseStep"/> to avoid code redundancy.
		/// </summary>
		/// <remarks>See the source code of <see cref="joinWithChildStepwiseStep"/> for details.</remarks>
		protected void joinWithChildStepwiseHelper1()
		{
			if (!(jsdThis.i < jsdThis.listVerticesOuter.Count - 1))
			{
				jsdThis.i--;

				// If we have an intersection, test for other points in the triangle, else join
				if (jsdThis.bFound)
				{
					jsdThis.jssNext = JoinStepwiseState.GET_TRIANGLE;
					jssnThis.jssNext = jsdThis.jssNext;
				}
				else
				{
					jsdThis.iFoundIndex = jsdThis.iOuterLeftMostRightMostIndex;

					jssnThis.listSegmentsInvestigated.Clear();

					jsdThis.jssNext = JoinStepwiseState.JOIN;
					jssnThis.jssNext = jsdThis.jssNext;
				}

				jssnThis.jssNext = jsdThis.jssNext;
			}
			else
			{
				if (jsdThis.i == jsdThis.iOuterLeftMostRightMostIndex || jsdThis.i + 1 == jsdThis.iOuterLeftMostRightMostIndex)
				{
					jsdThis.i++;
					joinWithChildStepwiseHelper1();
				}
			}
		}

		/// <summary>
		/// Helper function used in <see cref="joinWithChildStepwiseStep"/> to avoid code redundancy.
		/// </summary>
		/// <remarks>See the source code of <see cref="joinWithChildStepwiseStep"/> for details.</remarks>
		protected void joinWithChildStepwiseHelper2()
		{
			if (!(jsdThis.j < jsdThis.listVerticesOuter.Count - 1))
			{
				jsdThis.j--;

				jsdThis.iFoundIndex = jsdThis.iNearestIndex;

				jsdThis.jssNext = JoinStepwiseState.JOIN;
				jssnThis.jssNext = jsdThis.jssNext;
			}
			else
			{
				if (jsdThis.j == jsdThis.iFoundIndex || jsdThis.j == jsdThis.iFoundIndex + jsdThis.iFoundDir)
				{
					jsdThis.j++;
					joinWithChildStepwiseHelper2();
				}
				else
				{
					if (jsdThis.listVerticesOuter[jsdThis.j] == jsdThis.triTemp.ptA || jsdThis.listVerticesOuter[jsdThis.j] == jsdThis.triTemp.ptB || jsdThis.listVerticesOuter[jsdThis.j] == jsdThis.triTemp.ptC)
					{
						jsdThis.j++;
						joinWithChildStepwiseHelper2();
					}
				}
			}
		}

		#endregion

		#endregion
	}
}
