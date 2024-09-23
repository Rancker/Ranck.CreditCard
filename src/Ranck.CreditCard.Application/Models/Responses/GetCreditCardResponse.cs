using System.Text.Json.Serialization;

namespace Ranck.CreditCard.Application.Models.Responses
{
    /// <summary>
    /// Response to return from the get request.
    /// </summary>
    public class GetCreditCardResponse
    {
        /// <summary>
        /// Gets or sets the card number.
        /// </summary>
        /// <value>
        /// The card number.
        /// </value>
        [JsonPropertyName("cardnumber")]
        public string CardNumber { get; set; }

        /// <summary>
        /// Gets or sets the expiry date.
        /// </summary>
        /// <value>
        /// The expiry date.
        /// </value>
        [JsonPropertyName("expirydate")]
        public string? ExpiryDate { get; set; }


        /// <summary>
        /// Gets or sets the type of the card.
        /// </summary>
        /// <value>
        /// The type of the card.
        /// </value>
        [JsonPropertyName("cardtype")]
        public string? CardType { get; set; }

    }
}
