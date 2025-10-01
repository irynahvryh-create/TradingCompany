using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace TradingCompany.DAL.EF.MapperProfiles
{
    public class Product_Map : AutoMapper.Profile
    {
        public Product_Map()
        {
            CreateMap<DTO.Product, EF.Models.Product>()
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductID))
                .ForMember(dest => dest.CategoryId, opt => opt
                .MapFrom(src => src.CategoryID))
                .ForMember(dest => dest.ManufacturerId, opt => opt
                .MapFrom(src => src.ManufacturerID))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.PriceIn, opt => opt.MapFrom(src => src.PriceIn))
                .ForMember(dest => dest.PriceOut, opt => opt.MapFrom(src => src.PriceOut))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ReverseMap();
            CreateMap<EF.Models.Product, DTO.Product>()
                .ForMember(dest => dest.ProductID, opt => opt.MapFrom(src => src.ProductId))
                .ForMember(dest => dest.CategoryID, opt => opt
                .MapFrom(src => src.CategoryId))
                .ForMember(dest => dest.ManufacturerID, opt => opt
                .MapFrom(src => src.ManufacturerId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.PriceIn, opt => opt.MapFrom(src => src.PriceIn))
                .ForMember(dest => dest.PriceOut, opt => opt.MapFrom(src => src.PriceOut))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ReverseMap();

        }
    }
}