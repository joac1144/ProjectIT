namespace ProjectIT.Shared.Enums;

public enum StudentGroupStatus
{
    /// <summary>
    /// Status indicating that students have sent an application (projects) or request (requests) and are awaiting approval from supervisor.
    /// </summary>
    StudentsSentApplicationOrRequest,

    /// <summary>
    /// Status indicating that a supervisor has approved an application (projects) or request (requests) and is awaiting final confirmation from students.
    /// </summary>
    SupervisorApproved
}