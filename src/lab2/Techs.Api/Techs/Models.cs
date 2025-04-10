using FluentValidation;
using Riok.Mapperly.Abstractions;

namespace Techs.Api.Techs;


public record TechCreateModel(string FirstName, string LastName, string Sub, string Email, string Phone);

public record TechResponseModel(Guid Id, string FirstName, string LastName, string Sub, string Email, string Phone);

public record TechNameResponseModel(string Name);

public class TechCreateModelValidator : AbstractValidator<TechCreateModel>
{
    public TechCreateModelValidator()
    {
        RuleFor(t => t.FirstName).NotEmpty().WithMessage("FirstName is required.");
        RuleFor(t => t.LastName).NotEmpty().WithMessage("LastName is required.");
        RuleFor(t => t.Email).NotEmpty().Matches(@".+\@.+\..+").WithMessage("Email looks wrong.");
        RuleFor(t => t.Sub).Must(t => t.StartsWith('x') || t.StartsWith('a')).WithMessage("Sub must start with an x or a")
            .When(t => string.IsNullOrEmpty(t.Sub) == false);
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