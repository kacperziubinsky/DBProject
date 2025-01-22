namespace WebApplication3.Models;

public class Task
{
    public int taskID { get; set; }
    public string content { get; set; }
    public int projectID { get; set; }
    public DateTime startDate { get; set; }
    public DateTime endDate { get; set; }
    public string status { get; set; }

}