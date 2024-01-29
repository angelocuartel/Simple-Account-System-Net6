using Microsoft.AspNetCore.Identity;
using SimpleAccountSystem.Dto.Request;
using System.Security.Claims;

namespace SimpleAccountSystem.Domain.Service
{
    public interface IUserService
    {
        IEnumerable<IdentityUser> GetUsers(int recordCount, string? filter = null);
        Task<bool> AddUserAsync(IdentityUserRequestDto user);

        string GetUserId(ClaimsPrincipal currentUser);
    }
}
