using AutoMapper;
using EnterpriseApp.Application.DTOs;
using EnterpriseApp.Core.Entities;
using EnterpriseApp.Core.Interfaces.Repositories;
using MediatR;

namespace EnterpriseApp.Application.Queries.Products;

public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductDto?>
{
    private readonly IRepository<Product> _productRepository;
    private readonly IMapper _mapper;

    public GetProductByIdQueryHandler(IRepository<Product> productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<ProductDto?> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(request.Id, cancellationToken);
        return product != null ? _mapper.Map<ProductDto>(product) : null;
    }
}
