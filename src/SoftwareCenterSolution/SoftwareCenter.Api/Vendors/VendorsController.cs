using Microsoft.AspNetCore.Mvc;

namespace SoftwareCenter.Api.Vendors;

public class VendorsController : ControllerBase
{
    [HttpPost("/commercial-vendors")]
    public async Task<ActionResult> AddAVendorAsync(
        [FromBody] CommercialVendorCreate request
        )
    {
        // is it a valid request? All the required stuff there? right format, etc.
        // if not:
            // and can it be fixed (maybe, probably not)
            // if not, send an error to the user agent (400)
        // if it is valid:
            // save it somewhere?
            // send them a response saying everything is cool, or what we did.

        return Ok(request);
    }

    
}
