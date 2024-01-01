using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace G221210391_2C.Models;

using System;

public class Doctor
{
    public int DoctorID { get; set; }

    [Required(ErrorMessage = "First Name is required")]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "Last Name is required")]
    public string LastName { get; set; }

    [Required(ErrorMessage = "Specialization is required")]
    public string Specialization { get; set; }

    [Required(ErrorMessage = "Contact Number is required")]
    public string ContactNumber { get; set; }

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid Email Address")]
    public string Email { get; set; }
    
    // Foreign key for HospitalDepartment
    [ForeignKey("Department")]
    public int DepartmentID { get; set; }

    // Other relevant properties can be added as needed

    // Navigation properties
    public Department Department { get; set; }
    
    // Navigation property for appointments
    public ICollection<Appointment> Appointments { get; set; }
}
