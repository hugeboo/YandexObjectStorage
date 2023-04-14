using Amazon.S3.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dotkit.YandexObjectStorage.FileSystem
{
    public sealed class YObjectInfo
    {
        public string? BucketName { get; set; }
        public string? Key { get; set; }
        public string FullPath => $"{BucketName}://{Key}";
        public bool IsFolder => Key?.EndsWith(YClient.PATH_DELIMETER) ?? false;
        public string Folder => Path.GetDirectoryName(Key.TrimEndPathDelimeter()).NormalizePathDelimeter() ?? string.Empty;
        public string Name => Path.GetFileName(Key.TrimEndPathDelimeter()) ?? string.Empty;

        public string? ETag { get; set; }
        public DateTime LastModified { get; set; }
        public string? OwnerId { get; set; }
        public long Size { get; set; }
        public string? StorageClass { get; set; }

        public override string ToString()
        {
            return Key ?? "<NO_KEY>";
        }

        public override int GetHashCode()
        {
            return Key?.GetHashCode() ?? base.GetHashCode();
        }

        public override bool Equals(object? obj)
        {
            if (obj is not YObjectInfo other) return false;
            return Key == other.Key;
        }

        internal static YObjectInfo Create(S3Object obj)
        {
            return new YObjectInfo
            {
                BucketName = obj.BucketName,
                Key = obj.Key,
                ETag = obj.ETag,
                LastModified = obj.LastModified,
                OwnerId = obj.Owner?.Id,
                Size = obj.Size,
                StorageClass = obj.StorageClass.Value
            };
        }
    }
}
