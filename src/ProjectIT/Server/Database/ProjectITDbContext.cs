using Microsoft.EntityFrameworkCore;
using ProjectIT.Shared.Models;

namespace ProjectIT.Server.Database;

public class ProjectITDbContext : DbContext
{
    public DbSet<Topic> Topics { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Project> Projects { get; set; } = null!;

    public ProjectITDbContext(DbContextOptions<ProjectITDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        // modelBuilder.Entity<Project>()
        // .Property(p => p.Educations)
        // .HasConversion(
        //     v => string.Join(',', v),
        //     v => v.Split(',', StringSplitOptions.RemoveEmptyEntries)
        //           .Select(e => (Education)Enum.Parse(typeof(Education), e)));
        
        //  modelBuilder.Entity<Project>()
        //     .Property(p => p.Educations)
        //     .HasConversion(
        //         v => string.Join(',', v.Select(e => e.ToString())),
        //         v => v.Split(',', StringSplitOptions.RemoveEmptyEntries)
        //             .Select(e => (Education)Enum.Parse(typeof(Education), e))
        //             .ToList())
        //     .Metadata.SetValueComparer(new EnumerableValueComparer<Education>());



        //  modelBuilder.Entity<Project>()
        //     .Property(p => p.Languages)
        //     .HasConversion(
        //         v => string.Join(',', v.Select(e => e.ToString())),
        //         v => v.Split(',', StringSplitOptions.RemoveEmptyEntries)
        //             .Select(e => (Language)Enum.Parse(typeof(Language), e))
        //             .ToList())
        //     .Metadata.SetValueComparer(new EnumerableValueComparer<Language>());

        // modelBuilder.Entity<Project>()
        // .Property(p => p.Languages)
        // .HasConversion(
        //     v => string.Join(',', v),
        //     v => v.Split(',', StringSplitOptions.RemoveEmptyEntries)
        //           .Select(e => (Language)Enum.Parse(typeof(Language), e)));
        
        
        
        // modelBuilder
        // .Entity<Project>()
        // .Property(e => e.Educations)
        // .HasConversion(
        //     v => v.ToString(),
        //     v => (Education)Enum.Parse(typeof(Education), v));
    }
}