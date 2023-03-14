using ProjectIT.Shared.Models;

namespace ProjectIT.Server.Database;

public static class SeedData
{
    public static void SeedDatabase(ProjectITDbContext context)
    {
        if (!context.Tags.Any())
        {
            context.Tags.AddRange(
                new Tag { Name = "C#" },
                new Tag { Name = "Blazor" },
                new Tag { Name = "ASP.NET Core" },
                new Tag { Name = "Entity Framework Core" },
                new Tag { Name = "Visual Studio" },
                new Tag { Name = "Visual Studio Code" },
                new Tag { Name = "Azure" },
                new Tag { Name = "Azure DevOps" },
                new Tag { Name = "GitHub" },
                new Tag { Name = "Git" },
                new Tag { Name = "Docker" },
                new Tag { Name = "Kubernetes" },
                new Tag { Name = "SQL Server" },
                new Tag { Name = "PostgreSQL" },
                new Tag { Name = "MongoDB" },
                new Tag { Name = "JavaScript" },
                new Tag { Name = "TypeScript" },
                new Tag { Name = "Angular" },
                new Tag { Name = "React" },
                new Tag { Name = "Vue" },
                new Tag { Name = "WebAssembly" },
                new Tag { Name = "Web API" },
                new Tag { Name = "REST" },
                new Tag { Name = "GraphQL" },
                new Tag { Name = "SignalR" },
                new Tag { Name = "Razor" },
                new Tag { Name = "CSS" },
                new Tag { Name = "HTML" },
                new Tag { Name = "Bootstrap" },
                new Tag { Name = "Tailwind CSS" },
                new Tag { Name = "Material Design" },
                new Tag { Name = "Windows" },
                new Tag { Name = "macOS" },
                new Tag { Name = "Linux" },
                new Tag { Name = "Windows Terminal" },
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