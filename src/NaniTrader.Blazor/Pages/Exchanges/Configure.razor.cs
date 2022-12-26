using Blazorise;
using NaniTrader.Brokers.Fyers;
using NaniTrader.Exchanges;
using System.Threading.Tasks;
using static NaniTrader.Permissions.NaniTraderPermissions;

namespace NaniTrader.Blazor.Pages.Exchanges
{
    public partial class Configure
    {
        private ExchangeDto NSECMExchange { get; set; }
        private ConfigureExchangeDto ConfiguredNSECMExchange { get; set; }
        private Modal ConfigureNSECMExchangeModal { get; set; }
        private Validations ConfigureNSECMExchangeValidationsRef;

        private ExchangeDto NSEFNOExchange { get; set; }
        private ConfigureExchangeDto ConfiguredNSEFNOExchange { get; set; }
        private Modal ConfigureNSEFNOExchangeModal { get; set; }
        private Validations ConfigureNSEFNOExchangeValidationsRef;

        public Configure()
        {
            NSECMExchange = new ExchangeDto();
            ConfiguredNSECMExchange = new ConfigureExchangeDto();
            NSEFNOExchange = new ExchangeDto();
            ConfiguredNSEFNOExchange = new ConfigureExchangeDto();
        }

        protected override async Task OnInitializedAsync()
        {
            await GetExchangesAsync();
        }

        private async Task GetExchangesAsync()
        {
            NSECMExchange = await ExchangeAppService.GetByExchangeIdentifierAsync("NSE_CM");
            NSECMExchange ??= new ExchangeDto();

            NSEFNOExchange = await ExchangeAppService.GetByExchangeIdentifierAsync("NSE_FNO");
            NSEFNOExchange ??= new ExchangeDto();
        }

        private async Task OpenConfigureNSECMExchangeModalAsync()
        {
            await ConfigureNSECMExchangeValidationsRef.ClearAll();

            ConfiguredNSECMExchange = new ConfigureExchangeDto();
            await ConfigureNSECMExchangeModal.Show();
        }

        private async Task ConfigureNSECMExchangeAsync()
        {
            if (await ConfigureNSECMExchangeValidationsRef.ValidateAll())
            {
                await ExchangeAppService.ConfigureAsync(ConfiguredNSECMExchange);
                await GetExchangesAsync();
                await ConfigureNSECMExchangeModal.Hide();
            }
        }

        private void CloseConfigureNSECMExchangeModalAsync()
        {
            ConfigureNSECMExchangeModal.Hide();
        }

        private async Task OpenConfigureNSEFNOExchangeModalAsync()
        {
            await ConfigureNSEFNOExchangeValidationsRef.ClearAll();

            ConfiguredNSEFNOExchange = new ConfigureExchangeDto();
            await ConfigureNSEFNOExchangeModal.Show();
        }

        private async Task ConfigureNSEFNOExchangeAsync()
        {
            if (await ConfigureNSEFNOExchangeValidationsRef.ValidateAll())
            {
                await ExchangeAppService.ConfigureAsync(ConfiguredNSEFNOExchange);
                await GetExchangesAsync();
                await ConfigureNSEFNOExchangeModal.Hide();
            }
        }

        private void CloseConfigureNSEFNOExchangeModalAsync()
        {
            ConfigureNSEFNOExchangeModal.Hide();
        }
    }
}
