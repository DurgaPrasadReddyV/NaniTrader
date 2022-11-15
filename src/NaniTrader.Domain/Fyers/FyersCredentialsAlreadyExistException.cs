using Volo.Abp;

namespace NaniTrader.Fyers
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
