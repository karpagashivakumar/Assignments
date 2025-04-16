using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicGadgets.DAO
{
    public interface IPaymentDao
    {
        void RecordPayment(int orderId, decimal amount, string paymentMethod);
        void ProcessPayment(int orderId, decimal amount, string paymentMethod);
        bool IsPaymentAlreadyMade(int orderId);
    }
}
