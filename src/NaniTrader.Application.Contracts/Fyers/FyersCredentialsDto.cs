using System;
using Volo.Abp.Application.Dtos;

namespace NaniTrader.Fyers
{
    public class FyersCredentialsDto : EntityDto<Guid>
    {
        public string AppId { get; set; }
        public string SecretId { get; set; }
        public string RedirectUri { get; set; }
        public DateTimeOffset TokenExpiration { get; set; }
    }
}
