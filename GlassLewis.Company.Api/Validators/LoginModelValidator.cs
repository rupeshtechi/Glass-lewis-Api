using FluentValidation;

namespace GlassLewis.Company.Api.Model
{
    public class LoginModelValidator : AbstractValidator<LoginModel>
    {
        public LoginModelValidator()
        {
            RuleFor(x => x.UserName).NotNull();
            RuleFor(x => x.Password).Length(0, 10);
            RuleFor(x => x.UserName).EmailAddress();
        }
    }
}
