using LeHotelMax.Application.Users.Dtos;
using LeHotelMax.Application.Users.Interfaces;
using LeHotelMax.Infrastructure.Data.Identity;
using Microsoft.AspNetCore.Identity;

namespace LeHotelMax.Infrastructure.Data.Repository
{
    internal class IdentityRepository : IIdentityRepository
    {
        private readonly IAuthenticateService _authenticateService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public IdentityRepository(IAuthenticateService authenticateService, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _authenticateService = authenticateService;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public Task<string> AuthenticateAsync(UserDto userDto)
        {
            return _authenticateService.Authenticate(userDto);
        }

        public async Task<bool> PasswordSignInAsync(UserDto userDto, string password)
        {
            var user = await _userManager.FindByIdAsync(userDto.Id!);
            var result = await _signInManager.PasswordSignInAsync(user!, password, false, false);
            return result.Succeeded;
        }

        public async Task<UserDto> FindByEmailAsync(string username)
        {
            var user = await _userManager.FindByEmailAsync(username) ?? throw new Exception("User not found");
            UserDto result = new()
            {
                Id = user.Id,
                UserName = user.UserName,
            };
            return result;
        }
    }
}
