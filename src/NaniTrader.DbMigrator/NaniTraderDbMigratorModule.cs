using NaniTrader.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Modularity;

namespace NaniTrader.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(NaniTraderEntityFrameworkCoreModule),
    typeof(NaniTraderApplicationContractsModule)
    )]
public class NaniTraderDbMigratorModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpBackgroundJobOptions>(options => options.IsJobExecutionEnabled = false);
    }
}
