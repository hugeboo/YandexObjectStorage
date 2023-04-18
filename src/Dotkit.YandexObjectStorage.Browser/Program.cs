using Dotkit.S3;

namespace Dotkit.YandexObjectStorage.Browser
{
    internal static class Program
    {
        public static Configuration Config { get; private set; } = new Configuration();
        public static S3Configuration S3Configuration { get; private set; } = new S3Configuration();

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Config = Configuration.Load();
            //S3Configuration = S3ConfigurationExtension.Load("");

            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.EnableVisualStyles();
            Application.Run(new MainForm());
        }
    }
}