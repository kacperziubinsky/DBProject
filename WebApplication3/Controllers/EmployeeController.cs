using Microsoft.AspNetCore.Mvc;
using DBProject.Models;
using Microsoft.EntityFrameworkCore;

namespace DBProject.Controllers;

[ApiController]
[Route("[controller]")]
public class EmployeeController : ControllerBase
{
   private readonly DBProjectContext _context;

   public EmployeeController(DBProjectContext context)
   {
       _context = context;
   }

   [HttpGet]
   public async Task<ActionResult<IEnumerable<Employee>>> GetMeetings()
   {
       return await _context.Employees.ToListAsync();
   }

   [HttpGet("{EmployeeID}")]
   public async Task<ActionResult<Employee>> GetEmployee(int employeeid)
   {
       var employee = await _context.Employees.FindAsync(employeeid);
       if (employee == null) return NotFound(new { Message = "Employee not found." });
       return employee;
   }

   [HttpPost]
   public async Task<ActionResult<Employee>> PostEmployee(Employee employee)
   {
       _context.Employees.Add(employee);
       await _context.SaveChangesAsync();
       return CreatedAtAction(nameof(GetEmployee), new { id = employee.EmployeeID }, employee);
   }

    

    [HttpDelete("{EmployeeID}")]
    public async Task<ActionResult<Employee>> DeleteEmployee(int employeeid){
        var employee = await _context.Employees.FindAsync(employeeid);
        if (employee == null) return NotFound(new { Message = "Employee not found." });
        
        _context.Employees.Remove(employee);
        await _context.SaveChangesAsync();
        return Ok(new {Message = "Employee deleted."});
    }
}
