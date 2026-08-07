namespace AlirezaEShop.Models
{
    public class CategoryToProduct
    {
        public int CategoryID { get; set; }
        public int ProductID { get; set; }

        //Navigation Property
        public Category category { get; set; }
        public Product product { get; set; }
    }
}
