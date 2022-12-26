using JetBrains.Annotations;
using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;
using Volo.Abp;
using NaniTrader.Brokers.Fyers.Interfaces;
using NaniTrader.Exchanges.Interfaces;
using NaniTrader.Exchanges.Securities;

namespace NaniTrader.Exchanges
{
    public class ExchangeManager : DomainService
    {
        public Exchange Configure(ExchangeIdentifier exchangeIdentifier, SecuritiesProvider securitiesProvider ,string description)
        {
            return new Exchange(GuidGenerator.Create(), exchangeIdentifier, securitiesProvider, description);
        }
    }
}
