using Amazon.S3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dotkit.YandexObjectStorage.FileSystem
{
    public static class YObject
    {
        public static async Task<List<YObjectInfo>> GetAllAsync(YClient client, string bucketName, string? rootFolder = null)
        {
            return await client.GetObjectsAsync(bucketName, rootFolder);
        }
    }
}
