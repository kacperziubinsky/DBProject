# DBProject

DBProject is a database management system designed to help businesses manage and organize their operations efficiently. Built using **Entity Framework** and **.NET Core**, this project offers an easy way to interact with a relational database, providing essential entities such as Employees, Clients, Projects, Assets, and more.

## Features

The project includes various models to help organize the business's data. These models are mapped to database tables using **Entity Framework**'s ORM capabilities, enabling efficient querying and data manipulation.

### Key Models:

- **Asset**: Represents physical or digital assets owned by the company.
- **Location**: Stores details of the company’s locations or offices.
- **Meeting**: Manages the scheduling and recording of company meetings.
- **Employee**: Holds data about employees, including personal details and work information.
- **Client**: Information about clients the company works with.
- **Department**: Represents different departments within the company.
- **Project**: Contains details of ongoing or past projects.
- **Salary**: Information about employee salaries and payment details.
- **Task**: Task assignments for employees, part of projects or internal operations.

### Database Context:

The database context, **DBProjectContext**, manages interactions between the application and the underlying database. It includes **DbSet<TEntity>** properties for each model, allowing easy access and manipulation of the data.

```csharp
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
