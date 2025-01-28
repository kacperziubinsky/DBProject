using DBProject.Models;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Task = DBProject.Models.Task;


namespace DBProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly string _connectionString;

        public TaskController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("MySqlConnection");
        }

        [HttpGet]
        public IEnumerable<Task> Get()
        {
            var tasks = new List<Task>();
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Tasks";
                MySqlCommand command = new MySqlCommand(query, connection);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        tasks.Add(new Task
                        {
                            TaskID = reader.GetInt32("TaskID"),
                            TaskName = reader.GetString("TaskName"),
                            ProjectID = reader.GetInt32("ProjectID"),
                            AssignedTo = reader.GetInt32("AssignedTo"),
                            Status = reader.GetString("Status"),
                            DueDate = reader.GetDateTime("DueDate")
                        });
                    }
                }
            }
            return tasks;
        }

        [HttpGet("{TaskID}")]
        public IActionResult Get(int TaskID)
        {
            Task task = null;
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Tasks WHERE TaskID = @TaskID";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@TaskID", TaskID);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        task = new Task
                        {
                            TaskID = reader.GetInt32("TaskID"),
                            TaskName = reader.GetString("TaskName"),
                            ProjectID = reader.GetInt32("ProjectID"),
                            Status = reader.GetString("Status"),
                            DueDate = reader.GetDateTime("DueDate")
                        };
                    }
                }
            }
            return task != null ? Ok(task) : NotFound();
        }

        [HttpPost]
        public IActionResult Post([FromBody] Task task)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                string query = "INSERT INTO Tasks (TaskName, ProjectID, AssignedTo, Status, DueDate) " +
                               "VALUES (@TaskName, @ProjectID, @AssignedTo, @Status, @DueDate)";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@TaskName", task.TaskName);
                command.Parameters.AddWithValue("@ProjectID", task.ProjectID);
                command.Parameters.AddWithValue("@AssignedTo", task.AssignedTo);
                command.Parameters.AddWithValue("@Status", task.Status);
                command.Parameters.AddWithValue("@DueDate", task.DueDate);

                connection.Open();
                try
                {
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected == 1)
                    {
                        return Ok(new { Message = "Task added successfully." });
                    }
                    else
                    {
                        return StatusCode(500, new { Message = "Task could not be added." });
                    }
                }
                catch (Exception ex)
                {
                    return StatusCode(500, new { Message = "An error occurred.", Details = ex.Message });
                }
            }
        }

        [HttpPut("{TaskID}")]
        public IActionResult Put(int TaskID, [FromBody] Task task)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                string query = "UPDATE Tasks SET TaskName = @TaskName, ProjectID = @ProjectID, AssignedTo = @AssignedTo, " +
                               "Status = @Status, DueDate = @DueDate WHERE TaskID = @TaskID";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@TaskID", TaskID);
                command.Parameters.AddWithValue("@TaskName", task.TaskName);
                command.Parameters.AddWithValue("@ProjectID", task.ProjectID);
                command.Parameters.AddWithValue("@AssignedTo", task.AssignedTo);
                command.Parameters.AddWithValue("@Status", task.Status);
                command.Parameters.AddWithValue("@DueDate", task.DueDate);

                connection.Open();
                try
                {
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        return Ok(new { Message = "Task updated successfully." });
                    }
                    else
                    {
                        return NotFound(new { Message = "Task not found or no changes made." });
                    }
                }
                catch (Exception ex)
                {
                    return StatusCode(500, new { Message = "An error occurred.", Details = ex.Message });
                }
            }
        }

        [HttpDelete("{TaskID}")]
        public IActionResult Delete(int TaskID)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                string query = "DELETE FROM Tasks WHERE TaskID = @TaskID";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@TaskID", TaskID);

                connection.Open();
                try
                {
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        return Ok(new { Message = "Task deleted successfully." });
                    }
                    else
                    {
                        return NotFound(new { Message = "Task not found." });
                    }
                }
                catch (Exception ex)
                {
                    return StatusCode(500, new { Message = "An error occurred.", Details = ex.Message });
                }
            }
        }
    }
}
