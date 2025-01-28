namespace DBProject.Models;

public class Meeting
{
    public int MeetingID { get; set; }
    public DateTime MeetingDate { get; set; }
    public string Subject { get; set; }
    public string Percipants { get; set; }
    public string Participants { get; set; }
}