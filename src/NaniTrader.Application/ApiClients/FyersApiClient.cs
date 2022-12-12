namespace NaniTrader.ApiClients;

using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using FyersAPI;

public class FyersApiClient
{
    private readonly HttpClient _httpClient;

    public FyersApiClient(HttpClient httpClient) => _httpClient = httpClient;

    public async Task<TokenResponse> GenerateTokenAsync(TokenPayload tokenPayload)
    {
        var response = await _httpClient.PostAsJsonAsync($"/api/v2/validate-authcode", tokenPayload, new JsonSerializerOptions() { }).ConfigureAwait(false);

        return await response.Content.ReadFromJsonAsync<TokenResponse>().ConfigureAwait(false);
    }

    public async Task<List<Quote>> GetQuotesAsync(List<string> symbols, string appId, string token)
    {
        if (symbols == null || symbols.Count == 0)
            return new List<Quote>();

        var sym = string.Join(",", symbols);
        _httpClient.DefaultRequestHeaders.Clear();
        _httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", $"{appId}:{token}");
        var quotesResponse = await _httpClient.GetFromJsonAsync<QuotesResponse>($"/data-rest/v2/quotes/?symbols={sym}", new JsonSerializerOptions() { }).ConfigureAwait(false);
        return quotesResponse.d.ToList();
    }
}
