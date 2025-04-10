using Marten;
using Microsoft.AspNetCore.Mvc;

namespace Techs.Api.Techs;

public class TechsController((IDocumentSession session, IConfiguration configuration) ) : ControllerBase
{
    [HttpPost("/techs")]
    public async Task<ActionResult> AddTechs([FromBody] TechCreateModel request)
    {
        var createTechsTable = new TechEntity
        {
            Id = Guid.NewGuid(),
            FirstName = request.FirstName,
            LastName = request.LastName,
            Sub = request.Sub,
            Phone = request.Phone,
            Email = request.Email,

        };
        session.Store(createTechsTable);
        await session.SaveChangesAsync();

        return Created($"/Techs/{createTechsTable.Id}", createTechsTable);
    }

    [HttpGet("/Techs/{id:guid}")]
    public async Task<ActionResult> getTechs(Guid Id)
    {
        var response = await session.Query<TechEntity>().SingleOrDefaultAsync(t => t.Id == Id);
        if (response == null)
        {
            return NotFound();
        }
        else
        {

            return Ok(response);
        }
    }
}