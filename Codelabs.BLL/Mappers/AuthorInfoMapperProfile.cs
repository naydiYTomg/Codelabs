using AutoMapper;
using Codelabs.Core.DTOs;
using Codelabs.Core.InputModels;
using Codelabs.Core.OutputModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
