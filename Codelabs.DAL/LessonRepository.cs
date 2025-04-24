using Codelabs.Core.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Codelabs.DAL;

public class LessonRepository
{
    public async Task<List<LessonDTO>> GetAllExistingLessons()
    {
        await using var context = new Context();
        return await context.Lessons.Include(x => x.Author).Include(x => x.Language).Where(x => !(bool)x.IsDeleted).ToListAsync();
    }

    public async Task<List<LessonDTO>> GetAllExistingLessonsByAuthor(int authorId)
    {
        await using var context = new Context();
        return await context.Lessons.Include(x => x.Author).Include(x => x.Language).Where(x => x.Author.ID == authorId && !(bool)x.IsDeleted).ToListAsync();
    }

    public List<LanguageDTO> GetAllLanguages()
    {
        using var context = new Context();
        var languages = context.Languages.ToList();
        return languages;
    }

    public LanguageDTO GetLanguageByID(int ID)
    {
        using var context = new Context();
        var language = context.Languages.Include(l => l.Lessons).Where(l => l.ID == ID).FirstOrDefault();
        return language;
    }

    public LessonDTO? AddLesson(LessonDTO lesson, int? authorID, int? languageID)
    {
        using var context = new Context();
        lesson.IsDeleted = false;
        lesson.Author = context.Users.Include(u => u.Lessons)
                                    .Include(u => u.Purchases)
                                    .Where(u => u.ID == authorID)
                                    .FirstOrDefault();
        lesson.Language = context.Languages.Include(l => l.Lessons).Single(l => l.ID == languageID);
        context.Lessons.Add(lesson);
        context.SaveChanges();
        var newLesson = context.Lessons.ToList().LastOrDefault();
        return newLesson;
    }

    public void AddExercise(ExerciseDTO exercise, int? lessonID)
    {
        using var context = new Context();
        exercise.IsDeleted = false;
        exercise.Lesson = context.Lessons
                                .Include(l => l.Author)
                                .Include(l => l.Language)
                                .Include(l => l.Purchases)
                                .Include(l => l.Exercises)
                                .Where(l => l.ID == lessonID)
                                .FirstOrDefault();
        context.Exercises.Add(exercise);
        context.SaveChanges();
    }

    public LessonDTO? GetLessonByID(int ID)
    {
        using var context = new Context();
        var lesson = context.Lessons
                                .Include(l => l.Author)
                                .Include(l => l.Language)
                                .Include(l => l.Purchases)
                                .Include(l => l.Exercises)
                                .Where(l => l.ID == ID)
                                .FirstOrDefault();
        return lesson;
    }

    public void UpdateLessonByID(int ID, LessonDTO changedLesson, int? languageID)
    {
        using var context = new Context();
        var lesson = context.Lessons
                                .Include(l => l.Author)
                                .Include(l => l.Language)
                                .Include(l => l.Purchases)
                                .Include(l => l.Exercises)
                                .Where(l => l.ID == ID)
                                .FirstOrDefault();
        var language = context.Languages.Include(l => l.Lessons).Where(l => l.ID == languageID).FirstOrDefault();
        if (lesson != null)
        {
            lesson.ID = changedLesson.ID ?? lesson.ID;
            lesson.Name = changedLesson.Name ?? lesson.Name;
            lesson.Description = changedLesson.Description ?? lesson.Description;
            lesson.Cost = changedLesson.Cost ?? lesson.Cost;
            lesson.IsDeleted = changedLesson.IsDeleted ?? lesson.IsDeleted;
            lesson.Language = language ?? lesson.Language;
            context.SaveChanges();
        }
    }

    public void UpdateExerciseByID(int ID, ExerciseDTO changedExercise)
    {
        using var context = new Context();
        var exercise = context.Exercises
                                .Include(e => e.Lesson)
                                .Include(e => e.Solutions)
                                .Where(e => e.ID == ID)
                                .FirstOrDefault();
        if (exercise != null)
        {
            exercise.Name = changedExercise.Name ?? exercise.Name;
            exercise.DesiredOutput = changedExercise.DesiredOutput ?? exercise.DesiredOutput;
            exercise.ProgramInput = changedExercise.ProgramInput ?? exercise.ProgramInput;
            exercise.IsDeleted = changedExercise?.IsDeleted ?? exercise.IsDeleted;
            context.SaveChanges();
        }
    }
}