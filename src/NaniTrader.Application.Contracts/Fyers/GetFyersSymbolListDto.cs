using Volo.Abp.Application.Dtos;

namespace NaniTrader.Fyers
{
    public class GetFyersSymbolListDto : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}
