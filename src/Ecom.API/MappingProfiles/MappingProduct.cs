using AutoMapper;
using Ecom.Core.Dtos;
using Ecom.core.Entities;
using Ecom.API.Helper;

namespace Ecom.API.MappingProfiles
{
    public class MappingProduct : Profile
    {
        public MappingProduct()
        {
            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
                .ForMember(dest => dest.ProductPicture, opt => opt.MapFrom<ProductUrlResolver>())
                .ReverseMap();
            CreateMap<CreateProductDto, Product>().ReverseMap();

            CreateMap<UpdateProductDto, Product>().ReverseMap();
        }
    }
}
