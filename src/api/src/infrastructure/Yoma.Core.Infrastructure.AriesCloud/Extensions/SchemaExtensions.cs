using AriesCloudAPI.DotnetSDK.AspCore.Clients.Models;
using Newtonsoft.Json;
using Yoma.Core.Domain.Core.Extensions;
using Yoma.Core.Domain.SSI.Models;
using Yoma.Core.Domain.SSI.Models.Provider;

namespace Yoma.Core.Infrastructure.AriesCloud.Extensions
{
    public static class SchemaExtensions
    {
        #region Public Members
        public static List<Schema>? ToSchema(this ICollection<CredentialSchema> schemas, bool latestVersion)
        {
            if (!schemas.Any()) return null;

            var results = schemas.Select(ToSchema).ToList();

            results = FilterByLatestVersion(latestVersion, results);

            return results;
        }

        public static Schema ToSchema(this CredentialSchema o)
        {
            return new Schema
            {
                Id = o.Id,
                Name = o.Name,
                Version = Version.Parse(o.Version).ToMajorMinor(),
                ArtifactType = ArtifactType.Indy,
                AttributeNames = o.Attribute_names
            };
        }

        public static List<Schema>? ToSchema(this ICollection<Models.CredentialSchema> schemas, bool latestVersion)
        {
            if (!schemas.Any()) return null;

            var results = schemas.Select(ToSchema).ToList();

            results = FilterByLatestVersion(latestVersion, results);

            return results;
        }

        public static Schema ToSchema(this Models.CredentialSchema o)
        {
            return new Schema
            {
                Id = o.Id,
                Name = o.Name,
                Version = Version.Parse(o.Version).ToMajorMinor(),
                ArtifactType = o.ArtifactType,
                AttributeNames = JsonConvert.DeserializeObject<ICollection<string>>(o.AttributeNames) ?? new List<string>(),
            };
        }
        #endregion

        #region Private Members
        private static List<Schema> FilterByLatestVersion(bool latestVersion, List<Schema> results)
        {
            if (latestVersion)
                results = results
                  .GroupBy(s => s.Name)
                  .Select(group => group.OrderByDescending(s => s.Version).First())
                  .ToList();
            return results;
        }
        #endregion
    }
}