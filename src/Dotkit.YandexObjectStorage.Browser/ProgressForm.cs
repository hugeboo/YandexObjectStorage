using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dotkit.YandexObjectStorage.Browser
{
    public partial class ProgressForm : Form
    {
        private MainForm? _mainForm;

        [DllImport("user32")]
        static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);
        [DllImport("user32")]
        static extern bool EnableMenuItem(IntPtr hMenu, uint uIDEnableItem, uint uEnable);

        const int MF_BYCOMMAND = 0;
        const int MF_DISABLED = 2;
        const int SC_CLOSE = 0xF060; 
        
        public string Message
        {
            get => label1.Text;
            set => label1.Text = value;
        }

        public ProgressForm()
        {
            InitializeComponent();

            var sm = GetSystemMenu(Handle, false);
            EnableMenuItem(sm, SC_CLOSE, MF_BYCOMMAND | MF_DISABLED);
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
