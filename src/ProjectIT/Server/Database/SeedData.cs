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
                new Supervisor { FullName = "John Andersen Doe", Email = "hed@itr.dk", Profession = "Professor" },
                new Supervisor { FullName = "Alice Jones", Email = "hedef@itr.dk", Profession = "PhD Student" },
                new Supervisor { FullName = "John Smith", Email = "hevdfd@itr.dk", Profession = "Professor" },
                new Supervisor { FullName = "Sarah Lee", Email = "hebgfd@itr.dk", Profession = "Professor" },
                new Student { FullName = "Josefine Henriksen", Email = "hehngd@itr.dk" },
                new Student { FullName = "Kristian Jespersen", Email = "hmgjhed@itr.dk" },
                new Student { FullName = "Michael Davis", Email = "hessdd@itr.dk" },
                new Student { FullName = "Emily Patel", Email = "hxcvxed@itr.dk" },
                new Student { FullName = "David Nguyen", Email = "hevbd@itr.dk" },
                new Student { FullName = "Olivia Brown", Email = "hentd@itr.dk" },
                new Student { FullName = "Robert Carlson", Email = "hesewed@itr.dk" }

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
                },
                new Project
                {
                    Id = 997,
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
                },
                 new Project
                 {
                     Id = 996,
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
                 },
                  new Project
                  {
                      Id = 995,
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
                  },
                   new Project
                   {
                       Id = 994,
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
                   },
                    new Project
                    {
                        Id = 993,
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
                    },
                     new Project
                     {
                         Id = 992,
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
                     },
                      new Project
                      {
                          Id = 991,
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