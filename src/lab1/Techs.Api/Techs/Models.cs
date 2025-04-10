using FluentValidation;
using SoftwareCenter.Api.Shared;

namespace Techs.Api.Techs;


public record TechCreateModel(string FirstName, string LastName, string Sub, string Email, string Phone);

public record TechResponseModel(Guid Id, string FirstName, string LastName, string Sub, string Email, string Phone);

public class TechCreateModelValidator : AbstractValidator<TechCreateModel>
{
    public TechCreateModelValidator()
    {
        RuleFor(t => t.FirstName).NotEmpty().WithMessage("FirstName is required.");
        RuleFor(t => t.LastName).NotEmpty().WithMessage("LastName is required.");
        RuleFor(t => t.Sub).Must(t => t.StartsWith('x') || t.StartsWith('a')).WithMessage("Sub must start with an x or a").When(t => string.IsNullOrEmpty(t.Sub) == false);
        RuleFor(t => t.Email).NotEmpty().Matches(@".+\@.+\..+").WithMessage("Email looks wrong.");
        RuleFor(t => t.Phone).NotEmpty().WithMessage("Phone is required");
    }
}