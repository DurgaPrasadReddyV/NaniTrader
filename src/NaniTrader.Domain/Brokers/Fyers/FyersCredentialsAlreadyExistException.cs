using Volo.Abp;

namespace NaniTrader.Brokers.Fyers
{
    public class FyersCredentialsAlreadyExistException : BusinessException
    {
        public FyersCredentialsAlreadyExistException(string appId)
            : base(NaniTraderDomainErrorCodes.FyersCredentialsAlreadyExist)
        {
            WithData("AppId", appId);
        }
    }
}
