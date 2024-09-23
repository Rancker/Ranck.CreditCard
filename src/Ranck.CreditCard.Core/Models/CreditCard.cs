namespace Ranck.CreditCard.Core.Models
{
    /// <summary>
    /// The data  model return from the external client call.
    /// </summary>
    public class CreditCard
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int id { get; set; }

        /// <summary>
        /// Gets or sets the uid.
        /// </summary>
        /// <value>
        /// The uid.
        /// </value>
       public Guid uid { get; set; }

        /// <summary>
        /// Gets the credit card number.
        /// </summary>
        /// <value>
        /// The credit card number.
        /// </value>
        public string credit_card_number { get; set; }

        /// <summary>
        /// Gets or sets the credit card expiry date.
        /// </summary>
        /// <value>
        /// The credit card expiry date.
        /// </value>
      public string? credit_card_expiry_date { get; set; }

        /// <summary>
        /// Gets or sets the type of the credit card.
        /// </summary>
        /// <value>
        /// The type of the credit card.
        /// </value>
        public string? credit_card_type { get; set; }
    }
}
