using Microsoft.AspNetCore.Authorization;
using NaniTrader.ApiClients;
using NaniTrader.Brokers.Fyers;
using NaniTrader.Brokers.Fyers.Interfaces;
using NaniTrader.Permissions;
using NaniTrader.Settings;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Settings;

namespace NaniTrader.Brokers.Fyers
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

        public async Task<FyersCredentialsDto> GetCurrentUserAsync()
        {
            var fyersCurrentUserCredentials = await _fyersCredentialsRepository.FindAsync(x => x.UserId == CurrentUser.Id.Value);
            return ObjectMapper.Map<FyersCredentials, FyersCredentialsDto>(fyersCurrentUserCredentials);
        }

        public async Task<FyersCredentialsDto> ConfigureAsync(ConfigureFyersCredentialsDto input)
        {
            var fyersCurrentUserCredentials = await _fyersCredentialsRepository.FindAsync(x => x.UserId == CurrentUser.Id.Value);
            
            if (fyersCurrentUserCredentials is not null)
                throw new InvalidOperationException("Account already exists.");

            var fyersCredentials = await _fyersCredentialsManager.ConfigureAsync(
                input.AppId,
                input.SecretId,
                await SettingProvider.GetOrNullAsync(NaniTraderSettings.Brokers.Fyers.RedirectUri),
                CurrentUser.Id.Value
            );

            await _fyersCredentialsRepository.InsertAsync(fyersCredentials);

            return ObjectMapper.Map<FyersCredentials, FyersCredentialsDto>(fyersCredentials);
        }

        public async Task ReconfigureAsync(Guid id, ReconfigureFyersCredentialsDto input)
        {
            var fyersCredentials = await _fyersCredentialsRepository.GetAsync(id);
            _fyersCredentialsManager.UpdateSecretIdAsync(fyersCredentials, input.SecretId);
            await _fyersCredentialsRepository.UpdateAsync(fyersCredentials);
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

            _fyersCredentialsManager.UpdateTokenAsync(fyersCredentials, response.access_token, GetTokenExpirationTime(response.access_token));

            await _fyersCredentialsRepository.UpdateAsync(fyersCredentials);
        }

        public async Task RemoveAsync(Guid id)
        {
            await _fyersCredentialsRepository.DeleteAsync(id);
        }

        private DateTime GetTokenExpirationTime(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(token);
            var tokenExp = jwtSecurityToken.Claims.First(claim => claim.Type.Equals("exp")).Value;
            var ticks = long.Parse(tokenExp);
            return DateTimeOffset.FromUnixTimeSeconds(ticks).UtcDateTime;
        }
    }
}
