namespace G221210391_2C.Models;
using System;
using System.ComponentModel.DataAnnotations;

public class Appointment
{
    public int AppointmentID { get; set; }

    [Required(ErrorMessage = "Patient is required")]
    public int PatientID { get; set; }

    [Required(ErrorMessage = "Doctor is required")]
    public int DoctorID { get; set; }

    [Required(ErrorMessage = "Appointment Date and Time are required")]
    [DataType(DataType.DateTime)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-ddTHH:mm}", ApplyFormatInEditMode = true)]
    public DateTime AppointmentDateTime { get; set; }

    [Required(ErrorMessage = "Appointment Status is required")]
    public AppointmentStatus Status { get; set; }

    public string Reason { get; set; }

    // Navigation properties
    public Patient Patient { get; set; }

    public Doctor Doctor { get; set; }
}

public enum AppointmentStatus
{
    Pending,
    Confirmed,
    Canceled
}
