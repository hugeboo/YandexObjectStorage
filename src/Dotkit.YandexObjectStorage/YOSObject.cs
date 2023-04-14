using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dotkit.YandexObjectStorage
{
    public sealed class YOSObject
    {
        public string BucketName { get; set; } = null!;
        public string? Name => Path.GetFileName(Key);
        public string? Prefix => Path.GetDirectoryName(Key) + YOSClient.PATH_DELIMETER;
        public string? Key { get; set; }

        public override string ToString()
        {
            return Key ?? "<NO_KEY>";
        }
    }
}
