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
    public partial class CreateFolderForm : Form
    {
        public string FolderName { get => textBox.Text; set { textBox.Text = value; } }

        public CreateFolderForm()
        {
            InitializeComponent();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(FolderName))
            {
                MessageBox.Show("Folder name cannot be empty", "Create Folder", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                DialogResult = DialogResult.OK;
            }
        }
    }
}
