using AlirezaEShop.Data;
using AlirezaEShop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace AlirezaEShop.Pages.Admin
{
    public class IndexModel : PageModel
    {
        private AlirezaEShopContext _context;
        public IndexModel(AlirezaEShopContext context)
        {
            _context = context;
        }
        public IEnumerable<Product> Products { get; set; }

        public void OnGet()
        {
            Products = _context.Products.Include(p=>p.item);
        }
        public void OnPost()
        {

        }
    }
}
