

using System.Net.Http.Json;
using Alba;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using SoftwareCenter.Api.Vendors;

namespace SoftwareCenter.Tests.Vendors;

public class AddingAVendor
{

    [Fact]
    public async Task CanAddVendorAsync()
    {

        var host = await  AlbaHost.For<Program>(config =>
        {
            var fakeIdentity = Substitute.For<IProvideIdentity>();
            fakeIdentity.GetNameOfCallerAsync().Returns("darth-vader");
            config.ConfigureServices(services =>
            {
               services.AddScoped<IProvideIdentity>(_ => fakeIdentity);
            });
        });
        // about 15 lines from that documentation, start up the api with our Program.cs configuration.

        var vendorToAdd = new CommercialVendorCreateModel
        {
            Name = "Oracle",
            Site = "https://oracle.com",
            ContactEmail = "larry@oracle.com",
            ContactFirstName = "Larry",
            ContactLastName = "Ellison",
            ContactPhone = "800 999-9999"
        };

        // System Tests are "scenarios".  
       var response =  await host.Scenario(api =>
        {
            api.Post.Json(vendorToAdd).ToUrl("/commercial-vendors");
            api.StatusCodeShouldBe(201);
        });

        var responseBody = response.ReadAsJson<VendorEntity>();

        Assert.NotNull(responseBody);

        Assert.Equal(vendorToAdd.Name, responseBody.Name);
        // etc. etc. BORING..

        var location = response.Context.Response.Headers.Location.Single();

        Assert.NotNull(location);

        var getResponse = await host.Scenario(api =>
        {
            api.Get.Url(location);
            api.StatusCodeShouldBeOk();
        });

        var getResponseBody = getResponse.ReadAsJson<VendorEntity>();
        Assert.NotNull(getResponseBody);

        //Assert.Equal(responseBody, getResponseBody);
        Assert.Equivalent(responseBody, getResponseBody);
    }
}


//public class FakeIdentityThing : IProvideIdentity
//{
//    public Task<string> GetNameOfCallerAsync()
//    {
//        return Task.FromResult("boba-fett");
//    }
//}