using Codelabs.BLL;
using Codelabs.Core.DTOs;
using Codelabs.DAL;
using Microsoft.EntityFrameworkCore;

namespace Codelabs.Tests;

class Program
{
    static async Task Main(string[] args)
    {
        //await using var ctx = new DAL.Context();

        //var author = await ctx.Users.SingleAsync(x => x.ID == 17);
        //var lang = await ctx.Languages.SingleAsync(x => x.Name.ToLower() == "rust");
        //var lesson = new LessonDTO
        //{
        //    Author = author,
        //    ContentPath = "sdsdsdasd",
        //    Cost = 1999,
        //    Language = lang,
        //    Name = "RustSUper",
        //    Description = "DADADADAD"
        //};

        //await ctx.Lessons.AddAsync(lesson);

        //var exercise1 = new ExerciseDTO
        //{
        //    Content = null,
        //    DesiredOutput = "programming",
        //    ProgramInput = "I love programming",
        //    RequirementsPath = "aaa",
        //    Name = "Практика Rust",
        //    Lesson = lesson
        //};

        //await ctx.Exercises.AddAsync(exercise1);
        //await ctx.SaveChangesAsync();

        var manager = new LessonManager();
        await manager.UpdateLessonByID(48, new()
        {
            Description = "Интересный урок на C++",
            Content = "# program.cpp\nИнтересно, как это работает?\n```cpp\n#include <iostream> \r\n\r\nint main() {\r\n  //BEGIN\r\n  std::cout << \"Hello, World!\" << std::endl;\r\n  //END\r\n  return 0;\r\n}\n```",
            Exercises = new()
            {

            }
        });
    }
}