namespace NaniTrader.ApiClients;

using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using FyersAPI;

public class FyersApiClient
{
    private readonly HttpClient _httpClient;

    public FyersApiClient(HttpClient httpClient) => this._httpClient = httpClient;

    public async Task<TokenResponse> GenerateTokenAsync(TokenPayload tokenPayload)
    {
        var response = await _httpClient.PostAsJsonAsync($"/api/v2/validate-authcode", tokenPayload, new JsonSerializerOptions() { }).ConfigureAwait(false);

        return await response.Content.ReadFromJsonAsync<TokenResponse>().ConfigureAwait(false);
    }
}
