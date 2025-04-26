using AutoMapper;
using Codelabs.Core.DTOs;
using Codelabs.Core.InputModels;
using Codelabs.Core.OutputModels;

namespace Codelabs.BLL.Mappers
{
    public class AuthorInfoMapperProfile : Profile
    {
        public AuthorInfoMapperProfile()
        {
            CreateMap<AuthorInfoDTO, AuthorInfoOutputModel>();
            CreateMap<AuthorInfoOutputModel, AuthorInfoDTO>();

            CreateMap<AuthorInfoInputModel, AuthorInfoDTO>();
        }
    }
}
