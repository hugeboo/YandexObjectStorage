namespace Dotkit.YandexObjectStorage.Browser
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            //_service.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            mainMenuStrip = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            exitToolStripMenuItem = new ToolStripMenuItem();
            helpToolStripMenuItem = new ToolStripMenuItem();
            aboutToolStripMenuItem = new ToolStripMenuItem();
            mainToolStrip = new ToolStrip();
            mainStatusStrip = new StatusStrip();
            toolStripStatusLabel1 = new ToolStripStatusLabel();
            toolStripStatusLabel2 = new ToolStripStatusLabel();
            toolStripStatusLabel3 = new ToolStripStatusLabel();
            mainSplitContainer = new SplitContainer();
            mainTreeView = new TreeView();
            treeImageList = new ImageList(components);
            mainListView = new ListView();
            listLargeImageList = new ImageList(components);
            listSmallImageList = new ImageList(components);
            settingsToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            mainMenuStrip.SuspendLayout();
            mainStatusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)mainSplitContainer).BeginInit();
            mainSplitContainer.Panel1.SuspendLayout();
            mainSplitContainer.Panel2.SuspendLayout();
            mainSplitContainer.SuspendLayout();
            SuspendLayout();
            // 
            // mainMenuStrip
            // 
            mainMenuStrip.ImageScalingSize = new Size(20, 20);
            mainMenuStrip.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, helpToolStripMenuItem });
            mainMenuStrip.Location = new Point(0, 0);
            mainMenuStrip.Name = "mainMenuStrip";
            mainMenuStrip.Padding = new Padding(5, 2, 0, 2);
            mainMenuStrip.Size = new Size(908, 28);
            mainMenuStrip.TabIndex = 0;
            mainMenuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { settingsToolStripMenuItem, toolStripSeparator1, exitToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(46, 24);
            fileToolStripMenuItem.Text = "File";
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.Size = new Size(224, 26);
            exitToolStripMenuItem.Text = "Exit";
            exitToolStripMenuItem.Click += exitToolStripMenuItem_Click;
            // 
            // helpToolStripMenuItem
            // 
            helpToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { aboutToolStripMenuItem });
            helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            helpToolStripMenuItem.Size = new Size(55, 24);
            helpToolStripMenuItem.Text = "Help";
            // 
            // aboutToolStripMenuItem
            // 
            aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            aboutToolStripMenuItem.Size = new Size(142, 26);
            aboutToolStripMenuItem.Text = "About...";
            aboutToolStripMenuItem.Click += aboutToolStripMenuItem_Click;
            // 
            // mainToolStrip
            // 
            mainToolStrip.ImageScalingSize = new Size(20, 20);
            mainToolStrip.Location = new Point(0, 28);
            mainToolStrip.Name = "mainToolStrip";
            mainToolStrip.Size = new Size(908, 25);
            mainToolStrip.TabIndex = 1;
            mainToolStrip.Text = "toolStrip1";
            // 
            // mainStatusStrip
            // 
            mainStatusStrip.ImageScalingSize = new Size(20, 20);
            mainStatusStrip.Items.AddRange(new ToolStripItem[] { toolStripStatusLabel1, toolStripStatusLabel2, toolStripStatusLabel3 });
            mainStatusStrip.Location = new Point(0, 460);
            mainStatusStrip.Name = "mainStatusStrip";
            mainStatusStrip.Padding = new Padding(1, 0, 12, 0);
            mainStatusStrip.Size = new Size(908, 22);
            mainStatusStrip.TabIndex = 2;
            mainStatusStrip.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            toolStripStatusLabel1.Size = new Size(895, 16);
            toolStripStatusLabel1.Spring = true;
            toolStripStatusLabel1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // toolStripStatusLabel2
            // 
            toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            toolStripStatusLabel2.Size = new Size(0, 16);
            // 
            // toolStripStatusLabel3
            // 
            toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            toolStripStatusLabel3.Size = new Size(0, 16);
            // 
            // mainSplitContainer
            // 
            mainSplitContainer.Dock = DockStyle.Fill;
            mainSplitContainer.FixedPanel = FixedPanel.Panel1;
            mainSplitContainer.Location = new Point(0, 53);
            mainSplitContainer.Name = "mainSplitContainer";
            // 
            // mainSplitContainer.Panel1
            // 
            mainSplitContainer.Panel1.Controls.Add(mainTreeView);
            // 
            // mainSplitContainer.Panel2
            // 
            mainSplitContainer.Panel2.Controls.Add(mainListView);
            mainSplitContainer.Size = new Size(908, 407);
            mainSplitContainer.SplitterDistance = 249;
            mainSplitContainer.TabIndex = 3;
            // 
            // mainTreeView
            // 
            mainTreeView.BorderStyle = BorderStyle.None;
            mainTreeView.Dock = DockStyle.Fill;
            mainTreeView.HideSelection = false;
            mainTreeView.ImageIndex = 0;
            mainTreeView.ImageList = treeImageList;
            mainTreeView.Location = new Point(0, 0);
            mainTreeView.Name = "mainTreeView";
            mainTreeView.SelectedImageIndex = 0;
            mainTreeView.Size = new Size(249, 407);
            mainTreeView.TabIndex = 0;
            // 
            // treeImageList
            // 
            treeImageList.ColorDepth = ColorDepth.Depth8Bit;
            treeImageList.ImageStream = (ImageListStreamer)resources.GetObject("treeImageList.ImageStream");
            treeImageList.TransparentColor = Color.Transparent;
            treeImageList.Images.SetKeyName(0, "bucket");
            treeImageList.Images.SetKeyName(1, "folder");
            treeImageList.Images.SetKeyName(2, "open_folder");
            treeImageList.Images.SetKeyName(3, "bucket_0");
            // 
            // mainListView
            // 
            mainListView.BorderStyle = BorderStyle.None;
            mainListView.Dock = DockStyle.Fill;
            mainListView.LargeImageList = listLargeImageList;
            mainListView.Location = new Point(0, 0);
            mainListView.Name = "mainListView";
            mainListView.ShowItemToolTips = true;
            mainListView.Size = new Size(655, 407);
            mainListView.SmallImageList = listSmallImageList;
            mainListView.TabIndex = 0;
            mainListView.UseCompatibleStateImageBehavior = false;
            // 
            // listLargeImageList
            // 
            listLargeImageList.ColorDepth = ColorDepth.Depth8Bit;
            listLargeImageList.ImageStream = (ImageListStreamer)resources.GetObject("listLargeImageList.ImageStream");
            listLargeImageList.TransparentColor = Color.Transparent;
            listLargeImageList.Images.SetKeyName(0, "folder");
            listLargeImageList.Images.SetKeyName(1, "file_default");
            // 
            // listSmallImageList
            // 
            listSmallImageList.ColorDepth = ColorDepth.Depth8Bit;
            listSmallImageList.ImageStream = (ImageListStreamer)resources.GetObject("listSmallImageList.ImageStream");
            listSmallImageList.TransparentColor = Color.Transparent;
            listSmallImageList.Images.SetKeyName(0, "file_default");
            listSmallImageList.Images.SetKeyName(1, "folder");
            // 
            // settingsToolStripMenuItem
            // 
            settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            settingsToolStripMenuItem.Size = new Size(224, 26);
            settingsToolStripMenuItem.Text = "Settings...";
            settingsToolStripMenuItem.Click += settingsToolStripMenuItem_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(221, 6);
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(908, 482);
            Controls.Add(mainSplitContainer);
            Controls.Add(mainStatusStrip);
            Controls.Add(mainToolStrip);
            Controls.Add(mainMenuStrip);
            DoubleBuffered = true;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = mainMenuStrip;
            Name = "MainForm";
            Text = "S3 Storage";
            FormClosing += MainForm_FormClosing;
            Load += MainForm_Load;
            mainMenuStrip.ResumeLayout(false);
            mainMenuStrip.PerformLayout();
            mainStatusStrip.ResumeLayout(false);
            mainStatusStrip.PerformLayout();
            mainSplitContainer.Panel1.ResumeLayout(false);
            mainSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)mainSplitContainer).EndInit();
            mainSplitContainer.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip mainMenuStrip;
        private ToolStrip mainToolStrip;
        private StatusStrip mainStatusStrip;
        private SplitContainer mainSplitContainer;
        private TreeView mainTreeView;
        private ListView mainListView;
        private ImageList treeImageList;
        private ImageList listLargeImageList;
        private ImageList listSmallImageList;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private ToolStripStatusLabel toolStripStatusLabel2;
        private ToolStripStatusLabel toolStripStatusLabel3;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem exitToolStripMenuItem;
        private ToolStripMenuItem helpToolStripMenuItem;
        private ToolStripMenuItem aboutToolStripMenuItem;
        private ToolStripMenuItem settingsToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator1;
    }
}