using System.ComponentModel.DataAnnotations;

namespace DBProject.Models;

public class Location
{
    [Key]
    public int LocationID { get; set; }
    public string LocationName { get; set; }
    public string Address { get; set; }
}