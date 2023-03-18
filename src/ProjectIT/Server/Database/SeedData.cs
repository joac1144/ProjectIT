using ProjectIT.Shared.Models;

namespace ProjectIT.Server.Database;

public static class SeedData
{
    public static void SeedDatabase(ProjectITDbContext context)
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
}