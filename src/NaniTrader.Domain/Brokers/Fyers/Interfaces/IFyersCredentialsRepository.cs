using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace NaniTrader.Brokers.Fyers.Interfaces
{
    public interface IFyersCredentialsRepository : IRepository<FyersCredentials, Guid>
    {
        Task<FyersCredentials> FindByAppIdAsync(string appId);

        Task<List<FyersCredentials>> GetListAsync(
            int skipCount,
            int maxResultCount,
            string sorting,
            string filter = null
        );
    }
}
