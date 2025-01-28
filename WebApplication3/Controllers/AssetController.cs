using DBProject.Models;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace DBProject.Controllers;

[ApiController]
[Route("[controller]")]
public class AssetController : ControllerBase
{
    private readonly string _connectionString;

    public AssetController(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("MySqlConnection");
    }

    [HttpGet]
    public IEnumerable<Asset> Get()
    {
        var assets = new List<Asset>();

        using (MySqlConnection connection = new MySqlConnection(_connectionString))
        {
            string query = "SELECT * FROM assets";
            MySqlCommand command = new MySqlCommand(query, connection);
            connection.Open();

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    assets.Add(new Asset
                    {
                        AssetID = reader.GetInt32("AssetID"),
                        AssetName = reader.GetString("AssetName"),
                        LocationID = reader.GetInt32("LocationID"),
                        PurchaseDate = reader.GetDateTime("PurchaseDate"),
                        Value = reader.GetDecimal("Value"),
                    });
                }
            }
        }
        return assets;
    }

    [HttpGet]
    [Route("{AssetID}")]
    public IActionResult Get(int AssetID)
    {
        Asset asset = null;
        using (MySqlConnection connection = new MySqlConnection(_connectionString))
        {
            string query = "SELECT * FROM assets WHERE AssetID = @AssetID";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@AssetID", AssetID);
            connection.Open();
            
            using (var reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    asset = new Asset
                    {
                        AssetID = reader.GetInt32("AssetID"),
                        AssetName = reader.GetString("AssetName"),
                        LocationID = reader.GetInt32("LocationID"),
                        PurchaseDate = reader.GetDateTime("PurchaseDate"),
                        Value = reader.GetDecimal("Value"),
                    };
                }
            }
        }
        return Ok(asset);
    }

    [HttpPost]
    public IActionResult Post(Asset asset)
    {
        using (MySqlConnection connection = new MySqlConnection(_connectionString))
        {
            string query = @"INSERT INTO `assets` (`AssetName`, `LocationID`, `PurchaseDate`, `Value`) 
                         VALUES (@AssetName, @LocationID, @PurchaseDate, @Value)";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@AssetName", asset.AssetName);
            command.Parameters.AddWithValue("@LocationID", asset.LocationID);
            command.Parameters.AddWithValue("@PurchaseDate", asset.PurchaseDate);
            command.Parameters.AddWithValue("@Value", asset.Value);

            connection.Open();

            try
            {
                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected == 1)
                {
                    return Ok(new { Message = "Asset added successfully." });
                }
                else
                {
                    return StatusCode(500, new { Message = "Asset could not be added." });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred.", Details = ex.Message });
            }
            finally
            {
                connection.Close();
            }
        }
    }


    [HttpPut]
    public IActionResult Put(Asset asset)
    {
        using (MySqlConnection connection = new MySqlConnection(_connectionString))
        {
            string query = @"UPDATE `assets` 
                         SET `AssetName` = @AssetName,  
                             `LocationID` = @LocationID, 
                             `PurchaseDate` = @PurchaseDate, 
                             `Value` = @Value 
                         WHERE `AssetID` = @AssetID";

            MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@AssetID", asset.AssetID);
            command.Parameters.AddWithValue("@AssetName", asset.AssetName);
            command.Parameters.AddWithValue("@LocationID", asset.LocationID);
            command.Parameters.AddWithValue("@PurchaseDate", asset.PurchaseDate);
            command.Parameters.AddWithValue("@Value", asset.Value);
            
            connection.Open();
            try
            {
                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    return Ok(new { Message = "Asset updated successfully." });
                }
                else
                {
                    return NotFound(new { Message = "Asset not found or no changes made." });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred.", Details = ex.Message });
            }
            finally
            {
                connection.Close();
            }
        }

    }

    [HttpDelete]
    [Route("{AssetID}")]
    public IActionResult Delete(int AssetID)
    {
        using (MySqlConnection connection = new MySqlConnection(_connectionString))
        {
            string query = "DELETE FROM `assets` WHERE `AssetID` = @AssetID";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@AssetID", AssetID);
            connection.Open();

            try
            {
                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    return Ok(new { Message = "Asset deleted successfully." });
                }
                else
                {
                    return NotFound(new { Message = "Asset not found or no changes made." });
                }
            }
            finally
            {
                connection.Close();
            }
        }
    }
}