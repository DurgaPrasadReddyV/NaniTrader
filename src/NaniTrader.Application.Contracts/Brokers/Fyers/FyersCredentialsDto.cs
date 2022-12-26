using System;
using Volo.Abp.Application.Dtos;

namespace NaniTrader.Brokers.Fyers
{
    public class FyersCredentialsDto : EntityDto<Guid>
    {
        public string AppId { get; set; }
        public string SecretId { get; set; }
        public string RedirectUri { get; set; }
        public DateTime TokenExpiration { get; set; }
    }
}
