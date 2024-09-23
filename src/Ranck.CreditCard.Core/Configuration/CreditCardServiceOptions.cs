namespace Ranck.CreditCard.Core.Configuration
{
    /// <summary>
    /// The configuration details for credit card service
    /// </summary>
    public class CreditCardServiceOptions
    {
        public const string CreditCardService = "CreditCardService";

        /// <summary>
        /// Gets or sets the base URL for external client.
        /// </summary>
        /// <value>
        /// The base URL.
        /// </value>
        public string BaseUrl { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the get credit card endpoint.
        /// </summary>
        /// <value>
        /// The get credit card endpoint.
        /// </value>
        public string GetCreditCardEndpoint { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the record count.
        /// </summary>
        /// <value>
        /// The record count.
        /// </value>
        public int RecordCount { get; set; }

        /// <summary>
        /// Gets or sets the expire after date.
        /// </summary>
        /// <value>
        /// The expire after date.
        /// </value>
        public DateTime ExpireAfterDate { get; set; }
    }
}
