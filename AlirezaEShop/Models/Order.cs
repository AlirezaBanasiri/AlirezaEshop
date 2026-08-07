using System.ComponentModel.DataAnnotations;

namespace AlirezaEShop.Models
{
    public class Order
    {
        [Key]
        public int OrderID { get; set; }
        [Required]
        public int UserID { get; set; }
        [Required]
        public DateTime CreateDate { get; set; }
        public bool IsFinal { get; set; }
        


        public User user { get; set; }
        public List<OrderDetails> orderDetails { get; set; }    
    }
}
