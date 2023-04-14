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
        public static async Task Create(YClient client, string bucketName)
        {
            try
            {
                await client.CreateBucketAsync(bucketName);
            }
            catch (AmazonS3Exception ex)
            {
                throw new YException(ex, $"Bucket={bucketName}");
            }
        }

        public static async Task Delete(YClient client, string bucketName)
        {
            try
            {
                await client.DeleteBucketAsync(bucketName);
            }
            catch (AmazonS3Exception ex)
            {
                throw new YException(ex, $"Bucket={bucketName}");
            }
        }

        public static async Task<IEnumerable<YBucketInfo>> GetBuckets(YClient client)
        {
            try
            {
                return await client.GetBucketsAsync();
            }
            catch (AmazonS3Exception ex)
            {
                throw new YException(ex);
            }
        }
    }
}
