using AutoMapper;
using NaniTrader.Brokers.Fyers;

namespace NaniTrader.Blazor;

public class NaniTraderBlazorAutoMapperProfile : Profile
{
    public NaniTraderBlazorAutoMapperProfile()
    {
        //Define your AutoMapper configuration here for the Blazor project.
        CreateMap<FyersCredentialsDto, ReconfigureFyersCredentialsDto>();
    }
}
