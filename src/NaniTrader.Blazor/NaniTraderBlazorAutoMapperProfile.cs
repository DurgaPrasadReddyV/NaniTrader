using AutoMapper;
using NaniTrader.Fyers;

namespace NaniTrader.Blazor;

public class NaniTraderBlazorAutoMapperProfile : Profile
{
    public NaniTraderBlazorAutoMapperProfile()
    {
        //Define your AutoMapper configuration here for the Blazor project.
        CreateMap<FyersCredentialsDto, UpdateFyersCredentialsDto>();
    }
}
