using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace DemoApi.Products;

[ApiController]
public class ProductsController : ControllerBase
{

    [HttpPost("/products")]
    public async Task<ActionResult> AddProduct([FromBody] ProductCreateModel request)
    {
        // what if you can fix it
        //if(!ModelState.IsValid)
        //{
        //    return BadRequest(ModelState);
        //}
       
        return Ok(request);
    }
}

public record ProductCreateModel
{
    [Required, MinLength(5)]
    public string Name { get; init; } = string.Empty;
    [Required]
    public string Sku { get; init; } = string.Empty;
    [Required, Range(1, int.MaxValue)]
    public int QtyOnHand { get; init; }
    [Required]
    public decimal? CostPerUnit { get; init; }
  
}
