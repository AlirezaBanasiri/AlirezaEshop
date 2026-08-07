namespace AlirezaEShop.Models
{
    public class Product
    {


        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int itemID { get; set; }
        public string PictureExtention { get; set; }

        public Item item { get; set; }
        public ICollection<CategoryToProduct> CategoryToProduct { get; set; }
        public List<OrderDetails> orderDetails { get; set; }

    }
}
