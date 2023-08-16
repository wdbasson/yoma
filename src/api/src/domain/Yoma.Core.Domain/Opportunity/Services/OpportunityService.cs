using Yoma.Core.Domain.Core.Extensions;
using Yoma.Core.Domain.Core.Interfaces;
using Yoma.Core.Domain.Entity.Models;
using Yoma.Core.Domain.Lookups.Models;
using Yoma.Core.Domain.Opportunity.Helpers;
using Yoma.Core.Domain.Opportunity.Interfaces;
using Yoma.Core.Domain.Opportunity.Models;

namespace Yoma.Core.Domain.Opportunity.Services
{
    public class OpportunityService : IOpportunityService
    {
        #region Class Variables
        private readonly IRepositoryValueContainsWithNavigation<Models.Opportunity> _opportunityRepository;
        private readonly IRepositoryValueContainsWithNavigation<Organization> _organizationRepository;
        private readonly IRepositoryValueContains<Models.Lookups.OpportunityType> _opportunityTypeRepository;
        private readonly IRepositoryValueContains<Models.Lookups.OpportunityCategory> _opportunityCategoryLookupRepository;
        private readonly IRepositoryBatchedWithValueContains<Skill> _skillRepository;
        private readonly IRepository<OpportunityCategory> _opportunityCategoryRepository;
        private readonly IRepository<OpportunityCountry> _opportunityCountryRepository;
        private readonly IRepository<OpportunityLanguage> _opportunityLanguageRepository;
        private readonly IRepository<OpportunitySkill> _opportunitySkillRepository;
        #endregion

        #region Constructor
        public OpportunityService(IRepositoryValueContainsWithNavigation<Models.Opportunity> opportunityRepository,
            IRepositoryValueContainsWithNavigation<Organization> organizationRepository,
            IRepositoryValueContains<Models.Lookups.OpportunityType> opportunityTypeRepository,
            IRepositoryValueContains<Models.Lookups.OpportunityCategory> opportunityCategoryLookupRepository,
            IRepositoryBatchedWithValueContains<Skill> skillRepository,
            IRepository<OpportunityCategory> opportunityCategoryRepository,
            IRepository<OpportunityCountry> opportunityCountryRepository,
            IRepository<OpportunityLanguage> opportunityLanguageRepository,
            IRepository<OpportunitySkill> opportunitySkillRepository)
        {
            _opportunityRepository = opportunityRepository;
            _organizationRepository = organizationRepository;
            _opportunityTypeRepository = opportunityTypeRepository;
            _opportunityCategoryLookupRepository = opportunityCategoryLookupRepository;
            _skillRepository = skillRepository;
            _opportunityCategoryRepository = opportunityCategoryRepository;
            _opportunityCountryRepository = opportunityCountryRepository;
            _opportunityLanguageRepository = opportunityLanguageRepository;
            _opportunitySkillRepository = opportunitySkillRepository;
        }
        #endregion

        #region Public Members
        public Models.Opportunity GetById(Guid id, bool includeChildren)
        {
            if (id == Guid.Empty)
                throw new ArgumentNullException(nameof(id));

            var result = _opportunityRepository.Query(includeChildren).SingleOrDefault(o => o.Id == id)
                ?? throw new ArgumentOutOfRangeException(nameof(id), $"{nameof(Models.Opportunity)} with id '{id}' does not exist");

            return result;
        }

        public OpportunityInfo GetInfoById(Guid id, bool includeChildren)
        {
            var result = GetById(id, includeChildren);

            return result.ToOpportunityInfo();
        }

        public OpportunitySearchResults Search(OpportunitySearchFilter filter)
        {
            if (filter == null)
                throw new ArgumentNullException(nameof(filter));

            //TODO: model validator

            var query = _opportunityRepository.Query(true);

            //date range
            if (filter.StartDate.HasValue)
                query = query.Where(o => o.DateCreated >= filter.StartDate.Value);

            if (filter.EndDate.HasValue)
                query = query.Where(o => o.DateCreated <= filter.EndDate.Value);

            //organization (explicitly specified)
            var filterByOrganization = false;
            var organizationIds = new List<Guid>();
            if (filter.OrganizationId.HasValue)
            {
                filterByOrganization = true;
                organizationIds.Add(filter.OrganizationId.Value);
            }

            //types (explicitly specified)
            var filterByType = false;
            var typeIds = new List<Guid>();
            if (filter.TypeIds != null)
            {
                filter.TypeIds = filter.TypeIds.Distinct().ToList();
                if (filter.TypeIds.Any())
                {
                    filterByType = true;
                    typeIds.AddRange(filter.TypeIds);
                }
            }

            //categories (explicitly specified)
            var filterByCategories = false;
            var categoryIds = new List<Guid>();
            if (filter.CategoryIds != null)
            {
                filter.CategoryIds = filter.CategoryIds.Distinct().ToList();
                if (filter.CategoryIds.Any())
                {
                    filterByCategories = true;
                    categoryIds.AddRange(filter.CategoryIds);
                }
            }

            //languages
            if (filter.LanguageIds != null)
            {
                filter.LanguageIds = filter.LanguageIds.Distinct().ToList();
                if (filter.LanguageIds.Any())
                {
                    var matchedOpportunityIds = _opportunityLanguageRepository.Query().Where(o => filter.LanguageIds.Contains(o.LanguageId)).Select(o => o.OpportunityId).ToList();
                    query = query.Where(o => matchedOpportunityIds.Contains(o.Id));
                }
            }

            //countries
            if (filter.CountryIds != null)
            {
                filter.CountryIds = filter.CountryIds.Distinct().ToList();
                if (filter.CountryIds.Any())
                {
                    var matchedOpportunityIds = _opportunityCountryRepository.Query().Where(o => filter.CountryIds.Contains(o.CountryId)).Select(o => o.OpportunityId).ToList();
                    query = query.Where(o => matchedOpportunityIds.Contains(o.Id));
                }
            }

            //statuses
            if (filter.StatusIds != null)
            {
                filter.StatusIds = filter.StatusIds.Distinct().ToList();
                if (filter.StatusIds.Any())
                    query = query.Where(o => filter.StatusIds.Contains(o.StatusId));
            }

            //valueContains (includes organizations, types, categories, opportunities and skills)
            if (!string.IsNullOrEmpty(filter.ValueContains))
            {
                var predicate = PredicateBuilder.False<Models.Opportunity>();

                //organizations
                var matchedOrganizationIds = _organizationRepository.Contains(_organizationRepository.Query(), filter.ValueContains).Select(o => o.Id).ToList();
                organizationIds.AddRange(matchedOrganizationIds.Except(organizationIds));
                predicate = predicate.Or(o => organizationIds.Contains(o.OrganizationId));

                //types
                var matchedTypeIds = _opportunityTypeRepository.Contains(_opportunityTypeRepository.Query(), filter.ValueContains).Select(o => o.Id).ToList();
                typeIds.AddRange(matchedTypeIds.Except(typeIds));
                predicate = predicate.Or(o => matchedTypeIds.Contains(o.TypeId));

                //categories
                var matchedCategoryIds = _opportunityCategoryLookupRepository.Contains(_opportunityCategoryLookupRepository.Query(), filter.ValueContains).Select(o => o.Id).ToList();
                categoryIds.AddRange(matchedCategoryIds.Except(categoryIds));
                var matchedOpportunities = _opportunityCategoryRepository.Query().Where(o => categoryIds.Contains(o.CategoryId)).Select(o => o.OpportunityId).ToList();
                predicate = predicate.Or(o => matchedOpportunities.Contains(o.Id));

                //opportunities
                predicate = _opportunityRepository.Contains(predicate, filter.ValueContains);

                //skills
                var matchedSkillIds = _skillRepository.Contains(_skillRepository.Query(), filter.ValueContains).Select(o => o.Id).ToList();
                matchedOpportunities = _opportunitySkillRepository.Query().Where(o => matchedSkillIds.Contains(o.SkillId)).Select(o => o.OpportunityId).ToList();
                predicate = predicate.Or(o => matchedOpportunities.Contains(o.Id));

                query = query.Where(predicate);
            }
            else
            {
                if (filterByOrganization)
                    query = query.Where(o => organizationIds.Contains(o.OrganizationId));

                if (filterByType)
                    query = query.Where(o => typeIds.Contains(o.TypeId));

                if (filterByCategories)
                {
                    var matchedOpportunities = _opportunityCategoryRepository.Query().Where(o => categoryIds.Contains(o.CategoryId)).Select(o => o.OpportunityId).ToList();
                    query = query.Where(o => matchedOpportunities.Contains(o.Id));
                }
            }

            var result = new OpportunitySearchResults();
            query = query.OrderByDescending(o => o.DateCreated);

            //pagination
            if (filter.PaginationEnabled)
            {
                filter.ValidatePagination();
                result.TotalCount = query.Count();
                query = query.Skip((filter.PageNumber.Value - 1) * filter.PageSize.Value).Take(filter.PageSize.Value);
            }

            result.Items = query.ToList();

            return result;
        }

        public OpportunitySearchResultsInfo SearchInfo(OpportunitySearchFilter filter)
        {
            var searchResult = Search(filter);

            var result = new OpportunitySearchResultsInfo
            {
                TotalCount = searchResult.TotalCount,
                Items = searchResult.Items.Select(o => o.ToOpportunityInfo()).ToList()
            };

            return result;
        }

        public Task<Models.Opportunity> Upsert(OpportunityRequest request)
        {
            throw new NotImplementedException();
        }

        public Task AssignCategories(Guid id, List<Guid> categoryIds)
        {
            throw new NotImplementedException();
        }

        public Task DeleteCountries(Guid id, List<Guid> countryIds)
        {
            throw new NotImplementedException();
        }

        public Task AssignCountries(Guid id, List<Guid> countryIds)
        {
            throw new NotImplementedException();
        }

        public Task DeleteCategories(Guid id, List<Guid> categoryIds)
        {
            throw new NotImplementedException();
        }

        public Task AssignLanguages(Guid id, List<Guid> LanguageIds)
        {
            throw new NotImplementedException();
        }

        public Task DeleteLanguages(Guid id, List<Guid> LanguageIds)
        {
            throw new NotImplementedException();
        }

        public Task AssignSkills(Guid id, List<Guid> skillIds)
        {
            throw new NotImplementedException();
        }

        public Task DeleteSkills(Guid id, List<Guid> skillIds)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
