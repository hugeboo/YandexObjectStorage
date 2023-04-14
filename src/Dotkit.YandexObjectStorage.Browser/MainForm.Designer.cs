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
            _yosClient.Dispose();
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
            mainToolStrip = new ToolStrip();
            mainStatusStrip = new StatusStrip();
            mainSplitContainer = new SplitContainer();
            mainTreeView = new TreeView();
            treeImageList = new ImageList(components);
            mainListView = new ListView();
            listLargeImageList = new ImageList(components);
            ((System.ComponentModel.ISupportInitialize)mainSplitContainer).BeginInit();
            mainSplitContainer.Panel1.SuspendLayout();
            mainSplitContainer.Panel2.SuspendLayout();
            mainSplitContainer.SuspendLayout();
            SuspendLayout();
            // 
            // mainMenuStrip
            // 
            mainMenuStrip.ImageScalingSize = new Size(20, 20);
            mainMenuStrip.Location = new Point(0, 0);
            mainMenuStrip.Name = "mainMenuStrip";
            mainMenuStrip.Padding = new Padding(5, 2, 0, 2);
            mainMenuStrip.Size = new Size(788, 24);
            mainMenuStrip.TabIndex = 0;
            mainMenuStrip.Text = "menuStrip1";
            // 
            // mainToolStrip
            // 
            mainToolStrip.ImageScalingSize = new Size(20, 20);
            mainToolStrip.Location = new Point(0, 24);
            mainToolStrip.Name = "mainToolStrip";
            mainToolStrip.Size = new Size(788, 25);
            mainToolStrip.TabIndex = 1;
            mainToolStrip.Text = "toolStrip1";
            // 
            // mainStatusStrip
            // 
            mainStatusStrip.ImageScalingSize = new Size(20, 20);
            mainStatusStrip.Location = new Point(0, 410);
            mainStatusStrip.Name = "mainStatusStrip";
            mainStatusStrip.Padding = new Padding(1, 0, 12, 0);
            mainStatusStrip.Size = new Size(788, 22);
            mainStatusStrip.TabIndex = 2;
            mainStatusStrip.Text = "statusStrip1";
            // 
            // mainSplitContainer
            // 
            mainSplitContainer.Dock = DockStyle.Fill;
            mainSplitContainer.FixedPanel = FixedPanel.Panel1;
            mainSplitContainer.Location = new Point(0, 49);
            mainSplitContainer.Name = "mainSplitContainer";
            // 
            // mainSplitContainer.Panel1
            // 
            mainSplitContainer.Panel1.Controls.Add(mainTreeView);
            // 
            // mainSplitContainer.Panel2
            // 
            mainSplitContainer.Panel2.Controls.Add(mainListView);
            mainSplitContainer.Size = new Size(788, 361);
            mainSplitContainer.SplitterDistance = 249;
            mainSplitContainer.TabIndex = 3;
            mainSplitContainer.SplitterMoved += mainSplitContainer_SplitterMoved;
            // 
            // mainTreeView
            // 
            mainTreeView.BorderStyle = BorderStyle.None;
            mainTreeView.Dock = DockStyle.Fill;
            mainTreeView.ImageIndex = 0;
            mainTreeView.ImageList = treeImageList;
            mainTreeView.Location = new Point(0, 0);
            mainTreeView.Name = "mainTreeView";
            mainTreeView.SelectedImageIndex = 0;
            mainTreeView.Size = new Size(249, 361);
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
            // 
            // mainListView
            // 
            mainListView.BorderStyle = BorderStyle.None;
            mainListView.Dock = DockStyle.Fill;
            mainListView.LargeImageList = listLargeImageList;
            mainListView.Location = new Point(0, 0);
            mainListView.Name = "mainListView";
            mainListView.Size = new Size(535, 361);
            mainListView.TabIndex = 0;
            mainListView.UseCompatibleStateImageBehavior = false;
            // 
            // listLargeImageList
            // 
            listLargeImageList.ColorDepth = ColorDepth.Depth8Bit;
            listLargeImageList.ImageStream = (ImageListStreamer)resources.GetObject("listLargeImageList.ImageStream");
            listLargeImageList.TransparentColor = Color.Transparent;
            listLargeImageList.Images.SetKeyName(0, "folder");
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(788, 432);
            Controls.Add(mainSplitContainer);
            Controls.Add(mainStatusStrip);
            Controls.Add(mainToolStrip);
            Controls.Add(mainMenuStrip);
            MainMenuStrip = mainMenuStrip;
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Yandex Object Storage";
            Load += MainForm_Load;
            Resize += MainForm_Resize;
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
    }
}