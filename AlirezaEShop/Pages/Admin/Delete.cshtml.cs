using AlirezaEShop.Data;
using AlirezaEShop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AlirezaEShop.Pages.Admin
{
    public class DeleteModel : PageModel
    {
        AlirezaEShopContext _context;
        public DeleteModel(AlirezaEShopContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Product Product { get; set; }
        public void OnGet(int id)
        {
            Product = _context.Products.FirstOrDefault(p => p.ID == id);
        }

        public IActionResult OnPost()
        {
            var product = _context.Products.Find(Product.ID);
            var Item = _context.Items.First(p => p.Id == product.itemID);

            _context.Items.Remove(Item);
            _context.Products.Remove(product);
            _context.SaveChanges();

            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", Product.ID + product.PictureExtention);
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }
            return RedirectToPage("Index");
        }
    }
}
