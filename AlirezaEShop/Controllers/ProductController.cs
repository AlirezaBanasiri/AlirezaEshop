using Microsoft.AspNetCore.Mvc;
using AlirezaEShop.Data;
using Microsoft.EntityFrameworkCore;
namespace AlirezaEShop.Controllers
{
    public class ProductController : Controller
    {
        AlirezaEShopContext _context;
        public ProductController(AlirezaEShopContext context)
        {
            _context = context;
        }

        [Route("Group/{id}/{name}")]
        public IActionResult ShowProductByGroup(int id,string name)
        {
            ViewData["GroupName"] = name;

            var products=_context.CategoryToProducts
                .Where(c=>c.CategoryID==id)
                .Include(c=>c.product)
                .Select(c=>c.product)
                .ToList();  

            return View(products);
        }
    }
}
