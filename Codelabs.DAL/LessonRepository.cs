using Codelabs.Core.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Codelabs.DAL;

public class LessonRepository
{
    public async Task<List<LessonDTO>> GetAllExistingLessons()
    {
        await using var context = new Context();
        return await context.Lessons
            .Include(x => x.Author)
            .Include(x => x.Language)
            .Where(x => !x.IsDeleted)
            .ToListAsync();
    }

    public async Task DeleteLessonByID(int ID)
    {
        await using var context = new Context();
        var lesson = await context.Lessons
            .Include(x => x.Author)
            .Include(x => x.Language)
            .Include(x => x.Exercises)
            .SingleAsync(x => x.ID == ID && !x.IsDeleted);
        lesson.IsDeleted = true;
        await context.SaveChangesAsync();
    }

    public async Task<List<LessonDTO>> GetAllExistingLessonsByAuthor(int authorID)
    {
        await using var context = new Context();
        return await context.Lessons
            .Include(x => x.Author)
            .Include(x => x.Language)
            .Where(x => x.Author.ID == authorID && !x.IsDeleted)
            .ToListAsync();
    }

    public async Task<LessonDTO> GetLessonByID(int lessonID)
    {
        await using var context = new Context();
        var lesson = await context.Lessons
            .Include(x => x.Author)
            .Include(x => x.Language)
            .Include(x => x.Exercises)
            .SingleAsync(x => x.ID == lessonID && !x.IsDeleted);
        return lesson;
    }

    public async Task<LessonDTO> GetRawLessonByID(int lessonID)
    {
        await using var context = new Context();
        var lesson = await context.Lessons.SingleAsync(x => x.ID == lessonID);
        return lesson;
    }

    public async Task<List<LessonDTO>> GetAllExistingLessonsFromPurchasesByUser(int userID)
    {
        await using var context = new Context();
        var lessons = await context.Purchases
            .Where(p => p.User.ID == userID && !p.Lesson.IsDeleted)
            .Include(p => p.Lesson)
                .ThenInclude(l => l.Language)
            .Select(p => p.Lesson)
            .ToListAsync();
        return lessons;
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
        var language = context.Languages
            .Include(l => l.Lessons)
            .Where(l => l.ID == ID)
            .FirstOrDefault();
        return language;
    }

    public async Task<LessonDTO?> AddLesson(LessonDTO lesson, int? authorID, int? languageID)
    {
        using var context = new Context();
        lesson.IsDeleted = false;
        lesson.Author = context.Users.Include(u => u.Lessons)
                                    .Include(u => u.Purchases)
                                    .Single(u => u.ID == authorID);

        lesson.Language = context.Languages
            .Include(l => l.Lessons)
            .Single(l => l.ID == languageID);

        lesson.CreatedTime = DateTime.UtcNow;
        Console.WriteLine(lesson.CreatedTime);
        var newLesson = context.Lessons.Add(lesson).Entity;
        await context.SaveChangesAsync();

        // var newLesson = context.Lessons
        //     .ToList()
        //     .LastOrDefault();
        return newLesson;
    }

    public async Task EditLessonByID(int ID, LessonDTO changedLesson, int? languageID)
    {
        using var context = new Context();
        var lesson = await context.Lessons
                                .Include(l => l.Author)
                                .Include(l => l.Language)
                                .Include(l => l.Purchases)
                                .Include(l => l.Exercises)
                                .SingleAsync(l => l.ID == ID);

        var language = await context.Languages
            .Include(l => l.Lessons)
            .SingleAsync(l => l.ID == languageID);

        if (lesson != null)
        {
            lesson.ID = changedLesson.ID ?? lesson.ID;
            lesson.Name = changedLesson.Name ?? lesson.Name;
            lesson.Description = changedLesson.Description ?? lesson.Description;
            lesson.Cost = changedLesson.Cost ?? lesson.Cost;
            lesson.IsDeleted = changedLesson.IsDeleted;
            lesson.Language = language ?? lesson.Language;
            await context.SaveChangesAsync();
        }
    }
}