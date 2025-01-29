using DBProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Task = DBProject.Models.Task;


namespace DBProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly DBProjectContext _context;

        public TaskController(DBProjectContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Task>>> GetTasks()
        {
            return await _context.Tasks.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Task>> GetTask(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if(task == null) return NotFound( new {Message = "Task not found"});
            return task;
        }

        [HttpPost]
        public async Task<ActionResult<Task>> PostTask(Task task)
        {
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetTask), new {id = task.TaskID}, task);
        }

        [HttpDelete]
        public async Task<ActionResult<Task>> DeleteTask(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if(task == null) return NotFound(new {Message = "Task not found"});
            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
            return Ok(new {Message = "Task deleted"});
        }
    }
}
