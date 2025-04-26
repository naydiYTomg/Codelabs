using AutoMapper;
using Codelabs.Core.DTOs;
using Codelabs.Core.InputModels;
using Codelabs.Core.OutputModels;
using System.Text;

namespace Codelabs.BLL.Mappers
{
    public class UserMapperProfile : Profile
    {
        public UserMapperProfile()
        {
            CreateMap<UserDTO, UserOutputModel>()
                .ForMember(dist => dist.Password, opt => 
                opt.MapFrom(u => Encoding.UTF8.GetString(u.Password)));

            CreateMap<UserOutputModel, UserDTO>()
                .ForMember(dist => dist.Password, opt => 
                opt.MapFrom(u => Encoding.UTF8.GetBytes(u.Password)));

            CreateMap<UserInputModel, UserDTO>()
                .ForMember(dist => dist.Password, opt => 
                opt.MapFrom(u => Encoding.UTF8.GetBytes(u.Password)));
        }
    }
}
