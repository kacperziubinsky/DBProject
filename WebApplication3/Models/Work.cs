namespace WebApplication3.Models;

public class Work
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int taskID { get; set; }
    public DateTime date { get; set; }
    public decimal hours { get; set; }
}