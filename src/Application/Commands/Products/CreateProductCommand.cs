using MediatR;

namespace EnterpriseApp.Application.Commands.Products;

public record CreateProductCommand(
    string Name,
    string Description,
    decimal Price,
    int StockQuantity,
    string Sku,
    Guid CategoryId
) : IRequest<Guid>;
