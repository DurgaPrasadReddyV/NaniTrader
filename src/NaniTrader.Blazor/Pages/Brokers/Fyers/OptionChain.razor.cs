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
    public partial class OptionChain
    {
        private IReadOnlyList<FyersRawSymbolStrikeDto> FyersRawSymbolStrikeList { get; set; }
        private List<string> UnderlyingSymbols { get; set; } = new List<string>();
        private List<string> SelectedUnderlyingExpiryDates { get; set; } = new List<string>();
        private List<string> SelectedUnderlyingStrikes { get; set; } = new List<string>();
        private string UnderlyingSelectedValue { get; set; }
        private string ExpiryDateSelectedValue { get; set; }
        private string StrikeSelectedValue { get; set; }
        private int PageSize { get; } = LimitedResultRequestDto.DefaultMaxResultCount;
        private int CurrentPage { get; set; }
        private string CurrentSorting { get; set; }
        private int TotalCount { get; set; }

        public OptionChain()
        {
        }

        protected override async Task OnInitializedAsync()
        {
            await GetUnderlyingSymbolsAsync();
            await GetUnderlyingSymbolStrikesAsync(UnderlyingSelectedValue);
            await GetUnderlyingSymbolExpiryDatessAsync(UnderlyingSelectedValue);
        }

        private async Task GetUnderlyingSymbolsAsync()
        {
            UnderlyingSymbols = await FyersRawSymbolAppService.GetUnderlyingSymbolsAsync();
            UnderlyingSelectedValue = UnderlyingSymbols[0];
        }

        private async Task GetUnderlyingSymbolStrikesAsync(string underlying)
        {
            SelectedUnderlyingStrikes = await FyersRawSymbolAppService.GetStrikesAsync(underlying);
            StrikeSelectedValue = SelectedUnderlyingStrikes[0];
        }

        private async Task GetUnderlyingSymbolExpiryDatessAsync(string underlying)
        {
            SelectedUnderlyingExpiryDates = await FyersRawSymbolAppService.GetExpiryDatesAsync(underlying);
            ExpiryDateSelectedValue = SelectedUnderlyingExpiryDates[0];
        }

        private async Task OnUnderlyingSelectedValueChangedAsync(string value)
        {
            UnderlyingSelectedValue = value;
            await GetUnderlyingSymbolStrikesAsync(UnderlyingSelectedValue);
            await GetUnderlyingSymbolExpiryDatessAsync(UnderlyingSelectedValue);
        }

        private async Task OnStrikeSelectedValueChangedAsync(string value)
        {
            StrikeSelectedValue = value;
            await Task.CompletedTask;
        }

        private async Task OnExpiryDateSelectedValueChangedAsync(string value)
        {
            ExpiryDateSelectedValue = value;
            await GetFyersRawSymbolStrikeAsync(UnderlyingSelectedValue, ExpiryDateSelectedValue);
        }

        private async Task GetFyersRawSymbolStrikeAsync(string underlyingSelectedValue, string expiryDateSelectedValue)
        {
            var result = await FyersRawSymbolAppService.GetOptionChainForExpiryAsync(underlyingSelectedValue, expiryDateSelectedValue);
            FyersRawSymbolStrikeList = result;
            TotalCount = (int)result.Count;
        }

        private async Task OnDataGridReadAsync(DataGridReadDataEventArgs<FyersRawSymbolStrikeDto> e)
        {
            CurrentSorting = e.Columns
                .Where(c => c.SortDirection != SortDirection.Default)
                .Select(c => c.Field + (c.SortDirection == SortDirection.Descending ? " DESC" : ""))
                .JoinAsString(",");
            CurrentPage = e.Page - 1;

            await GetFyersRawSymbolStrikeAsync(UnderlyingSelectedValue, ExpiryDateSelectedValue);

            StateHasChanged();
        }
    }
}
