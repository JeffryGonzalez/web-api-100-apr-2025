using FluentValidation.TestHelper;
using Techs.Api.Techs;

namespace Techs.Tests.Techs;

public class TechModelValidations
{

    
    [Theory, MemberData(nameof(ValidTechs))]
    public void ValidTechsShouldPass(TechCreateModel model)
    {
        var validator = new TechCreateModelValidator();
        var result = validator.TestValidate(model);
        Assert.True(result.IsValid);
    }
    
    [Theory, MemberData(nameof(InvalidTechs))]
    public void InvalidTechsShouldFail(TechCreateModel model, string errorMessage)
    {
        var validator = new TechCreateModelValidator();
        var result = validator.TestValidate(model);
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.ErrorMessage == errorMessage);
    }

    public static IEnumerable<object[]> ValidTechs()
    {
        yield return
        [
            new TechCreateModel("Joe", "Blow", "a1233", "joe@aol.com", "555-1234")
        ];
        yield return
        [
            new TechCreateModel("Sarah", "Smith", "a3333", "sarah@company.com", "555-5678")
        ];
    }

    public static IEnumerable<object[]> InvalidTechs()
    {
        yield return
        [
            new TechCreateModel("", "Smith", "a3333", "sarah@company.com", "555-5678"),
            "FirstName is required."
        ];
        yield return
        [
            new TechCreateModel("Sarah", "", "a3333", "sarah@company.com", "555-5678"),
            "LastName is required."
        ];
        yield return
        [
            new TechCreateModel("Sarah", "Smith", "a3333", "sarahcompany.com", "555-5678"),
            "Email looks wrong."
        ];
        yield return
        [
            new TechCreateModel("Sarah", "Smith", "p3333", "sarah@company.com", "555-5678"),
            "Sub must start with an x or a"
        ];
        
    }
    
}