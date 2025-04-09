using FluentValidation;
using Marten;
using Microsoft.AspNetCore.Mvc;

namespace SoftwareCenter.Api.Vendors;

[ApiController]
public class VendorsController(IDocumentSession documentSession, IProvideIdentity _identityLookup) : ControllerBase
{
    [HttpPost("/commercial-vendors")]
    public async Task<ActionResult> AddAVendorAsync([FromBody] CommercialVendorCreateModel request,
        [FromServices] IValidator<CommercialVendorCreateModel> validator)
    {
        var validationResults = validator.Validate(request);
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
            AddedOn = DateTimeOffset.Now,
            AddedBy = await _identityLookup.GetNameOfCallerAsync(),
            VendorType = VendorTypes.Commercial,
            Poc = new PointOfContact(new Dictionary<ContactMechanisms, string>
            {
                { ContactMechanisms.primaryPhone, request.ContactPhone },
                { ContactMechanisms.PrimaryEmail, request.ContactEmail }
            })
        };
        documentSession.Store(entityToSave);
        await documentSession.SaveChangesAsync();
        // save it
        // map it to the thing we are going to return.

        return Created($"/commercial-vendors/{entityToSave.Id}", entityToSave);
    }

    [HttpGet("/commercial-vendors/{id:guid}")]
    public async Task<ActionResult> GetVendorById(Guid id)
    {
        var response = await documentSession.Query<VendorEntity>().SingleOrDefaultAsync(v => v.Id == id);
        if (response is null)
        {
            return NotFound();
        }
        else
        {
            return Ok(response);
        }
    }
}

public interface IProvideIdentity
{
    Task<string> GetNameOfCallerAsync();
}