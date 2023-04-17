using Dotkit.S3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dotkit.YandexObjectStorage.Browser
{
    internal class Configuration
    {
        public S3Configuration S3Configuration { get; set; } = new S3Configuration();
        public UIState UIState { get; set; } = new UIState();
    }

    internal class UIState
    {
        public FormWindowState FormWindowState { get; set; } = FormWindowState.Normal;
        public int MainFormWidth { get; set; } = -1;
        public int MainFormHeight { get; set; } = -1;
        public int MainSlitterDistance { get; set; } = -1;
    }
}
