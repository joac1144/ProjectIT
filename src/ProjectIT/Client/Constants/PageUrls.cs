namespace ProjectIT.Client.Constants;

public class PageUrls
{
    // Accessible for everyone.
    public const string LandingPage = "/";
    public const string Projects = "/projects";
    public const string ProjectDetails = "/projects/{id:int}";
    public const string Supervisors = "/supervisors";
    public const string SupervisorDetails = "/supervisors/{id:int}";
    public const string MyRequests = "/my-requests";

    // Accessible for students.
    public const string CreateRequest = "/create-request";
    public const string AppliedProjects = "/applied-projects";

    // Accessible for supervisors.
    public const string CreateProject = "/create-project";
    public const string MyProjects = "/my-projects";
}