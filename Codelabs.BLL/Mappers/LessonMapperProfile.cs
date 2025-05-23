using AutoMapper;
using Codelabs.Core.DTOs;
using Codelabs.Core.InputModels;
using Codelabs.Core.OutputModels;

namespace Codelabs.BLL.Mappers;
public class LessonMapperProfile : Profile
{
    public LessonMapperProfile()
    {
        CreateMap<LessonDTO, LessonDTO>()
            .ForMember(l => l.Exercises, (options) => options.Ignore())
            .ForMember(l => l.ContentPath, (options) => options.Ignore());
        CreateMap<LessonDTO, LessonForShowcaseOutputModel>();
        CreateMap<LessonDTO, LessonOutputModel>();
        CreateMap<LessonInputModel, LessonDTO>()
            .ForMember(l => l.Exercises, (options) => options.Ignore());
    }
}