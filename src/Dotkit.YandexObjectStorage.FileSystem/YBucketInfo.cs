using Amazon.S3.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dotkit.YandexObjectStorage.FileSystem
{
    public sealed class YBucketInfo
    {
        public string Name { get; private set; } = null!;
        public DateTime CreationDate { get; set; }

        private YBucketInfo() { }

        public override string ToString()
        {
            return Name ?? "<NO_NAME>";
        }

        public override int GetHashCode()
        {
            return Name?.GetHashCode() ?? base.GetHashCode();
        }

        public override bool Equals(object? obj)
        {
            if (obj is not YBucketInfo other) return false;
            return Name == other.Name;
        }

        internal static YBucketInfo Create(S3Bucket b)
        {
            return new YBucketInfo 
            {
                Name = b.BucketName, 
                CreationDate = b.CreationDate 
            };
        }
    }
}
