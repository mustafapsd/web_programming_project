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
    public class IndexModel : PageModel
    {
        private readonly G221210391_2C.Data.DepartmentContext _context;

        public IndexModel(G221210391_2C.Data.DepartmentContext context)
        {
            _context = context;
        }

        public IList<Models.Department> Department { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Departments != null)
            {
                Department = await _context.Departments.ToListAsync();
            }
        }
    }
}
