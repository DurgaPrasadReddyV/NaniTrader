using Blazorise;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;
using NaniTrader.Fyers;
using System.Threading.Tasks;
using Blazorise.DataGrid;
using System.Linq;
using System;

namespace NaniTrader.Blazor.Pages.Brokers.Fyers
{
    public partial class OptionChain
    {
        private IReadOnlyList<FyersSymbolStrikeDto> FyersSymbolStrikeList { get; set; }
        private List<string> UnderlyingSymbols { get; set; } = new List<string>();
        private List<DateTimeOffset> UnderlyingSymbolExpiryDates { get; set; } = new List<DateTimeOffset>();
        private List<decimal> UnderlyingSymbolStrikes { get; set; } = new List<decimal>();
        private string UnderlyingSelectedValue { get; set; }
        private DateTimeOffset ExpiryDateSelectedValue { get; set; }
        private decimal StrikeSelectedValue { get; set; }
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
            UnderlyingSymbols = await FyersSymbolAppService.GetUnderlyingSymbolsAsync();
            UnderlyingSelectedValue = UnderlyingSymbols[0];
        }

        private async Task GetUnderlyingSymbolStrikesAsync(string underlying)
        {
            UnderlyingSymbolStrikes = await FyersSymbolAppService.GetStrikesAsync(underlying);
            StrikeSelectedValue = UnderlyingSymbolStrikes[0];
        }

        private async Task GetUnderlyingSymbolExpiryDatessAsync(string underlying)
        {
            UnderlyingSymbolExpiryDates = await FyersSymbolAppService.GetExpiryDatesAsync(underlying);
            ExpiryDateSelectedValue = UnderlyingSymbolExpiryDates[0];
        }

        private async Task OnUnderlyingSelectedValueChangedAsync(string value)
        {
            UnderlyingSelectedValue = value;
            await GetUnderlyingSymbolStrikesAsync(UnderlyingSelectedValue);
            await GetUnderlyingSymbolExpiryDatessAsync(UnderlyingSelectedValue);
        }

        private async Task OnStrikeSelectedValueChangedAsync(decimal value)
        {
            StrikeSelectedValue = value;
            await Task.CompletedTask;
        }

        private async Task OnExpiryDateSelectedValueChangedAsync(DateTimeOffset value)
        {
            ExpiryDateSelectedValue = value;
            await GetFyersSymbolStrikeAsync(UnderlyingSelectedValue);
        }

        private async Task GetFyersSymbolStrikeAsync(string underlyingSelectedValue)
        {
            var result = await FyersSymbolAppService.GetOptionChainAsync(underlyingSelectedValue);
        }

        private async Task OnDataGridReadAsync(DataGridReadDataEventArgs<FyersSymbolStrikeDto> e)
        {
            CurrentSorting = e.Columns
                .Where(c => c.SortDirection != SortDirection.Default)
                .Select(c => c.Field + (c.SortDirection == SortDirection.Descending ? " DESC" : ""))
                .JoinAsString(",");
            CurrentPage = e.Page - 1;

            await GetFyersSymbolStrikeAsync(UnderlyingSelectedValue);

            StateHasChanged();
        }
    }
}
