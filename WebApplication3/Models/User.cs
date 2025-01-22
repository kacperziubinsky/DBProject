namespace WebApplication3.Models
{
    public class User
    {
        public string userID { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string Position { get; set; }
        public string Email { get; set; }
        public DateTime hireDate { get; set; } 
        public decimal Salary { get; set; }
    }
}