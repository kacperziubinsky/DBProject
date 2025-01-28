using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using DBProject.Models;

namespace DBProject.Controllers;

[ApiController]
[Route("[controller]")]
public class EmployeeController : ControllerBase
{
    private readonly string _connectionString;

    public EmployeeController(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("MySqlConnection");
    }

    [HttpGet]
    public IEnumerable<Employee> Get()
    {
        var employees = new List<Employee>();

        using (MySqlConnection connection = new MySqlConnection(_connectionString))
        {
            string query = "SELECT * FROM Employees";
            MySqlCommand command = new MySqlCommand(query, connection);
            connection.Open();

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    employees.Add(new Employee
                    {
                        EmployeeID = reader.GetInt32("EmployeeID"),
                        FirstName = reader.GetString("FirstName"),
                        LastName = reader.GetString("LastName"),
                        DepartmentID = reader.GetInt32("DepartmentID"),
                        Email = reader.GetString("Email"),
                        Phone = reader.GetString("Phone"),
                        HireDate = reader.GetDateTime("HireDate"),
                    });
                }
            }
        }

        return employees;
    }

    [HttpGet("{EmployeeID}")]
    public IActionResult Get(int EmployeeID)
    {
        Employee employee = null;

        using (MySqlConnection connection = new MySqlConnection(_connectionString))
        {
            string query = "SELECT * FROM Employees WHERE EmployeeID = @EmployeeID";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@EmployeeID", EmployeeID);
            connection.Open();

            using (var reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    employee = new Employee
                    {
                        EmployeeID = reader.GetInt32("EmployeeID"),
                        FirstName = reader.GetString("FirstName"),
                        LastName = reader.GetString("LastName"),
                        DepartmentID = reader.GetInt32("DepartmentID"),
                        Email = reader.GetString("Email"),
                        Phone = reader.GetString("Phone"),
                        HireDate = reader.GetDateTime("HireDate"),
                    };
                }
            }
        }

        if (employee != null)
        {
            return Ok(employee);
        }
        else
        {
            return NotFound(new { Message = "Employee not found." });
        }
    }

    [HttpPost]
    public IActionResult Post(Employee employee)
    {
        using (MySqlConnection connection = new MySqlConnection(_connectionString))
        {
            string query = @"INSERT INTO Employees (FirstName, LastName, DepartmentID, Email, Phone, HireDate) 
                             VALUES (@FirstName, @LastName, @DepartmentID, @Email, @Phone, @HireDate)";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@FirstName", employee.FirstName);
            command.Parameters.AddWithValue("@LastName", employee.LastName);
            command.Parameters.AddWithValue("@DepartmentID", employee.DepartmentID);
            command.Parameters.AddWithValue("@Email", employee.Email);
            command.Parameters.AddWithValue("@Phone", employee.Phone);
            command.Parameters.AddWithValue("@HireDate", employee.HireDate);

            connection.Open();

            try
            {
                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected == 1)
                {
                    return Ok(new { Message = "Employee added successfully." });
                }
                else
                {
                    return StatusCode(500, new { Message = "Employee could not be added." });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred.", Details = ex.Message });
            }
        }
    }

    [HttpPut]
    public IActionResult Put(Employee employee)
    {
        using (MySqlConnection connection = new MySqlConnection(_connectionString))
        {
            string query = @"UPDATE Employees 
                             SET FirstName = @FirstName, 
                                 LastName = @LastName, 
                                 DepartmentID = @DepartmentID, 
                                 Email = @Email, 
                                 Phone = @Phone, 
                                 HireDate = @HireDate 
                             WHERE EmployeeID = @EmployeeID";

            MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@EmployeeID", employee.EmployeeID);
            command.Parameters.AddWithValue("@FirstName", employee.FirstName);
            command.Parameters.AddWithValue("@LastName", employee.LastName);
            command.Parameters.AddWithValue("@DepartmentID", employee.DepartmentID);
            command.Parameters.AddWithValue("@Email", employee.Email);
            command.Parameters.AddWithValue("@Phone", employee.Phone);
            command.Parameters.AddWithValue("@HireDate", employee.HireDate);

            connection.Open();

            try
            {
                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    return Ok(new { Message = "Employee updated successfully." });
                }
                else
                {
                    return NotFound(new { Message = "Employee not found or no changes made." });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred.", Details = ex.Message });
            }
        }
    }

    [HttpDelete("{EmployeeID}")]
    public IActionResult Delete(int EmployeeID)
    {
        using (MySqlConnection connection = new MySqlConnection(_connectionString))
        {
            string query = "DELETE FROM Employees WHERE EmployeeID = @EmployeeID";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@EmployeeID", EmployeeID);
            connection.Open();

            try
            {
                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    return Ok(new { Message = "Employee deleted successfully." });
                }
                else
                {
                    return NotFound(new { Message = "Employee not found." });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred.", Details = ex.Message });
            }
        }
    }
}
