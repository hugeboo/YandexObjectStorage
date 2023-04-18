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
    public partial class ProgressForm : Form
    {
        private MainForm? _mainForm;

        public string Message
        {
            get => label1.Text;
            set => label1.Text = value;
        }

        public ProgressForm()
        {
            InitializeComponent();
        }

        public void ShowEx(MainForm mainForm)
        {
            _mainForm = mainForm;
            _mainForm.Lock();

            this.Show(mainForm);
            SetLocation();
        }

        private void SetLocation()
        {
            if (_mainForm == null) return;
            this.Location = new Point(
                _mainForm.Left + (_mainForm.Width - this.Width) / 2,
                _mainForm.Top + (_mainForm.Height - this.Height) / 2);
        }

        private void ProgressForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _mainForm?.Unlock();
        }
    }
}
