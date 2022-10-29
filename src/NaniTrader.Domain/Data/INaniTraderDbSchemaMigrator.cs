using System.Threading.Tasks;

namespace NaniTrader.Data;

public interface INaniTraderDbSchemaMigrator
{
    Task MigrateAsync();
}
