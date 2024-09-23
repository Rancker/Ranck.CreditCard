using AutoMapper;
using Ranck.CreditCard.Application.Mappings;
using Ranck.CreditCard.Application.Models.Responses;
using NUnit.Framework;

namespace Ranck.CreditCard.Application.Tests.Mappings
{
    [TestFixture]
    public class CreateCreditServiceResponseProfileTest
    {
        private IMapper _mapper;

        [SetUp]
        public void Setup()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<CreateCreditServiceResponseProfile>();
            });
            _mapper = config.CreateMapper();
        }

        [Test]
        public void TestMapping_MapsPropertiesCorrectly()
        {
            // Arrange
            var source = new GetCreditCardResponse
            {
                ExpiryDate = "2025-12-31",
                CardType = "Visa"
            };

            // Act
            var destination = _mapper.Map<Core.Models.CreditCard>(source);

            // Assert
            Assert.AreEqual(source.ExpiryDate, destination.credit_card_expiry_date);
            Assert.AreEqual(source.CardType, destination.credit_card_type);
        }

        [Test]
        public void TestReverseMapping_MapsPropertiesCorrectly()
        {
            // Arrange
            var source = new Core.Models.CreditCard
            {
                credit_card_expiry_date = "2026-01-01",
                credit_card_type = "Mastercard"

            };

            // Act
            var destination = _mapper.Map<GetCreditCardResponse>(source);

            // Assert
            Assert.AreEqual(source.credit_card_expiry_date, destination.ExpiryDate);
            Assert.AreEqual(source.credit_card_type, destination.CardType);
            Assert.AreEqual(source.credit_card_number, destination.CardNumber);
        }
    }
}