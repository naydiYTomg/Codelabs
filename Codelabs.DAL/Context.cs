using Codelabs.Core;
using Codelabs.Core.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Codelabs.DAL;

public class Context : DbContext
{
    public DbSet<UserDTO> Users { get; set; }
    
    public DbSet<AuthorInfoDTO> AuthorInfos { get; set; }
    
    public DbSet<LanguageDTO> Languages { get; set; }
    
    public DbSet<LessonDTO> Lessons { get; set; }
    
    public DbSet<PurchaseDTO> Purchases { get; set; }
    
    public DbSet<SolutionDTO> Solutions { get; set; }
    
    public DbSet<ExerciseDTO> Exercises { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(Options.ConnectionString);
    }
}