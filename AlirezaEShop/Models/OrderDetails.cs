using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AlirezaEShop.Models
{
    public class OrderDetails
    {
        [Key]
        public int DeatilID { get; set; }
        [Required]
        public int OrderID { get; set; }
        [Required]
        public int ProductID { get; set; }
        [Required]
        public Decimal Price { get; set; }
        [Required]
        public int Count { get; set; }

        public Order order { get; set; }
        [ForeignKey("ProductID")]
        public Product product { get; set; }
    }
}
