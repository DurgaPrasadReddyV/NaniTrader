using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace NaniTrader.Fyers
{
    public class FyersCredentialsDto : EntityDto<Guid>
    {
        public string AppId { get; set; }
        public string SecretId { get; set; }
        public string RedirectUri { get; set; }
        public DateTime TokenExpiration { get; set; }
        public Guid UserId { get; set; }
    }
}
