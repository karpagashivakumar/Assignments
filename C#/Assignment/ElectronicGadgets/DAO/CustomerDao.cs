using ElectronicGadgets.Entity;
using ElectronicGadgets.Util;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicGadgets.DAO
{
    public class CustomerDao : ICustomerDao
    {
        private readonly string configFile = "appsettings.json";

        public CustomerDao()
        {
            using (SqlConnection conn = DBConnUtil.GetConnection(configFile)) ;
        }

        public void RegisterCustomer(Customers customer)
        {
            try
            {
                using (SqlConnection conn = DBConnUtil.GetConnection(configFile))
                {
                    conn.Open();

                    // Check for duplicate email
                    SqlCommand checkCmd = new SqlCommand("SELECT COUNT(*) FROM Customers WHERE Email = @Email", conn);
                    checkCmd.Parameters.AddWithValue("@Email", customer.Email);
                    int count = (int)checkCmd.ExecuteScalar();

                    if (count > 0)
                    {
                        throw new ArgumentException("Email address already registered.");
                    }

                    string query = "INSERT INTO Customers (FirstName, LastName, Email, Phone, Address) VALUES (@FirstName, @LastName, @Email, @Phone, @Address)";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@FirstName", customer.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", customer.LastName);
                    cmd.Parameters.AddWithValue("@Email", customer.Email);
                    cmd.Parameters.AddWithValue("@Phone", customer.Phone);
                    cmd.Parameters.AddWithValue("@Address", customer.Address);

                    cmd.ExecuteNonQuery();
                    Console.WriteLine("Customer registered successfully.");
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Database error: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        public List<Customers> GetAllCustomers()
        {
            List<Customers> customers = new List<Customers>();

            try
            {
                using (SqlConnection conn = DBConnUtil.GetConnection(configFile))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM Customers", conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Customers customer = new Customers
                        {
                            CustomerID = Convert.ToInt32(reader["CustomerID"]),
                            FirstName = reader["FirstName"].ToString(),
                            LastName = reader["LastName"].ToString(),
                            Email = reader["Email"].ToString(),
                            Phone = reader["Phone"].ToString(),
                            Address = reader["Address"].ToString()
                        };

                        customers.Add(customer);
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Database error: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            return customers;
        }

        public Customers GetCustomerByEmail(string email)
        {
            Customers customer = null;

            try
            {
                using (SqlConnection conn = DBConnUtil.GetConnection(configFile))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM Customers WHERE Email = @Email", conn);
                    cmd.Parameters.AddWithValue("@Email", email);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        customer = new Customers
                        {
                            CustomerID = Convert.ToInt32(reader["CustomerID"]),
                            FirstName = reader["FirstName"].ToString(),
                            LastName = reader["LastName"].ToString(),
                            Email = reader["Email"].ToString(),
                            Phone = reader["Phone"].ToString(),
                            Address = reader["Address"].ToString()
                        };
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Database error: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            return customer;
        }
        public void UpdateCustomerInfo(int customerId, string newEmail, string newPhone, string newAddress)
        {
            try
            {
                using (SqlConnection conn = DBConnUtil.GetConnection(configFile))
                {
                    conn.Open();
                    string query = "UPDATE Customers SET Email = @Email, Phone = @Phone WHERE CustomerID = @CustomerID";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Email", newEmail);
                    cmd.Parameters.AddWithValue("@Phone", newPhone);
                    cmd.Parameters.AddWithValue("@CustomerID", customerId);
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                        Console.WriteLine("Customer information updated successfully.");
                    else
                        Console.WriteLine("Customer not found.");
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error updating customer info: " + ex.Message);
            }
        }
    }
}
