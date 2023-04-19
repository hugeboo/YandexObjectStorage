using Dotkit.S3;

namespace Dotkit.YandexObjectStorage.Browser
{
    internal static class Program
    {
        public static Configuration Configuration { get; private set; } = new Configuration();
        public static S3Configuration S3Configuration { get; private set; } = new S3Configuration();

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(true);

            Configuration = Configuration.Load();

            var s3Config = LoadS3Configuration();
            if (s3Config == null)
            {
                return;
            }

            S3Configuration = s3Config!;

            if (!Configuration.Validate() || !S3Configuration.Validate())
            {
                using var dlg = new SettingsForm();
                dlg.ShowDialog();
            }

            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            //ApplicationConfiguration.Initialize();
            //Application.EnableVisualStyles();
            Application.Run(new MainForm());
        }

        private static S3Configuration? LoadS3Configuration()
        {
            if (S3Configuration.Exists())
            {
                using var dlg = new PasswordForm();
                while (true)
                {
                    dlg.Password = string.Empty;
                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        try
                        {
                            var s3Config = S3ConfigurationExtension.Load(dlg.Password);
                            return s3Config;
                        }
                        catch
                        {
                            MessageBox.Show("Password incorrect!", "Login", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            else
            {
                return new S3Configuration();
            }
        }
    }
}