namespace Yoma.Core.Infrastructure.Keycloak.Models
{
    public class KeycloakAuthOptions
    {
        public string ClientId { get; set; }

        public string ClientSecret { get; set; }  

        public Uri AuthorizationUrl { get; set; }

        public Uri TokenUrl { get; set; }
    }
}
