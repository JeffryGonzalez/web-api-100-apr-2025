using FluentValidation;
using Riok.Mapperly.Abstractions;

namespace Techs.Api.Techs;


public record TechCreateModel(string FirstName, string LastName, string Sub, string Email, string Phone);

public record TechResponseModel(Guid Id, string FirstName, string LastName, string Sub, string Email, string Phone);

public class TechCreateModelValidator : AbstractValidator<TechCreateModel>
{
    public TechCreateModelValidator()
    {
        // Put your rules here
        RuleFor(x => x.FirstName).NotEmpty().WithMessage("First Name is required");
        RuleFor(x => x.LastName).NotEmpty().WithMessage("Last Name is required");
        RuleFor(x => x.Sub).NotEmpty().WithMessage("Sub is required");

        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Email is required if no phone provided")
            .WithMessage("Contact email must be a valid email address")
            .When(x => !string.IsNullOrEmpty(x.Email));
        RuleFor(x => x.Phone)
            .NotEmpty()
            .WithMessage("Phone is required if no email provided")
            .When(x => string.IsNullOrEmpty(x.Email));
    }
}

[Mapper]
public static partial class Mappers
{
    public static partial TechEntity MapToEntity(this TechResponseModel model);

    [MapValue(nameof(TechResponseModel.Id), Use = nameof(MakeId))]
    public static partial TechResponseModel MapToResponse(this TechCreateModel model);
    public static partial IQueryable<TechResponseModel> ProjectToResponse(this IQueryable<TechEntity> entity);

    private static Guid MakeId()
    {
        return Guid.NewGuid();
    }

}