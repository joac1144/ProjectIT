using ProjectIT.Shared.Enums;
using ProjectIT.Shared.Models;

namespace ProjectIT.Server.Database;

public static class SeedData
{
    private static Topic topic2 = new() { Name = "JavaScript", Category = TopicCategory.ProgrammingLanguages };
    private static Topic topic5 = new() { Name = "C++", Category = TopicCategory.ProgrammingLanguages };
    private static Topic topic6 = new() { Name = "Machine learning", Category = TopicCategory.ArtificialIntelligence };
    private static Topic topic7 = new() { Name = "Eye-tracking", Category = TopicCategory.ArtificialIntelligence };
    private static Topic topic8 = new() { Name = "Cryptography", Category = TopicCategory.Security };
    private static Topic topic9 = new() { Name = "Penetration testing", Category = TopicCategory.Security };
    private static Topic topic13 = new() { Name = "Kotlin", Category = TopicCategory.ProgrammingLanguages };
    private static Topic topic14 = new() { Name = "C", Category = TopicCategory.ProgrammingLanguages };

    // programing ProgrammingLanguages
    private static Topic topic18 = new() { Name = "Python", Category = TopicCategory.ProgrammingLanguages };
    private static Topic topic50 = new() { Name = "Java", Category = TopicCategory.ProgrammingLanguages };
    private static Topic topic42 = new() { Name = "Go", Category = TopicCategory.ProgrammingLanguages };
    private static Topic topic43 = new() { Name = "Swift", Category = TopicCategory.ProgrammingLanguages };
    private static Topic topic44 = new() { Name = "Ruby", Category = TopicCategory.ProgrammingLanguages };
    private static Topic topic45 = new() { Name = "TypeScript", Category = TopicCategory.ProgrammingLanguages };
    private static Topic topic46 = new() { Name = "C#", Category = TopicCategory.ProgrammingLanguages };
    private static Topic topic47 = new() { Name = "PHP", Category = TopicCategory.ProgrammingLanguages };

    // Security
    private static Topic topic10 = new() { Name = "Network Security", Category = TopicCategory.Security };
    private static Topic topic15 = new() { Name = "Ethical Hacking", Category = TopicCategory.Security };
    private static Topic topic16 = new() { Name = "Data Encryption", Category = TopicCategory.Security };
    private static Topic topic19 = new() { Name = "Identity and Access Management", Category = TopicCategory.Security };
    private static Topic topic20 = new() { Name = "Cyber Threat Intelligence", Category = TopicCategory.Security };
    private static Topic topic21 = new() { Name = "Blockchain Security", Category = TopicCategory.Security };

    // SoftwareEngineering
    private static Topic topic1 = new() { Name  = "Software Development Methodologies", Category = TopicCategory.SoftwareEngineering };
    private static Topic topic11 = new() { Name = "DevOps", Category = TopicCategory.SoftwareEngineering };
    private static Topic topic12 = new() { Name = "Continuous Integration", Category = TopicCategory.SoftwareEngineering };
    private static Topic topic29 = new() { Name = "Continuous Delevery", Category = TopicCategory.SoftwareEngineering };
    private static Topic topic17 = new() { Name = "Software Testing", Category = TopicCategory.SoftwareEngineering };
    private static Topic topic22 = new() { Name = "Software Architecture", Category = TopicCategory.SoftwareEngineering };
    private static Topic topic27 = new() { Name = "Version Control", Category = TopicCategory.SoftwareEngineering };

    //ArtificialIntelligence
    private static Topic topic3 = new() { Name = "Natural Language Processing", Category = TopicCategory.ArtificialIntelligence };
    private static Topic topic4 = new() { Name = "Computer Vision", Category = TopicCategory.ArtificialIntelligence };
    private static Topic topic23 = new() { Name = "Speech Recognition", Category = TopicCategory.ArtificialIntelligence };
    private static Topic topic24 = new() { Name = "Machine Vision", Category = TopicCategory.ArtificialIntelligence };
    private static Topic topic25 = new() { Name = "Neural Networks", Category = TopicCategory.ArtificialIntelligence };
    private static Topic topic26 = new() { Name = "Deep Learning", Category = TopicCategory.ArtificialIntelligence };
    private static Topic topic28 = new() { Name = "Reinforcement Learning", Category = TopicCategory.ArtificialIntelligence };
    private static Topic topic31 = new() { Name = "AI in Robotics", Category = TopicCategory.ArtificialIntelligence };

    //DataManagement
    private static Topic topic30 = new() { Name = "Data Governance", Category = TopicCategory.DataManagement };
    private static Topic topic32 = new() { Name = "Data Quality Management", Category = TopicCategory.DataManagement };
    private static Topic topic34 = new() { Name = "Data Integration", Category = TopicCategory.DataManagement };
    private static Topic topic35 = new() { Name = "Data Warehousing", Category = TopicCategory.DataManagement };
    private static Topic topic36 = new() { Name = "Data Modeling", Category = TopicCategory.DataManagement };
    private static Topic topic38 = new() { Name = "Data Analytics", Category = TopicCategory.DataManagement };
    private static Topic topic40 = new() { Name = "Big Data", Category = TopicCategory.DataManagement };
    private static Topic topic41 = new() { Name = "Data design", Category = TopicCategory.DataManagement };



    private static Supervisor supervisor1 = new() { FirstName = "John", LastName = "Andersen", Email = "hed@itr.dk", Profession = SupervisorProfession.AssociateProfessor, Topics = new List<Topic>() { topic30, topic32, topic34, topic35, topic36, topic38, topic40, topic41 }, Status = SupervisorStatus.LimitedSupervision };
    private static Supervisor supervisor2 = new() { FirstName = "Alice", LastName = "Jones", Email = "alice@itu.dk", Profession = SupervisorProfession.ExternalProfessor, Topics = new List<Topic>() { topic1, topic11, topic12, topic29, topic17, topic22, topic27, topic5 }, Status = SupervisorStatus.Available };
    private static Supervisor supervisor3 = new() { FirstName = "Emil", LastName = "Smith", Email = "EmilS@itu.dk", Profession = SupervisorProfession.PhdStudent, Topics = new List<Topic>() { topic10, topic15, topic16, topic19, topic20, topic21, topic8, topic50 }, Status = SupervisorStatus.Inactive };
    private static Supervisor supervisor4 = new() { FirstName = "Sarah", LastName = "Lee", Email = "saralee@itu.dk", Profession = SupervisorProfession.Lecturer, Topics = new List<Topic>() { topic6, topic14, topic13, topic5, topic2, topic7, topic8, topic42 }, Status = SupervisorStatus.Available };
    private static Supervisor supervisor5 = new() { FirstName = "Alaa Imad", LastName = "Abdul-Al", Email = "alia@itu.dk", Profession = SupervisorProfession.Lecturer, Topics = new List<Topic>() { topic25, topic26, topic28, topic31, topic3, topic4, topic8, topic46 }, Status = SupervisorStatus.Available };
    private static Supervisor supervisor6 = new() { FirstName = "Emdah", LastName = "Habib", Email = "emdah1@hotmail.com", Profession = SupervisorProfession.Lecturer, Topics = new List<Topic>() { topic6, topic14, topic13, topic5, topic2, topic7, topic8, topic9 }, Status = SupervisorStatus.Available };
    private static Supervisor supervisorTEST = new() { FirstName = "Supervisor Test", LastName = "#1", Email = "testsupervisor@projectititu.onmicrosoft.com", Profession = SupervisorProfession.Lecturer, Topics = new List<Topic>() { topic6, topic14, topic13 }, Status = SupervisorStatus.Available };

    private static Student student1 = new() { FirstName = "Josefine", LastName = "Henriksen", Email = "Josefine@itu.dk" };
    private static Student student2 = new() { FirstName = "Kristian", LastName = "Jespersen", Email = "Kristian@itu.dk" };
    private static Student student3 = new() { FirstName = "Michael", LastName = "Davis", Email = "MichaelD@itu.dk" };
    private static Student student4 = new() { FirstName = "Olivia", LastName = "Brown", Email = "OliviaB@itu.dk" };
    private static Student student5 = new() { FirstName = "Alaa", LastName = "Abdul-Al", Email = "alaamohamed@hotmail.dk" };
    private static Student studentTEST = new() { FirstName = "Student Test", LastName = "#1", Email = "teststudent@projectititu.onmicrosoft.com" };

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
        Supervisor = supervisor1,
        Students = new Student[] { }
    };

    public static void Seed(ProjectITDbContext context)
    {
        SeedTopics(context);
        SeedStudents(context);
        SeedSupervisors(context);
        SeedProjects(context);
    }

    public static void SeedTopics(ProjectITDbContext context)
    {
        if (!context.Topics.Any())
        {
            context.Topics.AddRange(topic2, topic5, topic6, topic7, topic8, topic9, topic13, topic14, topic23, topic24, topic18, topic50, topic42, topic43, topic44, topic45, topic46, topic47);
        }
        context.SaveChanges();
    }

    public static void SeedStudents(ProjectITDbContext context)
    {
        if (!context.Students.Any())
        {
            context.Students.AddRange(student1, student2, student3, student4, student5, studentTEST);
        }
        context.SaveChanges();
    }

    public static void SeedSupervisors(ProjectITDbContext context)
    {
        if (!context.Supervisors.Any())
        {
            context.Supervisors.AddRange(supervisor1, supervisor2, supervisor3, supervisor4, supervisor5, supervisor6, supervisorTEST);
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
                    Supervisor = context.Supervisors.First(),
                    CoSupervisor = context.Supervisors.Skip(1).First(),
                    Students = context.Students.Skip(4).Take(1).ToList()
                },
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
                        FirstName = "Kenneth",
                        LastName = "Mathiasen",
                        Email = "knmt@itu.dk",
                        Topics = new Topic[] { topic6, topic14, topic13, topic5, topic2, topic7, topic8, topic9 },
                        Profession = SupervisorProfession.FullProfessor,
                        Status = SupervisorStatus.Available
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
                    Supervisor = supervisor4,
                    Students = new Student[] { }
                },
                new Project
                    {
                        Title = "Blockchain-based Supply Chain Management System",
                        DescriptionHtml = "Objective: To implement a blockchain-based supply chain management system for enhanced transparency and traceability of goods. Scope: The project involves analyzing the existing supply chain processes, identifying potential areas for improvement, designing and developing a blockchain solution, and testing its effectiveness in real-world scenarios.",
                        Topics = new[] { topic5, topic6 },
                        Languages = new[] { Language.English },
                        Programmes = new[] { Programme.BDS },
                        Ects = Ects.Master,
                        Semester = new Semester { Season = Season.Spring, Year = 2024 },
                        Supervisor = new Supervisor
                        {
                            FirstName = "Laura",
                            LastName = "Müller",
                            Email = "laura@itu.dk",
                            Topics = new[] { topic6, topic14, topic13, topic5, topic2, topic7, topic8, topic9 },
                            Profession = SupervisorProfession.FullProfessor,
                            Status = SupervisorStatus.Available
                        },
                        Students = new Student[] { }
                    },
                new Project
                    {
                        Title = "Data Privacy Framework for Healthcare Systems",
                        DescriptionHtml = "Objective: To develop a comprehensive data privacy and confidentiality framework for healthcare systems to ensure the protection of sensitive patient information. Scope: The project includes conducting a risk assessment, defining privacy policies and access controls, implementing encryption and anonymization techniques, and evaluating the framework's effectiveness through testing.",
                        Topics = new[] { topic8, topic9 },
                        Languages = new[] { Language.English },
                        Programmes = new[] { Programme.MDS },
                        Ects = Ects.Master,
                        Semester = new Semester { Season = Season.Autumn, Year = 2023 },
                        Supervisor = new Supervisor
                        {
                            FirstName = "Mark",
                            LastName = "Andersen",
                            Email = "mark@itu.dk",
                            Topics = new[] { topic6, topic14, topic13, topic5, topic2, topic7, topic8, topic9 },
                            Profession = SupervisorProfession.AssociateProfessor,
                            Status = SupervisorStatus.LimitedSupervision
                        },
                        Students = new Student[] { }
                    },
                new Project
                {
                    Title = "Home Automation System Using Internet of Things",
                    DescriptionHtml = "Objective: To create a smart home automation system leveraging IoT technologies to enhance convenience and energy efficiency. Scope: The project involves selecting and integrating IoT devices, designing a user-friendly interface, implementing automation rules, and evaluating the system's performance and usability.",
                    Topics = new[] { topic2, topic6 },
                    Languages = new[] { Language.English },
                    Programmes = new[] { Programme.BGBI },
                    Ects = Ects.Bachelor,
                    Semester = new Semester { Season = Season.Spring, Year = 2024 },
                    Supervisor = new Supervisor
                    {
                        FirstName = "Emma",
                        LastName = "Larsen",
                        Email = "emma@itu.dk",
                        Topics = new[] { topic6, topic14, topic13, topic5, topic2, topic7, topic8, topic9 },
                        Profession = SupervisorProfession.ExternalProfessor,
                        Status = SupervisorStatus.Available
                    },
                    CoSupervisor = supervisor2,
                    Students = new Student[] { }
                },
                new Project
                {
                    Title = "AR Game Development for Education",
                    DescriptionHtml = "Objective: To develop an educational augmented reality (AR) game to engage students in interactive learning experiences. Scope: The project includes designing game mechanics, creating educational content, integrating AR features, and evaluating the game's impact on learning outcomes.",
                    Topics = new[] { topic6, topic14, topic13 },
                    Languages = new[] { Language.English },
                    Programmes = new[] { Programme.MGAMES },
                    Ects = Ects.Master,
                    Semester = new Semester { Season = Season.Autumn, Year = 2023 },
                    Supervisor = new Supervisor
                    {
                        FirstName = "Sophie",
                        LastName = "Hansen",
                        Email = "sophie@itu.dk",
                        Topics = new[] { topic6, topic14, topic13, topic5, topic2, topic7, topic8, topic9 },
                        Profession = SupervisorProfession.PhdStudent,
                        Status = SupervisorStatus.Inactive
                    },
                    CoSupervisor = supervisor1,
                    Students = new Student[] { }
                },
                new Project
                {
                    Title = "Robotic Business Process Optimization",
                    DescriptionHtml = "Objective: To implement robotic process automation (RPA) techniques to optimize repetitive and time-consuming business processes. Scope: The project involves identifying target processes, designing and developing RPA workflows, integrating with existing systems, and evaluating the efficiency gains achieved through automation.",
                    Topics = new[] { topic13, topic6 },
                    Languages = new[] { Language.English },
                    Programmes = new[] { Programme.BDDIT },
                    Ects = Ects.Bachelor,
                    Semester = new Semester { Season = Season.Spring, Year = 2024 },
                    Supervisor = new Supervisor
                    {
                        FirstName = "Robert",
                        LastName = "Sørensen",
                        Email = "robert@itu.dk",
                        Topics = new[] { topic13, topic6 },
                        Profession = SupervisorProfession.Lecturer,
                        Status = SupervisorStatus.Available
                    },
                    CoSupervisor = supervisor3,
                    Students = new Student[] { }
                },
                new Project
                {
                    Title = "AI Chatbot for Customer Support",
                    DescriptionHtml = "Objective: To develop an AI-powered chatbot to provide automated customer support and improve response times. Scope: The project includes training natural language processing models, integrating with customer support systems, implementing conversation flows, and evaluating the chatbot's accuracy and user satisfaction.",
                    Topics = new[] { topic6, topic9 },
                    Languages = new[] { Language.English },
                    Programmes = new[] { Programme.BSWU },
                    Ects = Ects.Bachelor,
                    Semester = new Semester { Season = Season.Autumn, Year = 2023 },
                    Supervisor = new Supervisor
                    {
                        FirstName = "David",
                        LastName = "Petersen",
                        Email = "david@itu.dk",
                        Topics = new[] { topic6, topic9 },
                        Profession = SupervisorProfession.FullProfessor,
                        Status = SupervisorStatus.Available
                    },
                    Students = new Student[] { }
                },
                new Project
                {
                    Title = "ML-based Fraud Detection System",
                    DescriptionHtml = "Objective: To build a machine learning-based fraud detection system that can identify and prevent fraudulent financial transactions. Scope: The project involves collecting transaction data, training fraud detection models, integrating with banking systems, and evaluating the system's effectiveness in reducing financial fraud.",
                    Topics = new[] { topic6, topic8 },
                    Languages = new[] { Language.English },
                    Programmes = new[] { Programme.MCS },
                    Ects = Ects.Master,
                    Semester = new Semester { Season = Season.Spring, Year = 2024 },
                    Supervisor = new Supervisor
                    {
                        FirstName = "Julia",
                        LastName = "Andersson",
                        Email = "julia@itu.dk",
                        Topics = new[] { topic6, topic8 },
                        Profession = SupervisorProfession.AssociateProfessor,
                        Status = SupervisorStatus.LimitedSupervision
                    },
                    Students = new Student[] { }
                },
                new Project
                {
                    Title = "Computer Vision-based Quality Control System",
                    DescriptionHtml = "Objective: To develop a computer vision-based quality control system that can automatically detect defects in manufactured products. Scope: The project includes designing image processing algorithms, training object detection models, integrating with production lines, and evaluating the system's accuracy and efficiency in detecting defects.",
                    Topics = new[] { topic7, topic9 },
                    Languages = new[] { Language.English },
                    Programmes = new[] { Programme.MDS },
                    Ects = Ects.Master,
                    Semester = new Semester { Season = Season.Autumn, Year = 2023 },
                    Supervisor = new Supervisor
                    {
                        FirstName = "Daniel",
                        LastName = "Hansen",
                        Email = "daniel@itu.dk",
                        Topics = new[] { topic7, topic9 },
                        Profession = SupervisorProfession.AssistantProfessor,
                        Status = SupervisorStatus.LimitedSupervision
                    },
                    CoSupervisor = supervisor4,
                    Students = new Student[] { }
                },
                new Project
                {
                Title = "DL-based Image Recognition System",
                DescriptionHtml = "Objective: To develop a deep learning-based image recognition system that enables autonomous vehicles to perceive and interpret the surrounding environment. Scope: The project involves collecting and annotating training data, designing and training convolutional neural networks, integrating with vehicle control systems, and evaluating the system's accuracy and real-time performance.",
                Topics = new[] { topic6, topic14 },
                Languages = new[] { Language.English },
                Programmes = new[] { Programme.MDDIT },
                Ects = Ects.Master,
                Semester = new Semester { Season = Season.Spring, Year = 2024 },
                Supervisor = new Supervisor
                {
                    FirstName = "Sophia",
                    LastName = "Jensen",
                    Email = "sophia@itu.dk",
                    Topics = new[] { topic6, topic14 },
                    Profession = SupervisorProfession.ExternalProfessor,
                    Status = SupervisorStatus.Available
                },
                Students = new Student[] { }
                },
                new Project
                {
                    Title = "Data Mining and Predictive Analytics",
                    DescriptionHtml = "Objective: To apply data mining and predictive analytics techniques to forecast retail sales and optimize inventory management. Scope: The project includes collecting sales data, preprocessing and analyzing the data, building predictive models, and evaluating the accuracy and effectiveness of the sales forecasting system.",
                    Topics = new[] { topic6, topic8 },
                    Languages = new[] { Language.English },
                    Programmes = new[] { Programme.MGAMES },
                    Ects = Ects.Master,
                    Semester = new Semester { Season = Season.Autumn, Year = 2023 },
                    Supervisor = new Supervisor
                    {
                        FirstName = "Oliver",
                        LastName = "Andersen",
                        Email = "oliver@itu.dk",
                        Topics = new[] { topic6, topic8 },
                        Profession = SupervisorProfession.ResearchProfessor,
                        Status = SupervisorStatus.Available
                    },
                    CoSupervisor = supervisor1,
                    Students = new Student[] { }
                },
                new Project
                {
                    Title = "NLP for Sentiment Analysis in Social Media",
                    DescriptionHtml = "Objective: To develop a natural language processing system for sentiment analysis of social media data to gain insights into public opinion and sentiment trends. Scope: The project involves collecting and preprocessing social media data, training sentiment analysis models, visualizing sentiment trends, and evaluating the accuracy of sentiment classification.",
                    Topics = new[] { topic6, topic13 },
                    Languages = new[] { Language.English },
                    Programmes = new[] { Programme.BSWU },
                    Ects = Ects.Bachelor,
                    Semester = new Semester { Season = Season.Spring, Year = 2024 },
                    Supervisor = new Supervisor
                    {
                        FirstName = "Emily",
                        LastName = "Møller",
                        Email = "emily@itu.dk",
                        Topics = new[] { topic6, topic2 },
                        Profession = SupervisorProfession.FullProfessor,
                        Status = SupervisorStatus.Available
                    },
                    Students = new Student[] { }
                },
                new Project
                {
                    Title = "Cloud E-Learning Platform for Online Education",
                    DescriptionHtml = "Objective: To develop a cloud-based e-learning platform that enables collaborative online education through virtual classrooms and interactive learning tools. Scope: The project includes designing the platform architecture, implementing communication features, integrating multimedia content, and evaluating the platform's usability and performance.",
                    Topics = new[] { topic6, topic2 },
                    Languages = new[] { Language.English },
                    Programmes = new[] { Programme.BDDIT },
                    Ects = Ects.Bachelor,
                    Semester = new Semester { Season = Season.Autumn, Year = 2023 },
                    Supervisor = new Supervisor
                    {
                        FirstName = "William",
                        LastName = "Lund",
                        Email = "william@itu.dk",
                        Topics = new[] { topic6, topic13 },
                        Profession = SupervisorProfession.Lecturer,
                        Status = SupervisorStatus.Available
                    },
                    Students = new Student[] { }
                },
                new Project
                {
                    Title = "Cybersecurity Risk Assessment for Small Businesses",
                    DescriptionHtml = "Objective: To conduct a cybersecurity risk assessment for small businesses and develop mitigation strategies to enhance their security posture. Scope: The project involves assessing vulnerabilities, analyzing threat landscape, recommending security controls, and creating cybersecurity guidelines tailored for small businesses.",
                    Topics = new[] { topic6, topic7 },
                    Languages = new[] { Language.English },
                    Programmes = new[] { Programme.MCS },
                    Ects = Ects.Master,
                    Semester = new Semester { Season = Season.Spring, Year = 2024 },
                    Supervisor = new Supervisor
                    {
                        FirstName = "Jonathan",
                        LastName = "Mortensen",
                        Email = "jonathan@itu.dk",
                        Topics = new[] { topic6, topic7 },
                        Profession = SupervisorProfession.AssociateProfessor,
                        Status = SupervisorStatus.Available
                    },
                    CoSupervisor = supervisor2,
                    Students = new Student[] { }
                },
                new Project
                {
                    Title = "Mobile App for Mental Health Monitoring",
                    DescriptionHtml = "Objective: To develop a mobile application that enables mental health monitoring, provides personalized support, and connects users with mental health professionals. Scope: The project includes designing the user interface, implementing data collection features, integrating with external services, and evaluating the application's usability and effectiveness.",
                    Topics = new[] { topic8, topic6 },
                    Languages = new[] { Language.English },
                    Programmes = new[] { Programme.BGBI },
                    Ects = Ects.Bachelor,
                    Semester = new Semester { Season = Season.Spring, Year = 2024 },
                    Supervisor = new Supervisor
                    {
                        FirstName = "Hannah",
                        LastName = "Lund",
                        Email = "hannah@itu.dk",
                        Topics = new[] { topic8, topic6 },
                        Profession = SupervisorProfession.AssistantProfessor,
                        Status = SupervisorStatus.Available
                    },
                    Students = new Student[] { }
                },
                new Project
                {
                    Title = "Blockchain Supply Chain Traceability",
                    DescriptionHtml = "Objective: To develop a blockchain-based solution for supply chain traceability and transparency, enabling secure and immutable tracking of products from source to destination. Scope: The project involves designing smart contracts, implementing a distributed ledger, integrating with existing supply chain systems, and evaluating the system's efficiency in enhancing traceability and transparency.",
                    Topics = new[] { topic6, topic8 },
                    Languages = new[] { Language.English },
                    Programmes = new[] { Programme.MDS },
                    Ects = Ects.Master,
                    Semester = new Semester { Season = Season.Autumn, Year = 2023 },
                    Supervisor = supervisorTEST,
                    CoSupervisor = supervisor1,
                    Students = new Student[] { }
                }
            );
            // for (int i = 0; i < 15; i++)
            // {
            //     var random = new Random();

            //     var project = new Project
            //     {
            //         Title = "Project " + random.Next(1, 1000),
            //         DescriptionHtml = "Description " + random.Next(1, 1000),
            //         Topics = context.Topics.Skip(2).Take(2).ToList(),
            //         Languages = new List<Language>()
            //         {
            //             (Language)random.Next(0, 2)
            //         },
            //         Programmes = new List<Programme>()
            //         {
            //             (Programme)random.Next(0, 10)
            //         },
            //         Ects = (Ects)random.Next(0, 4),
            //         Semester = new()
            //         {
            //             Season = (Season)random.Next(0, 2),
            //             Year = random.Next(2023, 2026)
            //         },
            //         Supervisor = new Supervisor
            //         {
            //             FirstName = "Supervisor",
            //             LastName = random.Next(1, 1000).ToString(),
            //             Email = "supervisor" + random.Next(1, 1000) + "@itu.dk",
            //             Topics = new List<Topic>() { topic5, topic13, topic6, topic7 },
            //             Profession = SupervisorProfession.FullProfessor,
            //             Status = (SupervisorStatus)random.Next(0,2)
            //         },
            //         Students = new List<Student>()
            //         {
            //              new Student
            //              {
            //                  FirstName = "Anna",
            //                  LastName = "Sivertsen",
            //                  Email = "asiv@itu.dk"
            //              }
            //         }
            //     };

            //     context.Projects.Add(project);
            // }
        }
        context.SaveChanges();
    }
}