namespace G221210391_2C.Models;

using System.ComponentModel.DataAnnotations;

public class Department
{
    public int DepartmentID { get; set; }

    [Required(ErrorMessage = "Department Name is required")]
    public string DepartmentName { get; set; } = "";

    [Required(ErrorMessage = "Description is required")]
    public string Description { get; set; } = "";

    public ICollection<Doctor> Doctors { get; set; }
}