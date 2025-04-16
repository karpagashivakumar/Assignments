using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ElectronicGadgets.Entity
{
    public class Customers
    {
        public int CustomerID { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? Email { get; set; }

        public string? Phone { get; set; }

        public string? Address { get; set; }

        public int? OrderCount { get; set; }

        public Customers(int customerId, string firstName, string lastName, string email, string phone, string address)
        {
            CustomerID = customerId;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Phone = phone;
            Address = address;
        }

        public Customers() { }
        public int CalculateTotalOrders(List<Orders> allOrders)
        {
            return allOrders.Count(o => o.Customer.CustomerID == this.CustomerID);
        }

        public string GetCustomerDetails()
        {
            return $"ID: {CustomerID}, Name: {FirstName} {LastName}, Email: {Email}, Phone: {Phone}, Address: {Address}";
        }

        public void UpdateCustomerInfo(string? email = null, string? phone = null, string? address = null)
        {
            if (!string.IsNullOrEmpty(email) && !Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                throw new InvalidDataException("Invalid email address format.");
            }

            if (!string.IsNullOrEmpty(email)) Email = email;
            if (!string.IsNullOrEmpty(phone)) Phone = phone;
            if (!string.IsNullOrEmpty(address)) Address = address;
        }
    }
}
