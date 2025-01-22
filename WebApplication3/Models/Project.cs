namespace WebApplication3.Models;

public class Project
{
    public int projectID { get; set; }
    public string projectName { get; set; }
    public int clientID { get; set; }
    public DateTime startDate { get; set; }
    public DateTime endDate { get; set; }
    public string status { get; set; }
}