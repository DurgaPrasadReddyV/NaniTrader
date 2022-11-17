using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.AspNetCore.Authorization;
using NaniTrader.ApiClients;
using NaniTrader.Permissions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;

namespace NaniTrader.Fyers
{
    [Authorize(NaniTraderPermissions.FyersRawSymbols.Default)]
    public class FyersRawSymbolAppService : NaniTraderAppService, IFyersRawSymbolAppService
    {
        private readonly IFyersRawSymbolRepository _fyersRawSymbolRepository;
        private readonly FyersPublicApiClient _fyersPublicApiClient;

        public FyersRawSymbolAppService(
            IFyersRawSymbolRepository fyersRawSymbolRepository,
            FyersPublicApiClient fyersPublicApiClient)
        {
            _fyersRawSymbolRepository = fyersRawSymbolRepository;
            _fyersPublicApiClient = fyersPublicApiClient;
        }

        public async Task<FyersRawSymbolDto> GetAsync(Guid id)
        {
            var fyersRawSymbol = await _fyersRawSymbolRepository.GetAsync(id);
            return ObjectMapper.Map<FyersRawSymbol, FyersRawSymbolDto>(fyersRawSymbol);
        }

        public async Task<PagedResultDto<FyersRawSymbolDto>> GetListAsync(GetFyersRawSymbolListDto input)
        {
            if (input.Sorting.IsNullOrWhiteSpace())
            {
                input.Sorting = nameof(FyersRawSymbol.Exchange);
            }

            var fyersRawSymbols = await _fyersRawSymbolRepository.GetListAsync(
                input.SkipCount,
                input.MaxResultCount,
                input.Sorting,
                input.Filter
            );

            var totalCount = input.Filter == null
                ? await _fyersRawSymbolRepository.CountAsync()
                : await _fyersRawSymbolRepository.CountAsync(fyersCredentials => fyersCredentials.Exchange.Contains(input.Filter));

            return new PagedResultDto<FyersRawSymbolDto>(
                totalCount,
                ObjectMapper.Map<List<FyersRawSymbol>, List<FyersRawSymbolDto>>(fyersRawSymbols)
            );
        }

        public async Task CheckSymbolsAsync()
        {
            await Task.CompletedTask;
        }

        public async Task DownloadNewSymbolsAsync()
        {
            var stream = await _fyersPublicApiClient.DownloadSymbolsAsync("NSE_FO");
            var csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture) { Delimiter = ",", HasHeaderRecord = false };

            using (var reader = new StreamReader(stream))
            using (var csv = new CsvReader(reader, csvConfig))
            {
                csv.Context.RegisterClassMap<FyersRawSymbolMap>();
                var fyersRawSymbolDtos = csv.GetRecords<FyersRawSymbolDto>().ToList();
                var fyersRawSymbols = new List<FyersRawSymbol>();
                foreach (var fyersRawSymbolDto in fyersRawSymbolDtos)
                {
                    var fyersRawSymbol = new FyersRawSymbol(Guid.NewGuid(),
                        "NSEFO",
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
                    fyersRawSymbols.Add(fyersRawSymbol);

                }
                await _fyersRawSymbolRepository.InsertManyAsync(fyersRawSymbols);
            }
        }

        public async Task UpdateExistingSymbolsAsync()
        {
            await Task.CompletedTask;
        }

        public async Task DeleteExpiredSymbolsAsync()
        {
            await Task.CompletedTask;
        }
    }
}
