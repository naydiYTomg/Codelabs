using Microsoft.EntityFrameworkCore;

namespace Codelabs.Tests;

class Program
{
    static async Task Main(string[] args)
    {
        await using var ctx = new DAL.Context();
        (await ctx.Users.Where(x => x.Name != null).ToListAsync()).ForEach(Console.WriteLine);
        
    }
}