namespace Dotkit.YandexObjectStorage.Browser
{
    partial class CreateFolderForm
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
            label1 = new Label();
            textBox = new TextBox();
            okButton = new Button();
            cancelButton = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 21);
            label1.Name = "label1";
            label1.Size = new Size(174, 20);
            label1.TabIndex = 0;
            label1.Text = "Please enter folder name";
            // 
            // textBox
            // 
            textBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textBox.Location = new Point(12, 53);
            textBox.Name = "textBox";
            textBox.Size = new Size(412, 27);
            textBox.TabIndex = 1;
            // 
            // okButton
            // 
            okButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            okButton.Location = new Point(230, 98);
            okButton.Name = "okButton";
            okButton.Size = new Size(94, 29);
            okButton.TabIndex = 2;
            okButton.Text = "OK";
            okButton.UseVisualStyleBackColor = true;
            okButton.Click += okButton_Click;
            // 
            // cancelButton
            // 
            cancelButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            cancelButton.Location = new Point(330, 98);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(94, 29);
            cancelButton.TabIndex = 3;
            cancelButton.Text = "Cancel";
            cancelButton.UseVisualStyleBackColor = true;
            // 
            // CreateFolderForm
            // 
            AcceptButton = okButton;
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = cancelButton;
            ClientSize = new Size(436, 139);
            Controls.Add(cancelButton);
            Controls.Add(okButton);
            Controls.Add(textBox);
            Controls.Add(label1);
            DoubleBuffered = true;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "CreateFolderForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Create Folder";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox textBox;
        private Button okButton;
        private Button cancelButton;
    }
}