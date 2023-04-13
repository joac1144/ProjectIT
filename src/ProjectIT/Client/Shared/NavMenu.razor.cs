namespace ProjectIT.Client.Shared;

public partial class NavMenu
{
    private string home = "Home";
    private string createProject = "Create Project";
    private string createRequest = "Create Request";
    private string supervisors = "Supervisors";
    private string activeTab = "Home";

    private void SetActiveTab(string val)
    {
        activeTab = val;
    }
}