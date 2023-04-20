namespace Dotkit.YandexObjectStorage.Browser
{
    partial class DeleteItemsForm
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DeleteItemsForm));
            deleteButton = new Button();
            cancelButton = new Button();
            label1 = new Label();
            listView = new ListView();
            typeColumnHeader = new ColumnHeader();
            nameColumnHeader = new ColumnHeader();
            imageList = new ImageList(components);
            progressBar = new ProgressBar();
            SuspendLayout();
            // 
            // deleteButton
            // 
            deleteButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            deleteButton.Location = new Point(550, 408);
            deleteButton.Name = "deleteButton";
            deleteButton.Size = new Size(138, 30);
            deleteButton.TabIndex = 0;
            deleteButton.Text = "Delete";
            deleteButton.UseVisualStyleBackColor = true;
            deleteButton.Click += deleteButton_Click;
            // 
            // cancelButton
            // 
            cancelButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            cancelButton.Location = new Point(694, 408);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(94, 30);
            cancelButton.TabIndex = 1;
            cancelButton.Text = "Cancel";
            cancelButton.UseVisualStyleBackColor = true;
            cancelButton.Click += cancelButton_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 20);
            label1.Name = "label1";
            label1.Size = new Size(156, 20);
            label1.TabIndex = 2;
            label1.Text = "List of items to delete:";
            // 
            // listView
            // 
            listView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            listView.CheckBoxes = true;
            listView.Columns.AddRange(new ColumnHeader[] { typeColumnHeader, nameColumnHeader });
            listView.Location = new Point(12, 43);
            listView.MultiSelect = false;
            listView.Name = "listView";
            listView.Size = new Size(776, 319);
            listView.SmallImageList = imageList;
            listView.TabIndex = 3;
            listView.UseCompatibleStateImageBehavior = false;
            listView.View = View.Details;
            listView.ItemChecked += listView_ItemChecked;
            // 
            // typeColumnHeader
            // 
            typeColumnHeader.Text = "Type";
            // 
            // nameColumnHeader
            // 
            nameColumnHeader.Text = "Name";
            // 
            // imageList
            // 
            imageList.ColorDepth = ColorDepth.Depth8Bit;
            imageList.ImageStream = (ImageListStreamer)resources.GetObject("imageList.ImageStream");
            imageList.TransparentColor = Color.Transparent;
            imageList.Images.SetKeyName(0, "folder");
            imageList.Images.SetKeyName(1, "file");
            // 
            // progressBar
            // 
            progressBar.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            progressBar.Location = new Point(12, 368);
            progressBar.Name = "progressBar";
            progressBar.Size = new Size(776, 29);
            progressBar.Style = ProgressBarStyle.Marquee;
            progressBar.TabIndex = 4;
            progressBar.Visible = false;
            // 
            // DeleteItemsForm
            // 
            AcceptButton = deleteButton;
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = cancelButton;
            ClientSize = new Size(800, 450);
            Controls.Add(progressBar);
            Controls.Add(listView);
            Controls.Add(label1);
            Controls.Add(cancelButton);
            Controls.Add(deleteButton);
            DoubleBuffered = true;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "DeleteItemsForm";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Delete Items";
            FormClosing += DeleteItemsForm_FormClosing;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button deleteButton;
        private Button cancelButton;
        private Label label1;
        private ListView listView;
        private ProgressBar progressBar;
        private ColumnHeader typeColumnHeader;
        private ColumnHeader nameColumnHeader;
        private ImageList imageList;
    }
}