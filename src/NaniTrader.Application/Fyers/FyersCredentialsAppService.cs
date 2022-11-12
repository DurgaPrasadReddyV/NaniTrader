﻿using Microsoft.AspNetCore.Authorization;
using NaniTrader.ApiClients;
using NaniTrader.Permissions;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.ObjectMapping;

namespace NaniTrader.Fyers
{
    [Authorize(NaniTraderPermissions.FyersCredentials.Default)]
    public class FyersCredentialsAppService : NaniTraderAppService, IFyersCredentialsAppService
    {
        private readonly IFyersCredentialsRepository _fyersCredentialsRepository;
        private readonly FyersCredentialsManager _fyersCredentialsManager;
        private readonly FyersApiClient _fyersApiClient;

        public FyersCredentialsAppService(
            IFyersCredentialsRepository fyersCredentialsRepository,
            FyersCredentialsManager fyersCredentialsManager,
            FyersApiClient fyersApiClient)
        {
            _fyersCredentialsRepository = fyersCredentialsRepository;
            _fyersCredentialsManager = fyersCredentialsManager;
            _fyersApiClient = fyersApiClient;
        }

        public async Task<FyersCredentialsDto> GetAsync(Guid id)
        {
            var fyersCredentials = await _fyersCredentialsRepository.GetAsync(id);
            return ObjectMapper.Map<FyersCredentials, FyersCredentialsDto>(fyersCredentials);
        }

        public async Task<PagedResultDto<FyersCredentialsDto>> GetListAsync(GetFyersCredentialsListDto input)
        {
            if (input.Sorting.IsNullOrWhiteSpace())
            {
                input.Sorting = nameof(FyersCredentials.AppId);
            }

            var fyersCredentials = await _fyersCredentialsRepository.GetListAsync(
                input.SkipCount,
                input.MaxResultCount,
                input.Sorting,
                input.Filter
            );

            var totalCount = input.Filter == null
                ? await _fyersCredentialsRepository.CountAsync()
                : await _fyersCredentialsRepository.CountAsync(
                    fyersCredentials => fyersCredentials.AppId.Contains(input.Filter));

            return new PagedResultDto<FyersCredentialsDto>(
                totalCount,
                ObjectMapper.Map<List<FyersCredentials>, List<FyersCredentialsDto>>(fyersCredentials)
            );
        }

        public async Task<FyersCredentialsDto> CreateAsync(CreateFyersCredentialsDto input)
        {
            var fyersCredentials = await _fyersCredentialsManager.CreateAsync(
                input.AppId,
                input.SecretId,
                "https://localhost:44380/callback/fyers",
                CurrentUser.Id.Value
            );

            await _fyersCredentialsRepository.InsertAsync(fyersCredentials);

            return ObjectMapper.Map<FyersCredentials, FyersCredentialsDto>(fyersCredentials);
        }

        public async Task UpdateAsync(Guid id, UpdateFyersCredentialsDto input)
        {
            var author = await _fyersCredentialsRepository.GetAsync(id);
            _fyersCredentialsManager.UpdateSecretIdAsync(author, input.SecretId);
            await _fyersCredentialsRepository.UpdateAsync(author);
        }

        public async Task GenerateTokenAsync(string fyersApp, string authCode) //variable named fyersApp instead of appId to avoid id based endpoint
        {
            var fyersCredentials = await _fyersCredentialsRepository.FindByAppIdAsync(fyersApp);

            StringBuilder sb = new StringBuilder();
            using (SHA256 hash = SHA256.Create())
            {
                byte[] result = hash.ComputeHash(Encoding.UTF8.GetBytes($"{fyersCredentials.AppId}:{fyersCredentials.SecretId}"));

                foreach (var item in result)
                {
                    sb.Append(item.ToString("x2"));
                }
            }

            var check = sb.ToString();

            var tokenPayload = new FyersAPI.TokenPayload()
            {
                appIdHash = check,
                code = authCode,
                grant_type = "authorization_code"
            };

            var response = await _fyersApiClient.GenerateTokenAsync(tokenPayload);

            _fyersCredentialsManager.UpdateTokenAsync(fyersCredentials, response, DateTime.Now);

            await _fyersCredentialsRepository.UpdateAsync(fyersCredentials);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _fyersCredentialsRepository.DeleteAsync(id);
        }
    }
}