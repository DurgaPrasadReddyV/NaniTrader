using MongoDB.Driver;
using NaniTrader.MarketData;
using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace NaniTrader.MongoDB
{
    [ConnectionStringName("MongoDb")]
    public class NaniTraderMongoDbContext : AbpMongoDbContext
    {
        public IMongoCollection<Touchline> Touchlines => Collection<Touchline>();

        protected override void CreateModel(IMongoModelBuilder modelBuilder)
        {
            base.CreateModel(modelBuilder);
        }
    }
}
