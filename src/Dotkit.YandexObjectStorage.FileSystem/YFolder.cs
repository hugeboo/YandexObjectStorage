using Amazon.S3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Dotkit.YandexObjectStorage.FileSystem
{
    public static class YFolder
    {
        public static async Task<IEnumerable<YFolderInfo>> GetFolders(YClient client, string bucketName, string? rootFolder = null)
        {
            try
            {
                return await client.GetFoldersAsync(bucketName, rootFolder);
            }
            catch (AmazonS3Exception ex)
            {
                throw new YException(ex, $"Bucket={bucketName} RootFolder={rootFolder}");
            }
        }

        public static async Task Create(YClient client, string bucketName, string name, string? rootFolder = null)
        {
            try
            {
                await client.CreateFolderAsync(bucketName, name, rootFolder);
            }
            catch (AmazonS3Exception ex)
            {
                throw new YException(ex, $"Bucket={bucketName} Name={name} RootFolder={rootFolder}");
            }
        }

        public static async Task Delete(YClient client, YFolderInfo folderInfo)
        {
            try
            {
                await client.DeleteFolderAsync(folderInfo);
            }
            catch (AmazonS3Exception ex)
            {
                throw new YException(ex, $"Bucket={folderInfo.BucketName} Key={folderInfo.Key}");
            }
        }
    }
}
