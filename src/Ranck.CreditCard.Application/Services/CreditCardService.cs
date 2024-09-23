using AutoMapper;
using Ranck.CreditCard.Application.Models.Responses;
using Ranck.CreditCard.Application.Services.Interfaces;
using Ranck.CreditCard.Core.Configuration;
using Ranck.CreditCard.Infrastructure.Communication.Interfaces;
using Ranck.CreditCard.Infrastructure.Data.Interfaces;
using Microsoft.Extensions.Options;

namespace Ranck.CreditCard.Application.Services;

/// <inheritdoc cref="ICreditCardService"/>
public class CreditCardService : ICreditCardService
{
    private readonly ICreditCardServiceClient _creditCardServiceClient;
    private readonly ICreditCardRepository _creditCardRepository;
    private readonly IOptions<CreditCardServiceOptions> _options;
    private readonly IMapper _mapper;

    public CreditCardService(ICreditCardServiceClient creditCardServiceClient, IMapper mapper, ICreditCardRepository creditCardRepository, IOptions<CreditCardServiceOptions> options)
    {
        _creditCardServiceClient = creditCardServiceClient;
        _mapper = mapper;
        _creditCardRepository = creditCardRepository;
        _options = options;
    }

    /// <inheritdoc></inheritdoc>
    public async Task<IEnumerable<GetCreditCardResponse>> GetCreditCardsAsync(CancellationToken cancellationToken)
    {
        var creditCards = await _creditCardServiceClient.GetCreditCardAsync(cancellationToken);

        creditCards = creditCards.ToList();
        if (!creditCards.Any())
        {
            return null;
        }

        _creditCardRepository.SetCreditCards(creditCards);
        return _mapper.Map<IEnumerable<GetCreditCardResponse>>(creditCards);
    }

    /// <inheritdoc/>
    public int? GetExpiringCardAfter()
    {
        var creditCards = _creditCardRepository.GetCreditCards();
        if (!creditCards.Any())
        {
            return null;
        }
        var expiryDate = _options.Value.ExpireAfterDate;
        return _creditCardRepository.GetCreditCards().Count(card => card.credit_card_expiry_date != null && DateTime.Parse(card.credit_card_expiry_date) > expiryDate);
    }

    /// <inheritdoc/>
    public Dictionary<string, int> GetCardTypes()
    {
        var creditCards = _creditCardRepository.GetCreditCards();
        if (!creditCards.Any())
        {
            return null;
        }
        return creditCards
            .GroupBy(o => o.credit_card_type)
            .ToDictionary(g => g.Key, g => g.Count())!;
    }
}