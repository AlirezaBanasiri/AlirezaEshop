using AlirezaEShop.Data;
using AlirezaEShop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.RegularExpressions;
using static System.Net.Mime.MediaTypeNames;

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

        [BindProperty]
        public List<int> SelectedGroups { get; set; }

        public void OnGet()
        {
            Product = new AddEditViewModel
            {
                Categories = getCategory()
            };
        }
        public IActionResult OnPost()
        {
            if (Product.Picture == null || Product.Picture.Length == 0)
            {
                ModelState.AddModelError("Product.Picture", "انتخاب تصویر الزامی است.");
            }
            if (!ModelState.IsValid)
            {
                //foreach (var i in ModelState)
                //{
                //    foreach (var error in i.Value.Errors)
                //    {
                //        Console.WriteLine($"{i.Key}: {error.ErrorMessage}");
                //    }
                //}
                Product = new AddEditViewModel
                {
                    Categories = getCategory()
                };
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
                PictureExtention = Product.Picture != null
              ? Path.GetExtension(Product.Picture.FileName)
                : null
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

            foreach (int gr in SelectedGroups)
            {
                if (SelectedGroups.Any() && SelectedGroups.Count > 0)
                {
                    _context.CategoryToProducts.Add(new CategoryToProduct
                    {
                        CategoryID = gr,
                        ProductID = pro.ID
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
    }
}
