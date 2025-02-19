using GoodsBlazor.BLL.Services.Product.Commands.CreateProduct;
using GoodsBlazor.BLL.Services.Product.Commands.Delete;
using GoodsBlazor.BLL.Services.Product.Commands.Update;
using GoodsBlazor.BLL.Services.Product.Queries.GetAllProducts;
using GoodsBlazor.BLL.Services.Product.Queries.GetProductById;
using GoodsBlazor.BLL.Services.ProductType.Commands.Queries;
using GoodsBlazor.DAL.Entities;
using GoodsBlazor.Shared.Dtos;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GoodsBlazor.API.Controllers;

[ApiController]
[Route("api/products")]
[Authorize]
public class ProductController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetAll()
    {
        var products = await mediator.Send(new GetAllProductsQuery()); 
        return Ok(products);
    }

    [HttpGet("productstype")]
    [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<GoodsBlazor.Shared.Dtos.ProductTypeDto>>> GetAllProductType()
    {
        var products = await mediator.Send(new GetAllProductsTypeQuery());
        return Ok(products);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductDto?>> GetById([FromRoute] int id)
    {
        var product = await mediator.Send(new GetProductByIdQuery(id));
        return Ok(product);
    }

    [HttpPatch("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize(Roles = nameof(Role.Admin))]
    public async Task<IActionResult> UpdateProduct([FromRoute] int id, UpdateProductCommand command)
    {
        command.Id = id;
        await mediator.Send(command);

        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize(Roles = nameof(Role.Admin))]
    public async Task<IActionResult> DeleteProduct([FromRoute] int id)
    {
        await mediator.Send(new DeleteProductCommand(id));

        return NoContent();
    }

    [HttpPost]
    [Authorize(Roles = nameof(Role.Admin))]
    public async Task<IActionResult> CreateProduct(CreateProductCommand command)
    {
        int id = await mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id }, null);
    }
}
