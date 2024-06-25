using System.Collections.Generic;
using System.Linq;
using SalesTaxCalculator.Models;

namespace SalesTaxCalculator.Services
{
    public class TaxCalculator
    {
        // List of item names that are exempt from sales tax
        private readonly List<string> ExemptItems = new() { "book", "chocolate", "chocolates", "paracetamol","pills" };

        // Method to calculate tax for a single item
        public decimal CalculateTax(Item item)
        {
            decimal tax = 0;

            // Checks if the item's name contains any keyword from ExemptItems list.
            bool isExempt = ExemptItems.Any(ei => item.Name.ToLower().Contains(ei));

            if (!isExempt)
            {
                tax += 0.10m * item.Price; // A rate of 10% applied to all items except those that are exempt.
            }

            if (item.IsImported)
            {
                tax += 0.05m * item.Price; // An additional 5% tax applied to imported items regardless of their type.
            }

            // Round up to the nearest 0.05
            tax = decimal.Ceiling(tax * 20) / 20;
            return tax;
        }

        // Method to calculate totals for a list of items
        public (List<Item> UpdatedItems, decimal TotalTax, decimal TotalPrice) CalculateTotals(List<Item> items)
        {
            // Iterate through each item in the list (items), calculate its tax using CalculateTax, and update the item's Tax property.
            var updatedItems = items.Select(item =>
            {
                item.Tax = CalculateTax(item);
                return item;
            }).ToList();

            decimal totalTax = updatedItems.Sum(item => item.Tax * item.Quantity);
            decimal totalPrice = updatedItems.Sum(item => item.TotalPrice * item.Quantity);

            return (updatedItems, totalTax, totalPrice);
        }
    }
}

