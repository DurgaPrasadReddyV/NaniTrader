using Microsoft.AspNetCore.Authorization;
using NaniTrader.ApiClients;
using NaniTrader.Brokers.Fyers;
using NaniTrader.Brokers.Fyers.Interfaces;
using NaniTrader.Permissions;
using NaniTrader.Settings;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Settings;

namespace NaniTrader.Exchanges
{
    [Authorize(NaniTraderPermissions.ExchangeSecurities.Default)]
    public class ExchangeSecuritiesAppService : NaniTraderAppService
    {

    }
}
