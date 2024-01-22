namespace SimpleAccountSystem.Mvc.Models
{
    public class UserModel
    {
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? ConfirmPassword { get; set; }
        public bool EmailConfirmed { get; set; }
    }
}
