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
        public static async Task<IEnumerable<YObjectInfo>> GetObjects(YClient client, string bucketName, string? rootFolder = null)
        {
            try
            {
                return await client.GetObjectsAsync(bucketName, rootFolder);
            }
            catch (AmazonS3Exception ex)
            {
                throw new YException(ex);
            }
        }
    }
}
