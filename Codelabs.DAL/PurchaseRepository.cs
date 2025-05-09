using Codelabs.Core.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Codelabs.DAL;

public class PurchaseRepository
{
    public async Task<List<PurchaseDTO>> GetAllUserPurchases(int userID)
    {
        await using var context = new Context();
        var purchases = await context.Purchases
            .OrderBy(x => x.ID)
            .Include(x => x.User)
            .ThenInclude(x => x.Lessons)
            .Include(x => x.Lesson)
            .ToListAsync();
        return purchases;
    }

    public async Task<int> GetPurchaseIDWhereLessonHasExercise(int exerciseID, int userID)
    {
        await using var context = new Context();
        var lesson = (await context.Exercises.Include(x => x.Lesson)
            .Where(x => x.ID == exerciseID)
            .Select(x => x.Lesson)
            .FirstAsync()).ID;
        var purchase = (await context.Purchases.SingleAsync(x => x.Lesson.ID == lesson && x.User.ID == userID)).ID;
        return purchase;
    }

    public async Task CreatePurchase(PurchaseDTO purchase, int userID, int lessonID)
    {
        await using var context = new Context();
        purchase.User = await context.Users.SingleAsync(x => x.ID == userID);
        purchase.Lesson = await context.Lessons.SingleAsync(x => x.ID == lessonID);
        await context.Purchases.AddAsync(purchase);
        await context.SaveChangesAsync();
    }

    public bool IsUserBoughtLesson(int userID, int lessonID)
    {
        using var context = new Context();
        var purchase = context.Purchases
                                    .Include(p => p.Lesson)
                                    .Include(p => p.User)
                                    .Where(p => p.Lesson.ID == lessonID
                                                && p.User.ID == userID).FirstOrDefault();
        return purchase != null;
    }

    public async Task MarkTruePurchaseByUserAndLessonID(int userID, int lessonID)
    {
        await using var context = new Context();
        var purchase = await context.Purchases
            .Where(p => p.User.ID == userID)
            .Where(p => p.Lesson.ID == lessonID)
            .FirstOrDefaultAsync();

        if(purchase.IsVisited == false)
        {
            purchase.IsVisited = true;
            await context.SaveChangesAsync();
        }

        //.OrderBy(x => x.ID)
        //.Include(x => x.User)
        //.ThenInclude(x => x.Lessons)
        //.Include(x => x.Lesson)
    }
}