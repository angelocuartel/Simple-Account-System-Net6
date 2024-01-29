using Microsoft.AspNetCore.Identity;
using SimpleAccountSystem.Dto.Request;
using System.Security.Claims;

namespace SimpleAccountSystem.Domain.Service
{
    public sealed class UserService : IUserService
    {
        private readonly UserManager<IdentityUser> _userManager;

        public UserService(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public IEnumerable<IdentityUser> GetUsers(int recordCount, string? filter = null)
        {
            IEnumerable<IdentityUser>? users = null;

            if (!string.IsNullOrEmpty(filter))
            {
                users = GetFilteredUsers(filter);
            }
            else
            {
                users = GetUsers(recordCount);
            }

            return users;
        }

        public async Task<bool> AddUserAsync(IdentityUserRequestDto user)
        {
            var resultCreation = await _userManager.CreateAsync(new IdentityUser
            {
                Email = user.Email,
                UserName = user.UserName,
                EmailConfirmed = user.EmailConfirmed
            }
                , user.Password);

            return resultCreation.Succeeded;
        }

        public string GetUserId(ClaimsPrincipal currentUser)
        => _userManager.GetUserId(currentUser);
        

        private IEnumerable<IdentityUser> GetFilteredUsers(string filter)
        {
            IEnumerable<IdentityUser>? result = null;

            result = _userManager.Users.Where(i => i.UserName.Contains(filter)
            || i.Email.Contains(filter)
            || i.UserName.Contains(filter))
                .Select(i => new IdentityUser {
                    Email = i.Email, 
                    UserName = i.UserName,
                    TwoFactorEnabled = i.TwoFactorEnabled
                });

            return result;
        }

        private IEnumerable<IdentityUser> GetUsers(int recordCount)
        => _userManager.Users
            .Take(recordCount);
    }
}
