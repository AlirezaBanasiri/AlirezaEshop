

using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Shared;

namespace AlirezaEShop.Models
{
    public class AddEditViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int QuantityInStock { get; set; }
        public IFormFile? Picture { get; set; }
        public string? PictureExtention { get; set; }
        [ValidateNever]
        public List<Category> Categories { get; set; }
    }
}
