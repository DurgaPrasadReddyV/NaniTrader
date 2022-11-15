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

        var dashboardMenuItem = new ApplicationMenuItem("NaniTrader.Dashboard", l["Menu:Dashboard"],url: "/", icon: "fas fa-home");
        context.Menu.AddItem(dashboardMenuItem);

        var brokersMenu = new ApplicationMenuItem("NaniTrader.Brokers", l["Menu:Brokers"], icon: "fa fa-book");
        var fyersMenuItem = new ApplicationMenuItem("NaniTrader.Brokers.Fyers", l["Menu:Brokers:Fyers"], icon: "fa fa-book");
        var fyersCredentialsMenuItem = new ApplicationMenuItem("NaniTrader.Brokers.Fyers.Credentials", l["Menu:Brokers:Fyers:Credentials"],url: "/brokers/fyers/credentials", icon: "fa fa-book");
        fyersMenuItem.AddItem(fyersCredentialsMenuItem);
        var fyersRawSymbolsMenuItem = new ApplicationMenuItem("NaniTrader.Brokers.Fyers.RawSymbols", l["Menu:Brokers:Fyers:RawSymbols"], url: "/brokers/fyers/rawsymbols", icon: "fa fa-book");
        fyersMenuItem.AddItem(fyersRawSymbolsMenuItem);
        brokersMenu.AddItem(fyersMenuItem);
        context.Menu.AddItem(brokersMenu);

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
