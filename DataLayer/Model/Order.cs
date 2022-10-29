﻿using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Model
{
    public class Order
    {
        //[ForeignKey("OrderDetails")]
        public int Id { get; set; }

        public string CustomerId { get; set; }
        public int EmployeeId { get; set; }
        public DateTime Date { get; set; }
        //public DateTime ShippingDate { get; set; }
        public DateTime RequiredDate { get; set; }
        public DateTime? ShippedDate { get; set; }
        public int? Freight { get; set; }
        public string? ShipName { get; set; }
        public string? ShipAddress { get; set; }
        public string? ShipCity { get; set; }
        public string? ShipPostalCode { get; set; }
        public string? ShipCountry { get; set; }

        
        //public IList<OrderDetails>? OrderDetails { get; set; }
        //public IList<OrderDetails> OrderDetails { get; set; } = new List<OrderDetails>();
        public IList<OrderDetails> OrderDetails { get; set; }
    }
}