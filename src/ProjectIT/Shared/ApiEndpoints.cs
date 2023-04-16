namespace ProjectIT.Shared;

public static class ApiEndpoints
{
    public const string Topics = $"{prefix}topics";
    public const string Projects = $"{prefix}projects";
    public const string Requests = $"{prefix}requests";
    public const string Supervisors = $"{prefix}supervisors";
    public const string Gpt = $"{prefix}gpt";

    private const string prefix = "api/";
}