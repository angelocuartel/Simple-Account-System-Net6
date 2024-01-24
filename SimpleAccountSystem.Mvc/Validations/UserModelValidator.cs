using FluentValidation;
using SimpleAccountSystem.Mvc.Models;

namespace SimpleAccountSystem.Mvc.Validations
{
    public sealed class UserModelValidator : AbstractValidator<UserModel>
    {
        public UserModelValidator()
        {
            RuleFor(i => i.Password).NotEmpty()
                .NotNull()
                .Equal(i => i.ConfirmPassword)
                .WithMessage("password does not match");

            RuleFor(i => i.Email)
                .NotEmpty()
                .NotNull()
                .EmailAddress();

            RuleFor(i => i.UserName)
                .NotEmpty()
                .NotNull();
        }
    }
}
