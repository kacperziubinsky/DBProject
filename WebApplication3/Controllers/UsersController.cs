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
            VALUES (@firstName, @lastName, @Position, @Email, @hireDate, @Salary);
            SELECT LAST_INSERT_ID();";

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

    }
}
