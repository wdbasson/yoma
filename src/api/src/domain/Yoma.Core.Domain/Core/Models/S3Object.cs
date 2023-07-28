namespace Yoma.Core.Domain.Core.Models
{
    public class S3Object
    {
        public Guid Id { get; set; }

        public string ObjectKey { get; set; }

        public DateTimeOffset DateCreated { get; set; }
    }
}
