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

    public async Task DeleteExerciseByID(int ID)
    {
        using var context = new Context();
        var exercise = await context.Exercises
                                .Include(e => e.Lesson)
                                .Include(e => e.Solutions)
                                .SingleAsync(e => e.ID == ID);
        if (exercise != null)
        {
            exercise.IsDeleted = true;
            await context.SaveChangesAsync();
        }
    }

    public async Task AddExercise(ExerciseDTO exercise, int? lessonID)
    {
        using var context = new Context();
        exercise.IsDeleted = false;
        exercise.Lesson = await context.Lessons
                                .Include(l => l.Author)
                                .Include(l => l.Language)
                                .Include(l => l.Purchases)
                                .Include(l => l.Exercises)
                                .SingleAsync(l => l.ID == lessonID);
        context.Exercises.Add(exercise);
        await context.SaveChangesAsync();
    }

    public async Task EditExerciseByID(int ID, ExerciseDTO changedExercise)
    {
        using var context = new Context();
        var exercise = await context.Exercises
            .Include(x => x.Lesson)
            .ThenInclude(x => x.Language)
            .SingleAsync(x => x.ID == ID);
        if (changedExercise != null)
        {
            exercise.Name = changedExercise.Name ?? exercise.Name;
            exercise.ProgramInput = changedExercise.ProgramInput ?? exercise.ProgramInput;
            exercise.DesiredOutput = changedExercise.DesiredOutput ?? exercise.DesiredOutput;
            exercise.IsDeleted = changedExercise.IsDeleted;
            await context.SaveChangesAsync();
        }
    }

    public async Task<List<ExerciseDTO>> GetAllExercisesOfLessonByExerciseID(int ID)
    {
        await using var context = new Context();
        var lesson = (await context.Exercises
            .Include(x => x.Lesson)
                .ThenInclude(lessonDto => lessonDto.Exercises)
            .Include(x => x.Lesson.Language)
            .SingleAsync(x => x.ID == ID)).Lesson;
        var exercises = lesson.Exercises ?? [];
        return exercises;
    }
}