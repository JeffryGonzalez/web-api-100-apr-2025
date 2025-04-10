
using JasperFx.CodeGeneration.Frames;
using SoftwareCenter.Api.Shared;
using System.Security.Claims;

namespace SoftwareCenter.Api.Vendors.Services;

public class ProvidesIdentityFromJwt(IHttpContextAccessor context, ILookupTechsFromTechApi _techsApiLookup) : IProvideIdentity
{


    public async Task<string> GetNameOfCallerAsync()
    {
        if(context.HttpContext is null)
        {
            throw new ChaosException("Not to be used in an unauthenticated request");
        }
        var user = context.HttpContext.User.FindFirstValue("sub");

        // call that other other api url, and get the name from them.
        TechNameResponse techName = await _techsApiLookup.GetTechFromSubAsync(user!);
        return techName.Name;
    }
}

public record TechNameResponse
{
    public string Name { get; set; } = string.Empty;
}

public interface ILookupTechsFromTechApi
{
    Task<TechNameResponse> GetTechFromSubAsync(string sub);
}

public class TechApiHttp(HttpClient client) : ILookupTechsFromTechApi
{
    public async Task<TechNameResponse> GetTechFromSubAsync(string sub)
    {
        var response = await client.GetAsync($"/software/techs/{sub}");

        response.EnsureSuccessStatusCode();

        var body = await response.Content.ReadFromJsonAsync<TechNameResponse>();

        if(body is null)
        {
            throw new ChaosException("not expecting this...");
        }
        return body;
    }
}