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
        }
    }
}
