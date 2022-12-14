using Microsoft.AspNetCore.Mvc;
using NaniTrader.Brokers.Fyers;
using System.Threading.Tasks;

namespace NaniTrader.Controllers
{
    [Route("callback")]
    public class CallbackController : NaniTraderController
    {
        private readonly IFyersCredentialsAppService _fyersCredentialsAppService;
        public CallbackController(IFyersCredentialsAppService fyersCredentialsAppService)
        {
            _fyersCredentialsAppService = fyersCredentialsAppService;
        }

        [HttpGet]
        [Route("fyers")]
        public async Task<ActionResult> Fyers(string s,string code,string auth_code, string state)
        {
            await _fyersCredentialsAppService.GenerateTokenAsync(state, auth_code);
            return LocalRedirect("/brokers/fyers/credentials");
        }
    }
}
