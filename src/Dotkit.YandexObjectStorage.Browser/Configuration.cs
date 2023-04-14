using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dotkit.YandexObjectStorage.Browser
{
    internal class Configuration
    {
        public YOSConfig YOSConfig { get; set; } = new YOSConfig();
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
