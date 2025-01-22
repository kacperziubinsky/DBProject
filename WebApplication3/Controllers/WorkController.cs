using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using WebApplication3.Models;


namespace WebApplication3.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WorkController : Controller
    {
        private readonly string _connectionString;

        public WorkController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("MySqlConnection");
        }

        [HttpGet]
        public IEnumerable<Work> Get()
        {
            var workList = new List<Work>();

            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                string query = "SELECT * FROM work";
                MySqlCommand command = new MySqlCommand(query, connection);
                connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        workList.Add(new Work
                        {
                            Id = reader.GetInt32("Id"),
                            UserId = reader.GetInt32("UserId"),
                            taskID = reader.GetInt32("taskID"),
                            date = reader.GetDateTime("date"),
                            hours = reader.GetDecimal("hours"),
                        });
                    }
                }
            }
            return workList;
        }

        [HttpGet]
        [Route("user/{userId}")]
        public IEnumerable<Work> GetByUserId(int userId)
        {
            var workList = new List<Work>();

            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                string query = "SELECT * FROM work WHERE UserId = @UserId";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@UserId", userId);
                connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        workList.Add(new Work
                        {
                            Id = reader.GetInt32("Id"),
                            UserId = reader.GetInt32("UserId"),
                            taskID = reader.GetInt32("taskID"),
                            date = reader.GetDateTime("date"),
                            hours = reader.GetDecimal("hours"),
                        });
                    }
                }
            }

            return workList;
        }

        [HttpGet]
        [Route("task/{taskId}")]
        public IEnumerable<Work> GetByTaskId(int taskId)
        {
            var workList = new List<Work>();

            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                string query = "SELECT * FROM work WHERE taskID = @taskID";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@taskID", taskId);
                connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        workList.Add(new Work
                        {
                            Id = reader.GetInt32("Id"),
                            UserId = reader.GetInt32("UserId"),
                            taskID = reader.GetInt32("taskID"),
                            date = reader.GetDateTime("date"),
                            hours = reader.GetDecimal("hours"),
                        });
                    }
                }
            }

            return workList;
        }

        [HttpPost]
        public async Task<ActionResult<Work>> AddWork(Work work)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string sql = "INSERT INTO work (UserId, taskID, date, hours) VALUES (@UserId, @taskID, @date, @hours)";
                MySqlCommand command = new MySqlCommand(sql, connection);
                command.Parameters.AddWithValue("@UserId", work.UserId);
                command.Parameters.AddWithValue("@taskID", work.taskID);
                command.Parameters.AddWithValue("@date", work.date);
                command.Parameters.AddWithValue("@hours", work.hours);
                await command.ExecuteNonQueryAsync();

                return Ok(work);
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> DeleteWork(int id)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                string query = "DELETE FROM work WHERE Id = @Id";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", id);
                await connection.OpenAsync();
                int rowsAffected = await command.ExecuteNonQueryAsync();

                if (rowsAffected > 0)
                {
                    return Ok(new { Message = "Work record deleted successfully." });
                }
                else
                {
                    return NotFound(new { Message = "Work record not found." });
                }
            }
        }

        [HttpPut]
        public async Task<ActionResult<Work>> UpdateWork(Work work)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                string updateQuery = "UPDATE work SET UserId = @UserId, taskID = @taskID, date = @date, hours = @hours WHERE Id = @Id";
                MySqlCommand command = new MySqlCommand(updateQuery, connection);
                command.Parameters.AddWithValue("@Id", work.Id);
                command.Parameters.AddWithValue("@UserId", work.UserId);
                command.Parameters.AddWithValue("@taskID", work.taskID);
                command.Parameters.AddWithValue("@date", work.date);
                command.Parameters.AddWithValue("@hours", work.hours);
                await connection.OpenAsync();

                try
                {
                    int rowsAffected = await command.ExecuteNonQueryAsync();

                    if (rowsAffected > 0)
                    {
                        return Ok(new { Message = "Work record updated successfully." });
                    }
                    else
                    {
                        return NotFound(new { Message = "Work record not found or no changes made." });
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
