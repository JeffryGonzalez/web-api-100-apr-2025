using Alba;
using Microsoft.Extensions.Hosting;
using Techs.Api.Techs;

namespace Techs.Tests.Techs;

public class AddingTechs : IAsyncLifetime
{

    private IAlbaHost _host = null!;


    [Fact]
    [Trait("Category", "SystemTest")]
    public async Task CanAddTechs()
    {
        var requestModel = new TechCreateModel("Ray", "Palmer", "x3333", "ray@company.com", "555-1234");

        // start up the API from scratch...
        var postResponse = await _host.Scenario(api =>
        {
            api.Post.Json(requestModel).ToUrl("/techs");
            api.StatusCodeShouldBe(201);
        });

        var postResponseModel = postResponse.ReadAsJson<TechResponseModel>();

        Assert.NotNull(postResponseModel);

        Assert.Equal("Ray", postResponseModel.FirstName);
        Assert.Equal("Palmer", postResponseModel.LastName);
        Assert.Equal("x3333", postResponseModel.Sub);
        Assert.Equal("ray@company.com", postResponseModel.Email);
        Assert.Equal("555-1234", postResponseModel.Phone);
        Assert.NotEqual(Guid.Empty, postResponseModel.Id);

        var location = postResponse.Context.Response.Headers.Location.Single();

        var getSubsResponse = await _host.Scenario(api =>
        {
            api.Get.Url($"/techs/{postResponseModel.Sub}");
            api.StatusCodeShouldBe(200); // Assert that the GET request returns 200 OK
        });

        var getResponseModel = getSubsResponse.ReadAsJson<dynamic>();

        Assert.Equal("Ray Palmer", getResponseModel.Name.ToString());
    }


    public async Task InitializeAsync()
    {
        // test containers - https://dotnet.testcontainers.org/
        _host = await AlbaHost.For<Program>();
    }
    public async Task DisposeAsync()
    {   // throw away that container - use a different one for the next set.
        await _host.DisposeAsync();
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