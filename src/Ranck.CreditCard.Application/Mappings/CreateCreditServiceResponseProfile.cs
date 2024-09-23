using AutoMapper;
using Ranck.CreditCard.Application.Models.Responses;

namespace Ranck.CreditCard.Application.Mappings
{
    /// <summary>
    /// Map DTO
    /// </summary>
    /// <seealso cref="AutoMapper.Profile" />
    public class CreateCreditServiceResponseProfile : Profile
    {
        public CreateCreditServiceResponseProfile()
        {
            CreateMap<GetCreditCardResponse, Core.Models.CreditCard>()
                .ForMember(src => src.credit_card_expiry_date, options => options.MapFrom(dst => dst.ExpiryDate))
                .ForMember(src => src.credit_card_type, options => options.MapFrom(dst => dst.CardType))
                .ForMember(src => src.credit_card_number, options => options.MapFrom(dst => dst.CardNumber))
                .ReverseMap();

        }
    }
}
