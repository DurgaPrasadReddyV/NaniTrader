using AutoMapper;
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
        CreateMap<FyersCredentials, CreateFyersCredentialsDto>();
    }
}
