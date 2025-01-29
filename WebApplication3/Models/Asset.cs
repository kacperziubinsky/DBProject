using System.ComponentModel.DataAnnotations;

namespace DBProject.Models;

public class Asset
{
    [Key]
    public int AssetID { get; set; }
    public string AssetName { get; set; }
    public int LocationID { get; set; }
    public DateTime PurchaseDate { get; set; }
    public decimal Value { get; set; }
}