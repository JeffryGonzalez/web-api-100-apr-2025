using SoftwareCenter.Api.Vendors;
using FluentValidation.TestHelper;
namespace SoftwareCenter.Tests.Vendors;

[Trait("Category", "Unit")]
public class CommercialVendorValidationTests
{
    [Theory, MemberData(nameof(ValidExampleData))]
    public void ValidExamples(CommercialVendorCreateModel model)
    {
        var validator = new CommercialVendorCreateModelValidator();
        var result = validator.TestValidate(model);
        Assert.True(result.IsValid);
    }
    
    [Theory, MemberData(nameof(InvalidExampleData))]
    public void InvalidExamples(CommercialVendorCreateModel model, string expectedErrorMessage)
    {
        var validator = new CommercialVendorCreateModelValidator();
        var result = validator.TestValidate(model);
        Assert.False(result.IsValid);
        Assert.Equal(expectedErrorMessage, result.Errors[0].ErrorMessage);
    }

    public static IEnumerable<object[]> ValidExampleData()
    {
        yield return
        [
            new CommercialVendorCreateModel()
            {
                Name = "Test Vendor",
                Site = "https://testvendor.com",
                ContactFirstName = "John",
                ContactLastName = "Doe",
                ContactEmail = "john.doe@testvendor.com",
                ContactPhone = "800-555-1234"
            }
        ];
    }

    public static IEnumerable<object[]> InvalidExampleData()
    {
        yield return
        [
            new CommercialVendorCreateModel()
            {
                Name = "",
                Site = "https://testvendor.com",
                ContactFirstName = "John",
                ContactLastName = "Doe",
                ContactEmail = "john.doe@testvendor.com",
                ContactPhone = "800-555-1234"
            },
            "Name is required"
        ];

        yield return
        [
            new CommercialVendorCreateModel()
            {
                Name = "Bob Smith",
                Site = "https://testvendor.com",
                ContactFirstName = "John",
                ContactLastName = "Doe",
                ContactEmail = "",
                ContactPhone = ""
            },
            "Contact phone is required if no email provided"
        ];
        yield return
        [
            new CommercialVendorCreateModel()
            {
                Name = "Bob Smith",
                Site = "https://testvendor.com",
                ContactFirstName = "John",
                ContactLastName = "Doe",
                ContactEmail = "bobsmith",
                ContactPhone = "888 555-1234"
            },
            "Contact email must be a valid email address"
        ];
    }
}