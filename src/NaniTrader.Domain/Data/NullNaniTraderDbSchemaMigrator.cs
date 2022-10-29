using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace NaniTrader.Data;

/* This is used if database provider does't define
 * INaniTraderDbSchemaMigrator implementation.
 */
public class NullNaniTraderDbSchemaMigrator : INaniTraderDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
