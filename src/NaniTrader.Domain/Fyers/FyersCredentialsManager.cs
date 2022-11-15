using JetBrains.Annotations;
using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;
using Volo.Abp;

namespace NaniTrader.Fyers
{
    public class FyersCredentialsManager : DomainService
    {
        private readonly IFyersCredentialsRepository _fyersCredentialsRepository;

        public FyersCredentialsManager(IFyersCredentialsRepository fyersCredentialsRepository)
        {
            _fyersCredentialsRepository = fyersCredentialsRepository;
        }

        public async Task<FyersCredentials> CreateAsync(
            [NotNull] string appId,
            [NotNull] string secretId,
            [NotNull] string redirectUri,
            Guid userId)
        {
            Check.NotNullOrWhiteSpace(appId, nameof(appId));

            var existingCredential = await _fyersCredentialsRepository.FindByAppIdAsync(appId);
            if (existingCredential != null)
            {
                throw new FyersCredentialsAlreadyExistException(appId);
            }

            return new FyersCredentials(
                GuidGenerator.Create(),
                appId,
                secretId,
                redirectUri,
                userId);
        }

        public void UpdateSecretIdAsync(
            FyersCredentials fyersCredentials,
            [NotNull] string secretId)
        {
            Check.NotNull(fyersCredentials, nameof(fyersCredentials));

            fyersCredentials.SecretId = secretId;

            fyersCredentials.Token = string.Empty;
            fyersCredentials.TokenExpiration = DateTime.UtcNow;
        }

        public void UpdateTokenAsync(
            FyersCredentials fyersCredentials,
            [NotNull] string token,
            [NotNull] DateTime tokenExpiration)
        {
            Check.NotNull(fyersCredentials, nameof(fyersCredentials));
            fyersCredentials.Token = token;
            fyersCredentials.TokenExpiration = tokenExpiration;
        }
    }
}
