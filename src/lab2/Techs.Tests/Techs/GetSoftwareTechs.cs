using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alba;
using Techs.Api.Techs;

namespace Techs.Tests.Techs;
public class GetSoftwareTechs
{
    [Fact]
    [Trait("Category", "SystemTest")]
    public async Task GetSoftwareTechNameValidNameFoundTest()
    {
        var host = await AlbaHost.For<Program>();
        var getResponse = await host.Scenario(api =>
        {
            api.Get.Url($"/software/techs/x3333");
            api.StatusCodeShouldBe(200);
        });

        var getResponseModel = getResponse.ReadAsText();
        Assert.NotNull(getResponseModel);
        Assert.Equal("Ray Palmer", getResponseModel);
    }

    [Fact]
    [Trait("Category", "SystemTest")]
    public async Task GetSoftwareTechNameNotFoundTest()
    {
        var host = await AlbaHost.For<Program>();
        var getResponse = await host.Scenario(api =>
        {
            api.Get.Url($"/software/techs/{1}");
            api.StatusCodeShouldBe(404);
        });
    }
}
