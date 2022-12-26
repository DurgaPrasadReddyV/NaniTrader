using System;
using Volo.Abp.Domain.Repositories;

namespace NaniTrader.MarketData.Interfaces
{
    public interface ITouchlineRepository : IRepository<Touchline, Guid>
    {
    }
}