using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using Yoma.Core.Infrastructure.Database.Core.Entities;

namespace Yoma.Core.Infrastructure.Database.Lookups.Entities
{
    [Table("Country", Schema = "lookup")]
    [Index(nameof(Name), IsUnique = true)]
    public class Language : BaseEntity<Guid>
    {
        [Column(TypeName = "varchar(125)")]
        public string Name { get; set; }

        [Column(TypeName = "varchar(2)")]
        public string CodeAlpha2 { get; set; }
    }
}
