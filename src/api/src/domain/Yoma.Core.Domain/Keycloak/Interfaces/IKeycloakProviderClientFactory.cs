namespace Yoma.Core.Domain.Keycloak.Interfaces
{
    public interface IKeycloakClientFactory
    {
        IKeycloakClient CreateClient();
    }
}
