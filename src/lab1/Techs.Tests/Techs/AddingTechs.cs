using Alba;
using Techs.Api.Techs;

namespace Techs.Tests.Techs;

public class AddingTechs
{
    [Fact]
    [Trait("Category", "SystemTest")]
    public async Task CanAddTechs()
    {
        var requestModel = new TechCreateModel("Ray", "Palmer", "x3333", "ray@company.com", "555-1234");

        var host = await AlbaHost.For<Program>();

        var postResponse = await host.Scenario(api =>
        {
            api.Post.Json(requestModel).ToUrl("/techs");
            api.StatusCodeShouldBe(201);
        });
        
        var postResponseModel = postResponse.ReadAsJson<TechResponseModel>();
        
        Assert.NotNull(postResponseModel);
        
        Assert.Equal("Ray", postResponseModel.FirstName);
        Assert.Equal("Palmer", postResponseModel.LastName);
        Assert.Equal("x3333", postResponseModel.Sub);
        // Was    Assert.Equal("555-1234", postResponseModel.Email);
        Assert.Equal("ray@company.com", postResponseModel.Email);
        Assert.Equal("555-1234", postResponseModel.Phone);
        Assert.NotEqual(Guid.Empty, postResponseModel.Id);

        var location = postResponse.Context.Response.Headers.Location.Single();
        
        var getResponse = await host.Scenario(api =>
        {
            api.Get.Url(location!);
            api.StatusCodeShouldBe(200);
        });
        
        var getResponseModel = getResponse.ReadAsJson<TechResponseModel>();
        
        Assert.Equal(postResponseModel, getResponseModel);
    }
    
    [Fact]
    public async Task ModelIsValidated()
    {
        var requestModel = new { };

        var host = await AlbaHost.For<Program>();

        var postResponse = await host.Scenario(api =>
        {
            api.Post.Json(requestModel).ToUrl("/techs");
            api.StatusCodeShouldBe(400);
        });
    }


}