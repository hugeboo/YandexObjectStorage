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
    public partial class PasswordForm : Form
    {
        public string Password
        {
            get => textBox.Text;
            set => textBox.Text = value;
        }

        public PasswordForm()
        {
            InitializeComponent();
        }

        private void textBox_TextChanged(object sender, EventArgs e)
        {
            okButton.Enabled = !string.IsNullOrEmpty(textBox.Text);
        }
    }
}
