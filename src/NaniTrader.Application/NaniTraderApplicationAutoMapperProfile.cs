using AutoMapper;
using NaniTrader.Brokers.Fyers;
using NaniTrader.Fyers;

namespace NaniTrader;

public class NaniTraderApplicationAutoMapperProfile : Profile
{
    public NaniTraderApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */

        CreateMap<FyersCredentials, FyersCredentialsDto>();
        CreateMap<FyersSymbol, FyersSymbolDto>()
            .ForMember(dest => dest.Exchange, opt => opt.MapFrom(src => src.Exchange.ToString()))
            .ForMember(dest => dest.OptionRight, opt => opt.MapFrom(src => src.OptionRight.ToString()))
            .ForMember(dest => dest.SymbolType, opt => opt.MapFrom(src => src.SymbolType.ToString()))
            .ForMember(dest => dest.PriceStep, opt => opt.MapFrom(src => src.PriceStep.Amount))
            .ForMember(dest => dest.StrikePrice, opt => opt.MapFrom(src => src.StrikePrice.Amount));

    }
}
