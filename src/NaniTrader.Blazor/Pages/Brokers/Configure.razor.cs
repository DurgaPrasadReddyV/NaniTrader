using Blazorise;
using NaniTrader.Brokers.Fyers;
using System.Threading.Tasks;
using static NaniTrader.Permissions.NaniTraderPermissions;

namespace NaniTrader.Blazor.Pages.Brokers
{
    public partial class Configure
    {
        private FyersCredentialsDto FyersCredentials { get; set; }
        private ConfigureFyersCredentialsDto ConfiguredFyersCredentials { get; set; }
        private Modal ConfigureFyersCredentialsModal { get; set; }
        private Validations ConfigureFyersCredentialsValidationsRef;

        public Configure()
        {
            FyersCredentials = new FyersCredentialsDto();
            ConfiguredFyersCredentials = new ConfigureFyersCredentialsDto();
        }

        protected override async Task OnInitializedAsync()
        {
            await GetFyersCredentialsAsync();
        }

        private async Task GetFyersCredentialsAsync()
        {
            FyersCredentials = await FyersCredentialsAppService.GetCurrentUserAsync();
            if (FyersCredentials is null)
                FyersCredentials = new FyersCredentialsDto();
        }

        private async Task OpenConfigureFyersCredentialsModalAsync()
        {
            await ConfigureFyersCredentialsValidationsRef.ClearAll();

            ConfiguredFyersCredentials = new ConfigureFyersCredentialsDto();
            await ConfigureFyersCredentialsModal.Show();
        }

        private async Task ConfigureFyersCredentialsAsync()
        {
            if (await ConfigureFyersCredentialsValidationsRef.ValidateAll())
            {
                await FyersCredentialsAppService.ConfigureAsync(ConfiguredFyersCredentials);
                await GetFyersCredentialsAsync();
                await ConfigureFyersCredentialsModal.Hide();
            }
        }

        private void CloseConfigureFyersCredentialsModalAsync()
        {
            ConfigureFyersCredentialsModal.Hide();
        }
    }
}
