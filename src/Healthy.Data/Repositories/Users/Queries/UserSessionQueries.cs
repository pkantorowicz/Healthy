using System;
using System.Threading.Tasks;
using Healthy.Core.Domain.Users.Entities;
using Healthy.Data.Mongo;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Healthy.Data.Repositories.Users.Queries
{
    public static class UserSessionQueries
    {
        public static IMongoCollection<UserSession> UserSessions(this IMongoDatabase database)
            => database.GetCollection<UserSession>();

        public static async Task<UserSession> GetByIdAsync(this IMongoCollection<UserSession> sessions, Guid id)
        {
            if (id == Guid.Empty)
                return null;

            return await sessions.AsQueryable().FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}