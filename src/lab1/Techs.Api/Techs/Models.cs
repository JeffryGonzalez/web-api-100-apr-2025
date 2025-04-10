using FluentValidation;

namespace Techs.Api.Techs;


public record TechCreateModel(string FirstName, string LastName, string Sub, string Email, string Phone);

public record TechResponseModel(Guid Id, string FirstName, string LastName, string Sub, string Email, string Phone);

public class TechCreateModelValidator : AbstractValidator<TechCreateModel>
{
    public TechCreateModelValidator()
    {
        // Put your rules here
    }
}