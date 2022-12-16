using Blazorise;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;
using NaniTrader.Fyers;
using System.Threading.Tasks;
using Blazorise.DataGrid;
using System.Linq;

namespace NaniTrader.Blazor.Pages.Brokers.Fyers
{
    public partial class Symbols
    {
        private IReadOnlyList<FyersSymbolDto> FyersSymbolList { get; set; }

        private int PageSize { get; } = LimitedResultRequestDto.DefaultMaxResultCount;
        private int CurrentPage { get; set; }
        private string CurrentSorting { get; set; }
        private string SearchFilter { get; set; }
        private int TotalCount { get; set; }

        private bool IsTaskRunning = false;

        public Symbols()
        {
        }

        protected override async Task OnInitializedAsync()
        {
            await GetFyersSymbolsAsync();
        }

        private async Task GetFyersSymbolsAsync()
        {
            var result = await FyersSymbolAppService.GetListAsync(new GetFyersSymbolListDto
                                        {
                                            MaxResultCount = PageSize,
                                            SkipCount = CurrentPage * PageSize,
                                            Sorting = CurrentSorting,
                                            Filter  = SearchFilter
                                        });

            FyersSymbolList = result.Items;
            TotalCount = (int)result.TotalCount;
        }

        private async Task OnDataGridReadAsync(DataGridReadDataEventArgs<FyersSymbolDto> e)
        {
            CurrentSorting = e.Columns
                .Where(c => c.SortDirection != SortDirection.Default)
                .Select(c => c.Field + (c.SortDirection == SortDirection.Descending ? " DESC" : ""))
                .JoinAsString(",");
            CurrentPage = e.Page - 1;

            var search = e.Columns.FirstOrDefault(c => c.SearchValue != null && c.Field == "Description");
            if (search != null) this.SearchFilter = search.SearchValue.ToString();

            await GetFyersSymbolsAsync();

            StateHasChanged();
        }

        private async Task LoadNewSymbolsAsync()
        {
            await FyersSymbolAppService.DownloadNewSymbolsAsync();
            IsTaskRunning = true;
        }

        private async Task UpdateExistingSymbolsAsync()
        {
            await FyersSymbolAppService.UpdateExistingSymbolsAsync();
            IsTaskRunning = true;
        }

        private async Task RemoveExpiredSymbolsAsync()
        {
            await FyersSymbolAppService.DeleteExpiredSymbolsAsync();
            IsTaskRunning = true;
        }
    }
}
