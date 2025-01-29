using System.ComponentModel.DataAnnotations;

namespace DBProject.Models;

public class Salary
{
    [Key]
    public int SalaryID { get; set; }
    public int EmployeeID { get; set; }
    public decimal MonthlySalary { get; set; }
    public DateTime PaymentDate { get; set; }
}