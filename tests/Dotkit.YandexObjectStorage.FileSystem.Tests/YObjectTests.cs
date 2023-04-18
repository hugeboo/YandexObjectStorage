using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dotkit.YandexObjectStorage.FileSystem.Tests
{
    public class YObjectTests
    {
        [Fact]
        public void CreateEnumerateDelete_YObject_Successfully()
        {
            var config = new YConfig();
            var client = new YClient(config);

            var objects = YObject.GetAllAsync(client, "test1-sesv").Result;
            Assert.Equal(0, objects.Count);
        }
    }
}
