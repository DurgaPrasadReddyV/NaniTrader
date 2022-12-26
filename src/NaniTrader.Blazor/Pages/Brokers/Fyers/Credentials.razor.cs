using Blazorise;
using System;
using NaniTrader.Brokers.Fyers;
using System.Threading.Tasks;

namespace NaniTrader.Blazor.Pages.Brokers.Fyers
{
    public partial class Credentials
    {
        private FyersCredentialsDto FyersCredentials { get; set; }

        public Credentials()
        {
            FyersCredentials = new FyersCredentialsDto();
        }

        protected override async Task OnInitializedAsync()
        {
            FyersCredentials = await FyersCredentialsAppService.GetCurrentUserAsync();
            if (FyersCredentials is null)
                FyersCredentials = new FyersCredentialsDto();
        }

        private async Task GenerateFyersTokenAsync()
        {
            NavigationManager.NavigateTo($"https://api.fyers.in/api/v2/generate-authcode?client_id={FyersCredentials.AppId}&redirect_uri={FyersCredentials.RedirectUri}&response_type=code&state={FyersCredentials.AppId}");
            await Task.CompletedTask;
        }
    }
}
