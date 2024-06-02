using AutoMapper;
using SE160548_IdetityAjaxASP.NETCoreWebAPI.Models;
using SE160548_IdetityAjaxASP.NETCoreWebAPI.Repository.Models;

namespace SE160548_IdetityAjaxASP.NETCoreWebAPI.Helper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Product, ProductMapper>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ProductId))
                .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.ProductName))
                .ForMember(dest => dest.Weight, opt => opt.MapFrom(src => src.Weight))
                .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => src.UnitPrice))
                .ForMember(dest => dest.UnitsInStock, opt => opt.MapFrom(src => src.UnitsInStock))
                .ReverseMap();
            CreateMap<Member, MemberMapper>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.MemberId))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password))
                .ReverseMap();

           CreateMap<Product,CreateProductMapper>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ProductId))
                .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.ProductName))
                .ForMember(dest => dest.Weight, opt => opt.MapFrom(src => src.Weight))
                .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => src.UnitPrice))
                .ForMember(dest => dest.UnitsInStock, opt => opt.MapFrom(src => src.UnitsInStock))
                .ReverseMap();
        }

    }
}
