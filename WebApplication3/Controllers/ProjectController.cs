using System.Data;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using WebApplication3.Models;

namespace WebApplication3.Controllers;

[ApiController]
[Route("[controller]")]
public class ProjectController : Controller
{
    private readonly string _connectionString;

    public ProjectController(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("MySqlConnection");
    }

    [HttpGet]
    public IEnumerable<Project> Get()
    {
        var projects = new List<Project>();
        using (MySqlConnection connection = new MySqlConnection(_connectionString))
        {
            string query = "SELECT * FROM projects";
            MySqlCommand command = new MySqlCommand(query, connection);
            connection.Open();

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    projects.Add(new Project
                    {
                        clientID = reader.GetInt32("clientID"),
                        projectID = reader.GetInt32("projectID"),
                        projectName = reader.GetString("projectName"),
                        startDate = reader.GetDateTime("startDate"),
                        endDate = reader.GetDateTime("endDate"),
                        status = reader.GetString("status"),
                    });
                }
            }
        }
        return projects;
    }

    [HttpGet]
    [Route("client/{clientID}")]
    public IEnumerable<Project> Get(int clientID)
    {
        var projects = new List<Project>();

        using (MySqlConnection connection = new MySqlConnection(_connectionString))
        {
            string query = "SELECT * FROM projects WHERE clientID = @clientID";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@clientID", clientID);
            connection.Open();

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    projects.Add(new Project()
                    {
                        clientID = reader.GetInt32("clientID"),
                        projectID = reader.GetInt32("projectID"),
                        projectName = reader.GetString("projectName"),
                        startDate = reader.GetDateTime("startDate"),
                        endDate = reader.GetDateTime("endDate"),
                        status = reader.GetString("status"),
                    });
                }
            }
        }
        return projects;
    }

    [HttpGet]
    [Route("project/{projectID}")]
    public async Task<ActionResult<Project>> GetProject(int projectID)
    {
        Project project = null;

        using (MySqlConnection connection = new MySqlConnection(_connectionString))
        {
            string query = "SELECT * FROM projects WHERE projectID = @projectID";
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@projectID", projectID);
                await connection.OpenAsync();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        project = new Project()
                        {
                            clientID = reader.GetInt32("clientID"),
                            projectID = reader.GetInt32("projectID"),
                            projectName = reader.GetString("projectName"),
                            startDate = reader.GetDateTime("startDate"),
                            endDate = reader.GetDateTime("endDate"),
                            status = reader.GetString("status"),
                        };
                    }
                }
            }
        }

        if (project == null)
        {
            return NotFound(new { Message = "Project not found." });
        }

        return Ok(project);
    }


    [HttpPost]
    public async Task<ActionResult<Project>> AddUser(Project project)
    {
        using (MySqlConnection connection = new MySqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            string sql =
                "INSERT INTO `Projects` ( `projectName`, `clientID`, `startDate`, `endDate`, `status`) VALUES (@projectName, @clientID, @startDate, @endDate, @status)";
            MySqlCommand command = new MySqlCommand(sql, connection);
            command.Parameters.AddWithValue("@projectName", project.projectName);
            command.Parameters.AddWithValue("@clientID", project.clientID);
            command.Parameters.AddWithValue("@startDate", project.startDate);
            command.Parameters.AddWithValue("@endDate", project.endDate);
            command.Parameters.AddWithValue("@status", project.status);
            command.ExecuteNonQuery();

            return Ok(project);
        }
    }

    [HttpDelete]
    public async Task<ActionResult<Project>> DeleteProject(int projectID)
    {
        using (MySqlConnection connection = new MySqlConnection(_connectionString))
        {
            string query = "DELETE FROM projects WHERE projectID = @projectID";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@projectID", projectID);
            connection.Open();
            await command.ExecuteNonQueryAsync();
            int rowsAffected = command.ExecuteNonQuery();
            
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
    public async Task<ActionResult<Project>> UpdateProject(Project project)
    {
        using (MySqlConnection connection = new MySqlConnection(_connectionString))
        {
            string updateQuery = " UPDATE projects SET projectName = @projectName, clientID = @clientID, startDate = @startDate, endDate = @endDate, status = @status WHERE projectID = @projectID";
            MySqlCommand command = new MySqlCommand(updateQuery, connection);
            command.Parameters.AddWithValue("@projectID", project.projectID);
            command.Parameters.AddWithValue("@projectName", project.projectName);
            command.Parameters.AddWithValue("@clientID", project.clientID);
            command.Parameters.AddWithValue("@startDate", project.startDate);
            command.Parameters.AddWithValue("@endDate", project.endDate);
            command.Parameters.AddWithValue("@status", project.status);
            await connection.OpenAsync();

            try
            {
                int rowsAffected = await command.ExecuteNonQueryAsync();

                if (rowsAffected > 0)
                {
                    return Ok(new { Message = "Project updated successfully." });
                }
                else
                {
                    return NotFound(new { Message = "Project not found or no changes made." });
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