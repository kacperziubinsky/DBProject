using System.ComponentModel.DataAnnotations;

namespace DBProject.Models;

public class Invoice
{
    [Key]
    public int InvoiceID { get; set; }
    public int ClientID { get; set; }
    public DateTime InvoiceDate { get; set; }
    public decimal TotalAmount { get; set; }
}