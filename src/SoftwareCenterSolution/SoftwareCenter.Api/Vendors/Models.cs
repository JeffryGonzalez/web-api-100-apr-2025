using System.Text.Json.Serialization;
using FluentValidation;

namespace SoftwareCenter.Api.Vendors;



public enum ContactMechanisms {
    // primary_phone
    [JsonStringEnumMemberName("primary_phone")]
    primaryPhone,
    // primary_email
    [JsonStringEnumMemberName("primary_email")]
    PrimaryEmail }


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
    }
}

public record PointOfContact(
    NameContact Name,
    Dictionary<ContactMechanisms, string> ContactMechanisms
    );


public record NameContact(string First, string Last);