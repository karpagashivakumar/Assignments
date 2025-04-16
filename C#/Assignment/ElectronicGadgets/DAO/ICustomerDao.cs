using ElectronicGadgets.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicGadgets.DAO
{
    public interface ICustomerDao
    {
        void RegisterCustomer(Customers customer);
        List<Customers> GetAllCustomers();
        Customers GetCustomerByEmail(string email);
        void UpdateCustomerInfo(int customerId, string newEmail, string newPhone, string newAddress);
    }
}
