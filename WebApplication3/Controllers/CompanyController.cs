using DBProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApplication3.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CompanyController : ControllerBase
    {
        private readonly DBProjectContext _context;

        public CompanyController(DBProjectContext context)
        {
            _context = context;
        }

        [HttpGet("EmployeesWithDepartments")]
        public IActionResult GetEmployeesWithDepartments()
        {
            var employees = _context.Employees
                .Include(e => e.Department)
                .Select(e => new
                {
                    e.EmployeeID,
                    e.FirstName,
                    e.LastName,
                    e.Department.DepartmentName
                })
                .ToList();

            return Ok(employees);
        }

        [HttpGet("ProjectsWithEmployees")]
        public IActionResult GetProjectsWithEmployees()
        {
            var projects = _context.Projects
                .Select(p => new
                {
                    p.ProjectName,
                    Employees = p.Tasks.Select(t => new
                    {
                        t.AssignedToNavigation.FirstName,
                        t.AssignedToNavigation.LastName
                    })
                })
                .ToList();

            return Ok(projects);
        }

        [HttpGet("ProfitForCurrentMonth")]
        public IActionResult GetProfitForCurrentMonth()
        {
            var totalInvoices = _context.Invoices
                .Where(i => i.InvoiceDate.Year == DateTime.Now.Year &&
                            i.InvoiceDate.Month == DateTime.Now.Month)
                .Sum(i => (decimal?)i.TotalAmount) ?? 0;

            var totalAssets = _context.Assets.Sum(a => (decimal?)a.Value) ?? 0;
            var profit = totalInvoices - totalAssets;

            return Ok(new { TotalInvoices = totalInvoices, TotalAssets = totalAssets, Profit = profit });
        }

        [HttpGet("SalariesWithEmployees")]
        public IActionResult GetSalariesWithEmployees()
        {
            var salaries = _context.Salaries
                .Include(s => s.Employee)
                .Select(s => new
                {
                    s.Employee.FirstName,
                    s.Employee.LastName,
                    s.MonthlySalary,
                    s.PaymentDate
                })
                .ToList();

            return Ok(salaries);
        }

        [HttpGet("TasksWithProjects")]
        public IActionResult GetTasksWithProjects()
        {
            var tasks = _context.Tasks
                .Include(t => t.Project)
                .Include(t => t.AssignedToNavigation)
                .Select(t => new
                {
                    t.TaskName,
                    t.Project.ProjectName,
                    t.AssignedToNavigation.FirstName,
                    t.AssignedToNavigation.LastName
                })
                .ToList();

            return Ok(tasks);
        }
    }
}
