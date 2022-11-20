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
using Volo.Abp.BackgroundJobs.Hangfire;
using Hangfire;
using Microsoft.Extensions.Configuration;
using Hangfire.SqlServer;

namespace NaniTrader;
[DependsOn(
    typeof(NaniTraderDomainModule),
    typeof(AbpAccountApplicationModule),
    typeof(NaniTraderApplicationContractsModule),
    typeof(AbpIdentityApplicationModule),
    typeof(AbpPermissionManagementApplicationModule),
    typeof(AbpTenantManagementApplicationModule),
    typeof(AbpFeatureManagementApplicationModule),
    typeof(AbpSettingManagementApplicationModule),
    typeof(AbpBackgroundJobsHangfireModule)
    )]
public class NaniTraderApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var configuration = context.Services.GetConfiguration();

        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<NaniTraderApplicationModule>();
        });

        context.Services.AddHttpClient<FyersApiClient>().ConfigureHttpClient((client) =>
        {
            client.BaseAddress = new Uri("https://api.fyers.in");
        });

        context.Services.AddHttpClient<FyersPublicApiClient>().ConfigureHttpClient((client) =>
        {
            client.BaseAddress = new Uri("https://public.fyers.in/");
        });

        context.Services.AddHangfire(config =>
        {
            config.UseSqlServerStorage(configuration.GetConnectionString("Default"), new SqlServerStorageOptions()
            {
                CommandTimeout = TimeSpan.FromSeconds(600)
            });
        });
    }
}
