using Codelabs.Core;
using Codelabs.Core.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Codelabs.Tests;

class Program
{
    static async Task Main(string[] args)
    {
        var ctx = new DAL.Context();
        var lalka = new UserDTO
            { Name = "LALKA", Role = RoleType.Author, Login = "lalka123", Password = "strong"u8.ToArray() };
        var lopaklosh =  new UserDTO
            { Name = "Lopaklosh", Role = RoleType.Author, Login = "lop", Password = "strong"u8.ToArray() };
        var jjd =  new UserDTO
            { Name = "JJD", Role = RoleType.Author, Login = "jdjd", Password = "strong"u8.ToArray() };
        var john =  new UserDTO
            { Name = "John", Role = RoleType.Author, Login = "jdh993", Password = "strong"u8.ToArray() };
        var kirk =  new UserDTO
            { Name = "Kirk", Role = RoleType.Author, Login = "kirin", Password = "strong"u8.ToArray() };
        var lal =  new UserDTO
            { Name = "Lal", Role = RoleType.Author, Login = "lalka321", Password = "strong"u8.ToArray() };
        var da = new UserDTO
            { Name = "Da", Role = RoleType.Author, Login = "dadada", Password = "strong"u8.ToArray() };
        await ctx.Users.AddRangeAsync(lalka, lopaklosh, jjd, john, kirk, lal, da);
        var cpp = new LanguageDTO { Name = "C++" };
        var rust = new LanguageDTO { Name = "Rust" };
        var assembly = new LanguageDTO { Name = "Assembly" };
        var java = new LanguageDTO { Name = "Java" };
        var typescript = new LanguageDTO { Name = "TypeScript" };
        var cs = new LanguageDTO { Name = "C#" };
        var bfuck = new LanguageDTO { Name = "Brainfuck" };
        await ctx.Languages.AddRangeAsync(cpp, rust, assembly, java, typescript, cs, bfuck);

        var cppL = new LessonDTO { Author = lalka, Language = cpp, Description = "SuperMega", Name = "SuperCppLesson", ContentPath = "test" };
        var rustL = new LessonDTO { Author = lopaklosh, Language = rust, Description = "After this lesson you can everything", Name = "MegaRustLesson", ContentPath = "test" };
        var asmL = new LessonDTO { Author = jjd, Language = assembly, Description = "AFTER THIS LESSON YOU WON'T WANT TO LIVE ANYMORE.", Name = "RIP AND TEAR", ContentPath = "test" };
        var javaL = new LessonDTO { Author = john, Language = java, Description = "learn to code!", Name = "Java beginner's guide", ContentPath = "test" };
        var tsL = new LessonDTO { Author = kirk, Language = typescript, Description = "typing!", Name = "How to Web", ContentPath = "test" };
        var csL = new LessonDTO { Author = lal, Language = cs, Description = "YOU SEE IT'S ALL PART OF THE PLAN", Name = "Roslyn Explained", ContentPath = "test" };
        var bfL = new LessonDTO { Author = da, Language = bfuck, Description = "BURN IN HELL AHHAH", Name = "Melting", ContentPath = "test" };
        await ctx.Lessons.AddRangeAsync(cppL, rustL, asmL, javaL, tsL, csL, bfL);

        await ctx.SaveChangesAsync();
    }
}