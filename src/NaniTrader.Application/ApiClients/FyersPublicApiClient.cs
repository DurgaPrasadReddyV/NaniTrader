namespace NaniTrader.ApiClients;

using System.IO;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using FyersAPI;

public class FyersPublicApiClient
{
    private readonly HttpClient _httpClient;

    public FyersPublicApiClient(HttpClient httpClient) => this._httpClient = httpClient;

    public async Task<Stream> DownloadSymbolsAsync(string exchange)
    {
        var response = await _httpClient.GetAsync($"/sym_details/{exchange.Trim()}.csv").ConfigureAwait(false);

        return await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
    }
}
