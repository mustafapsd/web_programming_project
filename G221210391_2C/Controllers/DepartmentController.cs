using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using G221210391_2C.Data;
using G221210391_2C.Models;

public class DepartmentController : Controller
{
    private readonly DepartmentContext _context;

    public DepartmentController(DepartmentContext context)
    {
        _context = context;
    }

    // GET: Department
    public async Task<IActionResult> Index()
    {
        var departments = await _context.Departments.ToListAsync();
        return View(departments);
    }

    // GET: Department/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var department = await _context.Departments
            .FirstOrDefaultAsync(m => m.DepartmentID == id);

        if (department == null)
        {
            return NotFound();
        }

        return View(department);
    }

    // POST: Department/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create([Bind("DepartmentID,DepartmentName,Description")] Department department)
    {
        if (department.DepartmentName != "" && department.Description != "")
        {
            _context.Add(department);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        return View(department);
    }

    // GET: Department/Create
    public IActionResult Create()
    {
        Department department = new Department();
        return View(department);
    }

    // GET: Department/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var department = await _context.Departments.FindAsync(id);

        if (department == null)
        {
            return NotFound();
        }

        return View(department);
    }

    // POST: Department/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id,
        [Bind("DepartmentID,DepartmentName,Description")]
        Department department)
    {
        if (id != department.DepartmentID)
        {
            return NotFound();
        }

        if (department.DepartmentName != "" && department.Description != "")
        {
            try
            {
                _context.Update(department);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DepartmentExists(department.DepartmentID))
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

        return View(department);
    }

    // GET: Department/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var department = await _context.Departments
            .FirstOrDefaultAsync(m => m.DepartmentID == id);

        if (department == null)
        {
            return NotFound();
        }

        return View(department);
    }

    // POST: Department/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var department = await _context.Departments.FindAsync(id);

        _context.Departments.Remove(department);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    private bool DepartmentExists(int id)
    {
        return _context.Departments.Any(e => e.DepartmentID == id);
    }
}