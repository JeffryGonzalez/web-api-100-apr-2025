using Microsoft.AspNetCore.Mvc;

namespace SoftwareCenter.Api.Vendors;

public class VendorsController : ControllerBase
{
    [HttpPost("/commercial-vendors")]
    public async Task<ActionResult> AddAVendorAsync(
        [FromBody] CommercialVendorCreate request
        )
    {
 
        // if they are a commercian vendor, do this stuff
        // if they are an open source vendor, do this stuff
        // if they are an in-house vendor, do this other stuff
        return Ok(request);
    }

    
}
