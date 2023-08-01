using Yoma.Core.Domain.Lookups.Models;

namespace Yoma.Core.Domain.Lookups.Interfaces
{
    public interface IProviderTypeService
    {
        ProviderType GetById(Guid id);

        ProviderType? GetByIdOrNull(Guid id);

        List<ProviderType> List();
    }
}
