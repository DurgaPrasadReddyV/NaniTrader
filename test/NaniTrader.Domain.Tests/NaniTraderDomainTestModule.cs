using NaniTrader.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace NaniTrader;

[DependsOn(
    typeof(NaniTraderEntityFrameworkCoreTestModule)
    )]
public class NaniTraderDomainTestModule : AbpModule
{

}
