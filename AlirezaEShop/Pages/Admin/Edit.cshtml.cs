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
        [BindProperty]
        public List<int> SelectedGroups { get; set; }
        public List<int> ProducGroups { get; set; }

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
            Product.Categories = getCategory();
            ProducGroups = getProductGroup(id);
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                foreach (var i in ModelState)
                {
                    foreach (var error in i.Value.Errors)
                    {
                        Console.WriteLine($"{i.Key}: {error.ErrorMessage}");
                    }
                }
                Product.Categories = getCategory();
                ProducGroups = getProductGroup(Product.ID);
                return Page();

            }

            var product = _context.Products.Find(Product.ID);
            var Item = _context.Items.First(p => p.Id == product.itemID);

            product.Name = Product.Name;
            product.Description = Product.Description;
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
                product.PictureExtention = Product.Picture != null
              ? Path.GetExtension(Product.Picture.FileName)
                : null;
            }

            _context.CategoryToProducts.Where(c => c.ProductID == product.ID).ToList().ForEach(g => _context.CategoryToProducts.Remove(g));
            foreach (int gr in SelectedGroups)
            {
                if (SelectedGroups.Any() && SelectedGroups.Count > 0)
                {
                    _context.CategoryToProducts.Add(new CategoryToProduct
                    {
                        CategoryID = gr,
                        ProductID = product.ID
                    });
                }
                _context.SaveChanges();
            }

            return RedirectToPage("Index");
        }

        public List<Category> getCategory()
        {
            return _context.categories.ToList();
        }
        public List<int> getProductGroup(int id)
        {
            return _context.CategoryToProducts.Where(c => c.ProductID == id).Select(c => c.CategoryID).ToList();
        }
    }
}