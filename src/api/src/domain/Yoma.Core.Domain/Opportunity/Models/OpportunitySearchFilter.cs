using Yoma.Core.Domain.Core.Models;

namespace Yoma.Core.Domain.Opportunity.Models
{
    public class OpportunitySearchFilter : PaginationFilter
    {
        public DateTimeOffset? StartDate { get; set; }

        public DateTimeOffset? EndDate { get; set; }

        public Guid? OrganizationId { get; set; }

        public List<Guid>? TypeIds { get; set; }

        public List<Guid>? CategoryIds { get; set; }

        public List<Guid>? LanguageIds { get; set; }

        public List<Guid>? CountryIds { get; set; }

        public List<Guid>? StatusIds { get; set; }

        public string? ValueContains { get; set; }
    }
}
