using Ranck.CreditCard.Core.Configuration;
using Microsoft.Extensions.Options;
using Ranck.CreditCard.Infrastructure.Communication.Interfaces;
using Flurl.Http;
using Microsoft.Extensions.Logging;

namespace Ranck.CreditCard.Infrastructure.Communication.ApiClients
{
    /// <inheritdoc cref="ICreditCardServiceClient"/>
    public class CreditCardServiceClient : ICreditCardServiceClient
    {
        private readonly CreditCardServiceOptions _options;
        private readonly ILogger<CreditCardServiceClient> _logger;

        public CreditCardServiceClient(IOptions<CreditCardServiceOptions> options, ILogger<CreditCardServiceClient> logger)
        {
            _options = options.Value;
            _logger = logger;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Ranck.CreditCard.Core.Models.CreditCard>> GetCreditCardAsync(
            CancellationToken cancellationToken)
        {
            try
            {
                var url = _options.BaseUrl + _options.GetCreditCardEndpoint + _options.RecordCount;
                return await url.GetJsonAsync<IEnumerable<Ranck.CreditCard.Core.Models.CreditCard>>(
                    cancellationToken: cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred getting all credit cards: {ex}");
                throw;
            }
        }
    }
}
