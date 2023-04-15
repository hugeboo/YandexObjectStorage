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
        public static async Task<List<YFolderInfo>> GetAllAsync(YClient client, string bucketName, string? baseFolder = null)
        {
            return await client.GetFoldersAsync(bucketName, baseFolder).ConfigureAwait(false);
        }

        public static async Task CreateAsync(YClient client, string bucketName, string name, string? baseFolder = null)
        {
            await client.CreateFolderAsync(bucketName, name, baseFolder).ConfigureAwait(false);
        }

        public static async Task DeleteAsync(YClient client, YFolderInfo folderInfo)
        {
            await client.DeleteFolderAsync(folderInfo).ConfigureAwait(false);
        }
        
        public static async Task DeleteAsync(YClient client, IEnumerable<YFolderInfo> folderInfos)
        {
            await client.DeleteFoldersAsync(folderInfos).ConfigureAwait(false);
        }


    }
}
