using AutoMapper;
using NaniTrader.Authors;
using NaniTrader.Books;

namespace NaniTrader.Blazor;

public class NaniTraderBlazorAutoMapperProfile : Profile
{
    public NaniTraderBlazorAutoMapperProfile()
    {
        //Define your AutoMapper configuration here for the Blazor project.
        CreateMap<BookDto, CreateUpdateBookDto>();
        CreateMap<AuthorDto, UpdateAuthorDto>();
    }
}
