using AutoMapper;
using Codelabs.Core.DTOs;
using Codelabs.Core.OutputModels;

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
