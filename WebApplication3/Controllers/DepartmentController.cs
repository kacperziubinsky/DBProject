using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using DBProject.Models;

namespace DBProject.Controllers;

[ApiController]
[Route("[controller]")]
public class DepartmentController : ControllerBase
{
    private readonly string _connectionString;

    public DepartmentController(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("MySqlConnection");
    }

    [HttpGet]
    public IEnumerable<Department> Get()
    {
        var departments = new List<Department>();

        using (MySqlConnection connection = new MySqlConnection(_connectionString))
        {
            string query = "SELECT * FROM Departments";
            MySqlCommand command = new MySqlCommand(query, connection);
            connection.Open();

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    departments.Add(new Department
                    {
                        DepartmentID = reader.GetInt32("DepartmentID"),
                        DepartmentName = reader.IsDBNull(reader.GetOrdinal("DepartmentName")) ? null : reader.GetString("DepartmentName"),
                        ManagerID = reader.GetInt32("ManagerID"),
                    });
                }
            }
        }
        return departments;
    }

    [HttpGet("{DepartmentID}")]
    public IActionResult Get(int DepartmentID)
    {
        Department department = null;

        using (MySqlConnection connection = new MySqlConnection(_connectionString))
        {
            string query = "SELECT * FROM Departments WHERE DepartmentID = @DepartmentID";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@DepartmentID", DepartmentID);
            connection.Open();

            using (var reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    department = new Department
                    {
                        DepartmentID = reader.GetInt32("DepartmentID"),
                        DepartmentName = reader.IsDBNull(reader.GetOrdinal("DepartmentName")) ? null : reader.GetString("DepartmentName"),
                        ManagerID = reader.GetInt32("ManagerID"),
                    };
                }
            }
        }

        if (department != null)
        {
            return Ok(department);
        }
        else
        {
            return NotFound(new { Message = "Department not found." });
        }
    }

    [HttpPost]
    public IActionResult Post(Department department)
    {
        using (MySqlConnection connection = new MySqlConnection(_connectionString))
        {
            string query = @"INSERT INTO Departments (DepartmentName, ManagerID) 
                             VALUES (@DepartmentName, @ManagerID)";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@DepartmentName", department.DepartmentName);
            command.Parameters.AddWithValue("@ManagerID", department.ManagerID);

            connection.Open();

            try
            {
                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected == 1)
                {
                    return Ok(new { Message = "Department added successfully." });
                }
                else
                {
                    return StatusCode(500, new { Message = "Department could not be added." });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred.", Details = ex.Message });
            }
        }
    }

    [HttpPut]
    public IActionResult Put(Department department)
    {
        using (MySqlConnection connection = new MySqlConnection(_connectionString))
        {
            string query = @"UPDATE Departments 
                             SET DepartmentName = @DepartmentName, 
                                 ManagerID = @ManagerID 
                             WHERE DepartmentID = @DepartmentID";

            MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@DepartmentID", department.DepartmentID);
            command.Parameters.AddWithValue("@DepartmentName", department.DepartmentName);
            command.Parameters.AddWithValue("@ManagerID", department.ManagerID);

            connection.Open();

            try
            {
                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    return Ok(new { Message = "Department updated successfully." });
                }
                else
                {
                    return NotFound(new { Message = "Department not found or no changes made." });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred.", Details = ex.Message });
            }
        }
    }

    [HttpDelete("{DepartmentID}")]
    public IActionResult Delete(int DepartmentID)
    {
        using (MySqlConnection connection = new MySqlConnection(_connectionString))
        {
            string query = "DELETE FROM Departments WHERE DepartmentID = @DepartmentID";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@DepartmentID", DepartmentID);
            connection.Open();

            try
            {
                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    return Ok(new { Message = "Department deleted successfully." });
                }
                else
                {
                    return NotFound(new { Message = "Department not found." });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred.", Details = ex.Message });
            }
        }
    }
}
