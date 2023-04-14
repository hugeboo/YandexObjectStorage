using Amazon.S3;
using Amazon.S3.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dotkit.YandexObjectStorage.FileSystem
{
    public static class YBucket
    {
        public static async Task CreateAsync(YClient client, string bucketName)
        {
            await client.CreateBucketAsync(bucketName);
        }

        public static async Task DeleteAsync(YClient client, string bucketName)
        {
            await client.DeleteBucketAsync(bucketName);
        }

        public static async Task<List<YBucketInfo>> GetAllAsync(YClient client)
        {
            return await client.GetBucketsAsync();
        }
    }
}
