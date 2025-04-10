using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alba;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Techs.Api.Techs;
using Techs.Api.Techs.Services;

namespace Techs.Tests.Techs;
public class GettingTechs : IAsyncLifetime
{

    private IAlbaHost _host = null!;


    [Fact]
    [Trait("Category", "SystemTest")]
    public async Task CanGetTechNameBySub()
    {
        var _host = await AlbaHost.For<Program>(config =>
        {
            var fake = Substitute.For<ITechRepository>();
            fake.GetTechNameBySubAsync("sue-jones").Returns(new TechNameResponseModel("Susan Jones"));
            config.ConfigureServices(sp =>
            {
                // use this to configure NEW services that don't already exist in the program.cs.
            });
            config.ConfigureTestServices(sp =>
            {
                sp.AddScoped<ITechRepository>(_ => fake);
            });
        });

        var getResponse = await _host.Scenario(api =>
        {
            api.Get.Url("/techs/sue-jones");
            api.StatusCodeShouldBeOk();
        });

        var responseBody = getResponse.ReadAsJson<TechNameResponseModel>();

        Assert.NotNull(responseBody);
        Assert.Equal("Susan Jones", responseBody.Name);
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
