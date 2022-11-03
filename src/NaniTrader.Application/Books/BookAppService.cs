using NaniTrader.Permissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace NaniTrader.Books
{
    public class BookAppService :
        CrudAppService<
            Book, //The Book entity
            BookDto, //Used to show books
            Guid, //Primary key of the book entity
            PagedAndSortedResultRequestDto, //Used for paging/sorting
            CreateUpdateBookDto>, //Used to create/update a book
        IBookAppService //implement the IBookAppService
    {
        public BookAppService(IRepository<Book, Guid> repository)
            : base(repository)
        {
            GetPolicyName = NaniTraderPermissions.Books.Default;
            GetListPolicyName = NaniTraderPermissions.Books.Default;
            CreatePolicyName = NaniTraderPermissions.Books.Create;
            UpdatePolicyName = NaniTraderPermissions.Books.Edit;
            DeletePolicyName = NaniTraderPermissions.Books.Delete;
        }
    }
}
