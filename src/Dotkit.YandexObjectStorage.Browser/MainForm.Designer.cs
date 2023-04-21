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
            refreshToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator3 = new ToolStripSeparator();
            settingsToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            exitToolStripMenuItem = new ToolStripMenuItem();
            editToolStripMenuItem = new ToolStripMenuItem();
            copyToolStripMenuItem = new ToolStripMenuItem();
            pasteToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator4 = new ToolStripSeparator();
            deleteToolStripMenuItem = new ToolStripMenuItem();
            helpToolStripMenuItem = new ToolStripMenuItem();
            aboutToolStripMenuItem = new ToolStripMenuItem();
            mainToolStrip = new ToolStrip();
            settingsToolStripButton = new ToolStripButton();
            toolStripSeparator = new ToolStripSeparator();
            copyToolStripButton = new ToolStripButton();
            pasteToolStripButton = new ToolStripButton();
            deleteToolStripButton = new ToolStripButton();
            toolStripSeparator2 = new ToolStripSeparator();
            refreshToolStripButton = new ToolStripButton();
            mainStatusStrip = new StatusStrip();
            toolStripStatusLabel1 = new ToolStripStatusLabel();
            toolStripStatusLabel2 = new ToolStripStatusLabel();
            toolStripStatusLabel3 = new ToolStripStatusLabel();
            toolStripStatusLabel4 = new ToolStripStatusLabel();
            mainSplitContainer = new SplitContainer();
            mainTreeView = new TreeView();
            treeImageList = new ImageList(components);
            mainListView = new ListView();
            listLargeImageList = new ImageList(components);
            listSmallImageList = new ImageList(components);
            folderPanel = new Panel();
            folderTextBox = new TextBox();
            uiUpdateTimer = new System.Windows.Forms.Timer(components);
            mainMenuStrip.SuspendLayout();
            mainToolStrip.SuspendLayout();
            mainStatusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)mainSplitContainer).BeginInit();
            mainSplitContainer.Panel1.SuspendLayout();
            mainSplitContainer.Panel2.SuspendLayout();
            mainSplitContainer.SuspendLayout();
            folderPanel.SuspendLayout();
            SuspendLayout();
            // 
            // mainMenuStrip
            // 
            mainMenuStrip.ImageScalingSize = new Size(20, 20);
            mainMenuStrip.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, editToolStripMenuItem, helpToolStripMenuItem });
            mainMenuStrip.Location = new Point(0, 0);
            mainMenuStrip.Name = "mainMenuStrip";
            mainMenuStrip.Padding = new Padding(5, 2, 0, 2);
            mainMenuStrip.Size = new Size(902, 28);
            mainMenuStrip.TabIndex = 0;
            mainMenuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { refreshToolStripMenuItem, toolStripSeparator3, settingsToolStripMenuItem, toolStripSeparator1, exitToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(46, 24);
            fileToolStripMenuItem.Text = "File";
            // 
            // refreshToolStripMenuItem
            // 
            refreshToolStripMenuItem.Image = Properties.Resources.icons8_synchro_24;
            refreshToolStripMenuItem.Name = "refreshToolStripMenuItem";
            refreshToolStripMenuItem.Size = new Size(154, 26);
            refreshToolStripMenuItem.Text = "Refresh";
            refreshToolStripMenuItem.Click += refreshToolStripMenuItem_Click;
            // 
            // toolStripSeparator3
            // 
            toolStripSeparator3.Name = "toolStripSeparator3";
            toolStripSeparator3.Size = new Size(151, 6);
            // 
            // settingsToolStripMenuItem
            // 
            settingsToolStripMenuItem.Image = Properties.Resources.icons8_settings_24;
            settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            settingsToolStripMenuItem.Size = new Size(154, 26);
            settingsToolStripMenuItem.Text = "Settings...";
            settingsToolStripMenuItem.Click += settingsToolStripMenuItem_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(151, 6);
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.Size = new Size(154, 26);
            exitToolStripMenuItem.Text = "Exit";
            exitToolStripMenuItem.Click += exitToolStripMenuItem_Click;
            // 
            // editToolStripMenuItem
            // 
            editToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { copyToolStripMenuItem, pasteToolStripMenuItem, toolStripSeparator4, deleteToolStripMenuItem });
            editToolStripMenuItem.Name = "editToolStripMenuItem";
            editToolStripMenuItem.Size = new Size(49, 24);
            editToolStripMenuItem.Text = "Edit";
            // 
            // copyToolStripMenuItem
            // 
            copyToolStripMenuItem.Enabled = false;
            copyToolStripMenuItem.Image = Properties.Resources.copyToolStripButton_Image;
            copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            copyToolStripMenuItem.Size = new Size(136, 26);
            copyToolStripMenuItem.Text = "Copy";
            copyToolStripMenuItem.Click += copyToolStripMenuItem_Click;
            // 
            // pasteToolStripMenuItem
            // 
            pasteToolStripMenuItem.Enabled = false;
            pasteToolStripMenuItem.Image = Properties.Resources.pasteToolStripButton_Image;
            pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            pasteToolStripMenuItem.Size = new Size(136, 26);
            pasteToolStripMenuItem.Text = "Paste";
            pasteToolStripMenuItem.Click += pasteToolStripMenuItem_Click;
            // 
            // toolStripSeparator4
            // 
            toolStripSeparator4.Name = "toolStripSeparator4";
            toolStripSeparator4.Size = new Size(133, 6);
            // 
            // deleteToolStripMenuItem
            // 
            deleteToolStripMenuItem.Enabled = false;
            deleteToolStripMenuItem.Image = Properties.Resources.icons8_delete_24;
            deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            deleteToolStripMenuItem.Size = new Size(136, 26);
            deleteToolStripMenuItem.Text = "Delete";
            deleteToolStripMenuItem.Click += deleteToolStripMenuItem_Click;
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
            mainToolStrip.Items.AddRange(new ToolStripItem[] { settingsToolStripButton, toolStripSeparator, copyToolStripButton, pasteToolStripButton, deleteToolStripButton, toolStripSeparator2, refreshToolStripButton });
            mainToolStrip.Location = new Point(0, 28);
            mainToolStrip.Name = "mainToolStrip";
            mainToolStrip.Size = new Size(902, 27);
            mainToolStrip.TabIndex = 1;
            mainToolStrip.Text = "toolStrip1";
            // 
            // settingsToolStripButton
            // 
            settingsToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            settingsToolStripButton.Image = (Image)resources.GetObject("settingsToolStripButton.Image");
            settingsToolStripButton.ImageTransparentColor = Color.Magenta;
            settingsToolStripButton.Name = "settingsToolStripButton";
            settingsToolStripButton.Size = new Size(29, 24);
            settingsToolStripButton.Text = "Settings";
            settingsToolStripButton.Click += settingsToolStripButton_Click;
            // 
            // toolStripSeparator
            // 
            toolStripSeparator.Name = "toolStripSeparator";
            toolStripSeparator.Size = new Size(6, 27);
            // 
            // copyToolStripButton
            // 
            copyToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            copyToolStripButton.Enabled = false;
            copyToolStripButton.Image = (Image)resources.GetObject("copyToolStripButton.Image");
            copyToolStripButton.ImageTransparentColor = Color.Magenta;
            copyToolStripButton.Name = "copyToolStripButton";
            copyToolStripButton.Size = new Size(29, 24);
            copyToolStripButton.Text = "Copy";
            copyToolStripButton.Click += copyToolStripButton_Click;
            // 
            // pasteToolStripButton
            // 
            pasteToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            pasteToolStripButton.Enabled = false;
            pasteToolStripButton.Image = (Image)resources.GetObject("pasteToolStripButton.Image");
            pasteToolStripButton.ImageTransparentColor = Color.Magenta;
            pasteToolStripButton.Name = "pasteToolStripButton";
            pasteToolStripButton.Size = new Size(29, 24);
            pasteToolStripButton.Text = "Paste";
            pasteToolStripButton.Click += pasteToolStripButton_Click;
            // 
            // deleteToolStripButton
            // 
            deleteToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            deleteToolStripButton.Enabled = false;
            deleteToolStripButton.Image = Properties.Resources.icons8_delete_24;
            deleteToolStripButton.ImageTransparentColor = Color.Magenta;
            deleteToolStripButton.Name = "deleteToolStripButton";
            deleteToolStripButton.Size = new Size(29, 24);
            deleteToolStripButton.Text = "Delete";
            deleteToolStripButton.Click += deleteToolStripButton_Click;
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(6, 27);
            // 
            // refreshToolStripButton
            // 
            refreshToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            refreshToolStripButton.Image = (Image)resources.GetObject("refreshToolStripButton.Image");
            refreshToolStripButton.ImageTransparentColor = Color.Magenta;
            refreshToolStripButton.Name = "refreshToolStripButton";
            refreshToolStripButton.Size = new Size(29, 24);
            refreshToolStripButton.Text = "Refresh";
            refreshToolStripButton.Click += refreshToolStripButton_Click;
            // 
            // mainStatusStrip
            // 
            mainStatusStrip.ImageScalingSize = new Size(20, 20);
            mainStatusStrip.Items.AddRange(new ToolStripItem[] { toolStripStatusLabel1, toolStripStatusLabel2, toolStripStatusLabel3, toolStripStatusLabel4 });
            mainStatusStrip.Location = new Point(0, 460);
            mainStatusStrip.Name = "mainStatusStrip";
            mainStatusStrip.Padding = new Padding(1, 0, 12, 0);
            mainStatusStrip.ShowItemToolTips = true;
            mainStatusStrip.Size = new Size(902, 22);
            mainStatusStrip.TabIndex = 2;
            mainStatusStrip.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            toolStripStatusLabel1.Size = new Size(889, 16);
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
            // toolStripStatusLabel4
            // 
            toolStripStatusLabel4.Name = "toolStripStatusLabel4";
            toolStripStatusLabel4.Size = new Size(0, 16);
            // 
            // mainSplitContainer
            // 
            mainSplitContainer.Dock = DockStyle.Fill;
            mainSplitContainer.FixedPanel = FixedPanel.Panel1;
            mainSplitContainer.Location = new Point(0, 55);
            mainSplitContainer.Name = "mainSplitContainer";
            // 
            // mainSplitContainer.Panel1
            // 
            mainSplitContainer.Panel1.Controls.Add(mainTreeView);
            // 
            // mainSplitContainer.Panel2
            // 
            mainSplitContainer.Panel2.Controls.Add(mainListView);
            mainSplitContainer.Panel2.Controls.Add(folderPanel);
            mainSplitContainer.Size = new Size(902, 405);
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
            mainTreeView.Size = new Size(249, 405);
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
            mainListView.Location = new Point(0, 30);
            mainListView.Name = "mainListView";
            mainListView.ShowItemToolTips = true;
            mainListView.Size = new Size(649, 375);
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
            listLargeImageList.Images.SetKeyName(2, "file_default_downloaded");
            // 
            // listSmallImageList
            // 
            listSmallImageList.ColorDepth = ColorDepth.Depth8Bit;
            listSmallImageList.ImageStream = (ImageListStreamer)resources.GetObject("listSmallImageList.ImageStream");
            listSmallImageList.TransparentColor = Color.Transparent;
            listSmallImageList.Images.SetKeyName(0, "folder");
            listSmallImageList.Images.SetKeyName(1, "file_default");
            listSmallImageList.Images.SetKeyName(2, "file_default_downloaded");
            // 
            // folderPanel
            // 
            folderPanel.Controls.Add(folderTextBox);
            folderPanel.Dock = DockStyle.Top;
            folderPanel.Location = new Point(0, 0);
            folderPanel.Name = "folderPanel";
            folderPanel.Size = new Size(649, 30);
            folderPanel.TabIndex = 3;
            // 
            // folderTextBox
            // 
            folderTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            folderTextBox.BorderStyle = BorderStyle.None;
            folderTextBox.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            folderTextBox.Location = new Point(14, 0);
            folderTextBox.Name = "folderTextBox";
            folderTextBox.ReadOnly = true;
            folderTextBox.Size = new Size(632, 20);
            folderTextBox.TabIndex = 1;
            folderTextBox.WordWrap = false;
            // 
            // uiUpdateTimer
            // 
            uiUpdateTimer.Enabled = true;
            uiUpdateTimer.Interval = 500;
            uiUpdateTimer.Tick += uiUpdateTimer_Tick;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(902, 482);
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
            mainToolStrip.ResumeLayout(false);
            mainToolStrip.PerformLayout();
            mainStatusStrip.ResumeLayout(false);
            mainStatusStrip.PerformLayout();
            mainSplitContainer.Panel1.ResumeLayout(false);
            mainSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)mainSplitContainer).EndInit();
            mainSplitContainer.ResumeLayout(false);
            folderPanel.ResumeLayout(false);
            folderPanel.PerformLayout();
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
        private ToolStripMenuItem refreshToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripMenuItem editToolStripMenuItem;
        private ToolStripMenuItem copyToolStripMenuItem;
        private ToolStripMenuItem pasteToolStripMenuItem;
        private ToolStripMenuItem deleteToolStripMenuItem;
        private ToolStripButton settingsToolStripButton;
        private ToolStripSeparator toolStripSeparator;
        private ToolStripButton copyToolStripButton;
        private ToolStripButton pasteToolStripButton;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripButton refreshToolStripButton;
        private ToolStripSeparator toolStripSeparator4;
        private ToolStripButton deleteToolStripButton;
        private System.Windows.Forms.Timer uiUpdateTimer;
        private TextBox folderTextBox;
        private Panel folderPanel;
        private ToolStripStatusLabel toolStripStatusLabel4;
    }
}