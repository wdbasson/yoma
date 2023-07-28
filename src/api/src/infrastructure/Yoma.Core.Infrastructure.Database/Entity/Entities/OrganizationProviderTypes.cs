using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Yoma.Core.Infrastructure.Database.Core.Entities;
using Yoma.Core.Infrastructure.Database.Lookups.Entities;

namespace Yoma.Core.Infrastructure.Database.Entity.Entities
{
    [Table("OrganizationProviderTypes", Schema = "entity")]
    [Index(nameof(OrganizationId), nameof(ProviderTypeId), IsUnique = true)]
    public class OrganizationProviderType : BaseEntity<Guid>
    {
        [Required]
        [ForeignKey("OrganizationId")]
        public Guid OrganizationId { get; set; }
        public Organization Organization { get; set; }

        [Required]
        [ForeignKey("ProviderTypeId")]
        public Guid ProviderTypeId { get; set; }
        public ProviderType ProviderType { get; set; }

        [Required]
        public DateTimeOffset DateCreated { get; set; }
    }
}
