using ElectronicGadgets.Util;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicGadgets.DAO
{
    public class CustomerAccountDao : ICustomerAccountDao
    {
        private readonly string configFile = "appsettings.json";

        public bool UpdateCustomerEmail(int customerId, string newEmail)
        {
            try
            {
                using (SqlConnection conn = DBConnUtil.GetConnection(configFile))
                {
                    conn.Open();
                    string query = "UPDATE Customers SET Email = @Email WHERE CustomerID = @CustomerID";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Email", newEmail);
                    cmd.Parameters.AddWithValue("@CustomerID", customerId);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error while updating email: " + ex.Message);
            }
            return false;
        }

        public bool UpdateCustomerPhone(int customerId, string newPhone)
        {
            try
            {
                using (SqlConnection conn = DBConnUtil.GetConnection(configFile))
                {
                    conn.Open();
                    string query = "UPDATE Customers SET Phone = @Phone WHERE CustomerID = @CustomerID";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Phone", newPhone);
                    cmd.Parameters.AddWithValue("@CustomerID", customerId);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error while updating phone: " + ex.Message);
            }
            return false;
        }

        public bool UpdateCustomerAddress(int customerId, string newAddress)
        {
            try
            {
                using (SqlConnection conn = DBConnUtil.GetConnection(configFile))
                {
                    conn.Open();
                    string query = "UPDATE Customers SET Address = @Address WHERE CustomerID = @CustomerID";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Address", newAddress);
                    cmd.Parameters.AddWithValue("@CustomerID", customerId);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error while updating address: " + ex.Message);
            }
            return false;
        }
    }
}
