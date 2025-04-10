using FluentValidation;
using Marten;
using Microsoft.AspNetCore.Mvc;
using Techs.Api.Techs.Services;

namespace Techs.Api.Techs;

public class TechsController(ITechRepository repository) : ControllerBase
{
    [HttpPost("/techs")]
    public async Task<ActionResult> AddTechAsync(
        [FromBody] TechCreateModel request,
        [FromServices] IValidator<TechCreateModel> validator
        )
    {
        if(validator.Validate(request).IsValid == false)
        {
            return BadRequest();
        }

        var response = await repository.AddTechAsync(request);
       
     
        return Created($"/techs/{response.Id}", response);
    }

    [HttpGet("/techs/{id:guid}")]
    public async Task<ActionResult> GetATech(Guid id, CancellationToken token)
    {

        var response = await repository.GetTechByIdAsync(id, token);


        return response switch
        {
            null => NotFound(),
            _ => Ok(response)
        };
    }

    [HttpGet("/techs/{sub}")]
    public async Task<ActionResult> GetATech(string sub)
    {

        var response = await repository.GetTechsBySubAsync(sub);


        return response switch
        {
            null => NotFound(),
            _ => Ok(response)
        };
    }

    [HttpGet("/techs")]
    public async Task<ActionResult> GetAllTechs()
    {

        var response = await repository.GetAllTechsAsync();


        return response switch
        {
            null => NotFound(),
            _ => Ok(response)
        };
    }

}