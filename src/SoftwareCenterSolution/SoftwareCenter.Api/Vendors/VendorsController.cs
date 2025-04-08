using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace SoftwareCenter.Api.Vendors;

[ApiController]
public class VendorsController : ControllerBase
{
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
     
        // if it is valid:
            // save it somewhere?
            // send them a response saying everything is cool, or what we did.

        return Ok(request);
    }

    
}
