using AutoMapper;
using Codelabs.Core.DTOs;
using Codelabs.Core.OutputModels;

namespace Codelabs.BLL.Mappers;

public class ExerciseMapperProfile : Profile
{
    public ExerciseMapperProfile()
    {
        CreateMap<ExerciseDTO, ExerciseForCompletingOutputModel>()
            .ForMember(dist => dist.LanguageName, opt => opt.MapFrom(e => e.Lesson.Language.Name.ToLower()));
    }
}