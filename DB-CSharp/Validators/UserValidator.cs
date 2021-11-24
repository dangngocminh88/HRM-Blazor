using DB_CSharp.Models;
using FluentValidation;

namespace DB_CSharp.Validators
{
    public class UserLoginRequestValidator : AbstractValidator<UserLoginRequest>
    {
        public UserLoginRequestValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("User Name is required");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required");
        }
    }
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
