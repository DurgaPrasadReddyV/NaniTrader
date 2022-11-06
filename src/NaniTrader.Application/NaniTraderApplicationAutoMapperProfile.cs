using AutoMapper;
using NaniTrader.Authors;
using NaniTrader.Books;

namespace NaniTrader;

public class NaniTraderApplicationAutoMapperProfile : Profile
{
    public NaniTraderApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */
        CreateMap<Book, BookDto>();
        CreateMap<CreateUpdateBookDto, Book>();
        CreateMap<Author, AuthorDto>();
        CreateMap<Author, AuthorLookupDto>();
    }
}
