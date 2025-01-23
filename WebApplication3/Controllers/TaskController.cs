using System.Data;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using WebApplication3.Models;
using Task = WebApplication3.Models.Task;

namespace WebApplication3.Controllers;

[ApiController]
[Route("[controller]")]
public class TaskController : Controller
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
            string query = "SELECT * FROM tasks";
            MySqlCommand command = new MySqlCommand(query, connection);
            connection.Open();

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    tasks.Add(new Task
                    {
                        taskID = reader.GetInt32("taskID"),
                        content = reader.GetString("content"),
                        projectID = reader.GetInt32("projectID"),
                        status = reader.GetString("status"),
                        startDate = reader.GetDateTime("startDate"),
                        endDate = reader.GetDateTime("endDate"),
                    });
                }
            }
        }
        return tasks;
    }

    [HttpGet]
    [Route("{taskID}")]
    public async Task<ActionResult<Task>> GetTask(int taskID)
    {
        Task task = null;

        using (MySqlConnection connection = new MySqlConnection(_connectionString))
        {
            string query = "SELECT * FROM tasks WHERE taskID = @taskID";
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@taskID", taskID);
                await connection.OpenAsync();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        task = new Task()
                        {
                            taskID = reader.GetInt32("taskID"),
                            content = reader.GetString("content"),
                            projectID = reader.GetInt32("projectID"),
                            status = reader.GetString("status"),
                            startDate = reader.GetDateTime("startDate"),
                            endDate = reader.GetDateTime("endDate"),
                        };
                    }
                }
            }
        }

        if (task == null)
        {
            return NotFound(new { Message = "Task not found." });
        }

        return Ok(task);
    }

    [HttpPost]
    public async Task<ActionResult<Task>> AddTask(Task task)
    {
        using (MySqlConnection connection = new MySqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            string sql =
                "INSERT INTO `tasks` (`content`, `projectID`, `status`, `startDate`, `endDate`) VALUES (@content, @projectID, @status, @startDate, @endDate)";
            MySqlCommand command = new MySqlCommand(sql, connection);
            command.Parameters.AddWithValue("@content", task.content);
            command.Parameters.AddWithValue("@projectID", task.projectID);
            command.Parameters.AddWithValue("@status", task.status);
            command.Parameters.AddWithValue("@startDate", task.startDate);
            command.Parameters.AddWithValue("@endDate", task.endDate);
            await command.ExecuteNonQueryAsync();

            return Ok(task);
        }
    }

    
    [HttpDelete]
    [Route("{taskID}")]
    public async Task<ActionResult> DeleteTask(int taskID)
    {
        using (MySqlConnection connection = new MySqlConnection(_connectionString))
        {
            string query = "DELETE FROM tasks WHERE taskID = @taskID";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@taskID", taskID);
            await connection.OpenAsync();
            int rowsAffected = await command.ExecuteNonQueryAsync();

            if (rowsAffected > 0)
            {
                return Ok(new { Message = "Task deleted successfully." });
            }
            else
            {
                return NotFound(new { Message = "Task not found." });
            }
        }
    }

    [HttpPut]
    public async Task<ActionResult<Task>> UpdateTask(Task task)
    {
        using (MySqlConnection connection = new MySqlConnection(_connectionString))
        {
            string updateQuery = "UPDATE tasks SET content = @content, projectID = @projectID, status = @status, startDate = @startDate, endDate = @endDate WHERE taskID = @taskID";
            MySqlCommand command = new MySqlCommand(updateQuery, connection);
            command.Parameters.AddWithValue("@taskID", task.taskID);
            command.Parameters.AddWithValue("@content", task.content);
            command.Parameters.AddWithValue("@projectID", task.projectID);
            command.Parameters.AddWithValue("@status", task.status);
            command.Parameters.AddWithValue("@startDate", task.startDate);
            command.Parameters.AddWithValue("@endDate", task.endDate);
            await connection.OpenAsync();

            try
            {
                int rowsAffected = await command.ExecuteNonQueryAsync();

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
            finally
            {
                await connection.CloseAsync();
            }
        }
    }
    [HttpGet]
    [Route("details/{taskID}")]
    public async Task<ActionResult> GetTaskWithDetails(int taskID)
    {
        using (MySqlConnection connection = new MySqlConnection(_connectionString))
        {
            string query = @"
            SELECT 
                tasks.taskID, 
                tasks.content, 
                tasks.status, 
                tasks.startDate, 
                tasks.endDate, 
                projects.projectName, 
                projects.clientID 
            FROM 
                tasks 
            INNER JOIN 
                projects 
            ON 
                tasks.projectID = projects.projectID
            WHERE 
                tasks.taskID = @taskID";
        
            using (var command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@taskID", taskID);
                await connection.OpenAsync();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        var taskDetails = new
                        {
                            TaskID = reader.GetInt32("taskID"),
                            Content = reader.GetString("content"),
                            Status = reader.GetString("status"),
                            StartDate = reader.GetDateTime("startDate"),
                            EndDate = reader.GetDateTime("endDate"),
                            ProjectName = reader.GetString("projectName"),
                            ClientID = reader.GetInt32("clientID")
                        };
                        return Ok(taskDetails);
                    }
                }
            }
        }
        return NotFound(new { Message = "Task not found." });
    }

    [HttpGet]
    [Route("overdue")]
    public async Task<ActionResult<IEnumerable<Task>>> GetOverdueTasks()
    {
        var tasks = new List<Task>();
        using (MySqlConnection connection = new MySqlConnection(_connectionString))
        {
            string query = "SELECT * FROM tasks WHERE endDate < NOW() AND status != 'Completed'";
            using (var command = new MySqlCommand(query, connection))
            {
                await connection.OpenAsync();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        tasks.Add(new Task
                        {
                            taskID = reader.GetInt32("taskID"),
                            content = reader.GetString("content"),
                            projectID = reader.GetInt32("projectID"),
                            status = reader.GetString("status"),
                            startDate = reader.GetDateTime("startDate"),
                            endDate = reader.GetDateTime("endDate"),
                        });
                    }
                }
            }
        }
        return Ok(tasks);
    }

    [HttpGet]
    [Route("groupedByStatus")]
    public async Task<ActionResult> GetTasksGroupedByStatus()
    {
        using (MySqlConnection connection = new MySqlConnection(_connectionString))
        {
            string query = "SELECT status, COUNT(*) AS taskCount FROM tasks GROUP BY status";
            using (var command = new MySqlCommand(query, connection))
            {
                await connection.OpenAsync();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    var groupedTasks = new List<object>();
                    while (await reader.ReadAsync())
                    {
                        groupedTasks.Add(new
                        {
                            Status = reader.GetString("status"),
                            TaskCount = reader.GetInt32("taskCount")
                        });
                    }
                    return Ok(groupedTasks);
                }
            }
        }
    }

}
