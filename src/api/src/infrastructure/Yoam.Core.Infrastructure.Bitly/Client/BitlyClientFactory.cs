using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Yoma.Core.Domain.Core.Interfaces;
using Yoma.Core.Domain.Core.Models;
using Yoma.Core.Domain.ShortLinkProvider.Interfaces;
using Yoma.Core.Infrastructure.Bitly.Models;

namespace Yoma.Core.Infrastructure.Bitly.Client
{
  public class BitlyClientFactory : IShortLinkProviderClientFactory
  {
    #region Class Variables
    private readonly ILogger<BitlyClient> _logger;
    private readonly IEnvironmentProvider _environmentProvider;
    private readonly AppSettings _appSettings;
    private readonly BitlyOptions _options;
    #endregion

    #region Constructor
    public BitlyClientFactory(ILogger<BitlyClient> logger,
      IEnvironmentProvider environmentProvider,
      IOptions<AppSettings> appSettings,
      IOptions<BitlyOptions> options)
    {
      _logger = logger;
      _environmentProvider = environmentProvider;
      _appSettings = appSettings.Value;
      _options = options.Value;
    }
    #endregion

    #region Public Members
    public IShortLinkProviderClient CreateClient()
    {
      return new BitlyClient(_logger, _environmentProvider, _appSettings, _options);
    }
    #endregion
  }
}
