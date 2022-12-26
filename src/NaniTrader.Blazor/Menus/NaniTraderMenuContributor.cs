using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using NaniTrader.Localization;
using Volo.Abp.Account.Localization;
using Volo.Abp.UI.Navigation;

namespace NaniTrader.Blazor.Menus;

public class NaniTraderMenuContributor : IMenuContributor
{
    private readonly IConfiguration _configuration;

    public NaniTraderMenuContributor(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name == StandardMenus.Main)
        {
            await ConfigureMainMenuAsync(context);
        }
        else if (context.Menu.Name == StandardMenus.User)
        {
            await ConfigureUserMenuAsync(context);
        }
    }

    private Task ConfigureMainMenuAsync(MenuConfigurationContext context)
    {
        var l = context.GetLocalizer<NaniTraderResource>();

        var dashboardMenuItem = new ApplicationMenuItem("NaniTrader.Dashboard", l["Dashboard"],url: "/dashboard", icon: "fas fa-home");
        context.Menu.AddItem(dashboardMenuItem);

        var brokersMenu = new ApplicationMenuItem("NaniTrader.Brokers", l["Brokers"], icon: "fa fa-bars");
        var brokersConfigureMenuItem = new ApplicationMenuItem("NaniTrader.Brokers.Configure", l["Configure"], url: "/brokers/configure", icon: "fa fa-bars");
        var fyersMenuItem = new ApplicationMenuItem("NaniTrader.Brokers.Fyers", l["Fyers"], icon: "fa fa-bars");
        var fyersCredentialsMenuItem = new ApplicationMenuItem("NaniTrader.Brokers.Fyers.Credentials", l["Credentials"],url: "/brokers/fyers/credentials", icon: "fa fa-bars");
        fyersMenuItem.AddItem(fyersCredentialsMenuItem);
        var fyersSymbolsMenuItem = new ApplicationMenuItem("NaniTrader.Brokers.Fyers.Symbols", l["Symbols"], url: "/brokers/fyers/symbols", icon: "fa fa-bars");
        fyersMenuItem.AddItem(fyersSymbolsMenuItem);
        brokersMenu.AddItem(brokersConfigureMenuItem);
        brokersMenu.AddItem(fyersMenuItem);
        context.Menu.AddItem(brokersMenu);

        var exchangesMenu = new ApplicationMenuItem("NaniTrader.Exchanges", l["Exchanges"], icon: "fa fa-bars");
        var exchangesConfigureMenuItem = new ApplicationMenuItem("NaniTrader.Exchanges.Configure", l["Configure"], url: "/exchanges/configure", icon: "fa fa-bars");
        exchangesMenu.AddItem(exchangesConfigureMenuItem);
        context.Menu.AddItem(exchangesMenu);
        return Task.CompletedTask;
    }

    private Task ConfigureUserMenuAsync(MenuConfigurationContext context)
    {
        var accountStringLocalizer = context.GetLocalizer<AccountResource>();

        var authServerUrl = _configuration["AuthServer:Authority"] ?? "";

        context.Menu.AddItem(new ApplicationMenuItem(
            "Account.Manage",
            accountStringLocalizer["MyAccount"],
            $"{authServerUrl.EnsureEndsWith('/')}Account/Manage?returnUrl={_configuration["App:SelfUrl"]}",
            icon: "fa fa-cog",
            order: 1000,
            null));

        return Task.CompletedTask;
    }
}
