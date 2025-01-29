using System.ComponentModel.DataAnnotations;

namespace DBProject.Models;

public class Project
{
    [Key]
    public int ProjectID { get; set; }
    public string ProjectName { get; set; }
    public int DepartmentID { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}