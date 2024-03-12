using ProductAndRequests.Models;

namespace ProductAndRequests.Service
{
    public class TokenService : ITokenService
    {
        private readonly HttpClient _httpClient;
        private string? _cachedToken;

        public TokenService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string?> GetTokenAsync()
        {
            if (!string.IsNullOrWhiteSpace(_cachedToken))
            {
                return _cachedToken;
            }

            // Assuming TokenRequest has the necessary properties for your token endpoint
            var tokenRequest = new TokenRequest ( "user", "password" );
            var response = await _httpClient.PostAsJsonAsync("Auth/generate-token", tokenRequest);

            if (response.IsSuccessStatusCode)
            {
                var tokenResponse = await response.Content.ReadFromJsonAsync<TokenResponse>();
                _cachedToken = tokenResponse?.Token;
                return _cachedToken;
            }

            // Handle failure cases appropriately
            return null;
        }
    }

}
