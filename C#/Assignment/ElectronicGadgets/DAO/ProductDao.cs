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
    public class ProductDao : IProductDao
    {
        private readonly string configFile = "appsettings.json";

        public void AddProduct(Products product)
        {
            try
            {
                using (SqlConnection conn = DBConnUtil.GetConnection(configFile))
                {
                    conn.Open();
                    string query = "INSERT INTO Products (ProductName, Description, Price) VALUES (@ProductName, @Description, @Price)";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@ProductName", product.ProductName);
                    cmd.Parameters.AddWithValue("@Description", product.Description);
                    cmd.Parameters.AddWithValue("@Price", product.Price);
                    cmd.ExecuteNonQuery();
                    Console.WriteLine("Product added successfully.");
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error while adding product: " + ex.Message);
            }
        }

        public void UpdateProduct(Products product)
        {
            try
            {
                using (SqlConnection conn = DBConnUtil.GetConnection(configFile))
                {
                    conn.Open();
                    string query = "UPDATE Products SET ProductName = @ProductName, Description = @Description, Price = @Price WHERE ProductID = @ProductID";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@ProductID", product.ProductID);
                    cmd.Parameters.AddWithValue("@ProductName", product.ProductName);
                    cmd.Parameters.AddWithValue("@Description", product.Description);
                    cmd.Parameters.AddWithValue("@Price", product.Price);
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                        Console.WriteLine("Product updated successfully.");
                    else
                        Console.WriteLine("No product found with the specified ID.");
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error while updating product: " + ex.Message);
            }
        }

        public void DeleteProduct(int productId)
        {
            try
            {
                using (SqlConnection conn = DBConnUtil.GetConnection(configFile))
                {
                    conn.Open();
                    string query = "DELETE FROM Products WHERE ProductID = @ProductID";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@ProductID", productId);
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                        Console.WriteLine("Product deleted successfully.");
                    else
                        Console.WriteLine("No product found with the specified ID.");
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error while deleting product: " + ex.Message);
            }
        }

        public Products GetProductById(int productId)
        {
            Products product = null;
            try
            {
                using (SqlConnection conn = DBConnUtil.GetConnection(configFile))
                {
                    conn.Open();
                    string query = "SELECT * FROM Products WHERE ProductID = @ProductID";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@ProductID", productId);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        product = new Products()
                        {
                            ProductID = Convert.ToInt32(reader["ProductID"]),
                            ProductName = reader["ProductName"].ToString(),
                            Description = reader["Description"].ToString(),
                            Price = Convert.ToDecimal(reader["Price"])
                        };
                    }
                    else
                    {
                        Console.WriteLine("Product not found.");
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error while retrieving product: " + ex.Message);
            }
            return product;
        }

        public List<Products> GetAllProducts()
        {
            List<Products> productList = new List<Products>();
            try
            {
                using (SqlConnection conn = DBConnUtil.GetConnection(configFile))
                {
                    conn.Open();
                    string query = "SELECT * FROM Products";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Products product = new Products()
                        {
                            ProductID = Convert.ToInt32(reader["ProductID"]),
                            ProductName = reader["ProductName"].ToString(),
                            Description = reader["Description"].ToString(),
                            Price = Convert.ToDecimal(reader["Price"])
                        };
                        productList.Add(product);
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error while retrieving products: " + ex.Message);
            }
            return productList;
        }
        public List<Products> SearchProducts(string keyword)
        {
            List<Products> results = new List<Products>();
            try
            {
                using (SqlConnection conn = DBConnUtil.GetConnection(configFile))
                {
                    conn.Open();
                    string query = "SELECT ProductID, ProductName, Description, Price FROM Products WHERE ProductName LIKE @Keyword OR Description LIKE @Keyword";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Keyword", "%" + keyword + "%");

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Products product = new Products(
                            Convert.ToInt32(reader["ProductID"]),
                            reader["ProductName"].ToString(),
                            reader["Description"].ToString(),
                            Convert.ToDecimal(reader["Price"])
                        );
                        results.Add(product);
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error while searching products: " + ex.Message);
            }
            return results;
        }
    }
}
