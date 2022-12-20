using JetBrains.Annotations;
using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace NaniTrader.Brokers.Fyers
{
    public class FyersCredentials : FullAuditedAggregateRoot<Guid>
    {
        public string AppId { get; private set; }
        public string SecretId { get; internal set; }
        public string Token { get; internal set; }
        public string RedirectUri { get; private set; }
        public DateTime TokenExpiration { get; internal set; }
        public Guid UserId { get; private set; }

        private FyersCredentials()
        {
            /* This constructor is for deserialization / ORM purpose */
        }

        internal FyersCredentials(
            Guid id,
            [NotNull] string appId,
            [NotNull] string secretId,
            [NotNull] string redirectUri,
            Guid userId)
            : base(id)
        {
            AppId = appId;
            SecretId = secretId;
            RedirectUri = redirectUri;
            UserId = userId;
        }
    }
}
