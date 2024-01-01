using G221210391_2C.Models;

namespace G221210391_2C.Data;
using Microsoft.EntityFrameworkCore;

public class DoctorContext : DbContext
{
    public DbSet<Doctor> Doctors { get; set; }

    public DoctorContext(DbContextOptions<DoctorContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configure relationships and constraints here, if needed

        modelBuilder.Entity<Doctor>()
            .HasOne(d => d.Department)
            .WithMany(dept => dept.Doctors)
            .HasForeignKey(d => d.DepartmentID);

        // Add other configurations as needed

        base.OnModelCreating(modelBuilder);
    }
}
