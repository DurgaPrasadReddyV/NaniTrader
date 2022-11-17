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
    public partial class RawSymbols
    {
        private IReadOnlyList<FyersRawSymbolDto> FyersRawSymbolList { get; set; }

        private int PageSize { get; } = LimitedResultRequestDto.DefaultMaxResultCount;
        private int CurrentPage { get; set; }
        private string CurrentSorting { get; set; }
        private int TotalCount { get; set; }

        public RawSymbols()
        {
        }

        protected override async Task OnInitializedAsync()
        {
            await GetFyersRawSymbolsAsync();
        }

        private async Task GetFyersRawSymbolsAsync()
        {
            var result = await FyersRawSymbolAppService.GetListAsync(new GetFyersRawSymbolListDto
                                        {
                                            MaxResultCount = PageSize,
                                            SkipCount = CurrentPage * PageSize,
                                            Sorting = CurrentSorting
                                        });

            FyersRawSymbolList = result.Items;
            TotalCount = (int)result.TotalCount;
        }

        private async Task OnDataGridReadAsync(DataGridReadDataEventArgs<FyersRawSymbolDto> e)
        {
            CurrentSorting = e.Columns
                .Where(c => c.SortDirection != SortDirection.Default)
                .Select(c => c.Field + (c.SortDirection == SortDirection.Descending ? " DESC" : ""))
                .JoinAsString(",");
            CurrentPage = e.Page - 1;

            await GetFyersRawSymbolsAsync();

            StateHasChanged();
        }

        private async Task LoadNewSymbolsAsync()
        {
            await FyersRawSymbolAppService.DownloadNewSymbolsAsync();
        }
    }
}
