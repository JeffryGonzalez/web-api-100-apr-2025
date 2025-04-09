using System.Text.Json.Serialization;
using FluentValidation;
using SoftwareCenter.Api.Shared;

namespace SoftwareCenter.Api.Vendors;



public class CommercialVendorCreateModel
{
    public string Name { get; set; } = string.Empty;
    public string Site { get; set; } = string.Empty;
    public string ContactFirstName { get; set; } = string.Empty;
    public string ContactLastName { get; set; } = string.Empty;
    public string ContactEmail { get; set; } = string.Empty;
    public string ContactPhone { get; set; } = string.Empty;
}

public class CommercialVendorCreateModelValidator : AbstractValidator<CommercialVendorCreateModel>
{
    public CommercialVendorCreateModelValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
        RuleFor(x => x.Site).NotEmpty().WithMessage("Site is required");
        RuleFor(x => x.ContactFirstName).NotEmpty().WithMessage("Contact first name is required");
        RuleFor(x => x.ContactLastName).NotEmpty().WithMessage("Contact last name is required");
        RuleFor(x => x.ContactEmail)
            .NotEmpty()
            .WithMessage("Contact email is required if no phone provided")
            .Matches(ValidationGenerators.ValidEmailRegularExpression())
            .WithMessage("Contact email must be a valid email address")
            .When(x => !string.IsNullOrEmpty(x.ContactEmail));
        RuleFor(x => x.ContactPhone)
            .NotEmpty()
            .WithMessage("Contact phone is required if no email provided")
            .When(x => string.IsNullOrEmpty(x.ContactEmail));
    }
}










