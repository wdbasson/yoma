using Yoma.Core.Domain.Entity.Models;

namespace Yoma.Core.Domain.Entity.Helpers
{
    public static class OrganizationHelper
    {
        public static OrganizationInfo ToInfo(this Organization value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            return new OrganizationInfo
            {
                Id = value.Id,
                Name = value.Name,
                Tagline = value.Tagline,
                Status = value.Status,
                LogoURL = value.LogoURL
            };
        }
    }
}
