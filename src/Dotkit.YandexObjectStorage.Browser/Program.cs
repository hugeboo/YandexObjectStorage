namespace Dotkit.YandexObjectStorage.Browser
{
    internal static class Program
    {
        public static Configuration Config { get; private set; } = new Configuration();

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.EnableVisualStyles();
            Application.Run(new MainForm());
        }
    }
}