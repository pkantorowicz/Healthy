using System.Threading.Tasks;
using Healthy.Core.Domain.Users.DomainClasses;
using Healthy.Infrastructure.Redis;

namespace Healthy.Storage.Caching
{
    public class UserCache : IUserCache
    {
        private readonly ICache _cache;

        public UserCache(ICache cache)
        {
            _cache = cache;
        }

        public async Task AddAsync(User user)
            => await _cache.AddAsync(GetCacheKey(user.UserId), user);
        
        public async Task DeleteAsync(string userId)
            => await _cache.DeleteAsync(GetCacheKey(userId));

        private static string GetCacheKey(string userId)
            => $"users:{userId}";
    }
}