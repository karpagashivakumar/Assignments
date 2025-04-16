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
    public class OrderDao : IOrderDao
    {
        private readonly string configFile = "appsettings.json";

        public void PlaceOrder(int customerId, int productId, int quantity)
        {
            try
            {
                using (SqlConnection conn = DBConnUtil.GetConnection(configFile))
                {
                    conn.Open();
                    SqlTransaction transaction = conn.BeginTransaction();

                    try
                    {
                        // Insert order
                        string insertOrderQuery = "INSERT INTO Orders (CustomerID, OrderDate, TotalAmount) OUTPUT INSERTED.OrderID VALUES (@CustomerID, @OrderDate, @TotalAmount)";
                        SqlCommand orderCmd = new SqlCommand(insertOrderQuery, conn, transaction);
                        orderCmd.Parameters.AddWithValue("@CustomerID", customerId);
                        orderCmd.Parameters.AddWithValue("@OrderDate", DateTime.Now);
                        orderCmd.Parameters.AddWithValue("@TotalAmount", 0); // Placeholder, update later
                        int orderId = (int)orderCmd.ExecuteScalar();

                        // Get product price
                        string priceQuery = "SELECT Price FROM Products WHERE ProductID = @ProductID";
                        SqlCommand priceCmd = new SqlCommand(priceQuery, conn, transaction);
                        priceCmd.Parameters.AddWithValue("@ProductID", productId);
                        decimal price = (decimal)priceCmd.ExecuteScalar();

                        // Insert order detail
                        decimal subtotal = price * quantity;
                        string insertDetailQuery = "INSERT INTO OrderDetails (OrderID, ProductID, Quantity) VALUES (@OrderID, @ProductID, @Quantity)";
                        SqlCommand detailCmd = new SqlCommand(insertDetailQuery, conn, transaction);
                        detailCmd.Parameters.AddWithValue("@OrderID", orderId);
                        detailCmd.Parameters.AddWithValue("@ProductID", productId);
                        detailCmd.Parameters.AddWithValue("@Quantity", quantity);
                        detailCmd.ExecuteNonQuery();

                        // Update total amount
                        string updateOrderQuery = "UPDATE Orders SET TotalAmount = @TotalAmount WHERE OrderID = @OrderID";
                        SqlCommand updateCmd = new SqlCommand(updateOrderQuery, conn, transaction);
                        updateCmd.Parameters.AddWithValue("@TotalAmount", subtotal);
                        updateCmd.Parameters.AddWithValue("@OrderID", orderId);
                        updateCmd.ExecuteNonQuery();

                        // Update inventory
                        string updateInventoryQuery = "UPDATE Inventory SET QuantityInStock = QuantityInStock - @Quantity WHERE ProductID = @ProductID";
                        SqlCommand inventoryCmd = new SqlCommand(updateInventoryQuery, conn, transaction);
                        inventoryCmd.Parameters.AddWithValue("@Quantity", quantity);
                        inventoryCmd.Parameters.AddWithValue("@ProductID", productId);
                        inventoryCmd.ExecuteNonQuery();

                        transaction.Commit();
                        Console.WriteLine("Order placed successfully.");
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        Console.WriteLine("Error while placing order: " + ex.Message);
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error while placing order: " + ex.Message);
            }
        }

        public Orders GetOrderById(int orderId)
        {
            Orders order = null;
            try
            {
                using (SqlConnection conn = DBConnUtil.GetConnection(configFile))
                {
                    conn.Open();
                    string query = "SELECT * FROM Orders WHERE OrderID = @OrderID";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@OrderID", orderId);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        order = new Orders()
                        {
                            OrderID = Convert.ToInt32(reader["OrderID"]),
                            OrderDate = Convert.ToDateTime(reader["OrderDate"]),
                            TotalAmount = Convert.ToDecimal(reader["TotalAmount"]),
                            Customer = new Customers { CustomerID = Convert.ToInt32(reader["CustomerID"]) }
                        };
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error while retrieving order: " + ex.Message);
            }
            return order;
        }

        public List<Orders> GetAllOrders()
        {
            List<Orders> orderList = new List<Orders>();
            try
            {
                using (SqlConnection conn = DBConnUtil.GetConnection(configFile))
                {
                    conn.Open();
                    string query = "SELECT * FROM Orders";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Orders order = new Orders()
                        {
                            OrderID = Convert.ToInt32(reader["OrderID"]),
                            OrderDate = Convert.ToDateTime(reader["OrderDate"]),
                            TotalAmount = Convert.ToDecimal(reader["TotalAmount"]),
                            Customer = new Customers { CustomerID = Convert.ToInt32(reader["CustomerID"]) }
                        };
                        orderList.Add(order);
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error while retrieving orders: " + ex.Message);
            }
            return orderList;
        }
        public string GetOrderStatus(int orderId)
        {
            string status = "Unknown";
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
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error while fetching order status: " + ex.Message);
            }
            return status;
        }
    }
}
