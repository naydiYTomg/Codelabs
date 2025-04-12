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

    public async Task<List<LessonDTO>> GetAllExistingLessonsFromPurchasesByUser(int userID)
    {
        await using var context = new Context();
        var step1 = context.Purchases.Include(x => x.User);

        var step2 = step1.Where(x => x.User.ID == userID);

        var step3 = step2.Select(p => p.Lesson);

        var step4 = step3.Include(x => x.Author);

        var step5 = step4.Include(x => x.Language);

        var step6 = step5.Where(x => !x.IsDeleted);

        var step7 = await step6.ToListAsync();

        //return await context.Purchases
        //            .Include(x => x.User)
        //            .Where(x => x.User.ID == userID)
        //            .Select(p => p.Lesson)
        //            .Include(x => x.Author)
        //            .Include(x => x.Language)
        //            .Where(x => !x.IsDeleted)
        //            .ToListAsync();
        return step7;
    }
}