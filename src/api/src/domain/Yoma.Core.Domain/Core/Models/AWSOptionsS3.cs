using Amazon.Extensions.NETCore.Setup;

namespace Yoma.Core.Domain.Core.Models
{
    public class AWSOptionsS3 : AWSOptions
    {
        public string BucketName { get; set; }

        public int? URLExpirationInMinutes { get; set; }
    }
}
