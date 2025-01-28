using DBProject.Models;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace DBProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SalaryController : ControllerBase
    {
        private readonly string _connectionString;

        public SalaryController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("MySqlConnection");
        }

        [HttpGet]
        public IEnumerable<Salary> Get()
        {
            var salaries = new List<Salary>();
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Salaries";
                MySqlCommand command = new MySqlCommand(query, connection);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        salaries.Add(new Salary
                        {
                            SalaryID = reader.GetInt32("SalaryID"),
                            EmployeeID = reader.GetInt32("EmployeeID"),
                            MonthlySalary = reader.GetDecimal("MonthlySalary"),
                            PaymentDate = reader.GetDateTime("PaymentDate")
                        });
                    }
                }
            }
            return salaries;
        }

        [HttpGet("{SalaryID}")]
        public IActionResult Get(int SalaryID)
        {
            Salary salary = null;
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Salaries WHERE SalaryID = @SalaryID";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@SalaryID", SalaryID);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        salary = new Salary
                        {
                            SalaryID = reader.GetInt32("SalaryID"),
                            EmployeeID = reader.GetInt32("EmployeeID"),
                            MonthlySalary = reader.GetDecimal("MonthlySalary"),
                            PaymentDate = reader.GetDateTime("PaymentDate")
                        };
                    }
                }
            }
            return salary != null ? Ok(salary) : NotFound();
        }

        [HttpPost]
        public IActionResult Post([FromBody] Salary salary)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                string query = "INSERT INTO Salaries (EmployeeID, MonthlySalary, PaymentDate) VALUES (@EmployeeID, @MonthlySalary, @PaymentDate)";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@EmployeeID", salary.EmployeeID);
                command.Parameters.AddWithValue("@MonthlySalary", salary.MonthlySalary);
                command.Parameters.AddWithValue("@PaymentDate", salary.PaymentDate);

                connection.Open();
                try
                {
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected == 1)
                    {
                        return Ok(new { Message = "Salary added successfully." });
                    }
                    else
                    {
                        return StatusCode(500, new { Message = "Salary could not be added." });
                    }
                }
                catch (Exception ex)
                {
                    return StatusCode(500, new { Message = "An error occurred.", Details = ex.Message });
                }
            }
        }

        [HttpPut("{SalaryID}")]
        public IActionResult Put(int SalaryID, [FromBody] Salary salary)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                string query = "UPDATE Salaries SET EmployeeID = @EmployeeID, MonthlySalary = @MonthlySalary, PaymentDate = @PaymentDate WHERE SalaryID = @SalaryID";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@SalaryID", SalaryID);
                command.Parameters.AddWithValue("@EmployeeID", salary.EmployeeID);
                command.Parameters.AddWithValue("@MonthlySalary", salary.MonthlySalary);
                command.Parameters.AddWithValue("@PaymentDate", salary.PaymentDate);

                connection.Open();
                try
                {
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        return Ok(new { Message = "Salary updated successfully." });
                    }
                    else
                    {
                        return NotFound(new { Message = "Salary not found or no changes made." });
                    }
                }
                catch (Exception ex)
                {
                    return StatusCode(500, new { Message = "An error occurred.", Details = ex.Message });
                }
            }
        }

        [HttpDelete("{SalaryID}")]
        public IActionResult Delete(int SalaryID)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                string query = "DELETE FROM Salaries WHERE SalaryID = @SalaryID";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@SalaryID", SalaryID);

                connection.Open();
                try
                {
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        return Ok(new { Message = "Salary deleted successfully." });
                    }
                    else
                    {
                        return NotFound(new { Message = "Salary not found." });
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
