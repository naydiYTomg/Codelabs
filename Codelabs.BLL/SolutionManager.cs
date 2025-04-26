using Codelabs.Core.DTOs;
using Codelabs.DAL;

namespace Codelabs.BLL;

public class SolutionManager
{
    private readonly SolutionRepository _repository = new();


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
}