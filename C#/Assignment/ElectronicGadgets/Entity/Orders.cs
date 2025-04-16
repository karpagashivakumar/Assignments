using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ElectronicGadgets.Exceptions.CustomException;

namespace ElectronicGadgets.Entity
{
    public class Orders
    {
        public int OrderID { get; set; }
        public Customers Customer { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; } = "Processing";
        public List<OrderDetails> OrderDetailList { get; set; } = new();

        public void ValidateOrder()
        {
            if (OrderDetailList.Count == 0 || OrderDetailList.Any(od => od.Product == null))
            {
                throw new IncompleteOrderException("Order is incomplete. Missing product references.");
            }
        }

        public void UpdateOrderStatusWithConcurrencyCheck(string newStatus, string currentStatusInDb)
        {
            if (Status != currentStatusInDb)
            {
                throw new ConcurrencyException("Order has been updated by another process.");
            }

            Status = newStatus;
        }
        public decimal CalculateTotalAmount()
        {
            TotalAmount = OrderDetailList.Sum(detail => detail.CalculateSubtotal());
            return TotalAmount;
        }

        public string GetOrderDetails()
        {
            var details = OrderDetailList.Select(od => od.GetOrderDetailInfo()).ToList();
            return $"Order ID: {OrderID}, Customer: {Customer.FirstName} {Customer.LastName}, Date: {OrderDate}, Total: {TotalAmount:C}\nDetails:\n{string.Join("\n", details)}";
        }

        public void UpdateOrderStatus(string newStatus)
        {
            Status = newStatus;
        }

        public void CancelOrder(List<Inventory> inventoryList)
        {
            foreach (var detail in OrderDetailList)
            {
                var stockItem = inventoryList.FirstOrDefault(i => i.Product.ProductID == detail.Product.ProductID);
                if (stockItem != null)
                {
                    stockItem.AddToInventory(detail.Quantity);
                }
            }

            Status = "Cancelled";
        }
    }
}
