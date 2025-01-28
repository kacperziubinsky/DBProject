namespace DBProject.Models;

public class Task
{
    public int TaskId { get; set; }
    public string TaskName { get; set; }
    public int ProjectId { get; set; }
    public int AssignedTo { get; set; }
    public string Status { get; set; }
    public DateTime DueDate { get; set; }
}