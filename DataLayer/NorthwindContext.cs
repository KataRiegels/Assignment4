using DataLayer.Model;
using Microsoft.EntityFrameworkCore;

namespace DataLayer
{
    public class NorthwindContext : DbContext
    {
        const string ConnectionString = "host=localhost;db=northwind;uid=postgres;pwd=Jse33pjp";

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }   

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            //optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information);
            optionsBuilder.UseNpgsql(ConnectionString);
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>().ToTable("categories");
            modelBuilder.Entity<Category>().Property(x => x.Id).HasColumnName("categoryid");
            modelBuilder.Entity<Category>().Property(x => x.Name).HasColumnName("categoryname");
            modelBuilder.Entity<Category>().Property(x => x.Description).HasColumnName("description");

            modelBuilder.Entity<Product>().ToTable("products");
            modelBuilder.Entity<Product>().Property(x => x.Id).HasColumnName("productid");
            modelBuilder.Entity<Product>().Property(x => x.Name).HasColumnName("productname");
            modelBuilder.Entity<Product>().Property(x => x.SupplierId).HasColumnName("supplierid");
            modelBuilder.Entity<Product>().Property(x => x.CategoryId).HasColumnName("categoryid");
            modelBuilder.Entity<Product>().Property(x => x.QuantityPerUnit).HasColumnName("quantityperunit");
            modelBuilder.Entity<Product>().Property(x => x.UnitPrice).HasColumnName("unitprice");
            modelBuilder.Entity<Product>().Property(x => x.UnitsInStock).HasColumnName("unitsinstock");


            modelBuilder.Entity<Order>().ToTable("orders");
            modelBuilder.Entity<Order>().Property(x => x.OrderId).HasColumnName("orderid");
            modelBuilder.Entity<Order>().Property(x => x.CustomerId).HasColumnName("customerid");
            modelBuilder.Entity<Order>().Property(x => x.OrderDate).HasColumnName("orderdate");
            modelBuilder.Entity<Order>().Property(x => x.EmployeeId).HasColumnName("employeeid");
            modelBuilder.Entity<Order>().Property(x => x.RequiredDate).HasColumnName("requireddate");
            modelBuilder.Entity<Order>().Property(x => x.ShippedDate).HasColumnName("shippeddate");
            modelBuilder.Entity<Order>().Property(x => x.Freight).HasColumnName("freight");
            modelBuilder.Entity<Order>().Property(x => x.ShipName).HasColumnName("shipname");
            modelBuilder.Entity<Order>().Property(x => x.ShipAddress).HasColumnName("shipaddress");
            modelBuilder.Entity<Order>().Property(x => x.ShipCity).HasColumnName("shipcity");
            modelBuilder.Entity<Order>().Property(x => x.ShipPostalCode).HasColumnName("shippostalcode");
            modelBuilder.Entity<Order>().Property(x => x.ShipCountry).HasColumnName("shipcountry");



            modelBuilder.Entity<OrderDetails>().ToTable("orderdetails");
            modelBuilder.Entity<OrderDetails>().HasKey(t => new { t.OrderId, t.ProductId });

            //modelBuilder.Entity<OrderDetails>().HasMany(o => o.OrderId).WithMany(p => p.ProductId).HasForeignKey();
            //modelBuilder.Entity<OrderDetails>().HasOne(o => o.Order).WithMany(p => p.OrderDetails);
            //modelBuilder.Entity<OrderDetails>().HasAlternateKey(a => new { a.OrderId, a.ProductId });
            //modelBuilder.Entity<Order>().HasMany(o => o.OrderDetails).WithRequired().HasForeignKey(od => od.OrderId);
            modelBuilder.Entity<OrderDetails>().Property(x => x.OrderId).HasColumnName("orderid");
            modelBuilder.Entity<OrderDetails>().Property(x => x.ProductId).HasColumnName("productid");
            modelBuilder.Entity<OrderDetails>().Property(x => x.UnitPrice).HasColumnName("unitprice");
            modelBuilder.Entity<OrderDetails>().Property(x => x.Quantity).HasColumnName("quantity");
            modelBuilder.Entity<OrderDetails>().Property(x => x.Discount).HasColumnName("discount");

        }
    }
}