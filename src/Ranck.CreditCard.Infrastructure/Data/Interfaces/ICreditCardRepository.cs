namespace Ranck.CreditCard.Infrastructure.Data.Interfaces
{
    /// <summary>
    /// Interface to set and get the data from the credit card repository
    /// </summary>
    public interface ICreditCardRepository
    {
        /// <summary>
        /// Sets the credit cards asynchronous.
        /// </summary>
        /// <param name="creditCards">The credit cards.</param>
        void SetCreditCards(IEnumerable<Core.Models.CreditCard> creditCards);

        /// <summary>
        /// Gets the credit cards.
        /// </summary>
        /// <returns></returns>
        IEnumerable<Core.Models.CreditCard> GetCreditCards();
    }
}
