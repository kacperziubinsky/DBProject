using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using DBProject.Models;
using Microsoft.EntityFrameworkCore;

namespace DBProject.Controllers;

[ApiController]
[Route("[controller]")]
public class DepartmentController : ControllerBase
{
    private readonly DBProjectContext _context;

    public DepartmentController(DBProjectContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Department>>> GetDepartments()
    {
        return await _context.Departments.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Department>> GetDepartment(int id)
    {
        var department = await _context.Departments.FindAsync(id);
        if(department == null) { return NotFound( new {message = "Department not found"}); }
        return department;
    }

    [HttpPost]
    public async Task<ActionResult<Department>> PostDepartment(Department department)
    {
        _context.Departments.Add(department);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetDepartment), new {id = department.DepartmentID}, department);
    }

    [HttpDelete]
    public async Task<ActionResult<Department>> DeleteDepartment(int id)
    {
        var department = await _context.Departments.FindAsync(id);
        if(department == null) { return NotFound(new {message = "Department not found"}); }
        _context.Departments.Remove(department);
        await _context.SaveChangesAsync();
        return Ok(new {message = "Department deleted"});
    }
    
}
