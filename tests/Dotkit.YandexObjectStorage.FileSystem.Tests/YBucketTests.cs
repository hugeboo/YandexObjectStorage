namespace Dotkit.YandexObjectStorage.FileSystem.Tests
{
    public class YBucketTests
    {
        [Fact]
        public void CreateEnumerateDelete_YBucket_Successfully()
        {
            var config = new YConfig();
            var client = new YClient(config);

            YBucket.CreateAsync(client, "test-bucket-bucket1-sesv").Wait();
            YBucket.CreateAsync(client, "test-bucket-bucket2-sesv").Wait();
            YBucket.CreateAsync(client, "test-bucket-bucket3-sesv").Wait();

            var buckets = YBucket.GetAllAsync(client).Result;

            Assert.NotNull(buckets);
            Assert.Contains(buckets, b => b.Name == "test-bucket-bucket1-sesv");
            Assert.Contains(buckets, b => b.Name == "test-bucket-bucket2-sesv");
            Assert.Contains(buckets, b => b.Name == "test-bucket-bucket3-sesv");

            YBucket.DeleteAsync(client, "test-bucket-bucket1-sesv").Wait();
            YBucket.DeleteAsync(client, "test-bucket-bucket2-sesv").Wait();
            YBucket.DeleteAsync(client, "test-bucket-bucket3-sesv").Wait();

            var buckets2 = YBucket.GetAllAsync(client).Result;

            Assert.NotNull(buckets2);
            Assert.DoesNotContain(buckets2, b => b.Name == "test-bucket-bucket1-sesv");
            Assert.DoesNotContain(buckets2, b => b.Name == "test-bucket-bucket2-sesv");
            Assert.DoesNotContain(buckets2, b => b.Name == "test-bucket-bucket3-sesv");
        }
    }
}