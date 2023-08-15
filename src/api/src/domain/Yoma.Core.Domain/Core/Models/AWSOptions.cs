namespace Yoma.Core.Domain.Core.Models
{
    public class AWSOptions
    {
        public const string Section = "AWS";

        public string S3Region { get; set; }

        public string S3AccessKey { get; set; }

        public string S3SecretKey { get; set; }

        public string S3BucketName { get; set; }

        public int S3URLExpirationInMinutes { get; set; }
    }
}
