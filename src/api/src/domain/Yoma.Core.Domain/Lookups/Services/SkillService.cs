using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Yoma.Core.Domain.Core.Interfaces;
using Yoma.Core.Domain.Core.Models;
using Yoma.Core.Domain.Emsi.Interfaces;
using Yoma.Core.Domain.Lookups.Interfaces;
using Yoma.Core.Domain.Lookups.Models;

namespace Yoma.Core.Domain.Lookups.Services
{
    public class SkillService : ISkillService
    {
        #region Class Variables
        private readonly AppSettings _appSettings;
        private ScheduleJobOptions _scheduleJobOptions;
        private readonly IMemoryCache _memoryCache;
        private readonly IEmsiClient _emsiClient;
        private readonly IRepositoryBatched<Skill> _skillRepository;
        #endregion

        #region Constructor
        public SkillService(IOptions<AppSettings> appSettings,
            IOptions<ScheduleJobOptions> scheduleJobOptions,
            IMemoryCache memoryCache,
            IEmsiClientFactory emsiClientFactory,
            IRepositoryBatched<Skill> skillRepository)
        {
            _appSettings = appSettings.Value;
            _scheduleJobOptions = scheduleJobOptions.Value;
            _memoryCache = memoryCache;
            _emsiClient = emsiClientFactory.CreateClient();
            _skillRepository = skillRepository;
        }
        #endregion

        #region Public Members
        public Skill GetByName(string name)
        {
            var result = GetByNameOrNull(name);

            return result ?? throw new ArgumentException($"{nameof(Skill)} with name '{name}' does not exists", nameof(name));
        }

        public Skill? GetByNameOrNull(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));
            name = name.Trim();

            return List().SingleOrDefault(o => o.Name == name);
        }

        public Skill GetById(Guid id)
        {
            var result = GetByIdOrNull(id);

            return result ?? throw new ArgumentException($"{nameof(Skill)} for '{id}' does not exists", nameof(id));
        }

        public Skill? GetByIdOrNull(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentNullException(nameof(id));

            return List().SingleOrDefault(o => o.Id == id);
        }

        public SkillSearchResults Search(FilterPagination filter)
        {
            if (filter == null)
                throw new ArgumentNullException(nameof(filter));

            if (!filter.PaginationEnabled)
                throw new ArgumentException("Pagination criteria not specified", nameof(filter));

            if (!filter.PageNumber.HasValue || filter.PageNumber.Value <= 0)
                throw new ArgumentException($"{nameof(filter.PageNumber)} must be greater than 0", nameof(filter));

            if (!filter.PageSize.HasValue || filter.PageSize.Value <= 0)
                throw new ArgumentException($"{nameof(filter.PageNumber)} must be greater than 0", nameof(filter));

            return new SkillSearchResults 
            { 
                TotalCount = List().Count,
                Items = List().Skip((filter.PageNumber.Value - 1) * filter.PageSize.Value).Take(filter.PageSize.Value).ToList()
            };
        }

        public List<Skill> List()
        {
            if (!_appSettings.CacheEnabledByReferenceDataTypes.HasFlag(Core.ReferenceDataType.Lookups))
                return _skillRepository.Query().ToList();

            var result = _memoryCache.GetOrCreate(nameof(Skill), entry =>
            {
                entry.SlidingExpiration = TimeSpan.FromHours(_appSettings.CacheSlidingExpirationLookupInHours);
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(_appSettings.CacheAbsoluteExpirationRelativeToNowLookupInDays);
                return _skillRepository.Query().OrderBy(o => o.Name).ToList();
            }) ?? throw new InvalidOperationException($"Failed to retrieve cached list of '{nameof(Skill)}s'");
            return result;
        }

        public async Task SeedSkills()
        {
            var incomingResults = await _emsiClient.ListSkills();
            if (incomingResults == null || !incomingResults.Any()) return;

            int batchSize = _scheduleJobOptions.SeedSkillsBatchSize; 
            int pageIndex = 0;
            do
            {
                var incomingBatch = incomingResults.Skip(pageIndex * batchSize).Take(batchSize).ToList();
                var incomingBatchIds = incomingBatch.Select(o => o.Id).ToList();
                var existingItems = _skillRepository.Query().Where(o => incomingBatchIds.Contains(o.ExternalId)).ToList();
                var newItems = new List<Skill>();
                foreach (var item in incomingBatch)
                {
                    var existItem = existingItems.SingleOrDefault(o => o.ExternalId == item.Id);
                    if(existItem != null)
                    {
                        existItem.Name = item.Name;
                        existItem.InfoURL = item.InfoURL;
                    }
                    else
                    {
                        newItems.Add(new Skill
                        {
                            Name = item.Name,
                            InfoURL = item.InfoURL,
                            ExternalId = item.Id
                        });
                    }
                }

                if (newItems.Any()) await _skillRepository.Create(newItems);
                if (existingItems.Any()) await _skillRepository.Update(existingItems);

                pageIndex++;
            } 
            while ((pageIndex - 1) * batchSize < incomingResults.Count);
        }
        #endregion
    }
}
