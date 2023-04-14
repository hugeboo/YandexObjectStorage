using Amazon.S3;
using Amazon.S3.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Dotkit.YandexObjectStorage
{
    public sealed class YOSClient : IDisposable
    {
        public const string PATH_DELIMETER = "/";

        private readonly AmazonS3Client _s3Client;

        public YOSClient(YOSConfig config)
        {
            if (config == null) throw new ArgumentNullException(nameof(config));
            var s3Config = new AmazonS3Config
            {
                ServiceURL = config.ServiceURL,
                //RegionEndpoint = Amazon.RegionEndpoint.GetBySystemName(config.Region)
            };
            _s3Client = new AmazonS3Client(config.AccessKeyId, config.SecretAccessKey, s3Config);
        }

        public void Dispose()
        {
            _s3Client.Dispose();
        }

        public async Task<List<YOSBucket>> GetBuckets()
        {
            var response = await _s3Client.ListBucketsAsync().ConfigureAwait(false);
            return response.Buckets.Select(b => new YOSBucket { Name = b.BucketName, CreationDate = b.CreationDate }).ToList();
        }

        public async Task<List<YOSFolder>> GetFolders(string bucketName, string? prefix = null)
        {
            var request = new ListObjectsV2Request { BucketName = bucketName, Prefix = prefix, Delimiter = PATH_DELIMETER };
            var response = await _s3Client.ListObjectsV2Async(request).ConfigureAwait(false);
            return response.CommonPrefixes
                .Select(p => new YOSFolder { BucketName = bucketName, Name = p.Replace(PATH_DELIMETER, ""), Prefix = prefix })
                .ToList();
        }

        public async Task<List<YOSFolder>> GetFolders(YOSFolder rootFolder)
        {
            var prefix = Path.Combine(rootFolder.Prefix ?? "", rootFolder.Name) + PATH_DELIMETER;
            var request = new ListObjectsV2Request { BucketName = rootFolder.BucketName, Prefix = prefix, Delimiter = PATH_DELIMETER };
            var response = await _s3Client.ListObjectsV2Async(request).ConfigureAwait(false);
            return response.CommonPrefixes
                .Select(p => new YOSFolder { BucketName = rootFolder.BucketName, Name = p.Replace(PATH_DELIMETER, ""), Prefix = prefix })
                .ToList();
        }

        public async Task<List<YOSObject>> GetObjects(string bucketName, string prefix)
        {
            var request = new ListObjectsV2Request { BucketName = bucketName, Prefix = prefix };
            var response = await _s3Client.ListObjectsV2Async(request).ConfigureAwait(false);
            return response.S3Objects
                .Where(o => o.Key != prefix)
                .Select(o => new YOSObject { BucketName = bucketName, Key = o.Key })
                .ToList();
        }

        public async Task<string> DownloadObject(string bucketName, string objectKey, string rootFolder)
        {
            var request = new GetObjectRequest { BucketName = bucketName, Key = objectKey, };
            using GetObjectResponse response = await _s3Client.GetObjectAsync(request).ConfigureAwait(false);
            if (response.HttpStatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new InvalidOperationException($"Cannot download object. HttpStatusCode={response.HttpStatusCode}");
            }
            var filePath = Path.Combine(rootFolder, bucketName);
            filePath = Path.Combine(filePath, objectKey);
            await response.WriteResponseStreamToFileAsync(filePath, false, CancellationToken.None).ConfigureAwait(false);
            return filePath;
        }

        public override string ToString()
        {
            return _s3Client.Config.ServiceURL;
        }
    }
}
