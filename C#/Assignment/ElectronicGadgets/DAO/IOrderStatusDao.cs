using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicGadgets.DAO
{
    public interface IOrderStatusDao
    {
        string GetOrderStatus(int orderId);
        List<(int OrderId, string Status)> GetAllOrderStatuses();
    }
}
