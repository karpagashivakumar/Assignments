using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicGadgets.DAO
{
    public interface ICustomerAccountDao
    {
        bool UpdateCustomerEmail(int customerId, string newEmail);
        bool UpdateCustomerPhone(int customerId, string newPhone);
        bool UpdateCustomerAddress(int customerId, string newAddress);
    }
}
