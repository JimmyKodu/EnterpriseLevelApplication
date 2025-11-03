using Microsoft.AspNetCore.Mvc;
using MediatR;
using EnterpriseApp.Application.Commands.Products;
using EnterpriseApp.Application.Queries.Products;
using EnterpriseApp.Application.DTOs;

namespace EnterpriseApp.Web.Controllers.Api;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<ProductsController> _logger;

    public ProductsController(IMediator mediator, ILogger<ProductsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ProductDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting product with ID: {ProductId}", id);

        var query = new GetProductByIdQuery(id);
        var result = await _mediator.Send(query, cancellationToken);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateProductCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating new product: {ProductName}", command.Name);

        var productId = await _mediator.Send(command, cancellationToken);

        return CreatedAtAction(nameof(GetById), new { id = productId }, productId);
    }
}
