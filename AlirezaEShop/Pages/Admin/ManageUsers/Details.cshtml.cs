using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AlirezaEShop.Data;
using AlirezaEShop.Models;

namespace AlirezaEShop.Pages_Admin_ManageUsers
{
    public class DetailsModel : PageModel
    {
        private readonly AlirezaEShop.Data.AlirezaEShopContext _context;

        public DetailsModel(AlirezaEShop.Data.AlirezaEShopContext context)
        {
            _context = context;
        }

        public User User { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FirstOrDefaultAsync(m => m.userId == id);

            if (user is not null)
            {
                User = user;

                return Page();
            }

            return NotFound();
        }
    }
}
