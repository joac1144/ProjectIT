using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProjectIT.Shared.Enums;
using ProjectIT.Shared.Models;

namespace ProjectIT.Server.Database;

public class ProjectITDbContext : DbContext, IProjectITDbContext
{
    public DbSet<Topic> Topics { get; set; } = null!;
    public DbSet<Student> Students { get; set; } = null!;
    public DbSet<Supervisor> Supervisors { get; set; } = null!;
    public DbSet<Project> Projects { get; set; } = null!;
    public DbSet<Request> Requests { get; set; } = null!;

    public ProjectITDbContext(DbContextOptions<ProjectITDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        /*** ValueConverters ***/
        var semesterConverter = 
            new ValueConverter<Semester, string>(
                semester => $"{semester.Season} {semester.Year}",
                s => new Semester
                {
                    Season = (Season)Enum.Parse(typeof(Season), s.Split(" ", StringSplitOptions.None)[0]),
                    Year = int.Parse(s.Split(" ", StringSplitOptions.None)[1])
                });

        /*** ValueComparers ***/
        /*var valueComparer = new ValueComparer<IEnumerable<string>>(
                    (c1, c2) => c1.SequenceEqual(c2),
                    c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                    c => (IEnumerable<string>)c.ToHashSet());*/

        /*** Tables ***/
        // Projects.
        modelBuilder.Entity<Project>()
            .ToTable("Project")
            .HasKey(p => p.Id);
        modelBuilder.Entity<Project>()
            .HasMany(p => p.Topics)
            .WithMany();
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
        modelBuilder.Entity<Project>()
            .Property(p => p.Semester)
            .HasConversion(semesterConverter);
        modelBuilder.Entity<Project>()
            .HasOne(p => p.Supervisor)
            .WithMany(s => s.Projects);
        modelBuilder.Entity<Project>()
            .HasMany(p => p.Students)
            .WithMany(s => s.Projects);

        // Requests.
        modelBuilder.Entity<Request>()
            .ToTable("Request")
            .HasKey(r => r.Id);
        modelBuilder.Entity<Request>()
            .HasMany(r => r.Topics)
            .WithMany();
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
            .HasMany(r => r.Members)
            .WithMany();
        modelBuilder.Entity<Request>()
            .Property(p => p.Ects)
            .HasConversion<string>();
        modelBuilder.Entity<Request>()
            .Property(r => r.Semester)
            .HasConversion(semesterConverter);
        modelBuilder.Entity<Request>()
            .HasMany(r => r.Supervisors)
            .WithMany(s => s.Requests);

        // Topics.
        modelBuilder.Entity<Topic>()
            .ToTable("Topic")
            .HasKey(t => t.Id);
        modelBuilder.Entity<Topic>()
            .Property(t => t.Category)
            .HasConversion<string>();

        // Students.
        modelBuilder.Entity<Student>()
            .ToTable("Student")
            .HasKey(s => s.Id);
        modelBuilder.Entity<Student>()
            .Property(s => s.Id)
            .HasColumnOrder(1);
        modelBuilder.Entity<Student>()
            .Property(s => s.FirstName)
            .HasColumnOrder(2);
        modelBuilder.Entity<Student>()
            .Property(s => s.LastName)
            .HasColumnOrder(3);
        modelBuilder.Entity<Student>()
            .Property(s => s.Email)
            .HasColumnOrder(4);
        modelBuilder.Entity<Student>()
            .Property(s => s.Programme)
            .HasColumnOrder(5)
            .HasConversion<string>();

        // Supervisors.
        modelBuilder.Entity<Supervisor>()
            .ToTable("Supervisor")
            .HasKey(s => s.Id);
        modelBuilder.Entity<Supervisor>()
            .Property(s => s.Id)
            .HasColumnOrder(1);
        modelBuilder.Entity<Supervisor>()
            .Property(s => s.FirstName)
            .HasColumnOrder(2);
        modelBuilder.Entity<Supervisor>()
            .Property(s => s.LastName)
            .HasColumnOrder(3);
        modelBuilder.Entity<Supervisor>()
            .Property(s => s.Email)
            .HasColumnOrder(4);
        modelBuilder.Entity<Supervisor>()
            .Property(s => s.Profession)
            .HasColumnOrder(5)
            .HasConversion<string>();
        modelBuilder.Entity<Supervisor>()
            .Property(s => s.Status)
            .HasColumnOrder(6)
            .HasConversion<string>();
        modelBuilder.Entity<Supervisor>()
            .HasMany(s => s.Topics)
            .WithMany();
    }
}