using DBProject.Models;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace DBProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProjectController : ControllerBase
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
                string query = "SELECT * FROM Projects";
                MySqlCommand command = new MySqlCommand(query, connection);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        projects.Add(new Project
                        {
                            ProjectID = reader.GetInt32("ProjectID"),
                            ProjectName = reader.GetString("ProjectName"),
                            DepartmentID = reader.GetInt32("DepartmentID"),
                            StartDate = reader.GetDateTime("StartDate"),
                            EndDate = reader.GetDateTime("EndDate"),
                        });
                    }
                }
            }
            return projects;
        }

        [HttpGet("{ProjectID}")]
        public IActionResult Get(int ProjectID)
        {
            Project project = null;
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Projects WHERE ProjectID = @ProjectID";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@ProjectID", ProjectID);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        project = new Project
                        {
                            ProjectID = reader.GetInt32("ProjectID"),
                            ProjectName = reader.GetString("ProjectName"),
                            DepartmentID = reader.GetInt32("DepartmentID"),
                            StartDate = reader.GetDateTime("StartDate"),
                            EndDate = reader.GetDateTime("EndDate"),
                        };
                    }
                }
            }
            return project != null ? Ok(project) : NotFound();
        }

        [HttpPost]
        public IActionResult Post([FromBody] Project project)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                string query = "INSERT INTO Projects (ProjectName, DepartmentID, StartDate, EndDate) VALUES (@ProjectName, @DepartmentID, @StartDate, @EndDate)";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@ProjectName", project.ProjectName);
                command.Parameters.AddWithValue("@DepartmentID", project.DepartmentID);
                command.Parameters.AddWithValue("@StartDate", project.StartDate);
                command.Parameters.AddWithValue("@EndDate", project.EndDate);

                connection.Open();
                try
                {
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected == 1)
                    {
                        return Ok(new { Message = "Project added successfully." });
                    }
                    else
                    {
                        return StatusCode(500, new { Message = "Project could not be added." });
                    }
                }
                catch (Exception ex)
                {
                    return StatusCode(500, new { Message = "An error occurred.", Details = ex.Message });
                }
            }
        }

        [HttpPut("{ProjectID}")]
        public IActionResult Put(int ProjectID, [FromBody] Project project)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                string query = "UPDATE Projects SET ProjectName = @ProjectName, DepartmentID = @DepartmentID, StartDate = @StartDate, EndDate = @EndDate WHERE ProjectID = @ProjectID";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@ProjectID", ProjectID);
                command.Parameters.AddWithValue("@ProjectName", project.ProjectName);
                command.Parameters.AddWithValue("@DepartmentID", project.DepartmentID);
                command.Parameters.AddWithValue("@StartDate", project.StartDate);
                command.Parameters.AddWithValue("@EndDate", project.EndDate);
                
                connection.Open();
                try
                {
                    int rowsAffected = command.ExecuteNonQuery();
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
            }
        }

        [HttpDelete("{ProjectID}")]
        public IActionResult Delete(int ProjectID)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                string query = "DELETE FROM Projects WHERE ProjectID = @ProjectID";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@ProjectID", ProjectID);

                connection.Open();
                try
                {
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        return Ok(new { Message = "Project deleted successfully." });
                    }
                    else
                    {
                        return NotFound(new { Message = "Project not found." });
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
