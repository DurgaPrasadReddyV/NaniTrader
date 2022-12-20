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
using System;
using NaniTrader.Brokers.Fyers.Interfaces;

namespace NaniTrader.BackgroundJobs.SymbolsUpdate
{
    public class UpdateExistingSymbolsJob : AsyncBackgroundJob<UpdateExistingSymbolsArgs>, ITransientDependency
    {
        private readonly IFyersSymbolRepository _fyersSymbolRepository;
        private readonly FyersPublicApiClient _fyersPublicApiClient;

        public UpdateExistingSymbolsJob(FyersPublicApiClient fyersPublicApiClient, IFyersSymbolRepository fyersSymbolRepository)
        {
            _fyersSymbolRepository = fyersSymbolRepository;
            _fyersPublicApiClient = fyersPublicApiClient;
        }

        public override async Task ExecuteAsync(UpdateExistingSymbolsArgs args)
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

            //TODO
            throw new NotImplementedException();
        }
    }
}
