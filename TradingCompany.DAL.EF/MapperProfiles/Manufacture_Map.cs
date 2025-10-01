using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingCompany.DAL.EF.MapperProfiles
{
    public class Manufacture_Map : AutoMapper.Profile
    {
        public Manufacture_Map()
        {
            CreateMap<DTO.Manufacture, Models.Manufacture>()
                .ForMember(dest => dest.ManufacturerId, opt => opt.MapFrom( src => src.ManufacturerID))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country))
                .ForMember(dest => dest.Products, opt => opt.Ignore());
            CreateMap<Models.Manufacture, DTO.Manufacture>()
                .ForMember(dest => dest.ManufacturerID, opt => opt.MapFrom(src => src.ManufacturerId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country));
        }
    }
}
