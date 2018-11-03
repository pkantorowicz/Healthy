using Healthy.Core.Domain.Users.DomainClasses;
using Healthy.Read.Dtos.Users;

namespace Healthy.Read.Mappers
{
    public class UserMapper : IUserMapper
    {
        public UserDto MapToUserDto(User entity)
            => new UserDto
            {
                UserId = entity.UserId,
                Email = entity.Email,
                Name = entity.Name,
                Provider = entity.Provider,
                Role = entity.Role,
                State = entity.State,
                ExternalUserId = entity.ExternalUserId,
                AvatarUrl = entity.Avatar.Url,
                CreatedAt = entity.CreatedAt
            };

        public UserInfoDto MapToUserInfoDto(User entity)
            => new UserInfoDto
            {
                UserId = entity.UserId,
                Name = entity.Name,
                Role = entity.Role,
                State = entity.State,
                AvatarUrl = entity.Avatar.Url,
                CreatedAt = entity.CreatedAt,
            };


        public AvailableResourceDto MapToAvailableResourceDto(bool availableResource)
            => new AvailableResourceDto
            {
                IsAvailable = availableResource
            };

        public UserSessionDto MapToUserSessionDto(UserSession entity)
            => new UserSessionDto
            {
                Id = entity.Id,
                UserId = entity.UserId,
                Key = entity.Key,
                UserAgent = entity.UserAgent,
                IpAddress = entity.IpAddress,
                ParentId = entity.ParentId,
                Refreshed = entity.Refreshed,
                Destroyed = entity.Destroyed,
                UpdatedAt = entity.UpdatedAt,
                CreatedAt = entity.CreatedAt,
            };
    }
}