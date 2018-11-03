using System.Threading.Tasks;
using Healthy.Write.Services.Users.Abstract;
using Healthy.Infrastructure.Handlers;
using Healthy.Read.Dtos.Users;
using Healthy.Read.Mappers;
using Healthy.Read.Queries;

namespace Healthy.Read.Handlers.QueryHandlers
{
    public sealed class GetUserInfoByNameHandler : IQueryHandler<GetUserInfoByName, UserInfoDto>
    {
        private readonly IUserService _userService;
        private readonly IUserMapper _userMapper;

        public GetUserInfoByNameHandler(IUserService userService,
            IUserMapper userMapper)
        {
            _userService = userService;
            _userMapper = userMapper;
        }

        public async Task<UserInfoDto> HandleAsync(GetUserInfoByName query)
        {
            var userInfo = await _userService.GetAsync(query.Name);
            var userInfoDto = _userMapper.MapToUserInfoDto(userInfo.Value);
            
            return userInfoDto;
        }
    }
}