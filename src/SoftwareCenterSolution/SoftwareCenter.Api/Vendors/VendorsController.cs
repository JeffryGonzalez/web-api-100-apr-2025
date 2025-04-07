using Microsoft.AspNetCore.Mvc;

namespace SoftwareCenter.Api.Vendors;

public class VendorsController : ControllerBase
{
    [HttpPost("/vendors")]
    public async Task<ActionResult> AddAVendorAsync()
    {
        return Ok();
    }
}
