using ElectronicGadgets.Util;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicGadgets.DAO
{
    public class OrderStatusDao : IOrderStatusDao
    {
        private readonly string configFile = "appsettings.json";

        public string GetOrderStatus(int orderId)
        {
            string status = string.Empty;
            try
            {
                using (SqlConnection conn = DBConnUtil.GetConnection(configFile))
                {
                    conn.Open();
                    string query = "SELECT Status FROM Orders WHERE OrderID = @OrderID";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@OrderID", orderId);

                    object result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        status = result.ToString();
                    }
                    else
                    {
                        Console.WriteLine("Order not found.");
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error while retrieving order status: " + ex.Message);
            }
            return status;
        }

        public List<(int OrderId, string Status)> GetAllOrderStatuses()
        {
            List<(int, string)> statusList = new List<(int, string)>();
            try
            {
                using (SqlConnection conn = DBConnUtil.GetConnection(configFile))
                {
                    conn.Open();
                    string query = "SELECT OrderID, Status FROM Orders";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        int orderId = Convert.ToInt32(reader["OrderID"]);
                        string status = reader["Status"].ToString();
                        statusList.Add((orderId, status));
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error while retrieving all order statuses: " + ex.Message);
            }
            return statusList;
        }
    }
}
