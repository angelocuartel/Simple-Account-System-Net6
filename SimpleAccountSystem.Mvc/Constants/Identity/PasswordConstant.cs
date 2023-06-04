namespace SimpleAccountSystem.Mvc.Constants.Identity
{
    public static class PasswordConstant
    {
        public const bool RequireNonAlphanumeric = true;
        public const bool RequireDigit = true;
        public const bool RequireUppercase = true;
        public const bool RequiredLowerCase = true;

        public const int RequiredLength = 15;
        public const int RequiredUniqueChars = 1;

    }
}
