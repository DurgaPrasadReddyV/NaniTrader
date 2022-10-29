using Volo.Abp.Modularity;

namespace NaniTrader;

[DependsOn(
    typeof(NaniTraderApplicationModule),
    typeof(NaniTraderDomainTestModule)
    )]
public class NaniTraderApplicationTestModule : AbpModule
{

}
