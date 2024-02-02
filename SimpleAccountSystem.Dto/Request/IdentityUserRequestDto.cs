using System.Diagnostics.CodeAnalysis;

namespace SimpleAccountSystem.Dto.Request
{
    [ExcludeFromCodeCoverage]
    public record IdentityUserRequestDto (string? UserName, string? Email, string? Password, string? ConfirmPassword, bool EmailConfirmed);

    
}
