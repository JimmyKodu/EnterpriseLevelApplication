using AutoMapper;
using EnterpriseApp.Application.DTOs;
using EnterpriseApp.Core.Entities;

namespace EnterpriseApp.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Product, ProductDto>()
            .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category != null ? src.Category.Name : null));
        CreateMap<Category, CategoryDto>();
    }
}
