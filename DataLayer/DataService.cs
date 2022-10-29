using Microsoft.EntityFrameworkCore;
using DataLayer.Model;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Numerics;

namespace DataLayer
{
    public class DataService
    {
        private static NorthwindContext _db = new NorthwindContext();
        public IList<Category> GetCategories() 
        {
            return _db.Categories.ToList();
        }

        public Category GetCategory(int id)
        {
            return _db.Categories.FirstOrDefault(x => x.Id == id);
 
        }

        public Category CreateCategory(string name, string description)
        {
            Category category = new Category();
            var maxId = _db.Categories.Max(x => x.Id);
            category.Id = maxId + 1;
            category.Name = name;
            category.Description = description;
            _db.Categories.Add(category);
            _db.SaveChanges();  
        
            return category;
        }

        public bool DeleteCategory(int id)
        {
            var category = GetCategory(id);
            if (category == null)
            {
                return false;
            }
            _db.Categories.Remove(GetCategory(id));
            _db.SaveChanges();
            return true;
        }

        public bool UpdateCategory(int id, string name, string description)
        {
            var category = GetCategory(id);
            if (category == null)
            {
                return false;
            }
            category.Name = name;
            category.Description = description;
            _db.SaveChanges();

            return true;
        }

        /* PRODUCTS */

        public IList<Product> GetProducts()
        {
            return _db.Products.ToList();
        }

        public Product GetProduct(int id)
        {
            Product product = new Product();
            product = _db.Products.FirstOrDefault(x => x.Id == id);
            if (product?.CategoryId != null)
            {
                product.Category = GetCategory((int) product.CategoryId);

            }
            
            return product;


        }

        public Product CreateProduct(string productname, int supplierid, int categoryid, string quantityperunit, double unitprice, int unitsinstock)
        {
            var maxId = _db.Products.Max(x => x.Id);
            
            Product product = new Product()
            {
                Id              = maxId + 1,
                Name            = productname,
                SupplierId      = supplierid,
                CategoryId      = categoryid,
                QuantityPerUnit = quantityperunit,
                UnitPrice       = unitprice,
                UnitsInStock    = unitsinstock

            };
            

            _db.Products.Add(product);
            _db.SaveChanges();

            return product;
        }
        
        public bool DeleteProduct(int id)
        {
            var product = GetProduct(id);
            if (product == null)
            {
                return false;
            }
            _db.Products.Remove(GetProduct(id));
            _db.SaveChanges();
            return true;
        }

        public bool UpdateProduct(int productid, string productname, int supplierid, int categoryid, string quantityperunit, double unitprice, int unitsinstock)
        {
            var product = GetProduct(productid);
            if (product == null)
            {
                return false;
            }
            product.Name = productname;
            product.SupplierId = supplierid;
            product.CategoryId = categoryid;
            product.QuantityPerUnit = quantityperunit;
            product.UnitPrice = unitprice;
            product.UnitsInStock = unitsinstock;
            _db.SaveChanges();

            return true;
        }


        public IList<Product> GetProductsByCategory(int categoryId)
        {
            IList<Product> products = _db.Products.Where(x => x.CategoryId == categoryId).ToList();

            foreach (var product in products)
            {
                if (product.CategoryId != null)
                {
                    product.Category = GetCategory((int) product.CategoryId);
                }
            }

            return products;
        }

        public IList<Product> GetProductByName(string productName)
        {
            IList<Product> products = _db.Products.Where(x => x.Name.Contains(productName)).ToList();

            foreach (var product in products)
            {
                if (product.CategoryId != null)
                {
                    product.Category = GetCategory((int)product.CategoryId);
                }
            }
            return products;
        }




        /* ORDERS  */

        /* Return the complete order, i.e.all attributes of the order, the complete list of
        order details.Each order detail should include the product which must include
        the category. */



        public Order GetOrder(int id)
        {

            Order order = _db.Orders.FirstOrDefault(x => x.Id == id);
            order.OrderDetails = ActualGetOrderDetailsByOrderId(order.Id);

            return order;

        }

        // Returns a list of Orders - Returns all orders if shipName not defined. Otherwise, returns orders with given ShipName
        // How it should be
        //public IList<Order> GetOrders(string shipName = null)
        //{
        //    if (!string.IsNullOrEmpty(shipName))
        //    {
        //        //IList<Order> orders = _db.Orders.Where(x => x.ShipName == shipName).ToList();
        //        return _db.Orders.Where(x => x.ShipName == shipName).ToList();

        //    }
        //        //IList<Order> orders = _db.Orders.ToList();
        //    return  _db.Orders.ToList();

        //}    

        
        // Bad version of the above, but may be what was meant in assignment
        // !!! NOT FIXED TO FIT REQUIREMENTS!!! 
        public IList<Order> GetOrders(string shipName = null)
        {
            IList<Order> orders = _db.Orders.Where(x => x.ShipName == shipName).ToList();
            if (!string.IsNullOrEmpty(shipName))
            {
                //IList<Order> orders = _db.Orders.Where(x => x.ShipName == shipName).ToList();
                return _db.Orders.Where(x => x.ShipName == shipName).ToList();

            }
            //IList<Order> orders = _db.Orders.ToList();
            return _db.Orders.ToList();

        }

        public IList<OrderDetails> ActualGetOrderDetailsByOrderId(int orderId)
        {
            IList<OrderDetails> orderDetails = _db.OrderDetails.Where(x => x.OrderId == orderId).ToList();
            //orderDetails.Product = GetProduct(orderDetails.ProductId);

            foreach (var detail in orderDetails)
            {
                detail.Product = GetProduct(detail.ProductId);
            }

            return orderDetails;
        }

        
         //Getting actual OrderDetails objects
        public IList<OrderDetails> GetOrderDetailsByOrderId(int orderId)
        {
            IList<OrderDetails> orderDetails = _db.OrderDetails.Where(x => x.OrderId == orderId).ToList();
            //orderDetails.Product = GetProduct(orderDetails.ProductId);

            foreach (var detail in orderDetails)
            {
                detail.Product = GetProduct(detail.ProductId);
            }

            return orderDetails;
        }

        // Gets a list of OrderDetails with specified Order and Product
        public IList<OrderDetails> GetOrderDetailsByProductId(int productId)
        {
            IList<OrderDetails> orderDetails = _db.OrderDetails.Where(x => x.ProductId == productId).OrderBy(x => x.OrderId).ToList();
            //orderDetails.Product = GetProduct(orderDetails.ProductId);

            foreach (var detail in orderDetails)
            {
                detail.Product = GetProduct(detail.ProductId);
                detail.Order = GetOrder(detail.OrderId);
            }

            return orderDetails;
        }
         


    }
}
