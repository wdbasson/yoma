using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Yoma.Core.Domain.Entity.Models;
using Yoma.Core.Infrastructure.Database.Core.Entities;
using Yoma.Core.Infrastructure.Database.Lookups.Entities;

namespace Yoma.Core.Infrastructure.Database.Opportunity.Entities
{
    [Table("Opportunity", Schema = "opportunity")]
    public class Opportunity : BaseEntity<Guid>
    {
        [Required]
        [Column(TypeName = "varchar(255)")]
        public string Title { get; set; }

        [Required]
        [Column(TypeName = "varchar(MAX)")]
        public string Description { get; set; }

        [Required]
        [ForeignKey("TypeId")]
        public Guid TypeId { get; set; }
        public OpportunityType Type { get; set; }

        [Column(TypeName = "varchar(MAX)")]
        public string? Instructions { get; set; }

        [Column(TypeName = "varchar(2048)")]
        public string? URL { get; set; }

        [Column(TypeName = "decimal(8,2)")]
        public decimal? ZltoReward { get; set; }

        [Column(TypeName = "decimal(8,2)")]
        public decimal? YomaReward { get; set; }


        [Required]
        [ForeignKey("OrganizationId")]
        public Guid OrganizationId { get; set; }
        public Organization Organization { get; set; }


        [Required]
        public DateTimeOffset DateCreated { get; set; }

        [Required]
        public DateTimeOffset DateModified { get; set; }
    }
}
