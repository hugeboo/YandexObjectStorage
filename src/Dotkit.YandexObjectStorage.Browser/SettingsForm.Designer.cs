namespace Dotkit.YandexObjectStorage.Browser
{
    partial class SettingsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
            cancelButton = new Button();
            okButton = new Button();
            tabControl1 = new TabControl();
            generalTabPage = new TabPage();
            selectFolderButton = new Button();
            localStorageRootTextBox = new TextBox();
            label1 = new Label();
            s3TabPage = new TabPage();
            label6 = new Label();
            label5 = new Label();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            regionTextBox = new TextBox();
            secretAccessKeyTextBox = new TextBox();
            accessKeyIdTextBox = new TextBox();
            bucketNameTextBox = new TextBox();
            serviceUrlTextBox = new TextBox();
            folderBrowserDialog = new FolderBrowserDialog();
            tabControl1.SuspendLayout();
            generalTabPage.SuspendLayout();
            s3TabPage.SuspendLayout();
            SuspendLayout();
            // 
            // cancelButton
            // 
            cancelButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            cancelButton.DialogResult = DialogResult.Cancel;
            cancelButton.Location = new Point(568, 398);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(94, 29);
            cancelButton.TabIndex = 4;
            cancelButton.Text = "Cancel";
            cancelButton.UseVisualStyleBackColor = true;
            // 
            // okButton
            // 
            okButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            okButton.Location = new Point(468, 398);
            okButton.Name = "okButton";
            okButton.Size = new Size(94, 29);
            okButton.TabIndex = 3;
            okButton.Text = "OK";
            okButton.UseVisualStyleBackColor = true;
            okButton.Click += okButton_Click;
            // 
            // tabControl1
            // 
            tabControl1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tabControl1.Controls.Add(generalTabPage);
            tabControl1.Controls.Add(s3TabPage);
            tabControl1.Location = new Point(12, 12);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(650, 380);
            tabControl1.TabIndex = 5;
            // 
            // generalTabPage
            // 
            generalTabPage.Controls.Add(selectFolderButton);
            generalTabPage.Controls.Add(localStorageRootTextBox);
            generalTabPage.Controls.Add(label1);
            generalTabPage.Location = new Point(4, 29);
            generalTabPage.Name = "generalTabPage";
            generalTabPage.Padding = new Padding(3);
            generalTabPage.Size = new Size(642, 347);
            generalTabPage.TabIndex = 0;
            generalTabPage.Text = "General";
            generalTabPage.UseVisualStyleBackColor = true;
            // 
            // selectFolderButton
            // 
            selectFolderButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            selectFolderButton.Location = new Point(598, 43);
            selectFolderButton.Name = "selectFolderButton";
            selectFolderButton.Size = new Size(27, 30);
            selectFolderButton.TabIndex = 2;
            selectFolderButton.Text = "...";
            selectFolderButton.UseVisualStyleBackColor = true;
            selectFolderButton.Click += selectFolderButton_Click;
            // 
            // localStorageRootTextBox
            // 
            localStorageRootTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            localStorageRootTextBox.Location = new Point(18, 43);
            localStorageRootTextBox.Name = "localStorageRootTextBox";
            localStorageRootTextBox.Size = new Size(574, 27);
            localStorageRootTextBox.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(18, 20);
            label1.Name = "label1";
            label1.Size = new Size(193, 20);
            label1.TabIndex = 0;
            label1.Text = "Local storage root directory";
            // 
            // s3TabPage
            // 
            s3TabPage.Controls.Add(label6);
            s3TabPage.Controls.Add(label5);
            s3TabPage.Controls.Add(label4);
            s3TabPage.Controls.Add(label3);
            s3TabPage.Controls.Add(label2);
            s3TabPage.Controls.Add(regionTextBox);
            s3TabPage.Controls.Add(secretAccessKeyTextBox);
            s3TabPage.Controls.Add(accessKeyIdTextBox);
            s3TabPage.Controls.Add(bucketNameTextBox);
            s3TabPage.Controls.Add(serviceUrlTextBox);
            s3TabPage.Location = new Point(4, 29);
            s3TabPage.Name = "s3TabPage";
            s3TabPage.Padding = new Padding(3);
            s3TabPage.Size = new Size(642, 347);
            s3TabPage.TabIndex = 1;
            s3TabPage.Text = "S3 Configuration";
            s3TabPage.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(80, 157);
            label6.Name = "label6";
            label6.Size = new Size(56, 20);
            label6.TabIndex = 9;
            label6.Text = "Region";
            label6.TextAlign = ContentAlignment.TopRight;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(10, 124);
            label5.Name = "label5";
            label5.Size = new Size(126, 20);
            label5.TabIndex = 8;
            label5.Text = "Secret Access Key";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(36, 91);
            label4.Name = "label4";
            label4.Size = new Size(100, 20);
            label4.TabIndex = 7;
            label4.Text = "Access Key ID";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(39, 58);
            label3.Name = "label3";
            label3.Size = new Size(97, 20);
            label3.TabIndex = 6;
            label3.Text = "Bucket Name";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(50, 25);
            label2.Name = "label2";
            label2.Size = new Size(86, 20);
            label2.TabIndex = 5;
            label2.Text = "Service URL";
            // 
            // regionTextBox
            // 
            regionTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            regionTextBox.Location = new Point(142, 154);
            regionTextBox.Name = "regionTextBox";
            regionTextBox.Size = new Size(484, 27);
            regionTextBox.TabIndex = 4;
            // 
            // secretAccessKeyTextBox
            // 
            secretAccessKeyTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            secretAccessKeyTextBox.Location = new Point(142, 121);
            secretAccessKeyTextBox.Name = "secretAccessKeyTextBox";
            secretAccessKeyTextBox.Size = new Size(484, 27);
            secretAccessKeyTextBox.TabIndex = 3;
            // 
            // accessKeyIdTextBox
            // 
            accessKeyIdTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            accessKeyIdTextBox.Location = new Point(142, 88);
            accessKeyIdTextBox.Name = "accessKeyIdTextBox";
            accessKeyIdTextBox.Size = new Size(484, 27);
            accessKeyIdTextBox.TabIndex = 2;
            // 
            // bucketNamTextBox
            // 
            bucketNameTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            bucketNameTextBox.Location = new Point(142, 55);
            bucketNameTextBox.Name = "bucketNamTextBox";
            bucketNameTextBox.Size = new Size(484, 27);
            bucketNameTextBox.TabIndex = 1;
            // 
            // serviceUrlTextBox
            // 
            serviceUrlTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            serviceUrlTextBox.Location = new Point(142, 22);
            serviceUrlTextBox.Name = "serviceUrlTextBox";
            serviceUrlTextBox.Size = new Size(484, 27);
            serviceUrlTextBox.TabIndex = 0;
            // 
            // folderBrowserDialog
            // 
            folderBrowserDialog.RootFolder = Environment.SpecialFolder.ApplicationData;
            // 
            // SettingsForm
            // 
            AcceptButton = okButton;
            AutoScaleDimensions = new SizeF(120F, 120F);
            AutoScaleMode = AutoScaleMode.Dpi;
            CancelButton = cancelButton;
            ClientSize = new Size(674, 439);
            Controls.Add(tabControl1);
            Controls.Add(cancelButton);
            Controls.Add(okButton);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "SettingsForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "S3 Storage Browser Settings";
            tabControl1.ResumeLayout(false);
            generalTabPage.ResumeLayout(false);
            generalTabPage.PerformLayout();
            s3TabPage.ResumeLayout(false);
            s3TabPage.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Button cancelButton;
        private Button okButton;
        private TabControl tabControl1;
        private TabPage generalTabPage;
        private TabPage s3TabPage;
        private Button selectFolderButton;
        private TextBox localStorageRootTextBox;
        private Label label1;
        private FolderBrowserDialog folderBrowserDialog;
        private Label label6;
        private Label label5;
        private Label label4;
        private Label label3;
        private Label label2;
        private TextBox regionTextBox;
        private TextBox secretAccessKeyTextBox;
        private TextBox accessKeyIdTextBox;
        private TextBox bucketNameTextBox;
        private TextBox serviceUrlTextBox;
    }
}