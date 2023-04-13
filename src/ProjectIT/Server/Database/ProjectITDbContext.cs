using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ProjectIT.Shared.Enums;
using ProjectIT.Shared.Models;

namespace ProjectIT.Server.Database;

public class ProjectITDbContext : DbContext, IProjectITDbContext
{
    public DbSet<Topic> Topics { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Project> Projects { get; set; } = null!;

    public DbSet<Request> Requests { get; set; } = null!;

    public ProjectITDbContext(DbContextOptions<ProjectITDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        /*var valueComparer = new ValueComparer<IEnumerable<string>>(
                    (c1, c2) => c1.SequenceEqual(c2),
                    c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                    c => (IEnumerable<string>)c.ToHashSet());*/

        modelBuilder.Entity<Project>()
            .Property(p => p.Languages)
            .HasConversion(
                e => string.Join(",", e.Select(x => x.ToString()).ToArray()),
                e => e.Split(new[] { ',' })
                    .Select(x => Enum.Parse(typeof(Language), x))
                    .Cast<Language>()
                    .ToList()
            /*, valueComparer*/);

        modelBuilder.Entity<Project>()
            .Property(p => p.Programmes)
            .HasConversion(
                e => string.Join(",", e.Select(x => x.ToString()).ToArray()),
                e => e.Split(new[] { ',' })
                    .Select(x => Enum.Parse(typeof(Programme), x))
                    .Cast<Programme>()
                    .ToList()
            /*, valueComparer*/);

        modelBuilder.Entity<Project>()
            .Property(p => p.Ects)
            .HasConversion<string>();

        modelBuilder.Entity<Semester>()
            .Property(s => s.Season)
            .HasConversion<string>();

        modelBuilder.Entity<Topic>()
            .Property(t => t.Category)
            .HasConversion<string>();

        modelBuilder.Entity<Request>()
            .Property(r => r.Languages)
            .HasConversion(
                e => string.Join(",", e.Select(x => x.ToString()).ToArray()),
                e => e.Split(new[] { ',' })
                    .Select(x => Enum.Parse(typeof(Language), x))
                    .Cast<Language>()
                    .ToList()
            /*, valueComparer*/);

        modelBuilder.Entity<Request>()
            .Property(r => r.Programmes)
            .HasConversion(
                e => string.Join(",", e.Select(x => x.ToString()).ToArray()),
                e => e.Split(new[] { ',' })
                    .Select(x => Enum.Parse(typeof(Programme), x))
                    .Cast<Programme>()
                    .ToList()
            /*, valueComparer*/);

        modelBuilder.Entity<Request>()
            .Property(p => p.Ects)
            .HasConversion<string>();

        modelBuilder.Entity<Supervisor>()
            .Property(s => s.Profession)
            .HasConversion(
                p => p.ToString(),
                p => (SupervisorProfession)Enum.Parse(typeof(SupervisorProfession), p)
            );

        modelBuilder.Entity<Supervisor>()
            .Property(s => s.Status)
            .HasConversion(
                p => p.ToString(),
                p => (SupervisorStatus)Enum.Parse(typeof(SupervisorStatus), p)
            );
    }
}