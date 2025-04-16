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
    public class InventoryDao : IInventoryDao
    {
        private readonly string configFile = "appsettings.json";

        public void AddNewProductToInventory(Products product, int quantity)
        {
            try
            {
                using (SqlConnection conn = DBConnUtil.GetConnection(configFile))
                {
                    conn.Open();
                    string query = "INSERT INTO Products (ProductID, ProductName, Description, Price) VALUES (@ProductID, @ProductName, @Description, @Price); " +
                                   "INSERT INTO Inventory (ProductID, QuantityInStock, LastStockUpdate) VALUES (@ProductID, @QuantityInStock, @LastStockUpdate);";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@ProductID", product.ProductID);
                    cmd.Parameters.AddWithValue("@ProductName", product.ProductName);
                    cmd.Parameters.AddWithValue("@Description", product.Description);
                    cmd.Parameters.AddWithValue("@Price", product.Price);
                    cmd.Parameters.AddWithValue("@QuantityInStock", quantity);
                    cmd.Parameters.AddWithValue("@LastStockUpdate", DateTime.Now);

                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error while adding product to inventory: " + ex.Message);
            }
        }

        public void UpdateProductQuantity(int productId, int newQuantity)
        {
            try
            {
                using (SqlConnection conn = DBConnUtil.GetConnection(configFile))
                {
                    conn.Open();
                    string query = "UPDATE Inventory SET QuantityInStock = @QuantityInStock, LastStockUpdate = @LastStockUpdate WHERE ProductID = @ProductID";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@QuantityInStock", newQuantity);
                    cmd.Parameters.AddWithValue("@LastStockUpdate", DateTime.Now);
                    cmd.Parameters.AddWithValue("@ProductID", productId);

                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error while updating product quantity: " + ex.Message);
            }
        }

        public void RemoveDiscontinuedProduct(int productId)
        {
            try
            {
                using (SqlConnection conn = DBConnUtil.GetConnection(configFile))
                {
                    conn.Open();
                    string query = "DELETE FROM Inventory WHERE ProductID = @ProductID; DELETE FROM Products WHERE ProductID = @ProductID;";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@ProductID", productId);

                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error while removing discontinued product: " + ex.Message);
            }
        }

        public List<Inventory> GetAllInventoryItems()
        {
            List<Inventory> inventoryList = new List<Inventory>();
            try
            {
                using (SqlConnection conn = DBConnUtil.GetConnection(configFile))
                {
                    conn.Open();
                    string query = "SELECT i.InventoryID, i.ProductID, p.ProductName, i.QuantityInStock, i.LastStockUpdate, p.Description, p.Price " +
                                   "FROM Inventory i JOIN Products p ON i.ProductID = p.ProductID";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        var product = new Products
                        {
                            ProductID = Convert.ToInt32(reader["ProductID"]),
                            ProductName = reader["ProductName"].ToString(),
                            Description = reader["Description"].ToString(),
                            Price = Convert.ToDecimal(reader["Price"])
                        };

                        var inventory = new Inventory
                        {
                            InventoryID = Convert.ToInt32(reader["InventoryID"]),
                            Product = product,
                            QuantityInStock = Convert.ToInt32(reader["QuantityInStock"]),
                            LastStockUpdate = Convert.ToDateTime(reader["LastStockUpdate"])
                        };

                        inventoryList.Add(inventory);
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error while retrieving inventory: " + ex.Message);
            }
            return inventoryList;
        }
        public void AddToInventory(int productId, int quantity)
        {
            try
            {
                using (SqlConnection conn = DBConnUtil.GetConnection(configFile))
                {
                    conn.Open();

                    // Check if product already exists in inventory
                    string checkQuery = "SELECT COUNT(*) FROM Inventory WHERE ProductID = @ProductID";
                    SqlCommand checkCmd = new SqlCommand(checkQuery, conn);
                    checkCmd.Parameters.AddWithValue("@ProductID", productId);
                    int exists = (int)checkCmd.ExecuteScalar();

                    if (exists > 0)
                    {
                        // Update existing quantity
                        string updateQuery = "UPDATE Inventory SET QuantityInStock = QuantityInStock + @Quantity, LastStockUpdate = GETDATE() WHERE ProductID = @ProductID";
                        SqlCommand updateCmd = new SqlCommand(updateQuery, conn);
                        updateCmd.Parameters.AddWithValue("@Quantity", quantity);
                        updateCmd.Parameters.AddWithValue("@ProductID", productId);
                        updateCmd.ExecuteNonQuery();
                    }
                    else
                    {
                        // Insert new inventory record
                        string insertQuery = "INSERT INTO Inventory (ProductID, QuantityInStock, LastStockUpdate) VALUES (@ProductID, @Quantity, GETDATE())";
                        SqlCommand insertCmd = new SqlCommand(insertQuery, conn);
                        insertCmd.Parameters.AddWithValue("@ProductID", productId);
                        insertCmd.Parameters.AddWithValue("@Quantity", quantity);
                        insertCmd.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error while adding to inventory: " + ex.Message);
            }
        }
    }
}
