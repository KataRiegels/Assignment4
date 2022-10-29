using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Model
{
    public class OrderDetails
    {
        //[ForeignKey("OrderId")]
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int UnitPrice { get; set; }
        public int Quantity { get; set; }
        public int Discount { get; set; }

        //public IList<Order> Orders { get; } = new List<Order>();
        public Product Product { get; set; }
        public Order Order { get; set; }  
    }
}