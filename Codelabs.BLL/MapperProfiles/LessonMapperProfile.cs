using AutoMapper;
using Codelabs.Core.DTOs;
using Codelabs.Core.OutputModels;

namespace Codelabs.BLL.MapperProfiles;

public class LessonMapperProfile : Profile
{
    public LessonMapperProfile()
    {
        CreateMap<LessonDTO, LessonForShowcaseOutputModel>();
    }
}