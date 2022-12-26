using Scriban.Parsing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Websocket.Client;

namespace NaniTrader.SocketClients
{
    public class FyersDataSocketClient
    {
        private IWebsocketClient _fyersDataSocketClient;

        public FyersDataSocketClient()
        {

        }

        public async Task ConnectAsync()
        {
            await Task.CompletedTask;
        }

        public async Task ForceReconnectAsync()
        {
            await Task.CompletedTask;
        }

        public async Task DisconnectAsync()
        {
            await Task.CompletedTask;
        }

        public async Task SubscribeLevel1Symbols(IEnumerable<string> symbols)
        {
            await Task.CompletedTask;
        }

        public async Task UnSubscribeLevel1Symbols(IEnumerable<string> symbols)
        {
            await Task.CompletedTask;
        }

        public async Task SubscribeLevel2Symbols(IEnumerable<string> symbols)
        {
            await Task.CompletedTask;
        }

        public async Task UnSubscribeLevel2Symbols(IEnumerable<string> symbols)
        {
            await Task.CompletedTask;
        }

    }
}
