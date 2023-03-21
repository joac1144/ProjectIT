using ProjectIT.Shared.Models;

namespace ProjectIT.Server.Database;

public static class SeedData
{
    public static void SeedTopics(ProjectITDbContext context)
    {
        if (!context.Topics.Any())
        {
            context.Topics.AddRange(
                new Topic { Category = "", Name = "SomeNewTopic"},
                new Topic { Name = "Blazor" },
                new Topic { Name = "C#" },
                new Topic { Name = "ASP.NET Core" },
                new Topic { Name = "Entity Framework Core" },
                new Topic { Name = "REST" },
                new Topic { Name = "PowerShell" },
                new Topic { Name = "Bash" },
                new Topic { Name = "Zsh" },
                new Topic { Name = "F#" },
                new Topic { Name = "C++" },
                new Topic { Name = "Rust" },
                new Topic { Name = "Go" },
                new Topic { Name = "Python 3" },
                new Topic { Name = "Ruby" },
                new Topic { Name = "Java" },
                new Topic { Name = "Kotlin" },
                new Topic { Name = "Swift" },
                new Topic { Name = "Objective-C" },
                new Topic { Name = "C" },
                new Topic { Name = "Cobol" },
                new Topic { Name = "Assembly" }
            );
        }
        context.SaveChanges();
    }

    public static void SeedUsers(ProjectITDbContext context)
    {
        if (!context.Users.Any())
        {
            context.Users.AddRange(
                new Supervisor { FullName = "John Andersen Doe"},
                new Supervisor { FullName = "Alice Jones" },
                new Supervisor { FullName = "John Smith" },
                new Supervisor { FullName = "Sarah Lee" },
                new Student { FullName = "Josefine Henriksen" },
                new Student { FullName = "Kristian Jespersen" },
                new Student { FullName = "Michael Davis" },
                new Student { FullName = "Emily Patel" },
                new Student { FullName = "David Nguyen" },
                new Student { FullName = "Olivia Brown" },
                new Student { FullName = "Robert Carlson " }
            );
        }
        context.SaveChanges();
    }

    public static void SeedProjects(ProjectITDbContext context)
    {
        if (!context.Projects.Any())
        {
            context.Projects.AddRange(
                new Project
                {
                    Title = "ProjectIT",
                    Description = "A project management system for students at IT University of Copenhagen",
                    Topics = context.Topics.Take(5),
                    Languages = new[]
                    {
                        Language.English,
                    },
                    Educations = new[]
                    {
                        Education.BSWU,
                        Education.BDS
                    },
                    Ects = Ects.Bachelor,
                    Supervisor = context.Users.OfType<Supervisor>().First(),
                    Students = context.Users.OfType<Student>().Take(2)
                }
            );
        }
        context.SaveChanges();
    }
}