using FluentValidation;
using Marten;
using Microsoft.AspNetCore.Mvc;

namespace SoftwareCenter.Api.Vendors;

[ApiController]
public class VendorsController(IDocumentSession documentSession) : ControllerBase
{

    private IDocumentSession _documentSession = documentSession;

    [HttpPost("/commercial-vendors")]
    public async Task<ActionResult> AddAVendorAsync(
        [FromBody] CommercialVendorCreate request,
        [FromServices] IValidator<CommercialVendorCreate> validator
        )
    {
        // is it a valid request? All the required stuff there? right format, etc.
        var validationResults = validator.Validate( request );
        if (!validationResults.IsValid)
        {

            return BadRequest(validationResults.ToDictionary()); // I'll talk about htis in a second
        }

        // create the thing we are going to save in the database (mapping)
        var entityToSave = new VendorEntity
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Site = request.Site,
            VendorType = VendorTypes.Commercial
        };
        _documentSession.Store( entityToSave );
        await _documentSession.SaveChangesAsync();
       // save it
       // map it to the thing we are going to return.

        return Ok(entityToSave);
    }

    
}
