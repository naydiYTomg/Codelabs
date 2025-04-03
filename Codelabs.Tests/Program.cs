using Codelabs.Core;
using Codelabs.Core.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Codelabs.Tests;

class Program
{
    static async Task Main(string[] args)
    {
        var ctx = new DAL.Context();
        //Console.WriteLine(Options.ConnectionString);
        //Console.WriteLine(await ctx.Database.EnsureCreatedAsync());
        // var rust = new LanguageDTO { Name = "rust" };
        // await ctx.Languages.AddAsync(rust);
        // var author = new UserDTO
        // {
        //     Login = "GENERICauthor",
        //     Password = "strong"u8.ToArray(),
        //     Name = "Author",
        //     Surname = "Authorovich",
        //     Role = RoleType.Author,
        //     Phone = "88005553535",
        //     Email = "author@amail.ug",
        // };
        // await ctx.Users.AddAsync(author);
        // var genericUser = new UserDTO
        // {
        //     Login = "GENERICuser",
        //     Password = "verystrong"u8.ToArray(),
        //     Name = "QQQ",
        //     Surname = "WWW",
        //     Role = RoleType.Buyer
        // };
        // await ctx.Users.AddAsync(genericUser);
        // await ctx.SaveChangesAsync();
        //
        // await ctx.AuthorInfos.AddAsync(new AuthorInfoDTO
        // {
        //     UserID = author.ID,
        //     TIN = "141142421-15125-5125",
        //     BankName = "Alpha",
        //     BIC = "15551223",
        //     SettlementAccount = "151958993",
        //     CorrespondentAccount = "15457129",
        // });
        // var lesson = new LessonDTO
        // {
        //     Author = author,
        //     Name = "SuperMegaRustLesson",
        //     Description = "it's cool",
        //     ContentPath = "/home/codelabs/authors/1/lessons/1/content.md",
        //     IsDeleted = false,
        //     Language = rust,
        // };
        // await ctx.Lessons.AddAsync(lesson);
        // var exercise = new ExerciseDTO
        // {
        //     Lesson = lesson,
        //     Name = "Fibonacci",
        //     RequirementsPath = "/home/codelabs/authors/1/exercises/1/req.md",
        //     DesiredOutput = "13",
        //     ProgramInput = "7",
        //     IsDeleted = false,
        // };
        // await ctx.Exercises.AddAsync(exercise);
        //
        // var purchase = new PurchaseDTO
        // {
        //     IsVisited = false,
        //     Lesson = lesson,
        //     Date = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc),
        //     User = genericUser,
        // };
        // await ctx.Purchases.AddAsync(purchase);
        // var solution = new SolutionDTO
        // {
        //     Exercise = exercise,
        //     Purchase = purchase,
        //     Attempts = 1,
        //     IsSolved = true,
        //     CorrectAttemptPath = "/home/codelabs/users/2/solutions/1/correct.txt"
        // };
        // await ctx.Solutions.AddAsync(solution);
        // await ctx.SaveChangesAsync();

    }
}