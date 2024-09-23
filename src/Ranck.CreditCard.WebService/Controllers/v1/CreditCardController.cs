using Ranck.CreditCard.Application.Models.Responses;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Ranck.CreditCard.Application.Services.Interfaces;
using Ranck.CreditCard.WebService.Framework;
using Ranck.CreditCard.WebService.Framework.Enum;

namespace Ranck.CreditCard.WebService.Controllers.v1
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Produces("application/json")]
    public class CreditCardController : ControllerBase
    {
        private readonly ILogger<CreditCardController> _logger;
        private readonly ICreditCardService _creditCardService;
        
        public CreditCardController(ILogger<CreditCardController> logger,  ICreditCardService creditCardService)
        {
            _logger = logger;
            _creditCardService = creditCardService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<GetCreditCardResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<ActionResult> GetAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("");
            var creditCards = await _creditCardService.GetCreditCardsAsync(cancellationToken);
            if (creditCards is null)
            {
                return  NotFound(new ServiceResult<IEnumerable<GetCreditCardResponse>>(new ErrorResult(ErrorType.CreditCardDataNotFound)));
            }
            return Ok(new ServiceResult<IEnumerable<GetCreditCardResponse>>(creditCards));
        }

        /// <summary>
        /// Gets the count of card types.
        /// </summary>
        /// <returns></returns>
        [HttpGet("card-types")]
        [ProducesResponseType(typeof(IEnumerable<GetCreditCardResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public ActionResult<Dictionary<string, int>> GetCardTypeCount()
        {
            var numberOfCardType = _creditCardService.GetCardTypes();
            if (numberOfCardType is null)
            {
                return NotFound(new ServiceResult<Dictionary<string, int>>(new ErrorResult(ErrorType.CreditCardDataNotFound)));
            }

            return Ok(new ServiceResult<Dictionary<string, int>>(numberOfCardType));
        }

        /// <summary>
        /// Get the card count expiring after certain date.
        /// </summary>
        /// <returns></returns>
        [HttpGet("expiring/after")]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public ActionResult<int> GetExpiringCount()
        {
            var expireAfter = _creditCardService.GetExpiringCardAfter();
            if (expireAfter is null)
            {
                return NotFound(new ServiceResult<int>(new ErrorResult(ErrorType.CreditCardDataNotFound)));
            }
            return Ok(new ServiceResult<int>(expireAfter ?? 0));
        }
    }
}
