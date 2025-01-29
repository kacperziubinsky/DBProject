using DBProject.Models;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DBProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SalaryController : ControllerBase
    {
        private readonly DBProjectContext _context;

        public SalaryController(DBProjectContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Salary>>> GetSalaries()
        {
            return await _context.Salaries.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Salary>> GetSalary(int id)
        {
            var salary = await _context.Salaries.FindAsync(id);
            if(salary == null) return NotFound(new { message = "Salary not found" });
            return salary;
        }

        [HttpPost]
        public async Task<ActionResult<Salary>> PostSalary(Salary salary)
        {
            _context.Salaries.Add(salary);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetSalary), new{id = salary.SalaryID}, salary);
        }

        [HttpDelete]
        public async Task<ActionResult<Salary>> DeleteSalary(int id)
        {
            var salary = await _context.Salaries.FindAsync(id);
            if(salary == null) return NotFound(new { message = "Salary not found" });
            _context.Salaries.Remove(salary);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Salary deleted" });
        }
    }
}
