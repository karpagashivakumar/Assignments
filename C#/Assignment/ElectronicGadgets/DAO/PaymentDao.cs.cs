using ElectronicGadgets.Util;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicGadgets.DAO
{
    public class PaymentDao : IPaymentDao
    {
        private readonly string configFile = "appsettings.json";

        public void RecordPayment(int orderId, decimal amount, string paymentMethod)
        {
            try
            {
                using (SqlConnection conn = DBConnUtil.GetConnection(configFile))
                {
                    conn.Open();

                    if (IsPaymentAlreadyMade(orderId))
                    {
                        Console.WriteLine($"Payment already exists for Order ID {orderId}.");
                        return;
                    }

                    string query = @"INSERT INTO Payments (OrderID, Amount, PaymentMethod, PaymentDate)
                                     VALUES (@OrderID, @Amount, @PaymentMethod, @PaymentDate)";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@OrderID", orderId);
                    cmd.Parameters.AddWithValue("@Amount", amount);
                    cmd.Parameters.AddWithValue("@PaymentMethod", paymentMethod);
                    cmd.Parameters.AddWithValue("@PaymentDate", DateTime.Now);

                    int rows = cmd.ExecuteNonQuery();
                    if (rows > 0)
                        Console.WriteLine("Payment recorded successfully.");
                    else
                        Console.WriteLine("Failed to record payment.");
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error while recording payment: " + ex.Message);
            }
        }

        public bool IsPaymentAlreadyMade(int orderId)
        {
            bool exists = false;
            try
            {
                using (SqlConnection conn = DBConnUtil.GetConnection(configFile))
                {
                    conn.Open();
                    string query = "SELECT COUNT(*) FROM Payments WHERE OrderID = @OrderID";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@OrderID", orderId);
                    int count = (int)cmd.ExecuteScalar();
                    exists = count > 0;
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error while checking existing payment: " + ex.Message);
            }
            return exists;
        }
        public void ProcessPayment(int orderId, decimal amount, string paymentMethod)
        {
            try
            {
                using (SqlConnection conn = DBConnUtil.GetConnection(configFile))
                {
                    conn.Open();
                    string query = "INSERT INTO Payments (OrderID, PaymentMethod, Amount, PaymentDate) VALUES (@OrderID, @PaymentMethod, @Amount, GETDATE())";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@OrderID", orderId);
                    cmd.Parameters.AddWithValue("@PaymentMethod", paymentMethod);
                    cmd.Parameters.AddWithValue("@Amount", amount);

                    int rows = cmd.ExecuteNonQuery();
                    Console.WriteLine(rows > 0 ? "Payment processed successfully." : "Payment failed.");
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error: " + ex.Message);
            }
        }
    }
}
