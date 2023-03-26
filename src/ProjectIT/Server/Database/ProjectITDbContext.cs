using Microsoft.EntityFrameworkCore;
using ProjectIT.Shared.Enums;
using ProjectIT.Shared.Models;

namespace ProjectIT.Server.Database;

public class ProjectITDbContext : DbContext, IProjectITDbContext
{
    public DbSet<Topic> Topics { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Project> Projects { get; set; } = null!;

    public ProjectITDbContext(DbContextOptions<ProjectITDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Project>()
            .Property(p => p.Languages)
            .HasConversion(
                e => string.Join(",", e.Select(x => x.ToString()).ToArray()),
                e => e.Split(new[] { ',' })
                    .Select(x => Enum.Parse(typeof(Language), x))
                    .Cast<Language>()
                    .ToList()
            );

        modelBuilder.Entity<Project>()
            .Property(p => p.Programmes)
            .HasConversion(
                e => string.Join(",", e.Select(x => x.ToString()).ToArray()),
                e => e.Split(new[] { ',' })
                    .Select(x => Enum.Parse(typeof(Programme), x))
                    .Cast<Programme>()
                    .ToList()
            );

        modelBuilder.Entity<Project>()
            .Property(p => p.Ects)
            .HasConversion<string>();
    }
}