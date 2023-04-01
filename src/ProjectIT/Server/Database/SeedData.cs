using ProjectIT.Shared.Enums;
using ProjectIT.Shared.Models;

namespace ProjectIT.Server.Database;

public static class SeedData
{
    private static List<Topic> topics = new()
    {
        new Topic { Name = "SomeNewTopic", Category = TopicCategory.SoftwareEngineering },
        new Topic { Name = "JavaScript", Category = TopicCategory.ProgrammingLanguages },
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
        new Topic { Name = "Assembly", Category = TopicCategory.ProgrammingLanguages },
        new Topic { Name = "Machine learning", Category = TopicCategory.ArtificialIntelligence },
        new Topic { Name = "Eye-tracking", Category = TopicCategory.ArtificialIntelligence },
        new Topic { Name = "Cryptography", Category = TopicCategory.Security },
        new Topic { Name = "Penetration testing", Category = TopicCategory.Security },
        new Topic { Name = "Software engineering", Category = TopicCategory.SoftwareEngineering }
    };

    private static User[] users = new User[]
    {
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
    };

    public static void SeedTopics(ProjectITDbContext context)
    {
        if (!context.Topics.Any())
        {
            context.Topics.AddRange(topics);
        }
        context.SaveChanges();
    }

    public static void SeedUsers(ProjectITDbContext context)
    {
        if (!context.Users.Any())
        {
            context.Users.AddRange(users);
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
                    Title = "Implementing a New Cybersecurity Solution",
                    Description = "Our company has identified a need for a new cybersecurity solution to better protect our network and data from potential threats. The current system is outdated and has shown vulnerabilities in recent testing.\r\n\r\nThe objective of this project is to research, evaluate, and implement a new cybersecurity solution that meets our company's specific needs and requirements. This will involve analyzing different options available in the market, comparing their features and benefits, and selecting the most suitable one.\r\n\r\nThe new solution will be installed and configured by our IT team, and all relevant stakeholders will be trained on its use and functionality. Regular testing and monitoring will be conducted to ensure the system is functioning as intended and providing the necessary protection.\r\n\r\nThe successful completion of this project will result in a more secure network and data environment, providing peace of mind for our company and its customers.",
                    Topics = context.Topics.Skip(16).Take(2).ToArray(),
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
                }/*,
                new Project
                {
                    Title = "Cloud-based Backup and Disaster Recovery System",
                    Description = "Objective: To implement a cloud-based backup and disaster recovery system to ensure business continuity and data protection in the event of a disaster or system failure. Scope: The project will involve the identification of critical business data and systems, selection of a suitable cloud-based backup and disaster recovery solution, and the implementation and testing of the system. The project team will also develop a training program for end-users and IT staff on the use and maintenance of the new system.",
                    Topics = context.Topics.Skip(4).Take(1).ToArray(),
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
                    Description = "Problem: The organization currently faces security risks due to the lack of a centralized password management system. Employees use weak or easily guessable passwords, reuse passwords across multiple accounts, and store them insecurely. This makes the organization vulnerable to data breaches and unauthorized access.",
                    Topics = context.Topics.Skip(1).Take(1).ToArray(),
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
                }*/
            );
            for (int i = 0; i < 15; i++)
            {
                var random = new Random();

                var project = new Project
                {
                    Title = "Project " + random.Next(1, 1000),
                    Description = "Description " + random.Next(1, 1000),
                    Topics = context.Topics.Skip(16).Take(2).ToList(),
                    Languages = new List<Language>()
                    {
                         (Language)random.Next(0, 3)
                    },
                    Programmes = new List<Programme>()
                    {
                        (Programme)random.Next(0, 3)
                    },
                    Ects = (Ects)random.Next(0, 3),
                    Semester = new()
                    {
                        Season = (Season)random.Next(0, 3),
                        Year = random.Next(2021, 2024)
                    },
                    Supervisor = new Supervisor
                    {
                        FullName = "Supervisor " + random.Next(1, 1000),
                        Email = "supervisor" + random.Next(1, 1000) + "@itu.dk",
                        Topics = new List<Topic>(),
                        Profession = "Professor"
                    },
                    Students = new List<Student>()
                    {
                         new Student
                         {
                             FullName = "Student " + random.Next(1, 1000),
                             Email = "student" + random.Next(1, 1000) + "@itu.dk",
                             Programme = (Programme)random.Next(0, 3)
                         }
                    }
                };

                context.Projects.Add(project);
            }
        }
        context.SaveChanges();
    }
}