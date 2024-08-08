using FluentValidation;

namespace GlassLewis.Company.Api.Model
{
    public class CompanyDtoValidator : AbstractValidator<CompanyDto>
    {
        public CompanyDtoValidator()
        {
            RuleFor(x => x.Name).NotNull().WithMessage("Name cannot be empty.");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name cannot be empty.");
            RuleFor(x => x.Isin).NotNull().WithMessage("ISIN cannot be empty.");
            RuleFor(x => x.Isin).NotEmpty().WithMessage("ISIN cannot be empty."); ;
            RuleFor(x => x.Isin).Matches(@"^[A-Z]{2}").WithMessage("ISIN must start with two letters.");
            RuleFor(x => x.Isin).Matches(@"^[A-Z]{2}\d*").WithMessage("ISIN must start with two letters followed by numbers.");
        }
    }
}
