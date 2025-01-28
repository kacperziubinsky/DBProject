using DBProject.Models;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace DBProject.Controllers;

[ApiController]
[Route("[controller]")]
public class ClientController : ControllerBase
{
    private readonly string _connectionString;

    public ClientController(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("MySqlConnection");
    }

    [HttpGet]
    public IEnumerable<Client> Get()
    {
        var clients = new List<Client>();

        using (MySqlConnection connection = new MySqlConnection(_connectionString))
        {
            string query = "SELECT * FROM clients";
            MySqlCommand command = new MySqlCommand(query, connection);
            connection.Open();

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    clients.Add(new Client
                    {
                        ClientID = reader.GetInt32("ClientID"),
                        ClientName = reader.GetString("ClientName"),
                        ContactEmail = reader.GetString("ContactEmail"),
                        ContactPhone = reader.GetString("ContactPhone"),
                    });
                }
            }
        }
        return clients;
    }

    [HttpGet]
    [Route("{clientID}")]
    public IActionResult Get(int clientID)
    {
        Client client = null;
        using (MySqlConnection connection = new MySqlConnection(_connectionString))
        {
            string query = "SELECT * FROM clients WHERE clientID = @clientID";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@ClientId", clientID);
            connection.Open();
            
            using (var reader = command.ExecuteReader())
            {
                if (reader.Read()) 
                {
                    client = new Client
                    {
                        ClientID = reader.GetInt32("ClientID"),
                        ClientName = reader.GetString("ClientName"),
                        ContactEmail = reader.GetString("ContactEmail"),
                        ContactPhone = reader.GetString("ContactPhone"),
                    };
                }
            }
        }
        return Ok(client);
    }

    [HttpPost]
    public IActionResult Post(Client client)
    {
        using (MySqlConnection connection = new MySqlConnection(_connectionString))
        {
            string query =
                @"INSERT INTO `clients` ( `ClientName`, `ContactEmail`, `ContactPhone`) VALUES (@name, @email, @phone)";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@name", client.ClientName);
            command.Parameters.AddWithValue("@email", client.ContactEmail);
            command.Parameters.AddWithValue("@phone", client.ContactPhone);
            connection.Open();
            command.ExecuteNonQuery();

            try
            {
                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected == 1)
                {
                    return Ok(new { Message = "Client added successfully." });
                }
                else
                {
                    return StatusCode(500, new { Message = "Client could not be added." });
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
    public IActionResult Put(Client client)
    {
        using (MySqlConnection connection = new MySqlConnection(_connectionString))
        {
            string query = @"UPDATE `clients` SET `ClientName` = @clientName,  `ContactEmail` = @email, `ContactPhone` = @phone WHERE `ClientID` = @clientID";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@clientID", client.ClientID);
            command.Parameters.AddWithValue("@clientName", client.ClientName);
            command.Parameters.AddWithValue("@email", client.ContactEmail);
            command.Parameters.AddWithValue("@phone", client.ContactPhone);
            connection.Open();

            try
            {
                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    return Ok(new { Message = "Client updated successfully." });
                }
                else
                {
                    return NotFound(new { Message = "Client not found or no changes made." });
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
    [Route("{clientID}")]
    public IActionResult Delete(int clientID)
    {
        using (MySqlConnection connection = new MySqlConnection(_connectionString))
        {
            string query = "DELETE FROM `clients` WHERE `ClientID` = @clientID";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@clientID", clientID);
            connection.Open();

            try
            {
                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    return Ok(new { Message = "Client deleted successfully." });
                }
                else
                {
                    return NotFound(new { Message = "Client not found or no changes made." });
                }
            }
            finally
            {
                connection.Close();
            }
        }
    }
}