using FyersAPI;
using Scriban.Parsing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Websocket.Client;

namespace NaniTrader.SocketClients
{
    public class FyersOrderSocketClient
    {
        private IWebsocketClient _fyersOrderSocketClient;

        public FyersOrderSocketClient()
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
    }
}
