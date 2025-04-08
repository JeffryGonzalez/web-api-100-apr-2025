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

 

        return Ok(request);
    }

    
}
