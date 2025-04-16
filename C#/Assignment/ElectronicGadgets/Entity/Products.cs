using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicGadgets.Entity
{
    public class Products
    {

        public int ProductID { get; set; }
        public string ProductName { get; set; } = "";
        public string Description { get; set; } = "";

        private decimal _price;

        public decimal Price
        {
            get => _price;
            set
            {
                if (value < 0)
                    throw new InvalidDataException("Price cannot be negative.");
                _price = value;
            }
        }

        public Products(int productId, string productName, string description, decimal price)
        {
            ProductID = productId;
            ProductName = productName;
            Description = description;
            Price = price;
        }
        public string GetProductDetails()
        {
            return $"Product ID: {ProductID}, Name: {ProductName}, Price: {Price:C}, Description: {Description}";
        }
        public Products() { }
        public void UpdateProductInfo(string? description = null, decimal? price = null)
        {
            if (!string.IsNullOrEmpty(description)) Description = description;
            if (price.HasValue)
            {
                if (price.Value < 0)
                    throw new InvalidDataException("Price must be a non-negative value.");
                Price = price.Value;        // This also goes through the property setter validation
            }
        }

        public bool IsProductInStock(List<Inventory> inventoryList)
        {
            var item = inventoryList.FirstOrDefault(i => i.Product.ProductID == this.ProductID);
            return item != null && item.QuantityInStock > 0;
        }
    }
}
