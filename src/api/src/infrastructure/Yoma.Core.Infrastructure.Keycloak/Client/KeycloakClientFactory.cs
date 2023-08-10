using Keycloak.AuthServices.Authentication;
using Microsoft.Extensions.Options;
using Yoma.Core.Domain.Core.Models;
using Yoma.Core.Domain.Keycloak.Interfaces;

namespace Yoma.Core.Infrastructure.Keycloak.Client
{
    public class KeycloakClientFactory : IKeycloakClientFactory
    {
        #region Class Variables
        private readonly AppSettings _appSettings;
        private readonly KeycloakAuthenticationOptions _keycloakAuthenticationOptions;
        #endregion

        #region Constructor
        public KeycloakClientFactory(IOptions<AppSettings> appSettings,
            IOptions<KeycloakAuthenticationOptions> keycloakAuthenticationOptions)
        {
            _appSettings = appSettings.Value;
            _keycloakAuthenticationOptions = keycloakAuthenticationOptions.Value;
        }
        #endregion

        #region Public Members
        public IKeycloakClient CreateClient()
        {
            return new KeycloakClient(_appSettings, _keycloakAuthenticationOptions);
        }
        #endregion
    }
}
