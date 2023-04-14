using Amazon.S3;
using Amazon.S3.Model;
using System.Net;
using System.Security.AccessControl;
using System.Xml.Linq;

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

        #region Buckets

        internal async Task CreateBucketAsync(string bucketName)
        {
            try
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
            catch (AmazonS3Exception ex)
            {
                throw new YException(ex, $"Bucket={bucketName}");
            }
        }

        internal async Task DeleteBucketAsync(string bucketName)
        {
            try
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
            catch (AmazonS3Exception ex)
            {
                throw new YException(ex, $"Bucket={bucketName}");
            }
        }

        internal async Task<List<YBucketInfo>> GetBucketsAsync()
        {
            try
            {
                var response = await _s3Client.ListBucketsAsync().ConfigureAwait(false);

                if (response.HttpStatusCode != HttpStatusCode.OK)
                    throw new YException($"Cannot get buckets", response.HttpStatusCode);

                return response.Buckets.Select(YBucketInfo.Create).ToList();
            }
            catch (AmazonS3Exception ex)
            {
                throw new YException(ex);
            }
        }

        #endregion

        #region Objects

        internal async Task<List<YObjectInfo>> GetObjectsAsync(string bucketName, string? baseFolder = null)
        {
            try
            {
                var request = new ListObjectsV2Request { BucketName = bucketName, Prefix = baseFolder.AddEndPathDelimeter() };
                var response = await _s3Client.ListObjectsV2Async(request).ConfigureAwait(false);

                if (response.HttpStatusCode != HttpStatusCode.OK)
                    throw new YException($"Cannot get objects from '{bucketName}://{baseFolder}'", response.HttpStatusCode);

                return response.S3Objects.Select(YObjectInfo.Create).ToList();
            }
            catch (AmazonS3Exception ex)
            {
                throw new YException(ex);
            }
        }

        #endregion

        #region Folders

        internal async Task<List<YFolderInfo>> GetFoldersAsync(string bucketName, string? baseFolder = null)
        {
            try
            {
                var request = new ListObjectsV2Request { BucketName = bucketName, Prefix = baseFolder.AddEndPathDelimeter(), Delimiter = PATH_DELIMETER };
                var response = await _s3Client.ListObjectsV2Async(request).ConfigureAwait(false);

                if (response.HttpStatusCode != HttpStatusCode.OK)
                    throw new YException($"Cannot get folders from '{bucketName}://{baseFolder}'", response.HttpStatusCode);

                return response.CommonPrefixes.Select(p => YFolderInfo.Create(bucketName, p)).ToList();
            }
            catch (AmazonS3Exception ex)
            {
                throw new YException(ex, $"Bucket={bucketName} BaseFolder={baseFolder}");
            }
        }

        internal async Task CreateFolderAsync(string bucketName, string name, string? baseFolder = null)
        {
            try
            {
                var key = Path.Combine(baseFolder ?? string.Empty, name).NormalizePathDelimeter().AddEndPathDelimeter();

                var request = new PutObjectRequest { BucketName = bucketName, Key = key };
                var response = await _s3Client.PutObjectAsync(request).ConfigureAwait(false);

                if (response.HttpStatusCode != HttpStatusCode.OK)
                    throw new YException($"Cannot create folder '{bucketName}://{key}'", response.HttpStatusCode);
            }
            catch (AmazonS3Exception ex)
            {
                throw new YException(ex, $"Bucket={bucketName} Name={name} BaseFolder={baseFolder}");
            }
        }

        internal async Task DeleteFolderAsync(YFolderInfo folderInfo)
        {
            if (folderInfo == null) throw new ArgumentNullException(nameof(folderInfo));
            try
            {
                var objects = await GetObjectsAsync(folderInfo.BucketName, folderInfo.Folder);
                foreach (var obj in objects)
                {
                    var response = await _s3Client.DeleteObjectAsync(folderInfo.BucketName, obj.Key).ConfigureAwait(false);
                    if (response.HttpStatusCode != HttpStatusCode.NoContent)
                        throw new YException($"Cannot delete object '{obj.FullPath}'", response.HttpStatusCode);
                }
                var response2 = await _s3Client.DeleteObjectAsync(folderInfo.BucketName, folderInfo.Key).ConfigureAwait(false);
                if (response2.HttpStatusCode != HttpStatusCode.NoContent)
                    throw new YException($"Cannot delete folder '{folderInfo.BucketName}://{folderInfo.Key}'", response2.HttpStatusCode);
            }
            catch (AmazonS3Exception ex)
            {
                throw new YException(ex, $"Bucket={folderInfo.BucketName} Key={folderInfo.Key}");
            }
        }

        #endregion
    }
}