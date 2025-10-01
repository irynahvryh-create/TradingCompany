using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingCompany.DAL.EF.MapperProfiles

/*CreateMap<TradingCompany.DTO.Category, TradingCompany.DAL.EF.Models.Category>()
            .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryID))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
            .ReverseMap();
*/
{
    public class ProductLog_Map : AutoMapper.Profile
    {
        public ProductLog_Map() {
            CreateMap<DTO.ProductLog, Models.ProductLog>()
                .ForMember(dest => dest.LogId, opt => opt.MapFrom(src => src.LogID))
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductID))
                .ForMember(dest => dest.OldPrice, opt => opt.MapFrom(src => src.OldPrice))
                .ForMember(dest => dest.NewPrice, opt => opt.MapFrom(src => src.NewPrice))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.Comment, opt => opt.MapFrom(src => src.Comment))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date))
                .ReverseMap();
            CreateMap<Models.ProductLog, DTO.ProductLog>()
                .ForMember(dest => dest.LogID, opt => opt.MapFrom(src => src.LogId))
                .ForMember(dest => dest.ProductID, opt => opt.MapFrom(src => src.ProductId))
                .ForMember(dest => dest.OldPrice, opt => opt.MapFrom(src => src.OldPrice))
                .ForMember(dest => dest.NewPrice, opt => opt.MapFrom(src => src.NewPrice))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.Comment, opt => opt.MapFrom(src => src.Comment))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date))
                .ReverseMap();
        }

    }
      
}