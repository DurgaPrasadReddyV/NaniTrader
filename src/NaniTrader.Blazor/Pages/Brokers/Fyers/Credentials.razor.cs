using Blazorise;
using System;
using NaniTrader.Fyers;
using System.Threading.Tasks;

namespace NaniTrader.Blazor.Pages.Brokers.Fyers
{
    public partial class Credentials
    {
        private FyersCredentialsDto FyersCredentials { get; set; }
        private CreateFyersCredentialsDto NewFyersCredentials { get; set; }
        private Guid EditingFyersCredentialsId { get; set; }
        private UpdateFyersCredentialsDto EditingFyersCredentials { get; set; }

        private Modal CreateFyersCredentialsModal { get; set; }
        private Modal EditFyersCredentialsModal { get; set; }

        private Validations CreateValidationsRef;
        private Validations EditValidationsRef;

        public Credentials()
        {
            NewFyersCredentials = new CreateFyersCredentialsDto();
            EditingFyersCredentials = new UpdateFyersCredentialsDto();
            FyersCredentials = new FyersCredentialsDto();
        }

        protected override async Task OnInitializedAsync()
        {
            await GetFyersCredentialsAsync();
        }

        private async Task GetFyersCredentialsAsync()
        {
            FyersCredentials = await FyersCredentialsAppService.GetCurrentUserAsync();
            StateHasChanged();
        }

        private void OpenCreateFyersCredentialsModal()
        {
            CreateValidationsRef.ClearAll();

            NewFyersCredentials = new CreateFyersCredentialsDto();
            CreateFyersCredentialsModal.Show();
        }

        private async Task CreateFyersCredentialsAsync()
        {
            if (await CreateValidationsRef.ValidateAll())
            {
                await FyersCredentialsAppService.CreateAsync(NewFyersCredentials);
                await GetFyersCredentialsAsync();
                await CreateFyersCredentialsModal.Hide();
            }
        }

        private void CloseCreateFyersCredentialsModal()
        {
            CreateFyersCredentialsModal.Hide();
        }

        private async Task OpenEditFyersCredentialsModal()
        {
            var fyersCredentials = await FyersCredentialsAppService.GetCurrentUserAsync();
            await EditValidationsRef.ClearAll();

            EditingFyersCredentialsId = fyersCredentials.Id;
            EditingFyersCredentials = ObjectMapper.Map<FyersCredentialsDto, UpdateFyersCredentialsDto>(fyersCredentials);
            await EditFyersCredentialsModal.Show();
        }

        private async Task UpdateFyersCredentialsAsync()
        {
            if (await EditValidationsRef.ValidateAll())
            {
                await FyersCredentialsAppService.UpdateAsync(EditingFyersCredentialsId, EditingFyersCredentials);
                await GetFyersCredentialsAsync();
                await EditFyersCredentialsModal.Hide();
            }
        }

        private void CloseEditFyersCredentialsModal()
        {
            EditFyersCredentialsModal.Hide();
        }

        private async Task DeleteFyersCredentialsAsync()
        {
            var fyersCredentials = await FyersCredentialsAppService.GetCurrentUserAsync();
            var confirmMessage = L["FyersCredentialsDeletionConfirmationMessage", fyersCredentials.AppId];
            if (!await Message.Confirm(confirmMessage))
            {
                return;
            }

            await FyersCredentialsAppService.DeleteAsync(fyersCredentials.Id);
            await GetFyersCredentialsAsync();
        }

        private async Task GenerateFyersTokenAsync()
        {
            NavigationManager.NavigateTo($"https://api.fyers.in/api/v2/generate-authcode?client_id={FyersCredentials.AppId}&redirect_uri={FyersCredentials.RedirectUri}&response_type=code&state={FyersCredentials.AppId}");
            await Task.CompletedTask;
        }
    }
}
