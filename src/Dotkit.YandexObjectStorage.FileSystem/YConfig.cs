using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dotkit.YandexObjectStorage.FileSystem
{
    public sealed class YConfig
    {
        public string ServiceURL { get; set; } = "https://s3.yandexcloud.net";
        public string AccessKeyId { get; set; } = "YCAJEIzcBfUuI2bK_G3l4k4br";
        public string SecretAccessKey { get; set; } = "YCNOYDJLZkFf292p-BZMrHLxsnuWzE2JCWCXlA1N";
        public string Region { get; set; } = "us-east-1";// "ru-central1"; // us-east-1

        public override string ToString()
        {
            return $"{ServiceURL} - {AccessKeyId}";
        }
    }
}
