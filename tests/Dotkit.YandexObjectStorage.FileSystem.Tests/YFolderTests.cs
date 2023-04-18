using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dotkit.YandexObjectStorage.FileSystem.Tests
{
    public class YFolderTests
    {
        [Fact]
        public void CreateEnumerateDelete_YFolder_Successfully()
        {
            var config = new YConfig();
            var client = new YClient(config);

            YBucket.CreateAsync(client, "test-folder-bucket1-sesv").Wait();

            YFolder.CreateAsync(client, "test-folder-bucket1-sesv", "test-folder1-sesv").Wait();
            YFolder.CreateAsync(client, "test-folder-bucket1-sesv", "test-folder2-sesv", "test-folder1-sesv").Wait();

            var folders = YFolder.GetAllAsync(client, "test-folder-bucket1-sesv").Result;
            Assert.Contains(folders, f => f.Name == "test-folder1-sesv");

            var folders2 = YFolder.GetAllAsync(client, "test-folder-bucket1-sesv", "test-folder1-sesv").Result;
            Assert.Contains(folders2, f => f.Name == "test-folder2-sesv");

            YFolder.DeleteAsync(client, folders.First(f => f.Name == "test-folder1-sesv")).Wait();
            //TODO: Assert
     
            folders = YFolder.GetAllAsync(client, "test-folder-bucket1-sesv").Result;
            Assert.DoesNotContain(folders, f => f.Name == "test-folder1-sesv");

            folders2 = YFolder.GetAllAsync(client, "test-folder-bucket1-sesv", "test-folder1-sesv").Result;
            Assert.DoesNotContain(folders2, f => f.Name == "test-folder2-sesv");

            YBucket.DeleteAsync(client, "test-folder-bucket1-sesv").Wait();
        }
    }
}
