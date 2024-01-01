using G221210391_2C.Data;
using G221210391_2C.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace G221210391_2C.Controllers;

public class AppointmentController : Controller
{
    private readonly AppointmentContext _context;
    private readonly DoctorContext _doctorContext;
    private readonly PatientContext _patientContext;


    public AppointmentController(AppointmentContext context, DoctorContext doctorContext, PatientContext patientContext)
    {
        _context = context;
        _doctorContext = doctorContext;
        _patientContext = patientContext;
    }

    // GET: Appointment
    public async Task<IActionResult> Index()
    {
        var appointments = await _context.Appointments
            .ToListAsync();

        var doctors = await _doctorContext.Doctors.ToListAsync();
        var patients = await _patientContext.Patients.ToListAsync();

        appointments.ForEach(appointment =>
        {
            var doctor = doctors.Find(d => d.DoctorID == appointment.DoctorID);
            appointment.Doctor = doctor;

            var patient = patients.Find(p => p.PatientID == appointment.PatientID);
            appointment.Patient = patient;
        });

        return View(appointments);
    }

    // GET: Appointment/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var appointment = await _context.Appointments
            .Include(a => a.Patient)
            .Include(a => a.Doctor)
            .FirstOrDefaultAsync(m => m.AppointmentID == id);

        if (appointment == null)
        {
            return NotFound();
        }

        return View(appointment);
    }

    // GET: Appointment/Create
    public IActionResult Create()
    {
        ViewBag.Patients = _patientContext.Patients.ToList()
            .ConvertAll(p =>
            {
                p.FirstName =
                    p.FirstName + " " + p.LastName;
                return p;
            });

        ViewBag.Doctors = _doctorContext.Doctors.ToList().ConvertAll(p =>
        {
            p.FirstName =
                p.FirstName + " " + p.LastName;
            return p;
        });
        ;

        Appointment appointment = new Appointment();
        return View(appointment);
    }

    // POST: Appointment/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
        [Bind("AppointmentID,Date,Time,PatientID,DoctorID")]
        Appointment appointment)
    {
        _context.Add(appointment);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    // GET: Appointment/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var appointment = await _context.Appointments.FindAsync(id);

        if (appointment == null)
        {
            return NotFound();
        }

        ViewBag.Patients = _patientContext.Patients.ToList();
        ViewBag.Doctors = _doctorContext.Doctors.ToList();

        return View(appointment);
    }

    // POST: Appointment/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id,
        [Bind("AppointmentID,Date,Time,PatientID,DoctorID")]
        Appointment appointment)
    {
        if (id != appointment.AppointmentID)
        {
            return NotFound();
        }


        try
        {
            _context.Update(appointment);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!AppointmentExists(appointment.AppointmentID))
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

    // GET: Appointment/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var appointment = await _context.Appointments
            .Include(a => a.Patient)
            .Include(a => a.Doctor)
            .FirstOrDefaultAsync(m => m.AppointmentID == id);

        if (appointment == null)
        {
            return NotFound();
        }

        return View(appointment);
    }

    // POST: Appointment/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var appointment = await _context.Appointments.FindAsync(id);
        _context.Appointments.Remove(appointment);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool AppointmentExists(int id)
    {
        return _context.Appointments.Any(e => e.AppointmentID == id);
    }
}