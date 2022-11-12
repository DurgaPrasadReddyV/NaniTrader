using NaniTrader.ApiClients;
using Microsoft.Extensions.DependencyInjection;
using System;
using Volo.Abp.Account;
using Volo.Abp.AutoMapper;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;
using Volo.Abp.SettingManagement;
using Volo.Abp.TenantManagement;

namespace NaniTrader;

[DependsOn(
    typeof(NaniTraderDomainModule),
    typeof(AbpAccountApplicationModule),
    typeof(NaniTraderApplicationContractsModule),
    typeof(AbpIdentityApplicationModule),
    typeof(AbpPermissionManagementApplicationModule),
    typeof(AbpTenantManagementApplicationModule),
    typeof(AbpFeatureManagementApplicationModule),
    typeof(AbpSettingManagementApplicationModule)
    )]
public class NaniTraderApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<NaniTraderApplicationModule>();
        });

        context.Services.AddHttpClient<FyersApiClient>().ConfigureHttpClient((client) =>
        {
            client.BaseAddress = new Uri("https://api.fyers.in");
        });
    }
}
