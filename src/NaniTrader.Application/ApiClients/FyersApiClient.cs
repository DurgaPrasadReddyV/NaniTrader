namespace NaniTrader.ApiClients;

using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FyersAPI;
using Newtonsoft.Json;

public class FyersApiClient
{
    private readonly HttpClient _httpClient;

    public FyersApiClient(HttpClient httpClient) => this._httpClient = httpClient;

    public async Task<string> GenerateTokenAsync(TokenPayload tokenPayload)
    {
        var response = await _httpClient.PostAsJsonAsync<TokenPayload>($"/api/v2/validate-authcode", tokenPayload).ConfigureAwait(false);
        if (response.IsSuccessStatusCode)
        {
            var responseString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var employees = JsonConvert.DeserializeObject<string>(responseString);
            return employees;
        }

        return string.Empty;
    }
}
