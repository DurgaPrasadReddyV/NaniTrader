﻿using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Volo.Abp.Domain.Repositories.MongoDB;
using Volo.Abp.MongoDB;
using NaniTrader.MongoDB;
using NaniTrader.MarketData.Interfaces;

namespace NaniTrader.MarketData
{
    public class MongoDbMarketDataRepository : MongoDbRepository<NaniTraderMongoDbContext, Touchline, Guid>, IMarketDataRepository
    {
        public MongoDbMarketDataRepository(IMongoDbContextProvider<NaniTraderMongoDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }
    }
}
