using Microsoft.AspNetCore.Mvc;
using FluentValidation;
using Marten;

namespace Techs.Api.Techs;

public class TechsController(IDocumentSession docSess) : ControllerBase
{
    [HttpPost("/techs")]
    public async Task<ActionResult> AddTechAsync(
        [FromBody] TechCreateModel request,
        [FromServices] IValidator<TechCreateModel> validator)
    {
        var validateResults = validator.Validate(request);

        if (!validateResults.IsValid)
        {
            return BadRequest();
        }

        var response = new TechResponseModel(Guid.NewGuid(), request.FirstName, request.LastName, request.Sub, request.Email, request.Phone);

        var techEntity = new TechEntity
        {
            Id = response.Id,
            FirstName = response.FirstName,
            LastName = response.LastName,
            Sub = response.Sub,
            Email = response.Email,
            Phone = response.Phone
        };

        docSess.Store(techEntity);
        await docSess.SaveChangesAsync();
        return Created($"/techs/{response.Id}", response);
    }

    [HttpGet("/techs/{id:guid}")]
    public async Task<ActionResult> GetATech(Guid id)
    {
        var techEntity = await docSess.Query<TechEntity>().SingleOrDefaultAsync(x => x.Id == id);
        if (techEntity is null)
        {
            return NotFound();
        }
        else
        {
            var response = new TechResponseModel(techEntity.Id, techEntity.FirstName, techEntity.LastName, techEntity.Sub, techEntity.Email, techEntity.Phone);
            return Ok(response);
        }
    }
}