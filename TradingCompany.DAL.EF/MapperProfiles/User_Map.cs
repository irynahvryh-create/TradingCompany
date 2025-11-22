using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using TradingCompany.DTO;


namespace TradingCompany.DAL.EF.MapperProfiles
{
    public class User_Map : Profile
    {   public User_Map()
        {
            CreateMap<User, Models.User>()
                .ForMember(dest => dest.Password, opt => opt.Ignore())
                .ForMember(dest => dest.Salt, opt => opt.Ignore())

                .ForMember(dest => dest.UserPrivileges,
                src => src.MapFrom(u => u.Privileges.Select(p => new Models.UserPrivilege
                {
                    UserId = u.UserID,
                    PrivilegeId = p.PrivilegeID,
                    RowInsertTime = DateTime.UtcNow
                })));
            CreateMap<Models.User, User>()
                .ForMember(dest => dest.Privileges,
                src => src.MapFrom(u => u.UserPrivileges.Select(up => up.Privilege)));
        }
    }
}
