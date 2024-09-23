namespace Ranck.CreditCard.Infrastructure.Communication.Interfaces;

public interface ICreditCardServiceClient
{
    /// <summary>
    /// Gets the credit card asynchronous.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    Task<IEnumerable<Core.Models.CreditCard>> GetCreditCardAsync(CancellationToken cancellationToken);
}