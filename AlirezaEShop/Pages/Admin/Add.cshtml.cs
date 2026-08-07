using AlirezaEShop.Data;
using AlirezaEShop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AlirezaEShop.Pages.Admin
{
    public class AddModel : PageModel
    {
        AlirezaEShopContext _context;
        public AddModel(AlirezaEShopContext context)
        {
            _context = context;
        }

        [BindProperty]
        public AddEditViewModel Product { get; set; }
        public void OnGet()
        {

        }
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var item = new Item()
            {
                price = Product.Price,
                quantityInStock = Product.QuantityInStock,
            };
            _context.Add(item);
            _context.SaveChanges();

            var pro = new Product()
            {
                Name = Product.Name,
                item = item,
                Description = Product.Description,
                PictureExtention = Path.GetExtension(Product.Picture.FileName)
            };
            _context.Add(pro);
            _context.SaveChanges();

            pro.itemID = item.Id;
            _context.SaveChanges();

            if (Product.Picture?.Length > 0)
            {
                string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", pro.ID + pro.PictureExtention);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    Product.Picture.CopyTo(stream);
                }
            }

            return RedirectToPage("Index");
        }
    }
}
