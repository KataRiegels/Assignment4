namespace DataLayer.Model
{
    public class Order
    {
        public int Id { get; set; }
        public string customerid { get; set; }
        public DateOnly? orderdate { get; set; }
        public DateOnly? requireddate { get; set; }
        public DateOnly? shippeddate { get; set; }
        public int? freight { get; set; }
        public string? shipname { get; set; }
        public string? shipaddress { get; set; }
        public string? shipcity { get; set; }
        public string? shippostalcode { get; set; }
        public string? shipcountry { get; set; }

    }
}