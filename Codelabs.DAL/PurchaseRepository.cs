using Codelabs.Core.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Codelabs.DAL;

public class PurchaseRepository
{
    public async Task<List<PurchaseDTO>> GetAllUserPurchases(int userID)
    {
        await using var context = new Context();
        var purchases = await context.Purchases.Include(x => x.User)
            .ThenInclude(x => x.Lessons)
            .Include(x => x.Lesson)
            .ToListAsync();
        return purchases;
    }

    public async Task CreatePurchase(PurchaseDTO purchase)
    {
        await using var context = new Context();
        await context.Purchases.AddAsync(purchase);
        await context.SaveChangesAsync();
    }
}