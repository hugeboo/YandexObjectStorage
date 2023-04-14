using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dotkit.YandexObjectStorage
{
    public sealed class YOSFolder
    {
        public string BucketName { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Prefix { get; set; }

        public override string ToString()
        {
            return Name ?? "<NO_NAME>";
        }
    }
}
