using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicGadgets.Entity
{
    public class OrderDetails
    {
        public int OrderDetailID { get; set; }
        public Orders Order { get; set; }
        public Products Product { get; set; }
        public int Quantity { get; set; }
        public decimal Discount { get; set; } = 0;

        public decimal CalculateSubtotal()
        {
            var subtotal = Product.Price * Quantity;
            return subtotal - Discount;
        }

        public string GetOrderDetailInfo()
        {
            return $"Product: {Product.ProductName}, Qty: {Quantity}, Subtotal: {CalculateSubtotal():C}";
        }

        public void UpdateQuantity(int newQuantity)
        {
            if (newQuantity <= 0)
            {
                throw new InvalidDataException("Quantity must be greater than 0.");
            }
            Quantity = newQuantity;
        }

        public void AddDiscount(decimal discountAmount)
        {
            Discount = discountAmount;
        }
    }
}
