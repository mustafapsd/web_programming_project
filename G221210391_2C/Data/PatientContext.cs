using G221210391_2C.Models;

namespace G221210391_2C.Data;
using Microsoft.EntityFrameworkCore;

public class PatientContext : DbContext
{
    public DbSet<Patient> Patients { get; set; }

    public PatientContext(DbContextOptions<PatientContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configure relationships and constraints here, if needed

        modelBuilder.Entity<Patient>()
            .HasMany(p => p.Appointments)
            .WithOne(a => a.Patient)
            .HasForeignKey(a => a.PatientID);

        // Add other configurations as needed

        base.OnModelCreating(modelBuilder);
    }
}
