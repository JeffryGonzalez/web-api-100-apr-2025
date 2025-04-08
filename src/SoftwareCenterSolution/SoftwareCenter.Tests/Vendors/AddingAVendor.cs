

using System.Net.Http.Json;
using Alba;

namespace SoftwareCenter.Tests.Vendors;

public class AddingAVendor
{

    [Fact]
    public async Task CanAddVendorAsync()
    {

        var host = await  AlbaHost.For<Program>();
        // about 15 lines from that documentation, start up the api with our Program.cs configuration.


        // System Tests are "scenarios".  
        await host.Scenario(api =>
        {
            api.Post.Json(new { }).ToUrl("/commercial-vendors");
            api.StatusCodeShouldBeOk();
        });

      //  // a "black box" test and can be SUPER flaky in your pipelines.

      //// bad. don't do this. don't even try to make this code better.
      //  var client = new HttpClient();
      //  client.BaseAddress = new Uri("http://localhost:1337");

      //  var response = await client.PostAsJsonAsync("vendors", new { });

      //  Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
    }
}
