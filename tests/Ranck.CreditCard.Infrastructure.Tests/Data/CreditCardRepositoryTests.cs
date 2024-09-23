using Ranck.CreditCard.Infrastructure.Data;
using NUnit.Framework;

namespace Ranck.CreditCard.Infrastructure.Tests.Data
{
    [TestFixture]
    public class CreditCardRepositoryTest
    {
        private CreditCardRepository _repository;

        [SetUp]
        public void Setup()
        {
            _repository = new CreditCardRepository();
        }

        [Test]
        public void SetCreditCards_SetsNewData()
        {
            // Arrange
            var creditCards = new [] { new Core.Models.CreditCard { credit_card_number = "9876543210987654" } };

            // Act
            _repository.SetCreditCards(creditCards);

            // Assert
            Assert.AreEqual(creditCards, _repository.GetCreditCards().ToList());
        }

        [Test]
        public void GetCreditCards_ReturnsEmptyList_WhenNoData()
        {
            // Act
            var creditCards = _repository.GetCreditCards();

            // Assert
            Assert.IsEmpty(creditCards);
        }

        [Test]
        public void GetCreditCards_ReturnsSetData()
        {
            // Arrange
            var creditCards = new[] { new Core.Models.CreditCard { credit_card_number = "1111222233334444" } };
            _repository.SetCreditCards(creditCards);

            // Act
            var retrievedCards = _repository.GetCreditCards();

            // Assert
            Assert.AreEqual(creditCards, retrievedCards.ToList());
        }



    }
}
