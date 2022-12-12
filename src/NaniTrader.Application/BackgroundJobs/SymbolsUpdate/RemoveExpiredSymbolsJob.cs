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

namespace NaniTrader.BackgroundJobs.SymbolsUpdate
{
    public class RemoveExpiredSymbolsJob : AsyncBackgroundJob<RemoveExpiredSymbolsArgs>, ITransientDependency
    {
        private readonly IFyersRawSymbolRepository _fyersRawSymbolRepository;
        private readonly FyersPublicApiClient _fyersPublicApiClient;

        public RemoveExpiredSymbolsJob(FyersPublicApiClient fyersPublicApiClient, IFyersRawSymbolRepository fyersRawSymbolRepository)
        {
            _fyersRawSymbolRepository = fyersRawSymbolRepository;
            _fyersPublicApiClient = fyersPublicApiClient;
        }

        public override async Task ExecuteAsync(RemoveExpiredSymbolsArgs args)
        {
            List<FyersRawSymbolDto> fyersRawSymbolDtos;
            var stream = await _fyersPublicApiClient.DownloadSymbolsAsync(args.Exchange);
            var csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture) { Delimiter = ",", HasHeaderRecord = false };
            using (var reader = new StreamReader(stream))
            using (var csv = new CsvReader(reader, csvConfig))
            {
                csv.Context.RegisterClassMap<FyersRawSymbolMap>();
                fyersRawSymbolDtos = csv.GetRecords<FyersRawSymbolDto>().ToList();
            }

            var symbolsFromDatabase = await _fyersRawSymbolRepository.GetListAsync(x => x.Exchange == args.Exchange);

            foreach (var symbolFromDatabase in symbolsFromDatabase)
            {
                var fyersRawSymbolDto = fyersRawSymbolDtos.FirstOrDefault(x => x.Column1 == symbolFromDatabase.Column1);
                if (fyersRawSymbolDto is null)
                    await _fyersRawSymbolRepository.DeleteAsync(symbolFromDatabase, true);
            }
        }
    }
}
