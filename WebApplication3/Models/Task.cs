using System.ComponentModel.DataAnnotations;

namespace DBProject.Models;

public class Task
{
    [Key]
    public int TaskID { get; set; }
    public string TaskName { get; set; }
    public int ProjectID { get; set; }
    public int AssignedTo { get; set; }
    public string Status { get; set; }
    public DateTime DueDate { get; set; }
}