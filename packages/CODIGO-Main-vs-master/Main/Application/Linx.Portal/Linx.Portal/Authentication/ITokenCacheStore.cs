using Microsoft.Identity.Client;

namespace Linx.Portal.Authentication
{
    public interface ITokenCacheStore
    {
        void RegisterCache(ITokenCache tokenCache);
        void Clear();
    }
}
