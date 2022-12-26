using Volo.Abp.Application.Dtos;

namespace NaniTrader.Brokers.Fyers
{
    public class GetFyersSymbolListDto : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}
