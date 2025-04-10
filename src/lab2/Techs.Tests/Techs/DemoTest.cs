

using Techs.Api.Techs;

namespace Techs.Tests.Techs;
public class DemoTest
{

    private TechCreateModel _model;

    public DemoTest()
    {
        _model =   new TechCreateModel("Joe", "Schmidt", "x0000", "joe@aol.com", "555-1212");
    }
    [Fact]
    public void HasFirstName()
    {
        
        
        Assert.Equal("Joe", _model.FirstName);
    }

    [Theory]
    [InlineData(3)]
    [InlineData(5)]
    public void IsOdd(int number)
    {
        Assert.True(number % 2 != 0);
    }

    [Fact]
    public void HasLastName()
    {
       

        Assert.Equal("Schmidt", _model.LastName);
    }
}
