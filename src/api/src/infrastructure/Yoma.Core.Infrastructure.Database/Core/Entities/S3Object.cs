using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Yoma.Core.Infrastructure.Database.Core.Entities
{
    [Table("File", Schema = "object")]
    [Index(nameof(ObjectKey), IsUnique = true)]
    public class S3Object : BaseEntity<Guid>
    {
        [Column(TypeName = "varchar(125)")]
        public string ObjectKey { get; set; }

        [Required]
        public DateTimeOffset DateCreated { get; set; }
    }
}
