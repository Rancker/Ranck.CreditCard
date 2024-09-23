using Ranck.CreditCard.Application.Models.Responses;

namespace Ranck.CreditCard.Application.Services.Interfaces
{

    /// <summary>
    /// Interface for the credit card service.
    /// </summary>
    public interface ICreditCardService
    {
        /// <summary>
        /// Gets the credit cards.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<IEnumerable<GetCreditCardResponse>> GetCreditCardsAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Gets the expiring card count.
        /// </summary>
        /// <returns></returns>
        int? GetExpiringCardAfter();

        /// <summary>
        /// Gets the number of card types.
        /// </summary>
        /// <returns></returns>
        Dictionary<string, int> GetCardTypes();
    }
}
