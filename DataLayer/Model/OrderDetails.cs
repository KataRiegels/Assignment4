namespace DataLayer.Model
{
    public class OrderDetails
    {
        public Order OrderID { get; set; }
        public Product ProductId { get; set; }
        public int unitprice { get; set; }
        public int quantity { get; set; }
        public int discount { get; set; }

    }
}