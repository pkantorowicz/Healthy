using System;
using System.Threading.Tasks;
using Healthy.Application.Services.Users.Abstract;
using Healthy.Core;
using Healthy.Core.Domain.Users.Entities;
using Healthy.Core.Domain.Users.Repositories;
using Healthy.Core.Domain.Users.Services;
using Healthy.Core.Exceptions;

namespace Healthy.Application.Services.Users
{
        public class PasswordService : IPasswordService
    {
        private readonly IUserRepository _userRepository;
        private readonly IOneTimeSecuredOperationService _oneTimeSecuredOperationService;
        private readonly IEncrypter _encrypter;

        public PasswordService(IUserRepository userRepository,
            IOneTimeSecuredOperationService oneTimeSecuredOperationService,
            IEncrypter encrypter)
        {
            _userRepository = userRepository;
            _oneTimeSecuredOperationService = oneTimeSecuredOperationService;
            _encrypter = encrypter;
        }

        public async Task ChangeAsync(string userId, string currentPassword, string newPassword)
        {
            var user = await _userRepository.GetByUserIdAsync(userId);
            if (user.HasNoValue)
            {
                throw new ServiceException(ErrorCodes.UserNotFound,
                    $"User with id: '{userId}' has not been found.");
            }
            if (user.Value.Provider != Providers.Healthy)
            {
                throw new ServiceException(ErrorCodes.InvalidAccountType,
                    $"Password can not be changed for the account type: ;{user.Value.Provider}'.");
            }
            if (!user.Value.ValidatePassword(currentPassword, _encrypter))
            {
                throw new ServiceException(ErrorCodes.InvalidCurrentPassword,
                    "Current password is invalid.");
            }

            user.Value.SetPassword(newPassword, _encrypter);
            await _userRepository.UpdateAsync(user.Value);
        }

        public async Task ResetAsync(Guid operationId, string email)
        {
            var user = await _userRepository.GetByEmailAsync(email, Providers.Healthy);
            if (user.HasNoValue)
            {
                throw new ServiceException(ErrorCodes.UserNotFound,
                    $"User with email: '{email}' has not been found.");
            }
            await _oneTimeSecuredOperationService.CreateAsync(operationId, OneTimeSecuredOperations.ResetPassword,
                email, DateTime.UtcNow.AddDays(1));
        }

        public async Task SetNewAsync(string email, string token, string password)
        {
            var user = await _userRepository.GetByEmailAsync(email, Providers.Healthy);
            if (user.HasNoValue)
            {
                throw new ServiceException(ErrorCodes.UserNotFound,
                    $"User with email: '{email}' has not been found.");
            }

            await _oneTimeSecuredOperationService.ConsumeAsync(OneTimeSecuredOperations.ResetPassword,
                email, token);
            user.Value.SetPassword(password, _encrypter);
            await _userRepository.UpdateAsync(user.Value);
        }
    }
}