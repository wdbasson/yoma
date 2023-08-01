using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Yoma.Core.Domain.Core.Interfaces;
using Yoma.Core.Domain.Core.Models;
using Yoma.Core.Domain.Lookups.Interfaces;
using Yoma.Core.Domain.Lookups.Models;

namespace Yoma.Core.Domain.Lookups.Services
{
    public class ProviderTypeService : IProviderTypeService
    {
        #region Class Variables
        private readonly AppSettings _appSettings;
        private readonly IMemoryCache _memoryCache;
        private readonly IRepository<ProviderType> _providerTypeRepository;
        #endregion

        #region Constructor
        public ProviderTypeService(IOptions<AppSettings> appSettings,
            IMemoryCache memoryCache,
            IRepository<ProviderType> providerTypeRepository)
        {
            _appSettings = appSettings.Value;
            _memoryCache = memoryCache;
            _providerTypeRepository = providerTypeRepository;
        }
        #endregion

        #region Public Members
        public ProviderType GetById(Guid id)
        {
            var result = GetByIdOrNull(id);

            if (result == null)
                throw new ArgumentException($"{nameof(ProviderType)} for '{id}' does not exists", nameof(id));

            return result;
        }

        public ProviderType? GetByIdOrNull(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentNullException(nameof(id));

            return List().SingleOrDefault(o => o.Id == id);
        }

        public List<ProviderType> List()
        {
            if (!_appSettings.CacheEnabledByReferenceDataTypes.HasFlag(Core.ReferenceDataType.Lookups))
                return _providerTypeRepository.Query().ToList();

            var result = _memoryCache.GetOrCreate(nameof(ProviderType), entry =>
            {
                entry.SlidingExpiration = TimeSpan.FromHours(_appSettings.CacheSlidingExpirationLookupInHours);
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(_appSettings.CacheAbsoluteExpirationRelativeToNowLookupInDays);
                return _providerTypeRepository.Query().OrderBy(o => o.Name).ToList();
            });

            if (result == null)
                throw new InvalidOperationException($"Failed to retrieve cached list of '{nameof(ProviderType)}'s");

            return result;
        }
        #endregion
    }
}
