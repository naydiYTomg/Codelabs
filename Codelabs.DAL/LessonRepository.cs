using Codelabs.Core.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Codelabs.DAL;

public class LessonRepository
{
    public async Task<List<LessonDTO>> GetAllExistingLessons()
    {
        await using var context = new Context();
        return await context.Lessons.Include(x => x.Author).Include(x => x.Language).Where(x => !x.IsDeleted).ToListAsync();
    }

    public async Task<List<LessonDTO>> GetAllExistingLessonsByAuthor(int authorId)
    {
        await using var context = new Context();
        return await context.Lessons.Include(x => x.Author).Include(x => x.Language).Where(x => x.Author.ID == authorId && !x.IsDeleted).ToListAsync();
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
        var language = context.Languages.Include(l => l.Lessons).Where(l => l.ID==ID).FirstOrDefault();
        return language;
    }

    public LessonDTO? AddLesson(LessonDTO lesson) 
    {
        using var context = new Context();
        context.Lessons.Add(lesson);
        context.SaveChanges();
        var newLesson = context.Lessons.LastOrDefault();
        return newLesson;
    }

    public void AddExercise(ExerciseDTO exercise)
    {
        using var context = new Context();
        context.Exercises.Add(exercise);
        context.SaveChanges();
    }
}