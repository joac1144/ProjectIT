using ProjectIT.Shared.Models;

namespace ProjectIT.Server.Database;

public static class SeedData
{
    public static void SeedTopics(ProjectITDbContext context)
    {
        if (!context.Topics.Any())
        {
            context.Topics.AddRange(
                new Topic { Name = "SomeNewTopic", Category = "Some Test Category" },
                new Topic { Name = "C#", Category = "Programming Languages" },
                new Topic { Name = "Blazor", Category = "Frameworks & Utilities" },
                new Topic { Name = "ASP.NET Core", Category = "Frameworks & Utilities" },
                new Topic { Name = "Entity Framework Core", Category = "Frameworks & Utilities" },
                new Topic { Name = "REST", Category = "Frameworks & Utilities" },
                new Topic { Name = "PowerShell", Category = "Frameworks & Utilities" },
                new Topic { Name = "Bash", Category = "Frameworks & Utilities" },
                new Topic { Name = "Zsh", Category = "Frameworks & Utilities" },
                new Topic { Name = "F#", Category = "Programming Languages" },
                new Topic { Name = "C++", Category = "Programming Languages" },
                new Topic { Name = "Rust", Category = "Programming Languages" },
                new Topic { Name = "Go", Category = "Programming Languages" },
                new Topic { Name = "Python 3", Category = "Programming Languages" },
                new Topic { Name = "Ruby", Category = "Programming Languages" },
                new Topic { Name = "Java", Category = "Programming Languages" },
                new Topic { Name = "Kotlin", Category = "Programming Languages" },
                new Topic { Name = "Swift", Category = "Programming Languages" },
                new Topic { Name = "Objective-C", Category = "Programming Languages" },
                new Topic { Name = "C", Category = "Programming Languages" },
                new Topic { Name = "Cobol", Category = "Programming Languages" },
                new Topic { Name = "Assembly", Category = "Programming Languages" }
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
                    Topics = context.Topics.Take(5).ToArray(),
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
                    Date = new Date 
                    { 
                        Season = Season.Spring, 
                        Year = 2024 
                    },
                    Supervisor = context.Users.OfType<Supervisor>().First(),
                    Students = context.Users.OfType<Student>().Take(2).ToArray()
                }
            );
        }
        context.SaveChanges();
    }
}