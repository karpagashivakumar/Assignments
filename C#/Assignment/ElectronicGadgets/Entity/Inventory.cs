using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicGadgets.Entity
{
    public class Inventory
    {
        public int InventoryID { get; set; }
        public Products Product { get; set; }
        public int QuantityInStock { get; set; }
        public DateTime LastStockUpdate { get; set; }

        public Products GetProduct() => Product;

        public int GetQuantityInStock() => QuantityInStock;

        public void AddToInventory(int quantity)
        {
            QuantityInStock += quantity;
            LastStockUpdate = DateTime.Now;
        }

        public void RemoveFromInventory(int quantity)
        {
            if (quantity > QuantityInStock)
                throw new InvalidOperationException("Insufficient stock to remove.");

            QuantityInStock -= quantity;
            LastStockUpdate = DateTime.Now;
        }

        public void UpdateStockQuantity(int newQuantity)
        {
            QuantityInStock = newQuantity;
            LastStockUpdate = DateTime.Now;
        }

        public bool IsProductAvailable(int quantityToCheck) => QuantityInStock >= quantityToCheck;

        public decimal GetInventoryValue() => Product.Price * QuantityInStock;

        public static List<Products> ListLowStockProducts(List<Inventory> allInventory, int threshold)
        {
            return allInventory.Where(i => i.QuantityInStock < threshold).Select(i => i.Product).ToList();
        }

        public static List<Products> ListOutOfStockProducts(List<Inventory> allInventory)
        {
            return allInventory.Where(i => i.QuantityInStock == 0).Select(i => i.Product).ToList();
        }

        public static void ListAllProducts(List<Inventory> allInventory)
        {
            foreach (var item in allInventory)
            {
                Console.WriteLine($"Product: {item.Product.ProductName}, In Stock: {item.QuantityInStock}");
            }
        }
    }
}
