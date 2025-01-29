using Microsoft.EntityFrameworkCore;

namespace DBProject.Models
{
    public class DBProjectContext : DbContext
    {
        public DBProjectContext(DbContextOptions<DBProjectContext> options) : base(options) { }

        public DbSet<Asset> Assets { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Meeting> Meetings { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Salary> Salaries { get; set; }
        public DbSet<Task> Tasks { get; set; }
    }
}