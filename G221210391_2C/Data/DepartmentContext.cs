using G221210391_2C.Models;

namespace G221210391_2C.Data;
using Microsoft.EntityFrameworkCore;

public class DepartmentContext : DbContext
{
    public DbSet<Department> Departments { get; set; }

    public DepartmentContext(DbContextOptions<DepartmentContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}
