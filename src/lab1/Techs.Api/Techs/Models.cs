using FluentValidation;
namespace Techs.Api.Techs;


public record TechCreateModel(string FirstName, string LastName, string Sub, string Email, string Phone);

public record TechResponseModel(Guid Id, string FirstName, string LastName, string Sub, string Email, string Phone);

public class TechCreateModelValidator : AbstractValidator<TechCreateModel>
{
    public TechCreateModelValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty().WithMessage("FirstName is required.");
        RuleFor(x => x.LastName).NotEmpty().WithMessage("LastName is required.");
        RuleFor(x => x.Email)
            .NotEmpty()
            .Matches(@".+\@.+\..+")
            .WithMessage("Email looks wrong.");
        RuleFor(x => x.Sub)
            .NotEmpty()
            .Must(value => value.StartsWith("a") || value.StartsWith("x"))
            .WithMessage("Sub must start with an x or a");
    }
}