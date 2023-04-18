using Dotkit.S3;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dotkit.YandexObjectStorage.Browser
{
    internal sealed class Configuration
    {
        public const string FileName = "configuration.json";

        public string LocalFileStorageRoot { get; set; } = string.Empty;

        public bool Validate()
        {
            return !string.IsNullOrEmpty(LocalFileStorageRoot);
        }

        public void Save()
        {
            var s = JsonConvert.SerializeObject(this);
            File.WriteAllText(FileName, s);
        }

        public static Configuration Load()
        {
            try
            {
                var jsonConfig = new JsonSerializerSettings
                {
                    Formatting = Formatting.Indented,
                };
                var s = File.ReadAllText(FileName);
                return JsonConvert.DeserializeObject<Configuration>(s, jsonConfig) ?? new Configuration();
            }
            catch
            {
                return new Configuration();
            }
        }
    }

    internal sealed class UIState
    {
        public const string FileName = "uistate.json";

        public FormWindowState FormWindowState { get; set; } = FormWindowState.Normal;
        public int MainFormWidth { get; set; } = -1;
        public int MainFormHeight { get; set; } = -1;
        public int MainFormX { get; set; } = -1;
        public int MainFormY { get; set; } = -1;
        public int MainSlitterDistance { get; set; } = -1;

        public void Save()
        {
            var jsonConfig = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
            };
            var s = JsonConvert.SerializeObject(this, jsonConfig);
            File.WriteAllText(FileName, s);
        }

        public static UIState Load()
        {
            try
            {
                var s = File.ReadAllText(FileName);
                return JsonConvert.DeserializeObject<UIState>(s) ?? new UIState();
            }
            catch
            {
                return new UIState();
            }
        }
    }

    internal static class S3ConfigurationExtension
    {
        public const string FileName = "s3configuration.json";

        public static void Save(this S3Configuration configuraion, string password)
        {
            var c = configuraion.Clone();

            if (!string.IsNullOrEmpty(c.SecretAccessKey))
            {
                c.SecretAccessKey = CryptoUtils.EncryptString(c.SecretAccessKey, password);
            }

            var jsonConfig = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
            };

            var s = JsonConvert.SerializeObject(c, jsonConfig);
            File.WriteAllText(FileName, s);
        }

        public static S3Configuration Load(string password)
        {
            S3Configuration c;

            try
            {
                var s = File.ReadAllText(FileName);
                c = JsonConvert.DeserializeObject<S3Configuration>(s) ?? new S3Configuration();
            }
            catch
            {
                return new S3Configuration();
            }

            if (!string.IsNullOrEmpty(c.SecretAccessKey))
            {
                c.SecretAccessKey = CryptoUtils.DecryptString(c.SecretAccessKey, password);
            }

            return c;
        }

        public static bool Exists(this S3Configuration configuration)
        {
            return File.Exists(FileName);
        }

        public static bool Validate(this S3Configuration configuration)
        {
            return !string.IsNullOrEmpty(configuration.SecretAccessKey) &&
                !string.IsNullOrEmpty(configuration.ServiceURL) &&
                !string.IsNullOrEmpty(configuration.AccessKeyId) &&
                !string.IsNullOrEmpty(configuration.BucketName);
        }

        public static S3Configuration Clone(this S3Configuration configuraion)
        {
            return new S3Configuration()
            {
                SecretAccessKey = configuraion.SecretAccessKey,
                ServiceURL = configuraion.ServiceURL,
                AccessKeyId = configuraion.AccessKeyId,
                BucketName = configuraion.BucketName,
                Region = configuraion.Region,
            };
        }
    }
}
