using AutoMapper;
using Codelabs.Core.DTOs;
using Codelabs.Core.InputModels;
using Codelabs.Core.OutputModels;

namespace Codelabs.BLL.Mappers
{
    public class ExerciseMapperProfile : Profile
    {
        public ExerciseMapperProfile()
        {
            CreateMap<ExerciseInputModel, ExerciseDTO>();
            CreateMap<ExerciseDTO, ExerciseOutputModel>();
            CreateMap<ExerciseOutputModel, ExerciseInputModel>();
            CreateMap<ExerciseDTO, ExerciseForCompletingOutputModel>()
                .ForMember(ex => ex.LanguageName,
                    opt => opt.MapFrom(e => e.Lesson.Language!.Name.Equals("c#", StringComparison.CurrentCultureIgnoreCase) ? "csharp" : e.Lesson.Language!.Name.ToLower()));
        }
    }
}
