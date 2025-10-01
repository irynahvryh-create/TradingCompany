using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingCompany.DAL.EF.MapperProfiles
{
    public class Category_Map : AutoMapper.Profile
    {
        public Category_Map()
        {
            CreateMap<TradingCompany.DTO.Category, TradingCompany.DAL.EF.Models.Category>()
                .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryID))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ReverseMap();
            CreateMap< TradingCompany.DAL.EF.Models.Category , TradingCompany.DTO.Category>()
                .ForMember(dest => dest.CategoryID, opt => opt
                .MapFrom(src => src.CategoryId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ReverseMap();
        }
    }
}