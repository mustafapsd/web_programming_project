namespace G221210391_2C.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class Patient
{
    public int PatientID { get; set; }

    [Required(ErrorMessage = "First Name is required")]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "Last Name is required")]
    public string LastName { get; set; }

    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    [Required(ErrorMessage = "Date of Birth is required")]
    public DateTime DateOfBirth { get; set; }

    public Gender Gender { get; set; }

    [Required(ErrorMessage = "Contact Number is required")]
    public string ContactNumber { get; set; }

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid Email Address")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Address is required")]
    public string Address { get; set; }

    // Navigation property for appointments
    public ICollection<Appointment> Appointments { get; set; }
}

public enum Gender
{
    Male,
    Female,
    Other
}
