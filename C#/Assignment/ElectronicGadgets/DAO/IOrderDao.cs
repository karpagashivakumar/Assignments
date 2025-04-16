using ElectronicGadgets.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicGadgets.DAO
{
    public interface IOrderDao
    {
        void PlaceOrder(int customerId, int productId, int quantity);
        string GetOrderStatus(int orderId);
        Orders GetOrderById(int orderId);
        List<Orders> GetAllOrders();
    }
}
