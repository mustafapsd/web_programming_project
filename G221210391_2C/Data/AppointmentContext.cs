using G221210391_2C.Models;

namespace G221210391_2C.Data;
using Microsoft.EntityFrameworkCore;

public class AppointmentContext : DbContext
{
    public DbSet<Appointment> Appointments { get; set; }

    public AppointmentContext(DbContextOptions<AppointmentContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Appointment>()
            .HasOne(a => a.Patient)
            .WithMany(p => p.Appointments)
            .HasForeignKey(a => a.PatientID);

        modelBuilder.Entity<Appointment>()
            .HasOne(a => a.Doctor)
            .WithMany(d => d.Appointments)
            .HasForeignKey(a => a.DoctorID);

        base.OnModelCreating(modelBuilder);
    }
}
