namespace DBProject.Models;

public class Project
{
    public int ProjectID { get; set; }
    public string ProjectName { get; set; }
    public int DepartmentID { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}