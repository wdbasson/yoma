using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yoma.Core.Infrastructure.Database.Core.Entities;
using Yoma.Core.Infrastructure.Database.Lookups.Entities;

namespace Yoma.Core.Infrastructure.Database.Opportunity.Entities
{
    [Table("OpportunityCountries", Schema = "opportunity")]
    [Index(nameof(OrganizationId), nameof(LanguageId), IsUnique = true)]
    public class OpportunityCountries : BaseEntity<Guid>
    {
        [Required]
        [ForeignKey("OpportunityID")]
        public Guid OpportunityID { get; set; }
        public Opportunity Opportunity { get; set; }

        [Required]
        [ForeignKey("CountryId")]
        public Guid CountryId { get; set; }
        public Country Country { get; set; }

        [Required]
        public DateTimeOffset DateCreated { get; set; }
    }
}
