
using JasperFx.CodeGeneration.Frames;
using SoftwareCenter.Api.Shared;
using System.Security.Claims;

namespace SoftwareCenter.Api.Vendors.Services;

public class ProvidesIdentityFromJwt(IHttpContextAccessor context) : IProvideIdentity
{


    public Task<string> GetNameOfCallerAsync()
    {
        if(context.HttpContext is null)
        {
            throw new ChaosException("Not to be used in an unauthenticated request");
        }
        var user = context.HttpContext.User.FindFirstValue("sub");

        return Task.FromResult(user!);
    }
}
