using Amazon.S3;
using Amazon.S3.Model;
using System.Net;
using System.Security.AccessControl;

namespace Dotkit.YandexObjectStorage.FileSystem
{
    public sealed class YClient : IDisposable
    {
        public const string PATH_DELIMETER = "/";

        private readonly AmazonS3Client _s3Client;

        public YClient(YConfig config)
        {
            if (config == null) throw new ArgumentNullException(nameof(config));
            var s3Config = new AmazonS3Config
            {
                ServiceURL = config.ServiceURL
            };
            _s3Client = new AmazonS3Client(config.AccessKeyId, config.SecretAccessKey, s3Config);
        }

        public void Dispose()
        {
            _s3Client.Dispose();
        }

        public override string ToString()
        {
            return _s3Client.Config.ServiceURL;
        }

        internal async Task CreateBucketAsync(string bucketName)
        {
            var request = new PutBucketRequest
            {
                BucketName = bucketName,
                UseClientRegion = false,
            };

            var response = await _s3Client.PutBucketAsync(request).ConfigureAwait(false);

            if (response.HttpStatusCode != HttpStatusCode.OK)
                throw new YException($"Cannot create bucket '{bucketName}'", response.HttpStatusCode);
        }

        internal async Task DeleteBucketAsync(string bucketName)
        {
            var request = new DeleteBucketRequest
            {
                BucketName = bucketName,
                UseClientRegion = false,
            };

            var response = await _s3Client.DeleteBucketAsync(request).ConfigureAwait(false);

            if (response.HttpStatusCode != HttpStatusCode.NoContent)
                throw new YException($"Cannot delete bucket '{bucketName}'", response.HttpStatusCode);
        }

        internal async Task<IEnumerable<YBucketInfo>> GetBucketsAsync()
        {
            var response = await _s3Client.ListBucketsAsync().ConfigureAwait(false);

            if (response.HttpStatusCode != HttpStatusCode.OK)
                throw new YException($"Cannot get buckets", response.HttpStatusCode);

            return response.Buckets.Select(YBucketInfo.Create);
        }

        internal async Task<IEnumerable<YObjectInfo>> GetObjectsAsync(string bucketName, string? rootFolder)
        {
            var request = new ListObjectsV2Request { BucketName = bucketName, Prefix = rootFolder.AddEndPathDelimeter() };
            var response = await _s3Client.ListObjectsV2Async(request).ConfigureAwait(false);

            if (response.HttpStatusCode != HttpStatusCode.OK)
                throw new YException($"Cannot get objects from '{bucketName}://{rootFolder}'", response.HttpStatusCode);

            return response.S3Objects.Select(YObjectInfo.Create);
        }

        internal async Task<IEnumerable<YFolderInfo>> GetFoldersAsync(string bucketName, string? rootFolder)
        {
            var request = new ListObjectsV2Request { BucketName = bucketName, Prefix = rootFolder.AddEndPathDelimeter(), Delimiter = PATH_DELIMETER };
            var response = await _s3Client.ListObjectsV2Async(request).ConfigureAwait(false);

            if (response.HttpStatusCode != HttpStatusCode.OK)
                throw new YException($"Cannot get folders from '{bucketName}://{rootFolder}'", response.HttpStatusCode);

            return response.CommonPrefixes.Select(p => new YFolderInfo { BucketName = bucketName, Key = p});
        }

        internal async Task CreateFolderAsync(string bucketName, string name, string? rootFolder)
        {
            var key = Path.Combine(rootFolder ?? string.Empty, name).NormalizePathDelimeter().AddEndPathDelimeter();

            var request = new PutObjectRequest { BucketName = bucketName, Key = key };
            var response = await _s3Client.PutObjectAsync(request).ConfigureAwait(false);

            if (response.HttpStatusCode != HttpStatusCode.OK)
                throw new YException($"Cannot create folder '{bucketName}://{key}'", response.HttpStatusCode);
        }

        internal async Task DeleteFolderAsync(YFolderInfo folderInfo)
        {
            var objects = await GetObjectsAsync(folderInfo.BucketName!, folderInfo.Folder);
            foreach(var obj in objects)
            {
                var response = await _s3Client.DeleteObjectAsync(folderInfo.BucketName, obj.Key).ConfigureAwait(false);

                if (response.HttpStatusCode != HttpStatusCode.NoContent)
                    throw new YException($"Cannot delete object '{obj.FullPath}'", response.HttpStatusCode);
            }
        }
    }
}