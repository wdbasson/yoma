using Microsoft.Extensions.Options;
using Yoma.Core.Domain.Emsi.Interfaces;
using Yoma.Core.Infrastructure.Emsi.Models;

namespace Yoma.Core.Infrastructure.Emsi.Client
{
    public class EmsiClientFactory : IEmsiClientFactory
    {
        #region Class Variables
        private readonly EmsiOptions _options;
        #endregion

        #region Constructor
        public EmsiClientFactory(IOptions<EmsiOptions> options)
        {
            _options = options.Value;
        }
        #endregion

        #region Public Members
        public IEmsiClient CreateClient()
        {
            return new EmsiClient(_options);
        }
        #endregion
    }
}
