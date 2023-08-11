using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Yoma.Core.Domain.Core.Interfaces;
using Yoma.Core.Domain.Core.Models;
using Yoma.Core.Domain.Lookups.Interfaces;
using Yoma.Core.Domain.Lookups.Models;

namespace Yoma.Core.Domain.Lookups.Services
{
    public class SkillService : ISkillService
    {
        #region Class Variables
        private readonly AppSettings _appSettings;
        private readonly IMemoryCache _memoryCache;
        private readonly IRepository<Skill> _skillRepository;
        #endregion

        #region Constructor
        public SkillService(IOptions<AppSettings> appSettings, 
            IMemoryCache memoryCache,
            IRepository<Skill> skillRepository)
        {
            _appSettings = appSettings.Value;
            _memoryCache = memoryCache;
            _skillRepository = skillRepository;
        }
        #endregion

        #region Public Members
        public Skill GetByName(string name)
        {
            var result = GetByNameOrNull(name);

            if (result == null)
                throw new ArgumentException($"{nameof(Skill)} with name '{name}' does not exists", nameof(name));

            return result;
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

            if (result == null)
                throw new ArgumentException($"{nameof(Skill)} for '{id}' does not exists", nameof(id));

            return result;
        }

        public Skill? GetByIdOrNull(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentNullException(nameof(id));

            return List().SingleOrDefault(o => o.Id == id);
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
            });

            if (result == null)
                throw new InvalidOperationException($"Failed to retrieve cached list of '{nameof(Skill)}s'");

            return result;
        }
        #endregion
    }
}
