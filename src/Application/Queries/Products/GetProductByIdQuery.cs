using EnterpriseApp.Application.DTOs;
using MediatR;

namespace EnterpriseApp.Application.Queries.Products;

public record GetProductByIdQuery(Guid Id) : IRequest<ProductDto?>;
