

namespace DataLayer.Model
{
    public class Product
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int? SupplierId { get; set; }
        public int? CategoryId { get; set; }  
        public string? QuantityPerUnit { get; set; }
        public double UnitPrice { get; set; } 
        public int UnitsInStock { get; set; }



        public Category? Category { get; set; }

        public IList<OrderDetails> Orderdetails { get; set; } = new List<OrderDetails>();
    }
}