using CsvHelper.Configuration;
using CsvHelper;
using NaniTrader.Fyers;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.DependencyInjection;
using NaniTrader.ApiClients;
using Volo.Abp.Domain.Repositories;
using NaniTrader.Exchanges;
using NaniTrader.Brokers.Fyers.Interfaces;

namespace NaniTrader.BackgroundJobs.SymbolsUpdate
{
    public class RemoveExpiredSymbolsJob : AsyncBackgroundJob<RemoveExpiredSymbolsArgs>, ITransientDependency
    {
        private readonly IFyersSymbolRepository _fyersSymbolRepository;
        private readonly FyersPublicApiClient _fyersPublicApiClient;

        public RemoveExpiredSymbolsJob(FyersPublicApiClient fyersPublicApiClient, IFyersSymbolRepository fyersSymbolRepository)
        {
            _fyersSymbolRepository = fyersSymbolRepository;
            _fyersPublicApiClient = fyersPublicApiClient;
        }

        public override async Task ExecuteAsync(RemoveExpiredSymbolsArgs args)
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
