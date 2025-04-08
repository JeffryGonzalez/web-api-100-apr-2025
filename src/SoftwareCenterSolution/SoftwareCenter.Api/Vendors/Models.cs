using System.Text.Json.Serialization;
using FluentValidation;

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



public enum ContactMechanisms
{
    // primary_phone
    [JsonStringEnumMemberName("primary_phone")]
    primaryPhone,
    // primary_email
    [JsonStringEnumMemberName("primary_email")]
    PrimaryEmail
}


public record CommercialVendorCreate(
    string Name,
    string Site,
    PointOfContact Poc

    );

public class CommercialVendorCreateValidator : AbstractValidator<CommercialVendorCreate>
{
    public CommercialVendorCreateValidator()
    {
        RuleFor(m => m.Name).NotEmpty();
        RuleFor(m => m.Site).NotEmpty().WithMessage("Need a site for reference");
        RuleFor(m => m.Poc).SetValidator(new PointOfContactValidator());
    }
}

public record PointOfContact(
    NameContact Name,
    Dictionary<ContactMechanisms, string> ContactMechanisms
    );


public partial class PointOfContactValidator : AbstractValidator<PointOfContact>
{
    public PointOfContactValidator()
    {
        RuleFor(p => p.Name).SetValidator(new NameContactValidator());
        RuleFor(p => p.ContactMechanisms).NotEmpty().WithMessage("You have to provide some way to contact the person");
        // if they don't have a phone number, they have to have an email
        RuleFor(p => p.ContactMechanisms[ContactMechanisms.PrimaryEmail])
            .NotEmpty()
            .Matches(ValidEmailRegularExpression())
            .When(p => p.ContactMechanisms.ContainsKey(ContactMechanisms.primaryPhone) == false);

        RuleFor(p => p.ContactMechanisms[ContactMechanisms.PrimaryEmail])

           .Matches(ValidEmailRegularExpression());

    }

    [System.Text.RegularExpressions.GeneratedRegex(@".+\@.+\..+")]
    private static partial System.Text.RegularExpressions.Regex ValidEmailRegularExpression();
}

public record NameContact(string First, string Last);

public class NameContactValidator : AbstractValidator<NameContact>
{
    public NameContactValidator()
    {
        RuleFor(n => n.First).NotEmpty().MaximumLength(20);
        RuleFor(n => n.Last).MaximumLength(20);
    }
}