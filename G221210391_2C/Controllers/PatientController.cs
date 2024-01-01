using G221210391_2C.Data;
using G221210391_2C.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace G221210391_2C.Controllers;

public class PatientController : Controller
{
    private readonly PatientContext _context;

    public PatientController(PatientContext context)
    {
        _context = context;
    }

    // GET: Patient
    public async Task<IActionResult> Index()
    {
        var patients = await _context.Patients.ToListAsync();
        return View(patients);
    }

    // GET: Patient/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var patient = await _context.Patients
            .FirstOrDefaultAsync(m => m.PatientID == id);

        if (patient == null)
        {
            return NotFound();
        }

        return View(patient);
    }

    // GET: Patient/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Patient/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
        [Bind("PatientID,FirstName,LastName,DateOfBirth,Gender,Address,ContactNumber,Email")]
        Patient patient, string? refPage)
    {
        if (PatientValidationIsValid(patient))
        {
            _context.Add(patient);
            await _context.SaveChangesAsync();
            if (refPage == null)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return RedirectToAction("Appointment", "Home");
            }
        }

        return View(patient);
    }

    // GET: Patient/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var patient = await _context.Patients.FindAsync(id);

        if (patient == null)
        {
            return NotFound();
        }

        return View(patient);
    }

    // POST: Patient/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id,
        [Bind("PatientID,FirstName,LastName,DateOfBirth,Gender,Address,ContactNumber,Email")]
        Patient patient)
    {
        if (id != patient.PatientID)
        {
            return NotFound();
        }

        if (PatientValidationIsValid(patient))
        {
            try
            {
                _context.Update(patient);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PatientExists(patient.PatientID))
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

        return View(patient);
    }

    // GET: Patient/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var patient = await _context.Patients
            .FirstOrDefaultAsync(m => m.PatientID == id);

        if (patient == null)
        {
            return NotFound();
        }

        return View(patient);
    }

    // POST: Patient/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var patient = await _context.Patients.FindAsync(id);
        _context.Patients.Remove(patient);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool PatientExists(int id)
    {
        return _context.Patients.Any(e => e.PatientID == id);
    }

    private bool PatientValidationIsValid(Patient patient)
    {
        if (string.IsNullOrWhiteSpace(patient.FirstName) ||
            string.IsNullOrWhiteSpace(patient.LastName) ||
            string.IsNullOrWhiteSpace(patient.ContactNumber) ||
            string.IsNullOrWhiteSpace(patient.Email))
        {
            ModelState.AddModelError(string.Empty, "All fields are required.");
            return false;
        }

        return true;
    }
}