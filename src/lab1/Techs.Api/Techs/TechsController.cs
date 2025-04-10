using FluentValidation;
using Marten;
using Microsoft.AspNetCore.Mvc;

namespace Techs.Api.Techs;

public class TechsController(IDocumentSession session) : ControllerBase
{
    [HttpPost("/techs")]
    public async Task<ActionResult> AddTechAsync([FromBody] TechCreateModel request,
        [FromServices] IValidator<TechCreateModel> validator)
    {
        var isValidatedRequest = validator.Validate(request);
        if (!isValidatedRequest.IsValid)
        {
            return BadRequest();
        }

        var modelResponse = new TechResponseModel(Guid.NewGuid(), request.FirstName, request.LastName, request.Sub, request.Email, request.Phone);
        var entityToSave = new TechEntity
        {
            Id = modelResponse.Id,
            FirstName = modelResponse.FirstName,
            LastName = modelResponse.LastName,
            Sub = modelResponse.Sub,
            Email = modelResponse.Email,
            Phone = modelResponse.Phone

        };
        session.Store(entityToSave);
        await session.SaveChangesAsync();

        return Created($"/techs/{modelResponse.Id}", modelResponse);

    }

    [HttpGet("/techs/{id:guid}")]
    public async Task<ActionResult> GetVendorById(Guid id)
    {
        var response = await session.Query<TechEntity>().SingleOrDefaultAsync(v => v.Id == id);
        if (response is null)
        {
            return NotFound();
        }
        else
        {
            var modelResponse = new TechResponseModel(response.Id, response.FirstName, response.LastName, response.Sub, response.Email, response.Phone);
            return Ok(modelResponse);
        }
    }




}