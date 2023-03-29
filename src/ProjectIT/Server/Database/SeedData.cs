using ProjectIT.Shared.Enums;
using ProjectIT.Shared.Models;

namespace ProjectIT.Server.Database;

public static class SeedData
{
    public static void SeedTopics(ProjectITDbContext context)
    {
        if (!context.Topics.Any())
        {
            context.Topics.AddRange(
                new Topic { Name = "SomeNewTopic", Category = TopicCategory.SoftwareEngineering },
                new Topic { Name = "C#", Category = TopicCategory.ProgrammingLanguages },
                new Topic { Name = "F#", Category = TopicCategory.ProgrammingLanguages },
                new Topic { Name = "C++", Category = TopicCategory.ProgrammingLanguages },
                new Topic { Name = "Rust", Category = TopicCategory.ProgrammingLanguages },
                new Topic { Name = "Go", Category = TopicCategory.ProgrammingLanguages },
                new Topic { Name = "Python 3", Category = TopicCategory.ProgrammingLanguages },
                new Topic { Name = "Ruby", Category = TopicCategory.ProgrammingLanguages },
                new Topic { Name = "Java", Category = TopicCategory.ProgrammingLanguages },
                new Topic { Name = "Kotlin", Category = TopicCategory.ProgrammingLanguages },
                new Topic { Name = "Swift", Category = TopicCategory.ProgrammingLanguages },
                new Topic { Name = "Objective-C", Category = TopicCategory.ProgrammingLanguages },
                new Topic { Name = "C", Category = TopicCategory.ProgrammingLanguages },
                new Topic { Name = "Cobol", Category = TopicCategory.ProgrammingLanguages },
                new Topic { Name = "Assembly", Category = TopicCategory.ProgrammingLanguages }
            );
        }
        context.SaveChanges();
    }

    public static void SeedUsers(ProjectITDbContext context)
    {
        if (!context.Users.Any())
        {
            context.Users.AddRange(
                new Supervisor { FullName = "John Andersen Doe", Email = "jad@mail.dk", Profession = "Supervisor"},
                new Supervisor { FullName = "Alice Jones", Email = "aj@mail.dk", Profession = "Co-supervisor" },
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
                    Id = 999,
                    Title = "ProjectIT",
                    Description = "A project management system for students at IT University of Copenhagen",
                    Topics = context.Topics.Take(5).ToArray(),
                    Languages = new[]
                    {
                        Language.English,
                    },
                    Programmes = new[]
                    {
                        Programme.BSWU,
                        Programme.BDS
                    },
                    Ects = Ects.Bachelor,
                    Semester = new Semester
                    {
                        Season = Season.Spring,
                        Year = 2024
                    },
                    Supervisor = context.Users.OfType<Supervisor>().First(),
                    CoSupervisor = context.Users.OfType<Supervisor>().Skip(1).First(),
                    Students = context.Users.OfType<Student>().Take(2).ToArray()
                },
                new Project
                {
                    Id = 998,
                    Title = "Test",
                    Description = "Test desc",
                    Topics = new[]
                    {
                        new Topic
                        {
                            Name = "Test Topic",
                            Category = TopicCategory.ArtificialIntelligence
                        },
                        new Topic
                        {
                            Name = "Test Topic 2",
                            Category = TopicCategory.SoftwareEngineering
                        }
                    },
                    Languages = new[]
                    {
                        Language.English
                    },
                    Programmes = new[]
                    {
                        Programme.BSWU
                    },
                    Ects = Ects.Bachelor,
                    Semester = new()
                    {
                        Season = Season.Spring,
                        Year = 2023
                    },
                    Supervisor = new()
                    {
                        FullName = "Joachim Alexander Kofoed",
                        Email = "jkof@itu.dk",
                        Topics = new Topic[] { },
                        Profession = "Professor"
                    },
                    Students = new Student[] { }
                }
            );
        }
        context.SaveChanges();
    }
}