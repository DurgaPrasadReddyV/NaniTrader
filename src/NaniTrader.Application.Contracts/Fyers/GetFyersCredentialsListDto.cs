using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace NaniTrader.Fyers
{
    public class GetFyersCredentialsListDto : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}
