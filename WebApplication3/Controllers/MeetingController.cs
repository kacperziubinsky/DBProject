using DBProject.Models;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("[controller]")]
public class MeetingController : ControllerBase
{
    private readonly DBProjectContext _context;

    public MeetingController(DBProjectContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Meeting>>> GetMeetings()
    {
        return await _context.Meetings.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Meeting>> GetMeeting(int id)
    {
        var meeting = await _context.Meetings.FindAsync(id);
        if(meeting == null) { return NotFound( new{message = "Meeting not found"} ); }
        return meeting;
    }

    [HttpPost]
    public async Task<ActionResult<Meeting>> PostMeeting(Meeting meeting)
    {
        _context.Meetings.Add(meeting);
        await _context.SaveChangesAsync();
        return meeting;
    }

    [HttpDelete]
    public async Task<ActionResult<Meeting>> DeleteMeeting(int id)
    {
        var meeting = await _context.Meetings.FindAsync(id);
        if(meeting == null) { return NotFound(new{message = "Meeting not found"} ); }
        _context.Meetings.Remove(meeting);
        await _context.SaveChangesAsync();
        return Ok(new{message = "Meeting deleted"});
    }
}
