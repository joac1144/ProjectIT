using ProjectIT.Shared.Models;

namespace ProjectIT.Server.Database;

public static class SeedData
{
    public static void SeedTags(ProjectITDbContext context)
    {
        if (!context.Tags.Any())
        {
            context.Tags.AddRange(
                new Topic { Category = "", Name = "SomeNewTopic"},
                new Language { Name = "Danish" },
                new Language { Name = "English" },
                new Tag { Name = "C#" },
                new Tag { Name = "Blazor" },
                new Tag { Name = "ASP.NET Core" },
                new Tag { Name = "Entity Framework Core" },
                new Tag { Name = "REST" },
                new Tag { Name = "PowerShell" },
                new Tag { Name = "Bash" },
                new Tag { Name = "Zsh" },
                new Tag { Name = "F#" },
                new Tag { Name = "C++" },
                new Tag { Name = "Rust" },
                new Tag { Name = "Go" },
                new Tag { Name = "Python 3" },
                new Tag { Name = "Ruby" },
                new Tag { Name = "Java" },
                new Tag { Name = "Kotlin" },
                new Tag { Name = "Swift" },
                new Tag { Name = "Objective-C" },
                new Tag { Name = "C" },
                new Tag { Name = "Cobol" },
                new Tag { Name = "Assembly" }
            );
        }
        context.SaveChanges();
    }

    public static void SeedUsers(ProjectITDbContext context)
    {
        if (!context.Users.Any())
        {
            context.Users.AddRange(
                new Supervisor { FullName = "SomeNewTopic"},
                new Student { FullName = "Danish" },
                new Student { FullName = "English" },
                new User { FullName = "C#" },
                new User { FullName = "Blazor" },
                new User { FullName = "ASP.NET Core" },
                new User { FullName = "Entity Framework Core" },
                new User { FullName = "REST" },
                new User { FullName = "PowerShell" },
                new User { FullName = "Bash" },
                new User { FullName = "Zsh" },
                new User { FullName = "F#" },
                new User { FullName = "C++" },
                new User { FullName = "Rust" },
                new User { FullName = "Go" },
                new User { FullName = "Python 3" },
                new User { FullName = "Ruby" },
                new User { FullName = "Java" },
                new User { FullName = "Kotlin" },
                new User { FullName = "Swift" },
                new User { FullName = "Objective-C" },
                new User { FullName = "C" },
                new User { FullName = "Cobol" },
                new User { FullName = "Assembly" }
            );
        }

    }
}