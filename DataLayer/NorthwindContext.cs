using DataLayer.Model;
using Microsoft.EntityFrameworkCore;

namespace DataLayer
{
    public class NorthwindContext : DbContext
    {
        const string ConnectionString = "host=localhost;db=northwind;uid=postgres;pwd=Jse33pjp";

        public IList<Category> Categories { get; set; }
        public IList<Product> Products { get; set; }
        public IList<Order> Orders { get; set; }

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
            modelBuilder.Entity<Product>().Property(x => x.CategoryId).HasColumnName("categoryid");

            modelBuilder.Entity<Order>().ToTable("orders");
            modelBuilder.Entity<Order>().Property(x => x.Id).HasColumnName("orderid");
            modelBuilder.Entity<Order>().Property(x => x.customerid).HasColumnName("customerid");
            modelBuilder.Entity<Order>().Property(x => x.orderdate).HasColumnName("orderdate");
            modelBuilder.Entity<Order>().Property(x => x.requireddate).HasColumnName("requireddate");
            modelBuilder.Entity<Order>().Property(x => x.shippeddate).HasColumnName("shippeddate");
            modelBuilder.Entity<Order>().Property(x => x.freight).HasColumnName("freight");
            modelBuilder.Entity<Order>().Property(x => x.shipname).HasColumnName("shipname");
            modelBuilder.Entity<Order>().Property(x => x.shipaddress).HasColumnName("shipaddress");
            modelBuilder.Entity<Order>().Property(x => x.shipcity).HasColumnName("shipcity");
            modelBuilder.Entity<Order>().Property(x => x.shippostalcode).HasColumnName("shippostalcode");
            modelBuilder.Entity<Order>().Property(x => x.shipcountry).HasColumnName("shipcountry");

        }
    }
}