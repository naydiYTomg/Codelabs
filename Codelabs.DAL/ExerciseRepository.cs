using Codelabs.Core.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Codelabs.DAL;

public class ExerciseRepository
{

    public async Task<ExerciseDTO> GetExerciseByID(int exerciseID)
    {
        await using var context = new Context();
        var result = await context.Exercises
            .Include(x => x.Lesson)
            .ThenInclude(x => x.Language)
            .SingleAsync(x => x.ID == exerciseID);
        return result;
    }
}