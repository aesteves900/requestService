namespace ProductAndRequests.Service
{
    public interface ITokenService
    {
        Task<string?> GetTokenAsync();
    }
}