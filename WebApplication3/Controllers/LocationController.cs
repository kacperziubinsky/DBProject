using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using DBProject.Models;

namespace DBProject.Controllers;

[ApiController]
[Route("[controller]")]
public class LocationController : ControllerBase
{
    private readonly string _connectionString;

    public LocationController(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("MySqlConnection");
    }

    [HttpGet]
    public IEnumerable<Location> Get()
    {
        var locations = new List<Location>();
        using (MySqlConnection connection = new MySqlConnection(_connectionString))
        {
            string query = "SELECT * FROM Locations";
            MySqlCommand command = new MySqlCommand(query, connection);
            connection.Open();
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    locations.Add(new Location
                    {
                        LocationID = reader.GetInt32("LocationID"),
                        LocationName = reader.GetString("LocationName"),
                        Address = reader.GetString("Address")
                    });
                }
            }
        }
        return locations;
    }

    [HttpGet("{LocationID}")]
    public IActionResult Get(int LocationID)
    {
        Location location = null;
        using (MySqlConnection connection = new MySqlConnection(_connectionString))
        {
            string query = "SELECT * FROM Locations WHERE LocationID = @LocationID";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@LocationID", LocationID);
            connection.Open();
            using (var reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    location = new Location
                    {
                        LocationID = reader.GetInt32("LocationID"),
                        LocationName = reader.GetString("LocationName"),
                        Address = reader.GetString("Address")
                    };
                }
            }
        }
        return location != null ? Ok(location) : NotFound(new { Message = "Location not found" });
    }

    [HttpPost]
    public IActionResult Post(Location location)
    {
        using (MySqlConnection connection = new MySqlConnection(_connectionString))
        {
            string query = @"INSERT INTO Locations (LocationName, Address) VALUES (@LocationName, @Address)";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@LocationName", location.LocationName);
            command.Parameters.AddWithValue("@Address", location.Address);
            connection.Open();
            int rowsAffected = command.ExecuteNonQuery();
            return rowsAffected > 0 ? Ok(new { Message = "Location added successfully" }) : StatusCode(500, new { Message = "Error adding location" });
        }
    }

    [HttpPut]
    public IActionResult Put(Location location)
    {
        using (MySqlConnection connection = new MySqlConnection(_connectionString))
        {
            string query = @"UPDATE Locations SET LocationName = @LocationName, Address = @Address WHERE LocationID = @LocationID";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@LocationID", location.LocationID);
            command.Parameters.AddWithValue("@LocationName", location.LocationName);
            command.Parameters.AddWithValue("@Address", location.Address);
            connection.Open();
            int rowsAffected = command.ExecuteNonQuery();
            return rowsAffected > 0 ? Ok(new { Message = "Location updated successfully" }) : NotFound(new { Message = "Location not found or no changes made" });
        }
    }

    [HttpDelete("{LocationID}")]
    public IActionResult Delete(int LocationID)
    {
        using (MySqlConnection connection = new MySqlConnection(_connectionString))
        {
            string query = "DELETE FROM Locations WHERE LocationID = @LocationID";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@LocationID", LocationID);
            connection.Open();
            int rowsAffected = command.ExecuteNonQuery();
            return rowsAffected > 0 ? Ok(new { Message = "Location deleted successfully" }) : NotFound(new { Message = "Location not found" });
        }
    }
}