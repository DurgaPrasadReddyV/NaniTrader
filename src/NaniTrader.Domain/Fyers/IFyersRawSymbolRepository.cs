using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace NaniTrader.Fyers
{
    public interface IFyersRawSymbolRepository : IRepository<FyersRawSymbol, Guid>
    {
        Task<List<FyersRawSymbol>> GetListAsync(
            int skipCount,
            int maxResultCount,
            string sorting,
            string filter = null
        );
    }
}
