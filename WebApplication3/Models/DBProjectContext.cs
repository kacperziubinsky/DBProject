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
        public DbSet<Invoice> Invoices { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Task>()
                .HasOne(t => t.Project)
                .WithMany(p => p.Tasks)
                .HasForeignKey(t => t.ProjectID);
            
            modelBuilder.Entity<Task>()
                .HasOne(t => t.AssignedToNavigation)
                .WithMany(t => t.Tasks)
                .HasForeignKey(t => t.AssignedTo);
            
            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Salary)
                .WithOne(s => s.Employee)
                .HasForeignKey<Salary>(s => s.EmployeeID);
             
        }
    }
}