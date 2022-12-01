using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.AspNetCore.Authorization;
using NaniTrader.ApiClients;
using NaniTrader.BackgroundJobs.SymbolsUpdate;
using NaniTrader.Permissions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Domain.Repositories;

namespace NaniTrader.Fyers
{
    [Authorize(NaniTraderPermissions.FyersRawSymbols.Default)]
    public class FyersRawSymbolAppService : NaniTraderAppService, IFyersRawSymbolAppService
    {
        private readonly IFyersRawSymbolRepository _fyersRawSymbolRepository;
        private readonly IBackgroundJobManager _backgroundJobManager;

        public FyersRawSymbolAppService(IFyersRawSymbolRepository fyersRawSymbolRepository, IBackgroundJobManager backgroundJobManager)
        {
            _fyersRawSymbolRepository = fyersRawSymbolRepository;
            _backgroundJobManager = backgroundJobManager;
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

            var fyersRawSymbols = await _fyersRawSymbolRepository.GetListAsync(input.SkipCount, input.MaxResultCount, input.Sorting, input.Filter);

            var totalCount = input.Filter == null
                ? await _fyersRawSymbolRepository.CountAsync()
                : await _fyersRawSymbolRepository.CountAsync(fyersCredentials => fyersCredentials.Exchange.Contains(input.Filter));

            return new PagedResultDto<FyersRawSymbolDto>(totalCount, ObjectMapper.Map<List<FyersRawSymbol>, List<FyersRawSymbolDto>>(fyersRawSymbols));
        }

        public async Task CheckSymbolsAsync()
        {
            await Task.CompletedTask;
        }

        public async Task DownloadNewSymbolsAsync()
        {
            await _backgroundJobManager.EnqueueAsync(new DownloadNewSymbolsArgs { Exchange = "NSE_FO" });
            await _backgroundJobManager.EnqueueAsync(new DownloadNewSymbolsArgs { Exchange = "NSE_CM" });
        }

        public async Task UpdateExistingSymbolsAsync()
        {
            await Task.CompletedTask;
        }

        public async Task DeleteExpiredSymbolsAsync()
        {
            await Task.CompletedTask;
        }

        public async Task<List<string>> GetUnderlyingSymbolsAsync()
        {
            return await _fyersRawSymbolRepository.GetUnderlyingSymbolsAsync();
        }

        public async Task<List<string>> GetExpiryDatesAsync(string underlyingSymbol)
        {
            List<string> expiryDates = await _fyersRawSymbolRepository.GetExpiryDatesAsync(underlyingSymbol);
            return expiryDates;

        }

        public async Task<List<string>> GetStrikesAsync(string underlyingSymbol)
        {
            List<string> strikes = await _fyersRawSymbolRepository.GetStrikesAsync(underlyingSymbol);
            return strikes;
        }

        public async Task<List<FyersRawSymbolStrikeDto>> GetOptionChainForExpiryAsync(string underlyingSymbol, string expiry)
        {
            var strikes = new List<FyersRawSymbolStrikeDto>();
            var CEStrikes = await _fyersRawSymbolRepository.GetCESymbolsForExpiryAsync(underlyingSymbol, expiry);
            var PEStrikes = await _fyersRawSymbolRepository.GetCESymbolsForExpiryAsync(underlyingSymbol, expiry);

            strikes = CEStrikes.Select( x => new FyersRawSymbolStrikeDto() {CESymbol =  x.Column10,StrikePrice = x.Column16}).ToList();
            strikes.ForEach(x =>
            {
                x.PESymbol = PEStrikes.Where(y => y.Column16 == x.StrikePrice).First().Column10;
            });

            return strikes;
        }
    }
}
