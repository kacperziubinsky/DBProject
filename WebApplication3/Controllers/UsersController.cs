using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using WebApplication3.Models; 


namespace WebApplication3.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly string _connectionString;

        public UsersController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("MySqlConnection");
        }
        
        [Route("allUsers")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            List<User> users = new List<User>();

            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT userID, firstName, lastName, Position, Email, hireDate, Salary FROM Users";
                using (var cmd = new MySqlCommand(query, connection))
                {
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            User user = new User();
                            user.userID = reader["userID"].ToString();
                            user.firstName = reader["firstName"].ToString();
                            user.lastName = reader["lastName"].ToString();
                            user.Position = reader["position"].ToString();
                            user.Email = reader["email"].ToString();
                            user.hireDate = (DateTime)reader["hireDate"];
                            user.Salary = (decimal)reader["Salary"];
                            users.Add(user);
                        }
                    }
                }
            }

            return Ok(users); 
        }

        [Route("User/{userID}")]
        [HttpGet]
        public async Task<ActionResult<User>> GetUser(int userID)
        {
            User user = null; 

            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT userID, firstName, lastName, Position, Email, hireDate, Salary FROM Users WHERE userID = @userID";
                
                using (var cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@userID", userID);

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            user = new User
                            {
                                userID = reader["userID"].ToString(),
                                firstName = reader["firstName"].ToString(),
                                lastName = reader["lastName"].ToString(),
                                Position = reader["Position"].ToString(),
                                Email = reader["Email"].ToString(),
                                hireDate = (DateTime)reader["hireDate"],
                                Salary = (decimal)reader["Salary"]
                            };
                        }
                    }
                }
            }

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user); 
        }


        [HttpPost]
        public async Task<ActionResult<User>> AddUser(User user)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = @"
            INSERT INTO Users (firstName, lastName, Position, Email, hireDate, Salary) 
            VALUES (@firstName, @lastName, @Position, @Email, @hireDate, @Salary);";

                using (var cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@firstName", user.firstName);
                    cmd.Parameters.AddWithValue("@lastName", user.lastName);
                    cmd.Parameters.AddWithValue("@Position", user.Position);
                    cmd.Parameters.AddWithValue("@Email", user.Email);
                    cmd.Parameters.AddWithValue("@hireDate", user.hireDate);
                    cmd.Parameters.AddWithValue("@Salary", user.Salary);
                    cmd.ExecuteNonQuery();
                }
                 
            }

            return Ok(user);
        }
        
        [Route("[controller]/{clientID}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteUser(int userId)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                string query = "DELETE FROM Users WHERE userID = @userID";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@clientID", userId);

                await connection.OpenAsync();
                int rowsAffected = await command.ExecuteNonQueryAsync();
                connection.Close();

                if (rowsAffected > 0)
                {
                    return Ok(new { Message = "User deleted successfully." });
                }
                else
                {
                    return NotFound(new { Message = "User not found." });
                }
            }
        }

        [HttpPut]
        public async Task<ActionResult<User>> UpdateUser(User user)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                string query = " UPDATE `Users` SET `firstName` = @firstName, `lastName` = @lastName, `position` = @position, `email` = @email, `hireDate` = @hireDate, `salary` = @salary WHERE `userID` = @userID";                
                MySqlCommand command = new MySqlCommand(query, connection);
                
                command.Parameters.AddWithValue("@userID", user.userID);
                command.Parameters.AddWithValue("@firstName", user.firstName);
                command.Parameters.AddWithValue("@lastName", user.lastName);
                command.Parameters.AddWithValue("@position", user.Position);
                command.Parameters.AddWithValue("@email", user.Email);
                command.Parameters.AddWithValue("@hireDate", user.hireDate);
                command.Parameters.AddWithValue("@salary", user.Salary);
                
                await connection.OpenAsync();
                
                try
                {
                    int rowsAffected = await command.ExecuteNonQueryAsync();

                    if (rowsAffected > 0)
                    {
                        return Ok(new { Message = "User updated successfully." });
                    }
                    else
                    {
                        return NotFound(new { Message = "User not found or no changes made." });
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
