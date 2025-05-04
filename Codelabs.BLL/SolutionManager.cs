using AutoMapper;
using Codelabs.BLL.Mappers;
using Codelabs.Core.DTOs;
using Codelabs.Core.OutputModels;
using Codelabs.DAL;

namespace Codelabs.BLL;

public class SolutionManager
{
    private readonly SolutionRepository _repository = new();
    private readonly Mapper _mapper;

    public SolutionManager()
    {
        var config = new MapperConfiguration(cfg => cfg.AddProfile(new SolutionMapperProfile()));
        _mapper = new Mapper(config);
    }
    
    public async Task<bool> IsUserHasSolutionOfExercise(int userID, int exerciseID)
    {
        var result = await _repository.IsUserHasSolutionOfExercise(userID, exerciseID);
        return result;
    }

    public async Task<int> AddSolution(int purchaseID, int exerciseID)
    {
        var got = await _repository.AddSolution(purchaseID, exerciseID);
        return got;
    }

    public async Task UpdateSolution(int solutionID, string attempt, bool solved)
    {
        await _repository.UpdateSolution(solutionID, attempt, solved);
    }

    public async Task<SolutionForExerciseCompetingOutputModel> GetSolutionByID(int solutionID)
    {
        var got = await _repository.GetSolutionByID(solutionID);
        var model = _mapper.Map<SolutionForExerciseCompetingOutputModel>(got);
        return model;
    }

    public async Task<SolutionForExerciseCompetingOutputModel> GetUserSolutionWithExerciseID(int userID, int exerciseID)
    {
        var got = await _repository.GetUserSolutionWithExerciseID(userID, exerciseID);
        var model = _mapper.Map<SolutionForExerciseCompetingOutputModel>(got);
        return model;
    }

    public async Task<List<SolutionForExerciseCompetingOutputModel>>
        GetAllSolutionsOfLessonExercisesByUserIDAndExerciseID(int exerciseID, int userID)
    {
        var got = await _repository.GetAllSolutionsOfLessonExercisesByUserIDAndExerciseID(exerciseID, userID);
        var models = _mapper.Map<List<SolutionForExerciseCompetingOutputModel>>(got);
        return models;
    }
}