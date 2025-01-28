using DBProject.Models;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

[ApiController]
[Route("[controller]")]
public class MeetingController : ControllerBase
{
    private readonly string _connectionString;

    public MeetingController(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("MySqlConnection");
    }

    [HttpGet]
    public IEnumerable<Meeting> Get()
    {
        var meetings = new List<Meeting>();
        using (MySqlConnection connection = new MySqlConnection(_connectionString))
        {
            string query = "SELECT * FROM Meetings";
            MySqlCommand command = new MySqlCommand(query, connection);
            connection.Open();
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    meetings.Add(new Meeting
                    {
                        MeetingID = reader.GetInt32("MeetingID"),
                        MeetingDate = reader.GetDateTime("MeetingDate"),
                        Subject = reader.GetString("Subject"),
                        Participants = reader.GetString("Participants")
                    });
                }
            }
        }
        return meetings;
    }

    [HttpGet("{MeetingID}")]
    public IActionResult Get(int MeetingID)
    {
        Meeting meeting = null;
        using (MySqlConnection connection = new MySqlConnection(_connectionString))
        {
            string query = "SELECT * FROM Meetings WHERE MeetingID = @MeetingID";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@MeetingID", MeetingID);
            connection.Open();
            using (var reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    meeting = new Meeting
                    {
                        MeetingID = reader.GetInt32("MeetingID"),
                        MeetingDate = reader.GetDateTime("MeetingDate"),
                        Subject = reader.GetString("Subject"),
                        Participants = reader.GetString("Participants")
                    };
                }
            }
        }
        return meeting != null ? Ok(meeting) : NotFound();
    }

    [HttpPost]
    public IActionResult Post([FromBody] Meeting meeting)
    {
        using (MySqlConnection connection = new MySqlConnection(_connectionString))
        {
            string query = "INSERT INTO Meetings (MeetingDate, Subject, Participants) VALUES (@MeetingDate, @Subject, @Participants)";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@MeetingDate", meeting.MeetingDate);
            command.Parameters.AddWithValue("@Subject", meeting.Subject);
            command.Parameters.AddWithValue("@Participants", meeting.Participants);

            connection.Open();
            try
            {
                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected == 1)
                {
                    return Ok(new { Message = "Meeting added successfully." });
                }
                else
                {
                    return StatusCode(500, new { Message = "Meeting could not be added." });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred.", Details = ex.Message });
            }
        }
    }

    [HttpPut("{MeetingID}")]
    public IActionResult Put(int MeetingID, [FromBody] Meeting meeting)
    {
        using (MySqlConnection connection = new MySqlConnection(_connectionString))
        {
            string query = "UPDATE Meetings SET MeetingDate = @MeetingDate, Subject = @Subject, Participants = @Participants WHERE MeetingID = @MeetingID";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@MeetingID", MeetingID);
            command.Parameters.AddWithValue("@MeetingDate", meeting.MeetingDate);
            command.Parameters.AddWithValue("@Subject", meeting.Subject);
            command.Parameters.AddWithValue("@Participants", meeting.Participants);

            connection.Open();
            try
            {
                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    return Ok(new { Message = "Meeting updated successfully." });
                }
                else
                {
                    return NotFound(new { Message = "Meeting not found or no changes made." });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred.", Details = ex.Message });
            }
        }
    }

    [HttpDelete("{MeetingID}")]
    public IActionResult Delete(int MeetingID)
    {
        using (MySqlConnection connection = new MySqlConnection(_connectionString))
        {
            string query = "DELETE FROM Meetings WHERE MeetingID = @MeetingID";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@MeetingID", MeetingID);

            connection.Open();
            try
            {
                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    return Ok(new { Message = "Meeting deleted successfully." });
                }
                else
                {
                    return NotFound(new { Message = "Meeting not found." });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred.", Details = ex.Message });
            }
        }
    }
}
