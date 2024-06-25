namespace SalesTaxCalculator.Models
{
    // This model encapsulates all necessary information about each item, including its calculated tax and total price after tax.
    public class Item
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public bool IsImported { get; set; }
        public decimal Tax { get; set; }
        public decimal TotalPrice => Price + Tax;
    }
}
