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

    public async Task<LessonDTO> GetLessonByID(int lessonID)
    {
        await using var context = new Context();
        return await context.Lessons
            .Include(x => x.Author)
            .Include(x => x.Language)
            .Where(x => x.ID == lessonID && !x.IsDeleted)
            .FirstOrDefaultAsync();
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