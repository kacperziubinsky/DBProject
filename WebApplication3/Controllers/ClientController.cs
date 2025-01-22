using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using WebApplication3.Models;
using System.Collections.Generic;

namespace WebApplication3.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientController : ControllerBase
    {
        private readonly string _connectionString;

        public ClientController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("MySqlConnection");
        }

        [Route("allClients")]
        [HttpGet]
        public IEnumerable<Client> GetClients()
        {
            var clients = new List<Client>();

            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Clients";
                MySqlCommand command = new MySqlCommand(query, connection);

                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        clients.Add(new Client
                        {
                            clientID = reader.GetInt32("clientID"),
                            name = reader.GetString("name"),
                            email = reader.GetString("email"),
                            industry = reader.GetString("industry"),
                            phone = reader.GetString("phone"),
                        });
                    }
                }
            }

            return clients;
        }


        [Route("[controller]/{clientID}")]
        [HttpGet]
        public IEnumerable<Client> GetClient(int clientID)
        {
            Client client = null;
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Clients WHERE clientID = @clientID";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@clientID", clientID);
                connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        client = new Client()
                        {
                            clientID = reader.GetInt32("clientID"),
                            name = reader.GetString("name"),
                            email = reader.GetString("email"),
                            industry = reader.GetString("industry"),
                            phone = reader.GetString("phone")
                        };
                    }
                }
            }

            return [client];
        }
        
        
        [Route("[controller]/{clientID}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteClient(int clientID)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                string query = "DELETE FROM Clients WHERE clientID = @clientID";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@clientID", clientID);

                await connection.OpenAsync();
                int rowsAffected = await command.ExecuteNonQueryAsync();
                connection.Close();

                if (rowsAffected > 0)
                {
                    return Ok(new { Message = "Client deleted successfully." });
                }
                else
                {
                    return NotFound(new { Message = "Client not found." });
                }
            }
        }
        
        [HttpPost]
        public async Task<IActionResult> PostClient(Client client)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                string query = @"
            INSERT INTO `Clients` (`name`, `industry`, `email`, `phone`) 
            VALUES (@name, @industry, @email, @phone)";
        
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@name", client.name);
                command.Parameters.AddWithValue("@industry", client.industry);
                command.Parameters.AddWithValue("@email", client.email);
                command.Parameters.AddWithValue("@phone", client.phone);

                await connection.OpenAsync();

                try
                {
                    int rowsAffected = await command.ExecuteNonQueryAsync();

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
                    await connection.CloseAsync();
                }
            }
        }

        [HttpPut]
        public async Task<IActionResult> PutClient(Client client)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                string query = @" UPDATE `Clients` SET `name` = @name, `industry` = @industry, `email` = @email, `phone` = @phone WHERE `clientID` = @clientID";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@clientID", client.clientID);
                command.Parameters.AddWithValue("@name", client.name);
                command.Parameters.AddWithValue("@industry", client.industry);
                command.Parameters.AddWithValue("@email", client.email);
                command.Parameters.AddWithValue("@phone", client.phone);
                
                await connection.OpenAsync();
                try
                {
                    int rowsAffected = await command.ExecuteNonQueryAsync();

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
                    await connection.CloseAsync();
                }

            }
        }

    }
}