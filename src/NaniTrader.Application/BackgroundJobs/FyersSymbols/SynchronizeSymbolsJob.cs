using CsvHelper.Configuration;
using CsvHelper;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.DependencyInjection;
using NaniTrader.ApiClients;
using System;
using NaniTrader.Exchanges;
using NaniTrader.Currencies;
using NaniTrader.Exchanges.Securities.Options;
using NaniTrader.Brokers.Fyers.Interfaces;
using NaniTrader.Brokers.Fyers;
using NaniTrader.CsvMappers;
using Hangfire;

namespace NaniTrader.BackgroundJobs.FyersSymbols
{
    [DisableConcurrentExecution(timeoutInSeconds: 10 * 60)]
    public class SynchronizeSymbolsJob : AsyncBackgroundJob<SynchronizeSymbolsArgs>, ITransientDependency
    {
        private readonly IFyersSymbolRepository _fyersSymbolRepository;
        private readonly FyersPublicApiClient _fyersPublicApiClient;

        public SynchronizeSymbolsJob(FyersPublicApiClient fyersPublicApiClient, IFyersSymbolRepository fyersSymbolRepository)
        {
            _fyersSymbolRepository = fyersSymbolRepository;
            _fyersPublicApiClient = fyersPublicApiClient;
        }

        public override async Task ExecuteAsync(SynchronizeSymbolsArgs args)
        {
            List<FyersSymbolCsvMap> fyersSymbolCsvMaps;
            var stream = await _fyersPublicApiClient.DownloadSymbolsAsync(args.Exchange);
            var csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture) { Delimiter = ",", HasHeaderRecord = false };
            using (var reader = new StreamReader(stream))
            using (var csv = new CsvReader(reader, csvConfig))
            {
                csv.Context.RegisterClassMap<FyersSymbolCsvMapper>();
                fyersSymbolCsvMaps = csv.GetRecords<FyersSymbolCsvMap>().ToList();
            }

            var exchange = ExchangeIdentifier.UNKNOWN;
            if (args.Exchange == "NSE_CM")
                exchange = ExchangeIdentifier.NSE_CM;
            if (args.Exchange == "NSE_FO")
                exchange = ExchangeIdentifier.NSE_FNO;

            foreach (var fyersSymbolCsvMap in fyersSymbolCsvMaps)
            {
                if (await _fyersSymbolRepository.FindAsync(x => x.SymbolLongId == fyersSymbolCsvMap.Column1) is not null)
                    continue;

                var optionRight = OptionRight.UNDEFINED;
                if (fyersSymbolCsvMap.Column17 == "CE")
                    optionRight = OptionRight.CE;
                if (fyersSymbolCsvMap.Column17 == "PE")
                    optionRight = OptionRight.PE;

                var symbolType = SymbolType.UNKNOWN;

                if (fyersSymbolCsvMap.Column3 == 0)
                    symbolType = SymbolType.EQUITY;
                if (fyersSymbolCsvMap.Column3 == 4)
                    symbolType = SymbolType.ETF;
                if (fyersSymbolCsvMap.Column3 == 10)
                    symbolType = SymbolType.INDEX;
                if (fyersSymbolCsvMap.Column3 == 11)
                    symbolType = SymbolType.INDEX_FUTURE;
                if (fyersSymbolCsvMap.Column3 == 13)
                    symbolType = SymbolType.EQUITY_FUTURE;
                if (fyersSymbolCsvMap.Column3 == 14)
                    symbolType = SymbolType.INDEX_OPTION;
                if (fyersSymbolCsvMap.Column3 == 15)
                    symbolType = SymbolType.EQUITY_OPTION;

                var updatedTime = DateTime.Parse(fyersSymbolCsvMap.Column8);
                var expiryTime = DateTimeOffset.FromUnixTimeSeconds(fyersSymbolCsvMap.Column9).UtcDateTime;
                var priceStep = new Currency("INR", fyersSymbolCsvMap.Column5);
                var strikePrice = new Currency("INR", fyersSymbolCsvMap.Column16);

                var fyersSymbol = new FyersSymbol(fyersSymbolCsvMap.Column13, fyersSymbolCsvMap.Column1, fyersSymbolCsvMap.Column15,
                    fyersSymbolCsvMap.Column18, fyersSymbolCsvMap.Column10, fyersSymbolCsvMap.Column14, fyersSymbolCsvMap.Column2,
                    exchange, symbolType, fyersSymbolCsvMap.Column4, priceStep, fyersSymbolCsvMap.Column6,fyersSymbolCsvMap.Column7,
                    updatedTime, expiryTime, strikePrice, optionRight, fyersSymbolCsvMap.Column11);

                await _fyersSymbolRepository.InsertAsync(fyersSymbol, true);
            }

            var symbolsFromDatabase = await _fyersSymbolRepository.GetListAsync(x => x.Exchange == exchange);

            foreach (var symbolFromDatabase in symbolsFromDatabase)
            {
                var fyersSymbolCsvMap = fyersSymbolCsvMaps.FirstOrDefault(x => x.Column1 == symbolFromDatabase.SymbolLongId);
                if (fyersSymbolCsvMap is null)
                    await _fyersSymbolRepository.DeleteAsync(symbolFromDatabase, true);
            }
        }
    }
}
