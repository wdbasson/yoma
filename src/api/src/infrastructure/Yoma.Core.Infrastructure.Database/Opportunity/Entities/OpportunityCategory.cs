using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yoma.Core.Domain.Entity.Models;
using Yoma.Core.Infrastructure.Database.Core.Entities;
using Yoma.Core.Infrastructure.Database.Lookups.Entities;

namespace Yoma.Core.Infrastructure.Database.Opportunity.Entities
{
    [Table("OpportunityCategories", Schema = "opportunity")]
    [Index(nameof(OpportunityID), nameof(CategoryId), IsUnique = true)]
    internal class OpportunityCategory : BaseEntity<Guid>
    {
        [Required]
        [ForeignKey("OpportunityID")]
        public Guid OpportunityID { get; set; }
        public Opportunity Opportunity { get; set; }

        [Required]
        [ForeignKey("CategoryId")]
        public Guid CategoryId { get; set; }
        public OpportunityCategory Category { get; set; }

        [Required]
        public DateTimeOffset DateCreated { get; set; }
    }
}
