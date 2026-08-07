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
    public class IndexModel : PageModel
    {
        private readonly AlirezaEShop.Data.AlirezaEShopContext _context;

        public IndexModel(AlirezaEShop.Data.AlirezaEShopContext context)
        {
            _context = context;
        }

        public IList<User> User { get;set; } = default!;

        public async Task OnGetAsync()
        {
            User = await _context.Users.ToListAsync();
        }
    }
}
