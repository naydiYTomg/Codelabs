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

    public async Task<List<LessonDTO>> GetAllExistingLessonsByAuthor(int authorID)
    {
        await using var context = new Context();
        return await context.Lessons.Include(x => x.Author).Include(x => x.Language).Where(x => x.Author.ID == authorID && !x.IsDeleted).ToListAsync();
    }

    public async Task<LessonDTO> GetLessonByID(int lessonID)
    {
        await using var context = new Context();
        return await context.Lessons
            .Include(x => x.Author)
            .Include(x => x.Language)
            .Where(x => x.ID == lessonID && !x.IsDeleted)
            .FirstOrDefaultAsync();
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
}