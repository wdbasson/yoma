namespace Yoma.Core.Domain.Core.Models
{
    public class AWSSettings
    {
        public string S3Region { get; set; }

        public string S3AccessKey { get; set; }

        public string S3SecretKey { get; set; }

        public string S3BucketName { get; set; }

        public int S3URLExpirationInMinutes { get; set; }
    }
}
