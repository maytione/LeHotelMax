using LeHotelMax.Application.Common.Models;
using LeHotelMax.Application.Users.Command;
using LeHotelMax.Application.Users.Dtos;
using LeHotelMax.Application.Users.Interfaces;
using MediatR;

namespace LeHotelMax.Application.Users.CommandHandlers
{
    internal class LoginCommandHandler : IRequestHandler<LoginCommand, OperationResult<UserDto>>
    {
        private readonly IIdentityRepository _identityRepository;

        public LoginCommandHandler(IIdentityRepository identityRepository)
        {
            _identityRepository = identityRepository;
        }

        public async Task<OperationResult<UserDto>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<UserDto>();

            try
            {
                // lets try find user by usernam/email
                var userDto = await _identityRepository.FindByEmailAsync(request.Username);
                
                // no user found, return some unclear message
                if (userDto == null)
                {
                    result.AddError(Common.Enums.ErrorCode.AuthError, "Check username and/or password");
                    return result;
                }

                // user found, lets try to sign in
                var signedIn = await _identityRepository.PasswordSignInAsync(userDto, request.Password);

                // password missmatched, return some unclear message
                if (!signedIn)
                {
                    result.AddError(Common.Enums.ErrorCode.AuthError, "Check username and/or password");
                    return result;
                }

                // all good, generate some JWT token
                var token = await _identityRepository.AuthenticateAsync(userDto);

                userDto.AccessToken = token;

                result.Data = userDto;
            }
            catch (Exception)
            {
                result.AddError(Common.Enums.ErrorCode.AuthError, "Check username and/or password");
            }

            return result;
       
        }
    }
}
