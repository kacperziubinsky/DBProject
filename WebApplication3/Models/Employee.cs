using System.ComponentModel.DataAnnotations;

namespace DBProject.Models;

public class Employee
{
    [Key]
    public int EmployeeID { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int DepartmentID { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public DateTime HireDate { get; set; }
    public Department Department { get; set; }
    public ICollection<Task> Tasks { get; set; } 
    
    public Salary Salary { get; set; }
}