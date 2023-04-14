using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dotkit.YandexObjectStorage.FileSystem
{
    public sealed class YFolderInfo
    {
        public string BucketName { get; private set; }
        public string Key { get; private set; }
        public string FullPath => $"{BucketName}://{Key}";
        public string Folder => Path.GetDirectoryName(Key.TrimEndPathDelimeter()).NormalizePathDelimeter() ?? string.Empty;
        public string Name => Path.GetFileName(Key.TrimEndPathDelimeter()) ?? string.Empty;

        private YFolderInfo() { }

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
            if (obj is not YFolderInfo other) return false;
            return Key == other.Key;
        }

        internal static YFolderInfo Create(string bucketName, string key)
        {
            return new YFolderInfo { BucketName = bucketName, Key = key };
        }
    }
}
