using AlirezaEShop.Data;
using AlirezaEShop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace AlirezaEShop.Pages.Admin
{
    public class EditModel : PageModel
    {
        AlirezaEShopContext _context;
        public EditModel(AlirezaEShopContext context)
        {
            _context = context;
        }
        [BindProperty]
        public AddEditViewModel Product { get; set; }
        public void OnGet(int id)
        {
            Product = _context.Products.Include(p => p.item).
                Where(p => p.ID == id)
                .Select(s => new AddEditViewModel()
                {
                    ID = s.ID,
                    Name = s.Name,
                    Description = s.Description,
                    QuantityInStock = s.item.quantityInStock,
                    Price = s.item.price
                }).FirstOrDefault();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            var product = _context.Products.Find(Product.ID);
            var Item = _context.Items.First(p=>p.Id==product.itemID);

            product.Name= Product.Name;
            product.Description= Product.Description;   
            Item.quantityInStock = Product.QuantityInStock; 
            Item.price = Product.Price;
            _context.SaveChanges(); 

            if (Product.Picture?.Length > 0)
            {
                string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", Product.ID + Path.GetExtension(Product.Picture.FileName));
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    Product.Picture.CopyTo(stream);
                }
            }

            return RedirectToPage("Index");
        }
    }
}