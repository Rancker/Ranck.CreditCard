using System.Globalization;
using AutoFixture.NUnit3;
using AutoMapper;
using Ranck.CreditCard.Application.Models.Responses;
using Ranck.CreditCard.Application.Services;
using Ranck.CreditCard.Core.Configuration;
using Ranck.CreditCard.Infrastructure.Communication.Interfaces;
using Ranck.CreditCard.Infrastructure.Data.Interfaces;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;

namespace Ranck.CreditCard.Application.Tests.Services
{
    [TestFixture]
    internal class CreditCardServiceTests
    {
        private Mock<ICreditCardServiceClient> _mockCreditCardServiceClient;
        private Mock<IMapper> _mockMapper;
        private Mock<ICreditCardRepository> _mockCreditCardRepository;
        private Mock<IOptions<CreditCardServiceOptions>> _mockOptions;
        private CreditCardService _creditCardService;

        [SetUp]
        public void Setup()
        {
            _mockCreditCardServiceClient = new Mock<ICreditCardServiceClient>();
            _mockMapper = new Mock<IMapper>();
            _mockCreditCardRepository = new Mock<ICreditCardRepository>();
            _mockOptions = new Mock<IOptions<CreditCardServiceOptions>>(MockBehavior.Strict);

            _mockOptions.SetupGet(x => x.Value)
                .Returns(new CreditCardServiceOptions
                {
                    ExpireAfterDate = DateTime.UtcNow
                });

            _creditCardService = new CreditCardService(
                _mockCreditCardServiceClient.Object,
                _mockMapper.Object,
                _mockCreditCardRepository.Object,
                _mockOptions.Object);
        }

        [Test]
        public async Task Given_WhenServiceClientReturnsEmptyList_ShouldReturnsEmpty()
        {
            // Arrange
            _mockCreditCardServiceClient.Setup(s => s.GetCreditCardAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(Enumerable.Empty<Core.Models.CreditCard>());

            // Act
            var result = await _creditCardService.GetCreditCardsAsync(CancellationToken.None);

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        [AutoData]
        public async Task Given_WhenServiceClientReturnsData_ShouldReturnsMappedData(List<Core.Models.CreditCard> creditCards, List<GetCreditCardResponse> mappedData)
        {
            // Arrange
            _mockCreditCardServiceClient.Setup(s => s.GetCreditCardAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(creditCards);
            _mockMapper.Setup(m => m.Map<IEnumerable<GetCreditCardResponse>>(It.IsAny<IEnumerable<Core.Models.CreditCard>>()))
                .Returns(mappedData);

            // Act
            var result = await _creditCardService.GetCreditCardsAsync(CancellationToken.None);

            // Assert
            Assert.AreEqual(mappedData, result);
            _mockCreditCardRepository.Verify(r => r.SetCreditCards(creditCards), Times.Once);
        }

        [Test]
        public void GivenGetExpiringCardAfterRequested_WhenNoCreditCardsExists_ShouldReturnsNull()
        {
            // Arrange
            _mockCreditCardRepository.Setup(r => r.GetCreditCards()).Returns(Enumerable.Empty<Core.Models.CreditCard>());
            
            // Act
            var result = _creditCardService.GetExpiringCardAfter();

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public void GivenGetExpiringCardAfterRequested_WhenCardsExist_ReturnsCount()
        {
            var creditCards = new[]
            {
                new Core.Models.CreditCard
                    {credit_card_expiry_date = _mockOptions.Object.Value.ExpireAfterDate.AddDays(4).ToString()}
            };
            // Arrange
            _mockCreditCardRepository.Setup(r => r.GetCreditCards()).Returns(creditCards);


            // Act
            var result = _creditCardService.GetExpiringCardAfter();

            Assert.AreEqual(1, result);
        }
    }
}
