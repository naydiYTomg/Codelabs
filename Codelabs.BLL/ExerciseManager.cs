using AutoMapper;
using Codelabs.BLL.Mappers;
using Codelabs.Core.OutputModels;
using Codelabs.DAL;

namespace Codelabs.BLL;
public class ExerciseManager
{
    private readonly ExerciseRepository _repository = new();
    private readonly Mapper _mapper;

    public ExerciseManager()
    {
        var config = new MapperConfiguration(cfg => cfg.AddProfile(new ExerciseMapperProfile()));
        _mapper = new Mapper(config);
    }

    public async Task<ExerciseForCompletingOutputModel> GetExerciseByID(int exerciseID)
    {
        var got = await _repository.GetExerciseByID(exerciseID);
        var model = _mapper.Map<ExerciseForCompletingOutputModel>(got);
        return model;
    }
}