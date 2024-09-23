using AutoFixture.NUnit3;
using Ranck.CreditCard.Core.Configuration;
using Ranck.CreditCard.Infrastructure.Communication.ApiClients;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using Flurl.Http.Testing;
using Microsoft.Extensions.Logging;

namespace Ranck.CreditCard.Infrastructure.Tests.Communication.ApiClients
{
    [TestFixture]
    public class CreditCardServiceClientTest
    {
        private Mock<IOptions<CreditCardServiceOptions>> _mockOptions;
        private CreditCardServiceClient _creditCardServiceClient;
        private HttpTest _httpTest;
        private Mock<ILogger<CreditCardServiceClient>> _mockLogger;

        [SetUp]
        public void Setup()
        {
            _mockOptions = new Mock<IOptions<CreditCardServiceOptions>>(MockBehavior.Strict);

            _mockOptions.SetupGet(x => x.Value)
                .Returns(new CreditCardServiceOptions
                {
                    ExpireAfterDate = DateTime.UtcNow,
                    BaseUrl = "https://example.com/api/",
                    GetCreditCardEndpoint = "creditcards",
                    RecordCount = 10
                });
            _mockLogger = new Mock<ILogger<CreditCardServiceClient>>();
            _creditCardServiceClient = new CreditCardServiceClient(_mockOptions.Object, _mockLogger.Object);
            _httpTest = new HttpTest();
        }

        [Test]
        [AutoData]
        public async Task GetCreditCardAsync_ReturnsData_WhenServiceRespondsWithSuccess(List<Core.Models.CreditCard> creditCards)
        {
            // Arrange
            _httpTest.RespondWithJson(creditCards);

            // Act
            var result = await _creditCardServiceClient.GetCreditCardAsync(CancellationToken.None);

            // Assert
           Assert.AreEqual(creditCards.Count, result.Count());
        }

        [TearDown]
        public void TearDown()
        {
            _httpTest.Dispose();
        }
    }
}
