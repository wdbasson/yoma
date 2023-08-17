using Yoma.Core.Domain.Opportunity.Models;

namespace Yoma.Core.Domain.Opportunity.Interfaces
{
    public interface IOpportunityService
    {
        Models.Opportunity GetById(Guid id, bool includeChildren);

        OpportunityInfo GetInfoById(Guid id, bool includeChildren);

        Models.Opportunity? GetByTitleOrNull(string title, bool includeChildItems);

        OpportunityInfo? GetInfoByTitleOrNull(string title, bool includeChildItems);

        OpportunitySearchResultsInfo SearchInfo(OpportunitySearchFilterInfo filter);

        OpportunitySearchResults Search(OpportunitySearchFilter filter);

        Task<Models.Opportunity> Upsert(OpportunityRequest request, string? username);

        Task IncrementParticipantCount(Guid id, int increment = 1);

        Task UpdateStatus(Guid id, Status status, string? username);

        Task ProcessExpiration();

        Task ExpirationNotifications();

        Task AssignCategories(Guid id, List<Guid> categoryIds);

        Task DeleteCategories(Guid id, List<Guid> categoryIds);

        Task AssignCountries(Guid id, List<Guid> countryIds);

        Task DeleteCountries(Guid id, List<Guid> countryIds);

        Task AssignLanguages(Guid id, List<Guid> languageIds);

        Task DeleteLanguages(Guid id, List<Guid> languageIds);

        Task AssignSkills(Guid id, List<Guid> skillIds);

        Task DeleteSkills(Guid id, List<Guid> skillIds);
    }
}
