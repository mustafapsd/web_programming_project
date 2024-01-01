using G221210391_2C.Data;
using G221210391_2C.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace G221210391_2C.Controllers;

public class DoctorController : Controller
{
    private readonly DoctorContext _context;
    private readonly DepartmentContext _departmentContext;

    public DoctorController(DoctorContext context, DepartmentContext departmentContext)
    {
        _context = context;
        _departmentContext = departmentContext;
    }

    // GET: Doctor
    public async Task<IActionResult> Index()
    {
        var doctors = await _context.Doctors.ToListAsync();
        return View(doctors);
    }

    // GET: Doctor/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var doctor = await _context.Doctors
            .Include(d => d.Department)
            .FirstOrDefaultAsync(m => m.DoctorID == id);

        if (doctor == null)
        {
            return NotFound();
        }

        return View(doctor);
    }

    // GET: Doctor/Create
    public IActionResult Create()
    {
        Doctor doctor = new Doctor();
        List<Department> departments = _departmentContext.Departments.ToList();

        ViewBag.Departments = departments;
        return View(doctor);
    }

    // GET: Doctor/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var doctor = await _context.Doctors.FindAsync(id);

        if (doctor == null)
        {
            return NotFound();
        }

        List<Department> departments = _departmentContext.Departments.ToList();

        ViewBag.Departments = departments;
        return View(doctor);
    }

    // POST: Doctor/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
        [Bind("DoctorID,FirstName,LastName,Specialization,ContactNumber,Email,DepartmentID")]
        Doctor doctor)
    {
        if (DoctorValidationIsValid(doctor))
        {
            _context.Add(doctor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        ViewBag.Departments = new SelectList(_departmentContext.Departments,
            "DepartmentID",
            "DepartmentName", doctor.DepartmentID);
        return View(doctor);
    }

// POST: Doctor/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id,
        [Bind("DoctorID,FirstName,LastName,Specialization,ContactNumber,Email,DepartmentID")]
        Doctor doctor)
    {
        if (id != doctor.DoctorID)
        {
            return NotFound();
        }

        if (DoctorValidationIsValid(doctor))
        {
            try
            {
                _context.Update(doctor);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DoctorExists(doctor.DoctorID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToAction(nameof(Index));
        }

        ViewBag.Departments = new SelectList(_departmentContext.Departments,
            "DepartmentID",
            "DepartmentName", doctor.DepartmentID);
        return View(doctor);
    }

    private bool DoctorValidationIsValid(Doctor doctor)
    {
        if (string.IsNullOrWhiteSpace(doctor.FirstName) ||
            string.IsNullOrWhiteSpace(doctor.LastName) ||
            string.IsNullOrWhiteSpace(doctor.Specialization) ||
            string.IsNullOrWhiteSpace(doctor.ContactNumber) ||
            string.IsNullOrWhiteSpace(doctor.Email))
        {
            ModelState.AddModelError(string.Empty, "All fields are required.");
            return false;
        }

        return true;
    }


    // GET: Doctor/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var doctor = await _context.Doctors
            .Include(d => d.Department)
            .FirstOrDefaultAsync(m => m.DoctorID == id);

        if (doctor == null)
        {
            return NotFound();
        }

        return View(doctor);
    }

    // POST: Doctor/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var doctor = await _context.Doctors.FindAsync(id);

        _context.Doctors.Remove(doctor);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    private bool DoctorExists(int id)
    {
        return _context.Doctors.Any(e => e.DoctorID == id);
    }
}