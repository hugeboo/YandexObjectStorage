using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dotkit.YandexObjectStorage.Browser
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            localStorageRootTextBox.Text = Program.Configuration.LocalFileStorageRoot;
            serviceUrlTextBox.Text = Program.S3Configuration.ServiceURL;
            accessKeyIdTextBox.Text = Program.S3Configuration.AccessKeyId;
            secretAccessKeyTextBox.Text = Program.S3Configuration.SecretAccessKey;
            regionTextBox.Text = Program.S3Configuration.Region;
            bucketNameTextBox.Text = Program.S3Configuration.BucketName;
        }

        private void GetData()
        {
            Program.Configuration.LocalFileStorageRoot = localStorageRootTextBox.Text;
            Program.S3Configuration.ServiceURL = serviceUrlTextBox.Text;
            Program.S3Configuration.AccessKeyId = accessKeyIdTextBox.Text;
            Program.S3Configuration.SecretAccessKey = secretAccessKeyTextBox.Text;
            Program.S3Configuration.Region = regionTextBox.Text;
            Program.S3Configuration.BucketName = bucketNameTextBox.Text;
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            var dlg = new PasswordForm();
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                GetData();
                Program.S3Configuration.Save(dlg.Password);
                Program.Configuration.Save();
                DialogResult = DialogResult.OK;
            }
        }

        private void selectFolderButton_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                localStorageRootTextBox.Text = folderBrowserDialog.SelectedPath;
            }
        }
    }
}
