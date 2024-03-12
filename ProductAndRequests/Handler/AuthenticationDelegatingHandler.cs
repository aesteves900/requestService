using ProductAndRequests.Service;
using System.Net.Http.Headers;

namespace ProductAndRequests.Handler
{
    public class AuthenticationDelegatingHandler : DelegatingHandler
    {

        private readonly ITokenService _tokenService;


        public AuthenticationDelegatingHandler(ITokenService tokenService)
        {
            _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
        }


        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var token = await _tokenService.GetTokenAsync();
            if (!string.IsNullOrWhiteSpace(token))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }
}