using System;

namespace Healthy.Read.Dtos.Users
{
    public class UserDto : UserInfoDto
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Provider { get; set; }
        public string Culture { get; set; }
        public string ExternalUserId { get; set; }
    }
}