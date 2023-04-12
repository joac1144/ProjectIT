using ProjectIT.Shared.Enums;
using ProjectIT.Shared.Models;

namespace ProjectIT.Server.Database;

public static class SeedData
{
    private static Topic topic1 = new() { Name = "SomeNewTopic", Category = TopicCategory.SoftwareEngineering };
    private static Topic topic2 = new() { Name = "JavaScript", Category = TopicCategory.ProgrammingLanguages };
    private static Topic topic3 = new() { Name = "C#", Category = TopicCategory.ProgrammingLanguages };
    private static Topic topic4 = new() { Name = "F#", Category = TopicCategory.ProgrammingLanguages };
    private static Topic topic5 = new() { Name = "C++", Category = TopicCategory.ProgrammingLanguages };
    private static Topic topic6 = new() { Name = "Machine learning", Category = TopicCategory.ArtificialIntelligence };
    private static Topic topic7 = new() { Name = "Eye-tracking", Category = TopicCategory.ArtificialIntelligence };
    private static Topic topic8 = new() { Name = "Cryptography", Category = TopicCategory.Security };
    private static Topic topic9 = new() { Name = "Penetration testing", Category = TopicCategory.Security };
    private static Topic topic10 = new() { Name = "Software engineering", Category = TopicCategory.SoftwareEngineering };
    private static Topic topic11 = new() { Name = "Go", Category = TopicCategory.ProgrammingLanguages };
    private static Topic topic12 = new() { Name = "Ruby", Category = TopicCategory.ProgrammingLanguages };
    private static Topic topic13 = new() { Name = "Kotlin", Category = TopicCategory.ProgrammingLanguages };
    private static Topic topic14 = new() { Name = "C", Category = TopicCategory.ProgrammingLanguages };
    private static Topic topic15 = new() { Name = "Assembly", Category = TopicCategory.ProgrammingLanguages };

    private static Supervisor supervisor1 = new() { FullName = "John Andersen Doe", Email = "hed@itr.dk", Profession = "Professor", Topics = new List<Topic>() { topic7, topic9, topic14, topic4, topic12 } };
    private static Supervisor supervisor2 = new() { FullName = "Alice Jones", Email = "hedef@itr.dk", Profession = "PhD Student", Topics = new List<Topic>() { topic3, topic1, topic2, topic15, topic12 } };
    private static Supervisor supervisor3 = new() { FullName = "John Smith", Email = "hevdfd@itr.dk", Profession = "Professor", Topics = new List<Topic>() { topic3, topic2, topic13, topic6, topic7 } };
    private static Supervisor supervisor4 = new() { FullName = "Sarah Lee", Email = "hebgfd@itr.dk", Profession = "Professor", Topics = new List<Topic>() { topic8, topic6, topic13 } };
    private static Student student1 = new() { FullName = "Josefine Henriksen", Email = "hehngd@itr.dk" };
    private static Student student2 = new() { FullName = "Kristian Jespersen", Email = "hmgjhed@itr.dk" };
    private static Student student3 = new() { FullName = "Michael Davis", Email = "hessdd@itr.dk" };
    private static Student student4 = new() { FullName = "Olivia Brown", Email = "hentd@itr.dk" };

    private static Project project1 = new()
    {
        Title = "Test",
        DescriptionHtml = "Test desc",
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
    };

    private static Project project2 = new() 
    {
        Title = "Implementing a New Cybersecurity Solution",
        DescriptionHtml = "Our company has identified a need for a new cybersecurity solution to better protect our network and data from potential threats. The current system is outdated and has shown vulnerabilities in recent testing.\r\n\r\nThe objective of this project is to research, evaluate, and implement a new cybersecurity solution that meets our company's specific needs and requirements. This will involve analyzing different options available in the market, comparing their features and benefits, and selecting the most suitable one.\r\n\r\nThe new solution will be installed and configured by our IT team, and all relevant stakeholders will be trained on its use and functionality. Regular testing and monitoring will be conducted to ensure the system is functioning as intended and providing the necessary protection.\r\n\r\nThe successful completion of this project will result in a more secure network and data environment, providing peace of mind for our company and its customers.",
        Topics = new List<Topic>() { topic8, topic9 },
        Languages = new[]
        {
            Language.English
        },
        Programmes = new[]
        {
            Programme.BSWU
        },
        Ects = Ects.Master,
        Semester = new()
        {
            Season = Season.Spring,
            Year = 2024
        },
        Supervisor = new()
        {
            FullName = "Joachim Kofoed",
            Email = "jkof@itu.dk",
            Topics = new Topic[] { },
            Profession = "Assistant Lecturer"
        },
        Students = new Student[] { }
    };

    public static void Seed(ProjectITDbContext context)
    {
        SeedTopics(context);
        SeedUsers(context);
        SeedProjects(context);
    }

    public static void SeedTopics(ProjectITDbContext context)
    {
        if (!context.Topics.Any())
        {
            context.Topics.AddRange(topic1, topic2, topic3, topic4, topic5, topic6, topic7, topic8, topic9, topic10, topic11, topic12, topic13, topic14, topic15);
        }
        context.SaveChanges();
    }

    public static void SeedUsers(ProjectITDbContext context)
    {
        if (!context.Users.Any())
        {
            context.Users.AddRange(supervisor1, supervisor2, supervisor3, supervisor4);
            context.Users.AddRange(student1, student2, student3, student4);
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
                    DescriptionHtml = "A project management system for students at IT University of Copenhagen",
                    Topics = context.Topics.Take(5).ToList(),
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
                project1,
                project2,
                new Project
                {
                    Title = "Cloud-based Backup and Disaster Recovery System",
                    DescriptionHtml = "Objective: To implement a cloud-based backup and disaster recovery system to ensure business continuity and data protection in the event of a disaster or system failure. Scope: The project will involve the identification of critical business data and systems, selection of a suitable cloud-based backup and disaster recovery solution, and the implementation and testing of the system. The project team will also develop a training program for end-users and IT staff on the use and maintenance of the new system.",
                    Topics = context.Topics.Skip(4).Take(1).ToList(),
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
                        Season = Season.Autumn,
                        Year = 2023
                    },
                    Supervisor = new()
                    {
                        FullName = "Alaa",
                        Email = "alia@itu.dk",
                        Topics = new Topic[] { },
                        Profession = "Professor"
                    },
                    Students = new Student[] { }
                },
                new Project
                {
                    Title = "Implementing a Password Management System",
                    DescriptionHtml = "Problem: The organization currently faces security risks due to the lack of a centralized password management system. Employees use weak or easily guessable passwords, reuse passwords across multiple accounts, and store them insecurely. This makes the organization vulnerable to data breaches and unauthorized access.",
                    Topics = context.Topics.Skip(1).Take(1).ToList(),
                    Languages = new[]
                    {
                        Language.English
                    },
                    Programmes = new[]
                    {
                        Programme.MCS
                    },
                    Ects = Ects.Master,
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
            for (int i = 0; i < 26; i++)
            {
                var random = new Random();

                var project = new Project
                {
                    Title = "Project " + random.Next(1, 1000),
                    DescriptionHtml = "Description " + random.Next(1, 1000),
                    Topics = context.Topics.Skip(12).Take(2).ToList(),
                    Languages = new List<Language>()
                    {
                        (Language)random.Next(0, 2)
                    },
                    Programmes = new List<Programme>()
                    {
                        (Programme)random.Next(0, 10)
                    },
                    Ects = (Ects)random.Next(0, 4),
                    Semester = new()
                    {
                        Season = (Season)random.Next(0, 2),
                        Year = random.Next(2023, 2026)
                    },
                    Supervisor = new Supervisor
                    {
                        FullName = "Supervisor " + random.Next(1, 1000),
                        Email = "supervisor" + random.Next(1, 1000) + "@itu.dk",
                        Topics = new List<Topic>() { topic1, topic10, topic12, topic5, topic13, topic12, topic11 },
                        Profession = "Professor"
                    },
                    Students = new List<Student>()
                    {
                         new Student
                         {
                             FullName = "Anna Sivertsen",
                             Email = "asiv@itu.dk"
                         }
                    }
                };

                context.Projects.Add(project);
            }
        }
        context.SaveChanges();
    }
}