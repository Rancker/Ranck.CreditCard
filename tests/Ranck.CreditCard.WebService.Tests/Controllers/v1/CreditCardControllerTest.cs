using AutoFixture.NUnit3;
using Ranck.CreditCard.Application.Models.Responses;
using Ranck.CreditCard.Application.Services.Interfaces;
using Ranck.CreditCard.WebService.Controllers.v1;
using Ranck.CreditCard.WebService.Framework;
using Ranck.CreditCard.WebService.Framework.Enum;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using FluentAssertions;

namespace Ranck.CreditCard.WebService.Tests.Controllers.v1
{
    [TestFixture]
    public class CreditCardControllerTest
    {
        private Mock<ICreditCardService> _mockCreditCardService;
        private CreditCardController _controller;
        private Mock<ILogger<CreditCardController>> _mockLogger;

        [SetUp]
        public void Setup()
        {
            _mockCreditCardService = new Mock<ICreditCardService>();
            _mockLogger = new Mock<ILogger<CreditCardController>>();
            _controller = new CreditCardController(_mockLogger.Object, _mockCreditCardService.Object);
        }

        [Test]
        [AutoData]
        public async Task Given_WhenCreditCardsExists_ShouldReturnsOk(List<GetCreditCardResponse> creditCards)
        {
            // Arrange
            _mockCreditCardService.Setup(s => s.GetCreditCardsAsync(It.IsAny<CancellationToken>()))
               .ReturnsAsync(creditCards);

            // Act
            var result = await _controller.GetAsync(CancellationToken.None);

            // Assert
            var actualResponse = ((OkObjectResult)result).Value as ServiceResult<IEnumerable<GetCreditCardResponse>>;
            Assert.AreEqual(creditCards,actualResponse?.Result);
            Assert.AreEqual(true,actualResponse?.Success);
            actualResponse.Errors.Should().BeNull();
        }

        [Test]
        public async Task Given_WhenCardsDoesNotExists_ShouldReturnsNotFound()
        {
            //Arrange
            _mockCreditCardService.Setup(s => s.GetCreditCardsAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync((IEnumerable<GetCreditCardResponse>)null);

            // Act
            var result = await _controller.GetAsync(CancellationToken.None);

            // Assert
            Assert.IsInstanceOf<NotFoundObjectResult>(result);
            var actualResponse = (ServiceResult<IEnumerable<GetCreditCardResponse>>)((NotFoundObjectResult)result).Value;
            Assert.IsInstanceOf<IEnumerable<ErrorResult>>(actualResponse.Errors);
            Assert.AreEqual(ErrorType.CreditCardDataNotFound, actualResponse.Errors[0].Type);
        }

        [Test]
        [AutoData]
        public void Given_WhenCardTypeExists_ShouldReturnsOk_WithCardTypeAndCount(Dictionary<string, int> cardTypeCount)
        {
            // Arrange
            _mockCreditCardService.Setup(s => s.GetCardTypes())
                .Returns(cardTypeCount);

            // Act
            var actualResponse = _controller.GetCardTypeCount();

            // Assert
            var result = (ServiceResult<Dictionary<string, int>>)((OkObjectResult)actualResponse.Result).Value;
            Assert.AreEqual(cardTypeCount, result?.Result);
        }

        [Test]
        public void Given_WhenCardsTypesDoesNotExists_ShouldReturnNotFound()
        {
            // Arrange
            _mockCreditCardService.Setup(s => s.GetCardTypes())
                .Returns((Dictionary<string, int>)null);

            // Act
            var actualResponse = _controller.GetCardTypeCount();

            // Assert
            var result = (ServiceResult<Dictionary<string, int>>)((NotFoundObjectResult)actualResponse.Result).Value;
            Assert.AreEqual(ErrorType.CreditCardDataNotFound, result.Errors[0].Type);
        }
        [Test]
        [AutoData]
        public void Given_WhenCardExists_ShouldReturnsOk_WithExpiredAfterCount(int cardExpireCount)
        {
            // Arrange
            _mockCreditCardService.Setup(s => s.GetExpiringCardAfter())
                .Returns(cardExpireCount);

            // Act
            var actualResponse = _controller.GetExpiringCount();

            // Assert
            var result = (ServiceResult<int>)((OkObjectResult)actualResponse.Result).Value;
            Assert.AreEqual(cardExpireCount, result?.Result);
        }

        [Test]
        public void Given_WhenCardDoesNotExists_ShouldReturnNotFound()
        {
            // Arrange
            _mockCreditCardService.Setup(s => s.GetExpiringCardAfter())
                .Returns((int?)null);

            // Act
            var actualResponse = _controller.GetExpiringCount();

            // Assert
            var result = (ServiceResult<int>)((NotFoundObjectResult)actualResponse.Result).Value;
            Assert.AreEqual(ErrorType.CreditCardDataNotFound, result.Errors[0].Type);
        }
    }
}
