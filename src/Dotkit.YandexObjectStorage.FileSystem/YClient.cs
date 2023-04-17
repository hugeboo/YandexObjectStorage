using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using System.IO;
using System.Linq;
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
                var request = new ListObjectsV2Request { BucketName = bucketName, Prefix = baseFolder.AddEndPathDelimeter(), Delimiter = PATH_DELIMETER };
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

        internal async Task UploadFileAsync(YFolderInfo folder, string filePath)
        {
            try 
            {
                var file = new FileInfo(filePath);

                var fileName = Path.GetFileName(filePath);
                var key = Path.Combine(folder.Key, fileName);

                var fileTransferUtility = new TransferUtility(_s3Client);

                var fileTransferUtilityRequest = new TransferUtilityUploadRequest
                {
                    BucketName = folder.BucketName,
                    FilePath = filePath,
                    StorageClass = S3StorageClass.StandardInfrequentAccess,
                    PartSize = 6291456, // 6 MB.
                    Key = key,
                    CannedACL = S3CannedACL.PublicRead
                };
                //fileTransferUtilityRequest.Metadata.Add("param1", "Value1");
                //fileTransferUtilityRequest.Metadata.Add("param2", "Value2");

                await fileTransferUtility.UploadAsync(fileTransferUtilityRequest);

                // Вариант 2:
                //var request = new PutObjectRequest()
                //{
                //    InputStream = file.OpenRead(),
                //    BucketName = folder.BucketName,
                //    Key = key
                //};
                //var response = await _s3Client.PutObjectAsync(request);
                //if (response.HttpStatusCode != HttpStatusCode.OK)
                //    throw new YException($"Cannot upload file '{filePath}'", response.HttpStatusCode);
            }
            catch (AmazonS3Exception ex)
            {
                throw new YException(ex, $"Bucket={folder.BucketName} Folder={folder.Key} FilePath={filePath}");
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
                var objects = await GetObjectsAsync(folderInfo.BucketName, folderInfo.Key);

                var keys = objects.Select(it => new KeyVersion { Key = it.Key }).ToList();

                var request = new DeleteObjectsRequest { BucketName = folderInfo.BucketName, Objects = keys };

                var response = await _s3Client.DeleteObjectsAsync(request).ConfigureAwait(false);
                if (response.HttpStatusCode != HttpStatusCode.OK)
                    throw new YException($"Error when deleting '{folderInfo.FullPath}'", response.HttpStatusCode);
            }
            catch (AmazonS3Exception ex)
            {
                throw new YException(ex, $"Bucket={folderInfo.BucketName} Key={folderInfo.Key}");
            }
        }
        
        internal async Task DeleteFoldersAsync(IEnumerable<YFolderInfo> folderInfos)
        {
            if (folderInfos == null) throw new ArgumentNullException(nameof(folderInfos));
            try
            {
                var objects = new List<YObjectInfo>();
                string? bucketName = null;
                foreach(var fi in folderInfos)
                {
                    if (bucketName == null) { bucketName = fi.BucketName; }
                    else
                    {
                        if (fi.BucketName != bucketName)
                            throw new YException("Cannot multiple deleting from different buckets");
                    }
                    var lst = await GetObjectsAsync(fi.BucketName, fi.Key);
                    objects.AddRange(lst);
                }

                var keys = objects.Select(it => new KeyVersion { Key = it.Key }).ToList();

                var request = new DeleteObjectsRequest { BucketName = bucketName, Objects = keys };

                var response = await _s3Client.DeleteObjectsAsync(request).ConfigureAwait(false);
                if (response.HttpStatusCode != HttpStatusCode.OK)
                    throw new YException("Error when multiple deleting", response.HttpStatusCode);
            }
            catch (AmazonS3Exception ex)
            {
                throw new YException(ex);
            }
        }

        #endregion
    }
}