using AutoMapper;
using Codelabs.Core.DTOs;
using Codelabs.Core.OutputModels;

namespace Codelabs.BLL.Mappers;

public class SolutionMapperProfile : Profile
{
    public SolutionMapperProfile()
    {
        CreateMap<SolutionDTO, SolutionForExerciseCompetingOutputModel>();
    }
}