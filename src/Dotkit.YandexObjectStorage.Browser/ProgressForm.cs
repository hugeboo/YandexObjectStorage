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
        public string Message
        {
            get => label1.Text;
            set => label1.Text = value;
        }

        public ProgressForm()
        {
            InitializeComponent();
        }
    }
}
