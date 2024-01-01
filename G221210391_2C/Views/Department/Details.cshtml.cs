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
    public class DetailsModel : PageModel
    {
        private readonly G221210391_2C.Data.DepartmentContext _context;

        public DetailsModel(G221210391_2C.Data.DepartmentContext context)
        {
            _context = context;
        }

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
    }
}
