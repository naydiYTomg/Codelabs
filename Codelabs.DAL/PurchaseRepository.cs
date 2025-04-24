using Codelabs.Core.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Codelabs.DAL;

public class PurchaseRepository
{
    public async Task<List<PurchaseDTO>> GetAllUserPurchases(int userID)
    {
        await using var context = new Context();
        var purchases = await context.Purchases.OrderBy(x => x.ID).Include(x => x.User)
            .ThenInclude(x => x.Lessons)
            .Include(x => x.Lesson)
            .ToListAsync();
        return purchases;
    }

    public async Task CreatePurchase(PurchaseDTO purchase, int userID, int lessonID)
    {
        await using var context = new Context();
        purchase.User = await context.Users.SingleAsync(x => x.ID == userID);
        purchase.Lesson = await context.Lessons.SingleAsync(x => x.ID == lessonID);
        await context.Purchases.AddAsync(purchase);
        await context.SaveChangesAsync();
        // Console.WriteLine($"Successfully created purchase with ID {context.Purchases.OrderBy(x => x.ID).Last().ID}");
    }
}