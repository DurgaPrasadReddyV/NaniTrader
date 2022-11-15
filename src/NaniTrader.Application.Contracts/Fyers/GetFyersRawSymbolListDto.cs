using Volo.Abp.Application.Dtos;

namespace NaniTrader.Fyers
{
    public class GetFyersRawSymbolListDto : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}
