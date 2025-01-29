﻿using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System.Dynamic;

namespace DBProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CompanyController : ControllerBase
    {
        private readonly string _connectionString;

        public CompanyController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("MySqlConnection");
        }

        [HttpGet("EmployeesWithDepartments")]
        public IActionResult GetEmployeesWithDepartments()
        {
            var result = new List<dynamic>();

            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                string query = @"SELECT e.EmployeeID, e.FirstName, e.LastName, d.DepartmentName 
                                 FROM Employees e
                                 JOIN Departments d ON e.DepartmentID = d.DepartmentID";

                MySqlCommand command = new MySqlCommand(query, connection);
                connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        dynamic employee = new ExpandoObject();
                        employee.EmployeeID = reader.GetInt32("EmployeeID");
                        employee.FirstName = reader.GetString("FirstName");
                        employee.LastName = reader.GetString("LastName");
                        employee.DepartmentName = reader.GetString("DepartmentName");

                        result.Add(employee);
                    }
                }
            }
            return Ok(result);
        }

        [HttpGet("ProjectsWithEmployees")]
        public IActionResult GetProjectsWithEmployees()
        {
            var result = new List<dynamic>();

            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                string query = @"SELECT p.ProjectName, e.FirstName, e.LastName 
                                 FROM Projects p
                                 JOIN Tasks t ON p.ProjectID = t.ProjectID
                                 JOIN Employees e ON t.AssignedTo = e.EmployeeID";

                MySqlCommand command = new MySqlCommand(query, connection);
                connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        dynamic project = new ExpandoObject();
                        project.ProjectName = reader.GetString("ProjectName");
                        project.FirstName = reader.GetString("FirstName");
                        project.LastName = reader.GetString("LastName");

                        result.Add(project);
                    }
                }
            }
            return Ok(result);
        }

        [HttpGet("ProfitForCurrentMonth")]
        public IActionResult GetProfitForCurrentMonth()
        {
            dynamic result = new ExpandoObject();

            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                string query = @"
                    SELECT IFNULL(SUM(i.TotalAmount), 0) AS TotalInvoices, IFNULL(SUM(a.Value), 0) AS TotalAssets, IFNULL(SUM(i.TotalAmount), 0) - IFNULL(SUM(a.Value), 0) AS Profit FROM Invoices i LEFT JOIN Assets a ON 1 = 1 WHERE YEAR(i.InvoiceDate) = YEAR(CURRENT_DATE) AND MONTH(i.InvoiceDate) = MONTH(CURRENT_DATE);
";

                MySqlCommand command = new MySqlCommand(query, connection);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        result.TotalInvoices = reader.GetDecimal("TotalInvoices");
                        result.TotalAssets = reader.GetDecimal("TotalAssets");
                        result.Profit = reader.GetDecimal("Profit");
                    }
                }
            }

            return Ok(result);
        }
        [HttpGet("SalariesWithEmployees")]
        public IActionResult GetSalariesWithEmployees()
        {
            var result = new List<dynamic>();

            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                string query = @"SELECT e.FirstName, e.LastName, s.MonthlySalary, s.PaymentDate 
                                 FROM Salaries s
                                 JOIN Employees e ON s.EmployeeID = e.EmployeeID";

                MySqlCommand command = new MySqlCommand(query, connection);
                connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        dynamic salary = new ExpandoObject();
                        salary.FirstName = reader.GetString("FirstName");
                        salary.LastName = reader.GetString("LastName");
                        salary.MonthlySalary = reader.GetDecimal("MonthlySalary");
                        salary.PaymentDate = reader.GetDateTime("PaymentDate");

                        result.Add(salary);
                    }
                }
            }
            return Ok(result);
        }

        [HttpGet("TasksWithProjects")]
        public IActionResult GetTasksWithProjects()
        {
            var result = new List<dynamic>();

            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                string query = @"SELECT t.TaskName, p.ProjectName, e.FirstName, e.LastName 
                                 FROM Tasks t
                                 JOIN Projects p ON t.ProjectID = p.ProjectID
                                 JOIN Employees e ON t.AssignedTo = e.EmployeeID";

                MySqlCommand command = new MySqlCommand(query, connection);
                connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        dynamic task = new ExpandoObject();
                        task.TaskName = reader.GetString("TaskName");
                        task.ProjectName = reader.GetString("ProjectName");
                        task.FirstName = reader.GetString("FirstName");
                        task.LastName = reader.GetString("LastName");

                        result.Add(task);
                    }
                }
            }
            return Ok(result);
        }
    }
}