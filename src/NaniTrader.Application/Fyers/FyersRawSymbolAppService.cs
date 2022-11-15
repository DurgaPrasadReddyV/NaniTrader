using CsvHelper;
using Microsoft.AspNetCore.Authorization;
using NaniTrader.ApiClients;
using NaniTrader.Permissions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
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

        public async Task CreateAsync()
        {
            var stream = await _fyersPublicApiClient.DownloadSymbolsAsync("NSE_FO");
            using (var reader = new StreamReader(stream))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var records = csv.GetRecords<dynamic>();
            }
        }

        public async Task UpdateAsync()
        {
            await Task.CompletedTask;
        }

        public async Task DeleteAsync(Guid id)
        {
            await _fyersRawSymbolRepository.DeleteAsync(id);
        }
    }
}
