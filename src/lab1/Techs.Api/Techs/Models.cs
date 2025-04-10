using FluentValidation;

namespace Techs.Api.Techs;


public record TechCreateModel(string FirstName, string LastName, string Sub, string Email, string Phone);

public record TechResponseModel(Guid Id, string FirstName, string LastName, string Sub, string Email, string Phone);

public class TechCreateModelValidator : AbstractValidator<TechCreateModel>
{
    public TechCreateModelValidator()
    {
        // Put your rules here
        RuleFor(x => x.FirstName).NotEmpty().WithMessage("FirstName is required.");
        RuleFor(x => x.LastName).NotEmpty().WithMessage("LastName is required.");
        RuleFor(x => x.Sub).Must(x => x.StartsWith('x') || x.StartsWith('a')).WithMessage("Sub must start with an x or a")
            .When(x => string.IsNullOrEmpty(x.Sub) == false);
        RuleFor(x => x.Email).NotEmpty().Matches(@".+\@.+\..+").WithMessage("Email looks wrong.");




    }
}