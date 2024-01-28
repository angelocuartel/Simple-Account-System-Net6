namespace SimpleAccountSystem.Dto.Request
{
    public record IdentityUserRequestDto (string? UserName, string? Email, string? Password, string? ConfirmPassword, bool EmailConfirmed);

    
}
