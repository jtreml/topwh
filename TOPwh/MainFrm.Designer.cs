namespace TOPwh
{
	/// <summary>
	/// Main application form.
	/// </summary>
	/// <remarks>Represents the main window of the application providing the user with means of interaction.
	/// In this case, this is especially (besides standard toolbars and form elements) the draw area that allows the user
	/// to design polygons and see algorithms in work as well as their results.
	/// 
	/// Therefore a lot of graphics and rendering functions can be found in this class.
	/// </remarks>
	partial class MainFrm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainFrm));
			this.toolStripDrawing = new System.Windows.Forms.ToolStrip();
			this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
			this.splitContainerLogWindow = new System.Windows.Forms.SplitContainer();
			this.splitContainerPolygonList = new System.Windows.Forms.SplitContainer();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.panel3 = new System.Windows.Forms.Panel();
			this.label3 = new System.Windows.Forms.Label();
			this.treeViewPolygons = new System.Windows.Forms.TreeView();
			this.contextMenuStripPolygonList = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.removeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.removePolygonAndAllchildsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.imageListPolygonTree = new System.Windows.Forms.ImageList(this.components);
			this.splitContainerToolbox = new System.Windows.Forms.SplitContainer();
			this.contextMenuStripDraw = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.closePolygonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.cancelPolygonToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.cancelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.tableLayoutPanelToolbox = new System.Windows.Forms.TableLayoutPanel();
			this.panel2 = new System.Windows.Forms.Panel();
			this.label2 = new System.Windows.Forms.Label();
			this.panel1 = new System.Windows.Forms.Panel();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
			this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
			this.panel4 = new System.Windows.Forms.Panel();
			this.label1 = new System.Windows.Forms.Label();
			this.listViewLog = new System.Windows.Forms.ListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.contextMenuStripLogWindow = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.clearLogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.saveLogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.imageListLog = new System.Windows.Forms.ImageList(this.components);
			this.contextMenuStripToolbars = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.standardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.viewToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
			this.drawToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.controlToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolbarsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.menuStripMain = new System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
			this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
			this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripSeparator();
			this.showStatusBarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripFile = new System.Windows.Forms.ToolStrip();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripView = new System.Windows.Forms.ToolStrip();
			this.toolStripControl = new System.Windows.Forms.ToolStrip();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.saveFileDialogMain = new System.Windows.Forms.SaveFileDialog();
			this.openFileDialogMain = new System.Windows.Forms.OpenFileDialog();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.toolStripStatusLabelPos = new System.Windows.Forms.ToolStripStatusLabel();
			this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
			this.timerLowSpeedMode = new System.Windows.Forms.Timer(this.components);
			this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
			this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.showPolygonListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.showToolboxToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.showLogWindowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.helpToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripButtonNewDoc = new System.Windows.Forms.ToolStripButton();
			this.toolStripButtonOpen = new System.Windows.Forms.ToolStripButton();
			this.toolStripButtonSave = new System.Windows.Forms.ToolStripButton();
			this.toolStripButtonExit = new System.Windows.Forms.ToolStripButton();
			this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
			this.toolStripButtonShowLog = new System.Windows.Forms.ToolStripButton();
			this.toolStripButtonShowToolbox = new System.Windows.Forms.ToolStripButton();
			this.toolStripButtonDrawMode = new System.Windows.Forms.ToolStripButton();
			this.toolStripButtonClear = new System.Windows.Forms.ToolStripButton();
			this.toolStripButtonAlgorithmModeFull = new System.Windows.Forms.ToolStripButton();
			this.toolStripButtonAlgorithmModeSlow = new System.Windows.Forms.ToolStripButton();
			this.toolStripButtonAlgorithmModeStep = new System.Windows.Forms.ToolStripButton();
			this.toolStripButtonAlgorithmPlay = new System.Windows.Forms.ToolStripButton();
			this.toolStripButtonAlgorithmPause = new System.Windows.Forms.ToolStripButton();
			this.toolStripButtonAlgorithmStop = new System.Windows.Forms.ToolStripButton();
			this.toolStripDrawing.SuspendLayout();
			this.toolStripContainer1.ContentPanel.SuspendLayout();
			this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
			this.toolStripContainer1.SuspendLayout();
			this.splitContainerLogWindow.Panel1.SuspendLayout();
			this.splitContainerLogWindow.Panel2.SuspendLayout();
			this.splitContainerLogWindow.SuspendLayout();
			this.splitContainerPolygonList.Panel1.SuspendLayout();
			this.splitContainerPolygonList.Panel2.SuspendLayout();
			this.splitContainerPolygonList.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.panel3.SuspendLayout();
			this.contextMenuStripPolygonList.SuspendLayout();
			this.splitContainerToolbox.Panel2.SuspendLayout();
			this.splitContainerToolbox.SuspendLayout();
			this.contextMenuStripDraw.SuspendLayout();
			this.tableLayoutPanelToolbox.SuspendLayout();
			this.panel2.SuspendLayout();
			this.panel1.SuspendLayout();
			this.toolStrip1.SuspendLayout();
			this.tableLayoutPanel3.SuspendLayout();
			this.panel4.SuspendLayout();
			this.contextMenuStripLogWindow.SuspendLayout();
			this.contextMenuStripToolbars.SuspendLayout();
			this.menuStripMain.SuspendLayout();
			this.toolStripFile.SuspendLayout();
			this.toolStripView.SuspendLayout();
			this.toolStripControl.SuspendLayout();
			this.statusStrip1.SuspendLayout();
			this.tableLayoutPanel2.SuspendLayout();
			this.SuspendLayout();
			// 
			// toolStripDrawing
			// 
			this.toolStripDrawing.Dock = System.Windows.Forms.DockStyle.None;
			this.toolStripDrawing.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonDrawMode,
            this.toolStripButtonClear});
			this.toolStripDrawing.Location = new System.Drawing.Point(3, 49);
			this.toolStripDrawing.Name = "toolStripDrawing";
			this.toolStripDrawing.Size = new System.Drawing.Size(56, 25);
			this.toolStripDrawing.TabIndex = 0;
			this.toolStripDrawing.Text = "toolStrip1";
			// 
			// toolStripContainer1
			// 
			// 
			// toolStripContainer1.ContentPanel
			// 
			this.toolStripContainer1.ContentPanel.Controls.Add(this.splitContainerLogWindow);
			this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(961, 532);
			this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
			this.toolStripContainer1.Margin = new System.Windows.Forms.Padding(0);
			this.toolStripContainer1.Name = "toolStripContainer1";
			this.toolStripContainer1.Size = new System.Drawing.Size(961, 606);
			this.toolStripContainer1.TabIndex = 1;
			this.toolStripContainer1.Text = "toolStripContainer1";
			// 
			// toolStripContainer1.TopToolStripPanel
			// 
			this.toolStripContainer1.TopToolStripPanel.ContextMenuStrip = this.contextMenuStripToolbars;
			this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.menuStripMain);
			this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStripFile);
			this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStripView);
			this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStripDrawing);
			this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStripControl);
			// 
			// splitContainerLogWindow
			// 
			this.splitContainerLogWindow.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainerLogWindow.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
			this.splitContainerLogWindow.Location = new System.Drawing.Point(0, 0);
			this.splitContainerLogWindow.Margin = new System.Windows.Forms.Padding(0);
			this.splitContainerLogWindow.Name = "splitContainerLogWindow";
			this.splitContainerLogWindow.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainerLogWindow.Panel1
			// 
			this.splitContainerLogWindow.Panel1.Controls.Add(this.splitContainerPolygonList);
			// 
			// splitContainerLogWindow.Panel2
			// 
			this.splitContainerLogWindow.Panel2.Controls.Add(this.tableLayoutPanel3);
			this.splitContainerLogWindow.Size = new System.Drawing.Size(961, 532);
			this.splitContainerLogWindow.SplitterDistance = 398;
			this.splitContainerLogWindow.TabIndex = 3;
			// 
			// splitContainerPolygonList
			// 
			this.splitContainerPolygonList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainerPolygonList.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
			this.splitContainerPolygonList.Location = new System.Drawing.Point(0, 0);
			this.splitContainerPolygonList.Margin = new System.Windows.Forms.Padding(0);
			this.splitContainerPolygonList.Name = "splitContainerPolygonList";
			// 
			// splitContainerPolygonList.Panel1
			// 
			this.splitContainerPolygonList.Panel1.Controls.Add(this.tableLayoutPanel1);
			// 
			// splitContainerPolygonList.Panel2
			// 
			this.splitContainerPolygonList.Panel2.Controls.Add(this.splitContainerToolbox);
			this.splitContainerPolygonList.Size = new System.Drawing.Size(961, 398);
			this.splitContainerPolygonList.SplitterDistance = 201;
			this.splitContainerPolygonList.TabIndex = 2;
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this.panel3, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.treeViewPolygons, 0, 1);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(201, 398);
			this.tableLayoutPanel1.TabIndex = 7;
			// 
			// panel3
			// 
			this.panel3.BackColor = System.Drawing.SystemColors.InactiveCaption;
			this.panel3.Controls.Add(this.label3);
			this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel3.Location = new System.Drawing.Point(0, 0);
			this.panel3.Margin = new System.Windows.Forms.Padding(0);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(201, 16);
			this.panel3.TabIndex = 9;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label3.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
			this.label3.Location = new System.Drawing.Point(3, -4);
			this.label3.Name = "label3";
			this.label3.Padding = new System.Windows.Forms.Padding(0, 6, 0, 6);
			this.label3.Size = new System.Drawing.Size(64, 25);
			this.label3.TabIndex = 9;
			this.label3.Text = "Polygon List";
			// 
			// treeViewPolygons
			// 
			this.treeViewPolygons.ContextMenuStrip = this.contextMenuStripPolygonList;
			this.treeViewPolygons.Dock = System.Windows.Forms.DockStyle.Fill;
			this.treeViewPolygons.HideSelection = false;
			this.treeViewPolygons.ImageIndex = 0;
			this.treeViewPolygons.ImageList = this.imageListPolygonTree;
			this.treeViewPolygons.Location = new System.Drawing.Point(0, 16);
			this.treeViewPolygons.Margin = new System.Windows.Forms.Padding(0);
			this.treeViewPolygons.Name = "treeViewPolygons";
			this.treeViewPolygons.SelectedImageIndex = 0;
			this.treeViewPolygons.Size = new System.Drawing.Size(201, 382);
			this.treeViewPolygons.TabIndex = 7;
			this.treeViewPolygons.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewPolygons_AfterSelect);
			this.treeViewPolygons.MouseDown += new System.Windows.Forms.MouseEventHandler(this.treeViewPolygons_MouseDown);
			// 
			// contextMenuStripPolygonList
			// 
			this.contextMenuStripPolygonList.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.removeToolStripMenuItem,
            this.removePolygonAndAllchildsToolStripMenuItem});
			this.contextMenuStripPolygonList.Name = "contextMenuStripPolygonList";
			this.contextMenuStripPolygonList.Size = new System.Drawing.Size(229, 48);
			this.contextMenuStripPolygonList.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStripPolygonList_Opening);
			// 
			// removeToolStripMenuItem
			// 
			this.removeToolStripMenuItem.Name = "removeToolStripMenuItem";
			this.removeToolStripMenuItem.Size = new System.Drawing.Size(228, 22);
			this.removeToolStripMenuItem.Text = "&Remove polygon";
			this.removeToolStripMenuItem.Click += new System.EventHandler(this.removePolygonToolStripMenuItem_Click);
			// 
			// removePolygonAndAllchildsToolStripMenuItem
			// 
			this.removePolygonAndAllchildsToolStripMenuItem.Name = "removePolygonAndAllchildsToolStripMenuItem";
			this.removePolygonAndAllchildsToolStripMenuItem.Size = new System.Drawing.Size(228, 22);
			this.removePolygonAndAllchildsToolStripMenuItem.Text = "Remove polygon and all &childs";
			this.removePolygonAndAllchildsToolStripMenuItem.Click += new System.EventHandler(this.removePolygonAndAllchildsToolStripMenuItem_Click);
			// 
			// imageListPolygonTree
			// 
			this.imageListPolygonTree.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListPolygonTree.ImageStream")));
			this.imageListPolygonTree.TransparentColor = System.Drawing.Color.Transparent;
			this.imageListPolygonTree.Images.SetKeyName(0, "ImagePolygon");
			this.imageListPolygonTree.Images.SetKeyName(1, "ImageDocument");
			// 
			// splitContainerToolbox
			// 
			this.splitContainerToolbox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainerToolbox.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
			this.splitContainerToolbox.Location = new System.Drawing.Point(0, 0);
			this.splitContainerToolbox.Margin = new System.Windows.Forms.Padding(0);
			this.splitContainerToolbox.Name = "splitContainerToolbox";
			// 
			// splitContainerToolbox.Panel1
			// 
			this.splitContainerToolbox.Panel1.ContextMenuStrip = this.contextMenuStripDraw;
			this.splitContainerToolbox.Panel1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.splitContainer1_Panel1_MouseMove);
			this.splitContainerToolbox.Panel1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.splitContainer1_Panel1_MouseClick);
			this.splitContainerToolbox.Panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.splitContainer1_Panel1_Paint);
			this.splitContainerToolbox.Panel1.ClientSizeChanged += new System.EventHandler(this.splitContainer1_Panel1_ClientSizeChanged);
			// 
			// splitContainerToolbox.Panel2
			// 
			this.splitContainerToolbox.Panel2.Controls.Add(this.tableLayoutPanelToolbox);
			this.splitContainerToolbox.Size = new System.Drawing.Size(756, 398);
			this.splitContainerToolbox.SplitterDistance = 487;
			this.splitContainerToolbox.TabIndex = 1;
			// 
			// contextMenuStripDraw
			// 
			this.contextMenuStripDraw.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.closePolygonToolStripMenuItem,
            this.cancelPolygonToolStripMenuItem1});
			this.contextMenuStripDraw.Name = "contextMenuStripDraw";
			this.contextMenuStripDraw.Size = new System.Drawing.Size(159, 48);
			this.contextMenuStripDraw.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStripDraw_Opening);
			// 
			// closePolygonToolStripMenuItem
			// 
			this.closePolygonToolStripMenuItem.Name = "closePolygonToolStripMenuItem";
			this.closePolygonToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
			this.closePolygonToolStripMenuItem.Text = "Close Polygon";
			this.closePolygonToolStripMenuItem.Click += new System.EventHandler(this.closePolygonToolStripMenuItem_Click_1);
			// 
			// cancelPolygonToolStripMenuItem1
			// 
			this.cancelPolygonToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cancelToolStripMenuItem});
			this.cancelPolygonToolStripMenuItem1.Name = "cancelPolygonToolStripMenuItem1";
			this.cancelPolygonToolStripMenuItem1.Size = new System.Drawing.Size(158, 22);
			this.cancelPolygonToolStripMenuItem1.Text = "Cancel Polygon";
			// 
			// cancelToolStripMenuItem
			// 
			this.cancelToolStripMenuItem.Name = "cancelToolStripMenuItem";
			this.cancelToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
			this.cancelToolStripMenuItem.Text = "Cancel";
			this.cancelToolStripMenuItem.Click += new System.EventHandler(this.cancelToolStripMenuItem_Click);
			// 
			// tableLayoutPanelToolbox
			// 
			this.tableLayoutPanelToolbox.ColumnCount = 1;
			this.tableLayoutPanelToolbox.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanelToolbox.Controls.Add(this.panel2, 0, 0);
			this.tableLayoutPanelToolbox.Controls.Add(this.panel1, 0, 1);
			this.tableLayoutPanelToolbox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanelToolbox.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanelToolbox.Margin = new System.Windows.Forms.Padding(0);
			this.tableLayoutPanelToolbox.Name = "tableLayoutPanelToolbox";
			this.tableLayoutPanelToolbox.RowCount = 2;
			this.tableLayoutPanelToolbox.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanelToolbox.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanelToolbox.Size = new System.Drawing.Size(265, 398);
			this.tableLayoutPanelToolbox.TabIndex = 0;
			// 
			// panel2
			// 
			this.panel2.BackColor = System.Drawing.SystemColors.InactiveCaption;
			this.panel2.Controls.Add(this.label2);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel2.Location = new System.Drawing.Point(0, 0);
			this.panel2.Margin = new System.Windows.Forms.Padding(0);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(265, 16);
			this.panel2.TabIndex = 0;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
			this.label2.Location = new System.Drawing.Point(3, -4);
			this.label2.Name = "label2";
			this.label2.Padding = new System.Windows.Forms.Padding(0, 6, 0, 6);
			this.label2.Size = new System.Drawing.Size(45, 25);
			this.label2.TabIndex = 10;
			this.label2.Text = "Toolbox";
			// 
			// panel1
			// 
			this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.panel1.Controls.Add(this.toolStrip1);
			this.panel1.Controls.Add(this.propertyGrid1);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(0, 16);
			this.panel1.Margin = new System.Windows.Forms.Padding(0);
			this.panel1.Name = "panel1";
			this.panel1.Padding = new System.Windows.Forms.Padding(3);
			this.panel1.Size = new System.Drawing.Size(265, 382);
			this.panel1.TabIndex = 0;
			// 
			// toolStrip1
			// 
			this.toolStrip1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.toolStrip1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
			this.toolStrip1.GripMargin = new System.Windows.Forms.Padding(0);
			this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton2});
			this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
			this.toolStrip1.Location = new System.Drawing.Point(234, 3);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
			this.toolStrip1.Size = new System.Drawing.Size(24, 26);
			this.toolStrip1.TabIndex = 0;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// propertyGrid1
			// 
			this.propertyGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.propertyGrid1.Location = new System.Drawing.Point(3, 3);
			this.propertyGrid1.Margin = new System.Windows.Forms.Padding(10);
			this.propertyGrid1.Name = "propertyGrid1";
			this.propertyGrid1.Size = new System.Drawing.Size(255, 372);
			this.propertyGrid1.TabIndex = 0;
			// 
			// tableLayoutPanel3
			// 
			this.tableLayoutPanel3.ColumnCount = 1;
			this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel3.Controls.Add(this.panel4, 0, 0);
			this.tableLayoutPanel3.Controls.Add(this.listViewLog, 0, 1);
			this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel3.Name = "tableLayoutPanel3";
			this.tableLayoutPanel3.RowCount = 2;
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel3.Size = new System.Drawing.Size(961, 130);
			this.tableLayoutPanel3.TabIndex = 0;
			// 
			// panel4
			// 
			this.panel4.BackColor = System.Drawing.SystemColors.InactiveCaption;
			this.panel4.Controls.Add(this.label1);
			this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel4.Location = new System.Drawing.Point(0, 0);
			this.panel4.Margin = new System.Windows.Forms.Padding(0);
			this.panel4.Name = "panel4";
			this.panel4.Size = new System.Drawing.Size(961, 16);
			this.panel4.TabIndex = 10;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.BackColor = System.Drawing.SystemColors.InactiveCaption;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
			this.label1.Location = new System.Drawing.Point(3, -4);
			this.label1.Name = "label1";
			this.label1.Padding = new System.Windows.Forms.Padding(0, 6, 0, 6);
			this.label1.Size = new System.Drawing.Size(67, 25);
			this.label1.TabIndex = 9;
			this.label1.Text = "Log Window";
			// 
			// listViewLog
			// 
			this.listViewLog.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader3,
            this.columnHeader2});
			this.listViewLog.ContextMenuStrip = this.contextMenuStripLogWindow;
			this.listViewLog.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listViewLog.FullRowSelect = true;
			this.listViewLog.GridLines = true;
			this.listViewLog.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.listViewLog.Location = new System.Drawing.Point(0, 16);
			this.listViewLog.Margin = new System.Windows.Forms.Padding(0);
			this.listViewLog.MultiSelect = false;
			this.listViewLog.Name = "listViewLog";
			this.listViewLog.Size = new System.Drawing.Size(961, 114);
			this.listViewLog.SmallImageList = this.imageListLog;
			this.listViewLog.TabIndex = 2;
			this.listViewLog.UseCompatibleStateImageBehavior = false;
			this.listViewLog.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "";
			this.columnHeader1.Width = 25;
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "#";
			this.columnHeader3.Width = 40;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "Message";
			this.columnHeader2.Width = 527;
			// 
			// contextMenuStripLogWindow
			// 
			this.contextMenuStripLogWindow.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clearLogToolStripMenuItem,
            this.saveLogToolStripMenuItem});
			this.contextMenuStripLogWindow.Name = "contextMenuStripLogWindow";
			this.contextMenuStripLogWindow.Size = new System.Drawing.Size(131, 48);
			// 
			// clearLogToolStripMenuItem
			// 
			this.clearLogToolStripMenuItem.Name = "clearLogToolStripMenuItem";
			this.clearLogToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
			this.clearLogToolStripMenuItem.Text = "Clear Log";
			this.clearLogToolStripMenuItem.Click += new System.EventHandler(this.clearLogToolStripMenuItem_Click);
			// 
			// saveLogToolStripMenuItem
			// 
			this.saveLogToolStripMenuItem.Enabled = false;
			this.saveLogToolStripMenuItem.Name = "saveLogToolStripMenuItem";
			this.saveLogToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
			this.saveLogToolStripMenuItem.Text = "Save Log";
			// 
			// imageListLog
			// 
			this.imageListLog.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListLog.ImageStream")));
			this.imageListLog.TransparentColor = System.Drawing.Color.Transparent;
			this.imageListLog.Images.SetKeyName(0, "IconInfo");
			this.imageListLog.Images.SetKeyName(1, "IconError");
			this.imageListLog.Images.SetKeyName(2, "IconAlgorithmModeFullSpeed");
			this.imageListLog.Images.SetKeyName(3, "IconAlgorithmModeSlow");
			this.imageListLog.Images.SetKeyName(4, "IconAlgorithmModeStep");
			this.imageListLog.Images.SetKeyName(5, "IconAlgorithmStep");
			this.imageListLog.Images.SetKeyName(6, "IconAlgorithmStop");
			this.imageListLog.Images.SetKeyName(7, "IconAlgorithmDone");
			// 
			// contextMenuStripToolbars
			// 
			this.contextMenuStripToolbars.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.standardToolStripMenuItem,
            this.viewToolStripMenuItem2,
            this.drawToolStripMenuItem,
            this.controlToolStripMenuItem1});
			this.contextMenuStripToolbars.Name = "contextMenuStripToolbars";
			this.contextMenuStripToolbars.Size = new System.Drawing.Size(130, 92);
			// 
			// standardToolStripMenuItem
			// 
			this.standardToolStripMenuItem.Checked = true;
			this.standardToolStripMenuItem.CheckOnClick = true;
			this.standardToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
			this.standardToolStripMenuItem.Name = "standardToolStripMenuItem";
			this.standardToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
			this.standardToolStripMenuItem.Text = "&Standard";
			this.standardToolStripMenuItem.Click += new System.EventHandler(this.standardToolStripMenuItem_Click);
			// 
			// viewToolStripMenuItem2
			// 
			this.viewToolStripMenuItem2.Checked = true;
			this.viewToolStripMenuItem2.CheckOnClick = true;
			this.viewToolStripMenuItem2.CheckState = System.Windows.Forms.CheckState.Checked;
			this.viewToolStripMenuItem2.Name = "viewToolStripMenuItem2";
			this.viewToolStripMenuItem2.Size = new System.Drawing.Size(129, 22);
			this.viewToolStripMenuItem2.Text = "&View";
			this.viewToolStripMenuItem2.Click += new System.EventHandler(this.viewToolStripMenuItem2_Click);
			// 
			// drawToolStripMenuItem
			// 
			this.drawToolStripMenuItem.Checked = true;
			this.drawToolStripMenuItem.CheckOnClick = true;
			this.drawToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
			this.drawToolStripMenuItem.Name = "drawToolStripMenuItem";
			this.drawToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
			this.drawToolStripMenuItem.Text = "&Draw";
			this.drawToolStripMenuItem.Click += new System.EventHandler(this.drawToolStripMenuItem_Click);
			// 
			// controlToolStripMenuItem1
			// 
			this.controlToolStripMenuItem1.Checked = true;
			this.controlToolStripMenuItem1.CheckOnClick = true;
			this.controlToolStripMenuItem1.CheckState = System.Windows.Forms.CheckState.Checked;
			this.controlToolStripMenuItem1.Name = "controlToolStripMenuItem1";
			this.controlToolStripMenuItem1.Size = new System.Drawing.Size(129, 22);
			this.controlToolStripMenuItem1.Text = "&Control";
			this.controlToolStripMenuItem1.Click += new System.EventHandler(this.controlToolStripMenuItem1_Click);
			// 
			// toolbarsToolStripMenuItem
			// 
			this.toolbarsToolStripMenuItem.DropDown = this.contextMenuStripToolbars;
			this.toolbarsToolStripMenuItem.Name = "toolbarsToolStripMenuItem";
			this.toolbarsToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
			this.toolbarsToolStripMenuItem.Text = "&Toolbars";
			// 
			// menuStripMain
			// 
			this.menuStripMain.Dock = System.Windows.Forms.DockStyle.None;
			this.menuStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.helpToolStripMenuItem});
			this.menuStripMain.Location = new System.Drawing.Point(0, 0);
			this.menuStripMain.Name = "menuStripMain";
			this.menuStripMain.Size = new System.Drawing.Size(961, 24);
			this.menuStripMain.TabIndex = 2;
			this.menuStripMain.Text = "menuStrip1";
			// 
			// fileToolStripMenuItem
			// 
			this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.toolStripMenuItem1,
            this.openToolStripMenuItem,
            this.toolStripMenuItem3,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.toolStripMenuItem2,
            this.exitToolStripMenuItem});
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
			this.fileToolStripMenuItem.Text = "&File";
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(121, 6);
			// 
			// toolStripMenuItem3
			// 
			this.toolStripMenuItem3.Name = "toolStripMenuItem3";
			this.toolStripMenuItem3.Size = new System.Drawing.Size(121, 6);
			// 
			// saveAsToolStripMenuItem
			// 
			this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
			this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
			this.saveAsToolStripMenuItem.Text = "S&ave As";
			this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
			// 
			// toolStripMenuItem2
			// 
			this.toolStripMenuItem2.Name = "toolStripMenuItem2";
			this.toolStripMenuItem2.Size = new System.Drawing.Size(121, 6);
			// 
			// viewToolStripMenuItem
			// 
			this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolbarsToolStripMenuItem,
            this.toolStripMenuItem4,
            this.showPolygonListToolStripMenuItem,
            this.showLogWindowToolStripMenuItem,
            this.showToolboxToolStripMenuItem,
            this.toolStripMenuItem5,
            this.showStatusBarToolStripMenuItem});
			this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
			this.viewToolStripMenuItem.Size = new System.Drawing.Size(41, 20);
			this.viewToolStripMenuItem.Text = "&View";
			// 
			// toolStripMenuItem4
			// 
			this.toolStripMenuItem4.Name = "toolStripMenuItem4";
			this.toolStripMenuItem4.Size = new System.Drawing.Size(169, 6);
			// 
			// toolStripMenuItem5
			// 
			this.toolStripMenuItem5.Name = "toolStripMenuItem5";
			this.toolStripMenuItem5.Size = new System.Drawing.Size(169, 6);
			// 
			// showStatusBarToolStripMenuItem
			// 
			this.showStatusBarToolStripMenuItem.Checked = true;
			this.showStatusBarToolStripMenuItem.CheckOnClick = true;
			this.showStatusBarToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
			this.showStatusBarToolStripMenuItem.Name = "showStatusBarToolStripMenuItem";
			this.showStatusBarToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
			this.showStatusBarToolStripMenuItem.Text = "Show S&tatus Bar";
			this.showStatusBarToolStripMenuItem.Click += new System.EventHandler(this.showStatusBarToolStripMenuItem_Click);
			// 
			// helpToolStripMenuItem
			// 
			this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.helpToolStripMenuItem1,
            this.aboutToolStripMenuItem});
			this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
			this.helpToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
			this.helpToolStripMenuItem.Text = "&Help";
			// 
			// aboutToolStripMenuItem
			// 
			this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
			this.aboutToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
			this.aboutToolStripMenuItem.Text = "&About";
			this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
			// 
			// toolStripFile
			// 
			this.toolStripFile.Dock = System.Windows.Forms.DockStyle.None;
			this.toolStripFile.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonNewDoc,
            this.toolStripSeparator2,
            this.toolStripButtonOpen,
            this.toolStripButtonSave,
            this.toolStripSeparator4,
            this.toolStripButtonExit});
			this.toolStripFile.Location = new System.Drawing.Point(3, 24);
			this.toolStripFile.Name = "toolStripFile";
			this.toolStripFile.Size = new System.Drawing.Size(114, 25);
			this.toolStripFile.TabIndex = 2;
			this.toolStripFile.Text = "toolStrip1";
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
			// 
			// toolStripSeparator4
			// 
			this.toolStripSeparator4.Name = "toolStripSeparator4";
			this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
			// 
			// toolStripView
			// 
			this.toolStripView.Dock = System.Windows.Forms.DockStyle.None;
			this.toolStripView.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.toolStripButtonShowLog,
            this.toolStripButtonShowToolbox});
			this.toolStripView.Location = new System.Drawing.Point(117, 24);
			this.toolStripView.Name = "toolStripView";
			this.toolStripView.Size = new System.Drawing.Size(79, 25);
			this.toolStripView.TabIndex = 1;
			this.toolStripView.Text = "toolStrip1";
			// 
			// toolStripControl
			// 
			this.toolStripControl.Dock = System.Windows.Forms.DockStyle.None;
			this.toolStripControl.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonAlgorithmModeFull,
            this.toolStripButtonAlgorithmModeSlow,
            this.toolStripButtonAlgorithmModeStep,
            this.toolStripSeparator3,
            this.toolStripButtonAlgorithmPlay,
            this.toolStripButtonAlgorithmPause,
            this.toolStripButtonAlgorithmStop});
			this.toolStripControl.Location = new System.Drawing.Point(59, 49);
			this.toolStripControl.Name = "toolStripControl";
			this.toolStripControl.Size = new System.Drawing.Size(154, 25);
			this.toolStripControl.TabIndex = 0;
			this.toolStripControl.Text = "toolStrip1";
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
			// 
			// saveFileDialogMain
			// 
			this.saveFileDialogMain.DefaultExt = "pwh";
			this.saveFileDialogMain.Filter = "TOPwh Files|*.pwh";
			// 
			// openFileDialogMain
			// 
			this.openFileDialogMain.DefaultExt = "pwh";
			this.openFileDialogMain.Filter = "TOPwh Files|*.pwh";
			// 
			// statusStrip1
			// 
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabelPos});
			this.statusStrip1.Location = new System.Drawing.Point(0, 606);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(961, 22);
			this.statusStrip1.TabIndex = 4;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// toolStripStatusLabelPos
			// 
			this.toolStripStatusLabelPos.Name = "toolStripStatusLabelPos";
			this.toolStripStatusLabelPos.Size = new System.Drawing.Size(0, 17);
			// 
			// tableLayoutPanel2
			// 
			this.tableLayoutPanel2.ColumnCount = 1;
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel2.Controls.Add(this.toolStripContainer1, 0, 0);
			this.tableLayoutPanel2.Controls.Add(this.statusStrip1, 0, 1);
			this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			this.tableLayoutPanel2.RowCount = 2;
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel2.Size = new System.Drawing.Size(961, 628);
			this.tableLayoutPanel2.TabIndex = 5;
			// 
			// timerLowSpeedMode
			// 
			this.timerLowSpeedMode.Interval = 800;
			this.timerLowSpeedMode.Tick += new System.EventHandler(this.timerLowSpeedMode_Tick);
			// 
			// toolStripButton2
			// 
			this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButton2.Image = global::TOPwh.Properties.Resources.RolledBack;
			this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButton2.Name = "toolStripButton2";
			this.toolStripButton2.Padding = new System.Windows.Forms.Padding(1, 2, 1, 1);
			this.toolStripButton2.Size = new System.Drawing.Size(23, 23);
			this.toolStripButton2.Text = "Reset Settings";
			this.toolStripButton2.Click += new System.EventHandler(this.toolStripButton2_Click);
			// 
			// newToolStripMenuItem
			// 
			this.newToolStripMenuItem.Image = global::TOPwh.Properties.Resources.NewDocumentHS;
			this.newToolStripMenuItem.Name = "newToolStripMenuItem";
			this.newToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
			this.newToolStripMenuItem.Text = "&New";
			this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
			// 
			// openToolStripMenuItem
			// 
			this.openToolStripMenuItem.Image = global::TOPwh.Properties.Resources.openHS;
			this.openToolStripMenuItem.Name = "openToolStripMenuItem";
			this.openToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
			this.openToolStripMenuItem.Text = "&Open";
			this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
			// 
			// saveToolStripMenuItem
			// 
			this.saveToolStripMenuItem.Image = global::TOPwh.Properties.Resources.saveHS;
			this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
			this.saveToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
			this.saveToolStripMenuItem.Text = "&Save";
			this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
			// 
			// exitToolStripMenuItem
			// 
			this.exitToolStripMenuItem.Image = global::TOPwh.Properties.Resources.DeleteHS;
			this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
			this.exitToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
			this.exitToolStripMenuItem.Text = "E&xit";
			this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
			// 
			// showPolygonListToolStripMenuItem
			// 
			this.showPolygonListToolStripMenuItem.Checked = true;
			this.showPolygonListToolStripMenuItem.CheckOnClick = true;
			this.showPolygonListToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
			this.showPolygonListToolStripMenuItem.Image = global::TOPwh.Properties.Resources.OrgChartHS;
			this.showPolygonListToolStripMenuItem.Name = "showPolygonListToolStripMenuItem";
			this.showPolygonListToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
			this.showPolygonListToolStripMenuItem.Text = "Show &Polygon List";
			this.showPolygonListToolStripMenuItem.Click += new System.EventHandler(this.showPolygonListToolStripMenuItem_Click);
			// 
			// showToolboxToolStripMenuItem
			// 
			this.showToolboxToolStripMenuItem.Checked = true;
			this.showToolboxToolStripMenuItem.CheckOnClick = true;
			this.showToolboxToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
			this.showToolboxToolStripMenuItem.Image = global::TOPwh.Properties.Resources.LegendHS;
			this.showToolboxToolStripMenuItem.Name = "showToolboxToolStripMenuItem";
			this.showToolboxToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
			this.showToolboxToolStripMenuItem.Text = "Show Tool&box";
			this.showToolboxToolStripMenuItem.Click += new System.EventHandler(this.showToolboxToolStripMenuItem_Click);
			// 
			// showLogWindowToolStripMenuItem
			// 
			this.showLogWindowToolStripMenuItem.Checked = true;
			this.showLogWindowToolStripMenuItem.CheckOnClick = true;
			this.showLogWindowToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
			this.showLogWindowToolStripMenuItem.Image = global::TOPwh.Properties.Resources.ShowRulelinesHS;
			this.showLogWindowToolStripMenuItem.Name = "showLogWindowToolStripMenuItem";
			this.showLogWindowToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
			this.showLogWindowToolStripMenuItem.Text = "Show &Log Window";
			this.showLogWindowToolStripMenuItem.Click += new System.EventHandler(this.showLogWindowToolStripMenuItem_Click);
			// 
			// helpToolStripMenuItem1
			// 
			this.helpToolStripMenuItem1.Image = global::TOPwh.Properties.Resources.Help;
			this.helpToolStripMenuItem1.Name = "helpToolStripMenuItem1";
			this.helpToolStripMenuItem1.Size = new System.Drawing.Size(114, 22);
			this.helpToolStripMenuItem1.Text = "&Help";
			this.helpToolStripMenuItem1.Click += new System.EventHandler(this.helpToolStripMenuItem1_Click);
			// 
			// toolStripButtonNewDoc
			// 
			this.toolStripButtonNewDoc.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButtonNewDoc.Image = global::TOPwh.Properties.Resources.NewDocumentHS;
			this.toolStripButtonNewDoc.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonNewDoc.Name = "toolStripButtonNewDoc";
			this.toolStripButtonNewDoc.Size = new System.Drawing.Size(23, 22);
			this.toolStripButtonNewDoc.Text = "New Document";
			this.toolStripButtonNewDoc.Click += new System.EventHandler(this.toolStripButtonNewDoc_Click);
			// 
			// toolStripButtonOpen
			// 
			this.toolStripButtonOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButtonOpen.Image = global::TOPwh.Properties.Resources.openHS;
			this.toolStripButtonOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonOpen.Name = "toolStripButtonOpen";
			this.toolStripButtonOpen.Size = new System.Drawing.Size(23, 22);
			this.toolStripButtonOpen.Text = "Open Document";
			this.toolStripButtonOpen.Click += new System.EventHandler(this.toolStripButtonOpen_Click);
			// 
			// toolStripButtonSave
			// 
			this.toolStripButtonSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButtonSave.Image = global::TOPwh.Properties.Resources.saveHS;
			this.toolStripButtonSave.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonSave.Name = "toolStripButtonSave";
			this.toolStripButtonSave.Size = new System.Drawing.Size(23, 22);
			this.toolStripButtonSave.Text = "Save Document";
			this.toolStripButtonSave.Click += new System.EventHandler(this.toolStripButtonSave_Click);
			// 
			// toolStripButtonExit
			// 
			this.toolStripButtonExit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButtonExit.Image = global::TOPwh.Properties.Resources.RecordHS;
			this.toolStripButtonExit.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonExit.Name = "toolStripButtonExit";
			this.toolStripButtonExit.Size = new System.Drawing.Size(23, 22);
			this.toolStripButtonExit.Text = "Exit Program";
			this.toolStripButtonExit.Click += new System.EventHandler(this.toolStripButtonExit_Click);
			// 
			// toolStripButton1
			// 
			this.toolStripButton1.Checked = true;
			this.toolStripButton1.CheckOnClick = true;
			this.toolStripButton1.CheckState = System.Windows.Forms.CheckState.Checked;
			this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButton1.Image = global::TOPwh.Properties.Resources.OrgChartHS;
			this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButton1.Name = "toolStripButton1";
			this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
			this.toolStripButton1.Text = "Show Polygon List";
			this.toolStripButton1.Click += new System.EventHandler(this.toolStripButtonShowPolygonList_Click);
			// 
			// toolStripButtonShowLog
			// 
			this.toolStripButtonShowLog.Checked = true;
			this.toolStripButtonShowLog.CheckOnClick = true;
			this.toolStripButtonShowLog.CheckState = System.Windows.Forms.CheckState.Checked;
			this.toolStripButtonShowLog.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButtonShowLog.Image = global::TOPwh.Properties.Resources.ShowRulelinesHS;
			this.toolStripButtonShowLog.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonShowLog.Name = "toolStripButtonShowLog";
			this.toolStripButtonShowLog.Size = new System.Drawing.Size(23, 22);
			this.toolStripButtonShowLog.Text = "Show Log Window";
			this.toolStripButtonShowLog.Click += new System.EventHandler(this.toolStripButtonShowLog_Click);
			// 
			// toolStripButtonShowToolbox
			// 
			this.toolStripButtonShowToolbox.Checked = true;
			this.toolStripButtonShowToolbox.CheckOnClick = true;
			this.toolStripButtonShowToolbox.CheckState = System.Windows.Forms.CheckState.Checked;
			this.toolStripButtonShowToolbox.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButtonShowToolbox.Image = global::TOPwh.Properties.Resources.LegendHS;
			this.toolStripButtonShowToolbox.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonShowToolbox.Name = "toolStripButtonShowToolbox";
			this.toolStripButtonShowToolbox.Size = new System.Drawing.Size(23, 22);
			this.toolStripButtonShowToolbox.Text = "Show Toolbox";
			this.toolStripButtonShowToolbox.Click += new System.EventHandler(this.toolStripButtonShowToolbox_Click);
			// 
			// toolStripButtonDrawMode
			// 
			this.toolStripButtonDrawMode.CheckOnClick = true;
			this.toolStripButtonDrawMode.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButtonDrawMode.Image = global::TOPwh.Properties.Resources.EditInformationHS;
			this.toolStripButtonDrawMode.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonDrawMode.Name = "toolStripButtonDrawMode";
			this.toolStripButtonDrawMode.Size = new System.Drawing.Size(23, 22);
			this.toolStripButtonDrawMode.Text = "Drawing Mode";
			this.toolStripButtonDrawMode.Click += new System.EventHandler(this.toolStripButtonDrawMode_Click);
			// 
			// toolStripButtonClear
			// 
			this.toolStripButtonClear.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButtonClear.Enabled = false;
			this.toolStripButtonClear.Image = global::TOPwh.Properties.Resources.DocumentHS;
			this.toolStripButtonClear.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonClear.Name = "toolStripButtonClear";
			this.toolStripButtonClear.Size = new System.Drawing.Size(23, 22);
			this.toolStripButtonClear.Text = "Clear Document";
			// 
			// toolStripButtonAlgorithmModeFull
			// 
			this.toolStripButtonAlgorithmModeFull.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButtonAlgorithmModeFull.Image = global::TOPwh.Properties.Resources.Run;
			this.toolStripButtonAlgorithmModeFull.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonAlgorithmModeFull.Name = "toolStripButtonAlgorithmModeFull";
			this.toolStripButtonAlgorithmModeFull.Size = new System.Drawing.Size(23, 22);
			this.toolStripButtonAlgorithmModeFull.Text = "Run Algorithm (Full Speed)";
			this.toolStripButtonAlgorithmModeFull.Click += new System.EventHandler(this.toolStripButtonModeFullSpeed_Click);
			// 
			// toolStripButtonAlgorithmModeSlow
			// 
			this.toolStripButtonAlgorithmModeSlow.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButtonAlgorithmModeSlow.Image = global::TOPwh.Properties.Resources.Slow;
			this.toolStripButtonAlgorithmModeSlow.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonAlgorithmModeSlow.Name = "toolStripButtonAlgorithmModeSlow";
			this.toolStripButtonAlgorithmModeSlow.Size = new System.Drawing.Size(23, 22);
			this.toolStripButtonAlgorithmModeSlow.Text = "Run Algorithm (Slow Motion)";
			this.toolStripButtonAlgorithmModeSlow.Click += new System.EventHandler(this.toolStripButtonAlgorithmModeSlow_Click);
			// 
			// toolStripButtonAlgorithmModeStep
			// 
			this.toolStripButtonAlgorithmModeStep.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButtonAlgorithmModeStep.Image = global::TOPwh.Properties.Resources.Step;
			this.toolStripButtonAlgorithmModeStep.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonAlgorithmModeStep.Name = "toolStripButtonAlgorithmModeStep";
			this.toolStripButtonAlgorithmModeStep.Size = new System.Drawing.Size(23, 22);
			this.toolStripButtonAlgorithmModeStep.Text = "Run Algorithm (Single Step Mode)";
			this.toolStripButtonAlgorithmModeStep.Click += new System.EventHandler(this.toolStripButtonModeStep_Click);
			// 
			// toolStripButtonAlgorithmPlay
			// 
			this.toolStripButtonAlgorithmPlay.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButtonAlgorithmPlay.Enabled = false;
			this.toolStripButtonAlgorithmPlay.Image = global::TOPwh.Properties.Resources.Play;
			this.toolStripButtonAlgorithmPlay.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonAlgorithmPlay.Name = "toolStripButtonAlgorithmPlay";
			this.toolStripButtonAlgorithmPlay.Size = new System.Drawing.Size(23, 22);
			this.toolStripButtonAlgorithmPlay.Text = "Play";
			this.toolStripButtonAlgorithmPlay.Click += new System.EventHandler(this.toolStripButtonAlgorithmForward_Click);
			// 
			// toolStripButtonAlgorithmPause
			// 
			this.toolStripButtonAlgorithmPause.CheckOnClick = true;
			this.toolStripButtonAlgorithmPause.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButtonAlgorithmPause.Enabled = false;
			this.toolStripButtonAlgorithmPause.Image = global::TOPwh.Properties.Resources.Pause;
			this.toolStripButtonAlgorithmPause.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonAlgorithmPause.Name = "toolStripButtonAlgorithmPause";
			this.toolStripButtonAlgorithmPause.Size = new System.Drawing.Size(23, 22);
			this.toolStripButtonAlgorithmPause.Text = "Pause";
			this.toolStripButtonAlgorithmPause.Click += new System.EventHandler(this.toolStripButtonAlgorithmPause_Click);
			// 
			// toolStripButtonAlgorithmStop
			// 
			this.toolStripButtonAlgorithmStop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButtonAlgorithmStop.Enabled = false;
			this.toolStripButtonAlgorithmStop.Image = global::TOPwh.Properties.Resources.Stop;
			this.toolStripButtonAlgorithmStop.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonAlgorithmStop.Name = "toolStripButtonAlgorithmStop";
			this.toolStripButtonAlgorithmStop.Size = new System.Drawing.Size(23, 22);
			this.toolStripButtonAlgorithmStop.Text = "Stop (Reset)";
			this.toolStripButtonAlgorithmStop.Click += new System.EventHandler(this.toolStripButtonAlgorithmStop_Click);
			// 
			// MainFrm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(961, 628);
			this.Controls.Add(this.tableLayoutPanel2);
			this.DoubleBuffered = true;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MainMenuStrip = this.menuStripMain;
			this.Name = "MainFrm";
			this.Text = "Form1";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
			this.toolStripDrawing.ResumeLayout(false);
			this.toolStripDrawing.PerformLayout();
			this.toolStripContainer1.ContentPanel.ResumeLayout(false);
			this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
			this.toolStripContainer1.TopToolStripPanel.PerformLayout();
			this.toolStripContainer1.ResumeLayout(false);
			this.toolStripContainer1.PerformLayout();
			this.splitContainerLogWindow.Panel1.ResumeLayout(false);
			this.splitContainerLogWindow.Panel2.ResumeLayout(false);
			this.splitContainerLogWindow.ResumeLayout(false);
			this.splitContainerPolygonList.Panel1.ResumeLayout(false);
			this.splitContainerPolygonList.Panel2.ResumeLayout(false);
			this.splitContainerPolygonList.ResumeLayout(false);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.panel3.ResumeLayout(false);
			this.panel3.PerformLayout();
			this.contextMenuStripPolygonList.ResumeLayout(false);
			this.splitContainerToolbox.Panel2.ResumeLayout(false);
			this.splitContainerToolbox.ResumeLayout(false);
			this.contextMenuStripDraw.ResumeLayout(false);
			this.tableLayoutPanelToolbox.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			this.panel2.PerformLayout();
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.tableLayoutPanel3.ResumeLayout(false);
			this.panel4.ResumeLayout(false);
			this.panel4.PerformLayout();
			this.contextMenuStripLogWindow.ResumeLayout(false);
			this.contextMenuStripToolbars.ResumeLayout(false);
			this.menuStripMain.ResumeLayout(false);
			this.menuStripMain.PerformLayout();
			this.toolStripFile.ResumeLayout(false);
			this.toolStripFile.PerformLayout();
			this.toolStripView.ResumeLayout(false);
			this.toolStripView.PerformLayout();
			this.toolStripControl.ResumeLayout(false);
			this.toolStripControl.PerformLayout();
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.tableLayoutPanel2.ResumeLayout(false);
			this.tableLayoutPanel2.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ToolStrip toolStripDrawing;
		private System.Windows.Forms.ToolStripContainer toolStripContainer1;
		private System.Windows.Forms.SplitContainer splitContainerToolbox;
		private System.Windows.Forms.ToolStripButton toolStripButtonDrawMode;
		private System.Windows.Forms.MenuStrip menuStripMain;
		private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
		private System.Windows.Forms.ContextMenuStrip contextMenuStripDraw;
		private System.Windows.Forms.ToolStripMenuItem closePolygonToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem cancelPolygonToolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem cancelToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
		private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
		private System.Windows.Forms.SaveFileDialog saveFileDialogMain;
		private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
		private System.Windows.Forms.OpenFileDialog openFileDialogMain;
		private System.Windows.Forms.ListView listViewLog;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ImageList imageListLog;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelPos;
		private System.Windows.Forms.SplitContainer splitContainerLogWindow;
		private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem showToolboxToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem showLogWindowToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem toolbarsToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem5;
		private System.Windows.Forms.ToolStripMenuItem showStatusBarToolStripMenuItem;
		private System.Windows.Forms.ToolStrip toolStripView;
		private System.Windows.Forms.ToolStrip toolStripControl;
		private System.Windows.Forms.ToolStripButton toolStripButtonShowToolbox;
		private System.Windows.Forms.ToolStripButton toolStripButtonShowLog;
		private System.Windows.Forms.ToolStripButton toolStripButtonClear;
		private System.Windows.Forms.ToolStripButton toolStripButtonAlgorithmModeFull;
		private System.Windows.Forms.ToolStripButton toolStripButtonAlgorithmStop;
		private System.Windows.Forms.ToolStripButton toolStripButtonAlgorithmModeStep;
		private System.Windows.Forms.ToolStrip toolStripFile;
		private System.Windows.Forms.ToolStripButton toolStripButtonNewDoc;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripButton toolStripButtonOpen;
		private System.Windows.Forms.ToolStripButton toolStripButtonSave;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
		private System.Windows.Forms.ToolStripButton toolStripButtonExit;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ContextMenuStrip contextMenuStripToolbars;
		private System.Windows.Forms.ToolStripMenuItem standardToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem2;
		private System.Windows.Forms.ToolStripMenuItem drawToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem controlToolStripMenuItem1;
		private System.Windows.Forms.ContextMenuStrip contextMenuStripLogWindow;
		private System.Windows.Forms.ToolStripMenuItem clearLogToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem saveLogToolStripMenuItem;
		private System.Windows.Forms.SplitContainer splitContainerPolygonList;
		private System.Windows.Forms.ToolStripMenuItem showPolygonListToolStripMenuItem;
		private System.Windows.Forms.ToolStripButton toolStripButton1;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.TreeView treeViewPolygons;
		private System.Windows.Forms.ImageList imageListPolygonTree;
		private System.Windows.Forms.ContextMenuStrip contextMenuStripPolygonList;
		private System.Windows.Forms.ToolStripMenuItem removeToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem removePolygonAndAllchildsToolStripMenuItem;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.PropertyGrid propertyGrid1;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanelToolbox;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.ToolStripButton toolStripButtonAlgorithmModeSlow;
		private System.Windows.Forms.ToolStripButton toolStripButtonAlgorithmPlay;
		private System.Windows.Forms.ToolStripButton toolStripButtonAlgorithmPause;
		private System.Windows.Forms.Timer timerLowSpeedMode;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Panel panel3;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
		private System.Windows.Forms.Panel panel4;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripButton toolStripButton2;
	}
}

