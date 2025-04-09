using FluentValidation;
using Marten;
using Microsoft.AspNetCore.Mvc;

namespace Techs.Api.Techs;

public class TechsController(IDocumentSession session) : ControllerBase
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

        var response = new TechResponseModel(Guid.NewGuid(), request.FirstName, request.LastName, request.Sub, request.Email, request.Phone);
        var entity = new TechEntity
        {
            Id = response.Id,
            FirstName = response.FirstName,
            LastName = response.LastName,
            Sub = response.Sub,
            Email = response.Email,
            Phone = response.Phone

        };
        session.Store(entity);
        await session.SaveChangesAsync();
        return Created($"/techs/{response.Id}", response);
    }

    [HttpGet("/techs/{id:guid}")]
    public async Task<ActionResult> GetATech(Guid id)
    {
        var entity = await session.Query<TechEntity>().SingleOrDefaultAsync(t => t.Id == id);
       if(entity is null)
        {
            return NotFound();
        }
       else
        {
            var response = new TechResponseModel(entity.Id, entity.FirstName, entity.LastName, entity.Sub, entity.Email, entity.Phone);
            return Ok(response);
        }
    }
}