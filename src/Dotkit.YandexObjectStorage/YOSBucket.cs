using Amazon.S3.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dotkit.YandexObjectStorage
{
    public sealed class YOSBucket
    {
        public string Name { get; set; } = null!;
        public DateTime CreationDate { get; set; }

        public override string ToString()
        {
            return Name ?? "<NO_NAME>";
        }
    }
}
