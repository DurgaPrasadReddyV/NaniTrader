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

namespace NaniTrader.BackgroundJobs.SymbolsUpdate
{
    public class UpdateExistingSymbolsJob : AsyncBackgroundJob<UpdateExistingSymbolsArgs>, ITransientDependency
    {
        private readonly IFyersRawSymbolRepository _fyersRawSymbolRepository;
        private readonly FyersPublicApiClient _fyersPublicApiClient;

        public UpdateExistingSymbolsJob(FyersPublicApiClient fyersPublicApiClient, IFyersRawSymbolRepository fyersRawSymbolRepository)
        {
            _fyersRawSymbolRepository = fyersRawSymbolRepository;
            _fyersPublicApiClient = fyersPublicApiClient;
        }

        public override async Task ExecuteAsync(UpdateExistingSymbolsArgs args)
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

            foreach (var fyersRawSymbolDto in fyersRawSymbolDtos)
            {
                var fyersRawSymbol = new FyersRawSymbol(
                    args.Exchange,
                    fyersRawSymbolDto.Column1,
                    fyersRawSymbolDto.Column2,
                    fyersRawSymbolDto.Column3,
                    fyersRawSymbolDto.Column4,
                    fyersRawSymbolDto.Column5,
                    fyersRawSymbolDto.Column6,
                    fyersRawSymbolDto.Column7,
                    fyersRawSymbolDto.Column8,
                    fyersRawSymbolDto.Column9,
                    fyersRawSymbolDto.Column10,
                    fyersRawSymbolDto.Column11,
                    fyersRawSymbolDto.Column12,
                    fyersRawSymbolDto.Column13,
                    fyersRawSymbolDto.Column14,
                    fyersRawSymbolDto.Column15,
                    fyersRawSymbolDto.Column16,
                    fyersRawSymbolDto.Column17,
                    fyersRawSymbolDto.Column18);

                var symbolFromDatabase = await _fyersRawSymbolRepository.FindAsync(x => x.Column1 == fyersRawSymbol.Column1);

                if (symbolFromDatabase is not null)
                    await _fyersRawSymbolRepository.UpdateAsync(fyersRawSymbol, true);
            }
        }
    }
}
