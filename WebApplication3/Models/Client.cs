using System.ComponentModel.DataAnnotations;

namespace DBProject.Models;

public class Client
{
    [Key]
    public int ClientID { get; set; }
    public string ClientName { get; set; }
    public string ContactEmail { get; set; }
    public string ContactPhone { get; set; }
}