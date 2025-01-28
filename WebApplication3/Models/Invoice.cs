namespace DBProject.Models;

public class Invoice
{
    public int InvoiceID { get; set; }
    public int ClientID { get; set; }
    public DateTime InvoiceDate { get; set; }
    public decimal TotalAmount { get; set; }
}