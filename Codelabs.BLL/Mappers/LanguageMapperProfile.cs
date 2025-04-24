using AutoMapper;
using Codelabs.Core.DTOs;
using Codelabs.Core.OutputModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codelabs.BLL.Mappers
{
    public class LanguageMapperProfile : Profile
    {
        public LanguageMapperProfile() 
        {
            CreateMap<LanguageDTO, LanguageOutputModel>();
            CreateMap<LanguageOutputModel, LanguageDTO>();
        }
    }
}
