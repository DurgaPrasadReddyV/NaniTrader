using Blazorise;
using System.Collections.Generic;
using System;
using Volo.Abp.Application.Dtos;
using NaniTrader.Fyers;
using System.Threading.Tasks;
using Blazorise.DataGrid;
using System.Linq;

namespace NaniTrader.Blazor.Pages.Brokers.Fyers
{
    public partial class Credentials
    {
        private IReadOnlyList<FyersCredentialsDto> FyersCredentialsList { get; set; }

        private int PageSize { get; } = LimitedResultRequestDto.DefaultMaxResultCount;
        private int CurrentPage { get; set; }
        private string CurrentSorting { get; set; }
        private int TotalCount { get; set; }

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
        }

        protected override async Task OnInitializedAsync()
        {
            await GetFyersCredentialsAsync();
        }

        private async Task GetFyersCredentialsAsync()
        {
            var result = await FyersCredentialsAppService.GetListAsync(new GetFyersCredentialsListDto
                                        {
                                            MaxResultCount = PageSize,
                                            SkipCount = CurrentPage * PageSize,
                                            Sorting = CurrentSorting
                                        });

            FyersCredentialsList = result.Items;
            TotalCount = (int)result.TotalCount;
        }

        private async Task OnDataGridReadAsync(DataGridReadDataEventArgs<FyersCredentialsDto> e)
        {
            CurrentSorting = e.Columns
                .Where(c => c.SortDirection != SortDirection.Default)
                .Select(c => c.Field + (c.SortDirection == SortDirection.Descending ? " DESC" : ""))
                .JoinAsString(",");
            CurrentPage = e.Page - 1;

            await GetFyersCredentialsAsync();

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

        private void OpenEditFyersCredentialsModal(FyersCredentialsDto fyersCredentials)
        {
            EditValidationsRef.ClearAll();

            EditingFyersCredentialsId = fyersCredentials.Id;
            EditingFyersCredentials = ObjectMapper.Map<FyersCredentialsDto, UpdateFyersCredentialsDto>(fyersCredentials);
            EditFyersCredentialsModal.Show();
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

        private async Task DeleteFyersCredentialsAsync(FyersCredentialsDto fyersCredentials)
        {
            var confirmMessage = L["FyersCredentialsDeletionConfirmationMessage", fyersCredentials.AppId];
            if (!await Message.Confirm(confirmMessage))
            {
                return;
            }

            await FyersCredentialsAppService.DeleteAsync(fyersCredentials.Id);
            await GetFyersCredentialsAsync();
        }

        private async Task GenerateFyersTokenAsync(FyersCredentialsDto fyersCredentials)
        {
            NavigationManager.NavigateTo($"https://api.fyers.in/api/v2/generate-authcode?client_id={fyersCredentials.AppId}&redirect_uri={fyersCredentials.RedirectUri}&response_type=code&state={fyersCredentials.AppId}");
            await Task.CompletedTask;
        }
    }
}
