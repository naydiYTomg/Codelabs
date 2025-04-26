using Codelabs.Core.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Codelabs.DAL;

public class SolutionRepository
{


    public async Task<bool> IsUserHasSolutionOfExercise(int userID, int exerciseID)
    {
        await using var context = new Context();
        var result = await context.Solutions
            .AnyAsync(x => x.Exercise.ID == exerciseID && x.Purchase.User.ID == userID);
        return result;
    }

    public async Task<int> AddSolution(SolutionDTO dto)
    {
        await using var context = new Context();
        var id = (await context.Solutions.AddAsync(dto)).Entity.ID;
        return id;
    }

    public async Task<int> AddSolution(int purchaseID, int exerciseID)
    {
        await using var context = new Context();
        var purchase = await context.Purchases.SingleAsync(x => x.ID == purchaseID);
        var exercise = await context.Exercises.SingleAsync(x => x.ID == exerciseID);
        var dto = new SolutionDTO { Attempts = 1, Exercise = exercise, Purchase = purchase };
        var id = (await context.Solutions.AddAsync(dto)).Entity.ID;
        return id;
    }
    
    public async Task UpdateSolution(int solutionID, string attempt, bool solved)
    {
        await using var context = new Context();
        var result = await context.Solutions.SingleAsync(x => x.ID == solutionID);
        result.Attempts++;
        if (solved) result.CorrectAttemptPath = attempt;
        result.IsSolved = solved;

        await context.SaveChangesAsync();

    }
    public async Task<SolutionDTO> GetSolutionByID(int solutionID)
    {
        await using var context = new Context();
        var result = await context.Solutions.SingleAsync(x => x.ID == solutionID);
        return result;
    }
}