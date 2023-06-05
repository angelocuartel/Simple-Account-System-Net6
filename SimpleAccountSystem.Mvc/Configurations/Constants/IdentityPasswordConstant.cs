namespace SimpleAccountSystem.Mvc.Configurations.Constants
{
    public static class IdentityPasswordConstant
    {
        public const bool RequireNonAlphanumeric = true;
        public const bool RequireDigit = true;
        public const bool RequireUppercase = true;
        public const bool RequiredLowerCase = true;

        public const int RequiredLength = 15;
        public const int RequiredUniqueChars = 1;

    }
}
