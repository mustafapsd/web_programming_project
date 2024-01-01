using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using G221210391_2C.Data;
using G221210391_2C.Models;

namespace G221210391_2C.Views.Department
{
    public class DeleteModel : PageModel
    {
        private readonly G221210391_2C.Data.DepartmentContext _context;

        public DeleteModel(G221210391_2C.Data.DepartmentContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Models.Department Department { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Departments == null)
            {
                return NotFound();
            }

            var department = await _context.Departments.FirstOrDefaultAsync(m => m.DepartmentID == id);

            if (department == null)
            {
                return NotFound();
            }
            else 
            {
                Department = department;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Departments == null)
            {
                return NotFound();
            }
            var department = await _context.Departments.FindAsync(id);

            if (department != null)
            {
                Department = department;
                _context.Departments.Remove(Department);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
