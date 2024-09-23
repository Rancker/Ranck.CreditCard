using Ranck.CreditCard.Infrastructure.Data.Interfaces;

namespace Ranck.CreditCard.Infrastructure.Data
{
    public class CreditCardRepository: ICreditCardRepository
    { 
        private List<Core.Models.CreditCard> _creditCards = new();
        
        public void SetCreditCards(IEnumerable<Core.Models.CreditCard> creditCards)
        {
            if (_creditCards.Any())
            {
                ClearData();
            }

            _creditCards = creditCards.ToList();
        }

        public IEnumerable<Core.Models.CreditCard> GetCreditCards()
        {
            return _creditCards;
        }

        private void ClearData()
        {
            _creditCards.Clear();
        }
    }
}
