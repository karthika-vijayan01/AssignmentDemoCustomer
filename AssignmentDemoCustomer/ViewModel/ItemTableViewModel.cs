namespace AssignmentDemoCustomer.ViewModel
{
    public class ItemTableViewModel
    {
        public int CustomerId { get; set; }
        public string? CustomerName { get; set; }

        public string? CustomerNumber { get; set; }


        public DateTime? OrderDate { get; set; }

        public string? ItemName { get; set; }

        public decimal? Price { get; set; }
        public int? Quantity { get; set; }

     
    }
}
