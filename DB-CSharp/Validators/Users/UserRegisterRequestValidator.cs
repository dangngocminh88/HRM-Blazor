using DB_CSharp.Models.Users;
using FluentValidation;

namespace DB_CSharp.Validators.Users
{
    public class UserRegisterRequestValidator : AbstractValidator<UserRegisterRequest>
    {
        public UserRegisterRequestValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("User Name is required");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required");
            RuleFor(x => x.Password).Equal(x => x.PasswordConfirmation).WithMessage("Password is not the same as Confirm Password");
            RuleFor(x => x.Email).EmailAddress().WithMessage("Email is not correct");
        }
    }
}
