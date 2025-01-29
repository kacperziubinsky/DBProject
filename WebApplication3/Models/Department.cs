using System.ComponentModel.DataAnnotations;

namespace DBProject.Models;

public class Department
{
    [Key]
    public int DepartmentID { get; set; }
    public string DepartmentName { get; set; }
    public int ManagerID { get; set; }
}