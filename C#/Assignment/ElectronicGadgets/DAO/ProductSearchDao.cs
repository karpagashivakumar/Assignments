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
    public class ProductSearchDao : IProductSearchDao
    {
        private readonly string configFile = "appsettings.json";

        public List<string> SearchProductsByName(string name)
        {
            List<string> products = new List<string>();
            try
            {
                using (SqlConnection conn = DBConnUtil.GetConnection(configFile))
                {
                    conn.Open();
                    string query = "SELECT ProductName FROM Products WHERE ProductName LIKE @Name";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Name", "%" + name + "%");

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        products.Add(reader["ProductName"].ToString());
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error while searching products by name: " + ex.Message);
            }
            return products;
        }

        public List<string> SearchProductsByCategory(string category)
        {
            List<string> products = new List<string>();
            try
            {
                using (SqlConnection conn = DBConnUtil.GetConnection(configFile))
                {
                    conn.Open();
                    string query = "SELECT ProductName FROM Products WHERE Category = @Category";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Category", category);

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        products.Add(reader["ProductName"].ToString());
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error while searching products by category: " + ex.Message);
            }
            return products;
        }

        public List<string> GetRecommendedProducts(int topN)
        {
            List<string> recommended = new List<string>();
            try
            {
                using (SqlConnection conn = DBConnUtil.GetConnection(configFile))
                {
                    conn.Open();
                    string query = @"SELECT TOP (@TopN) P.ProductName
                                     FROM OrderDetails OD
                                     JOIN Products P ON OD.ProductID = P.ProductID
                                     GROUP BY P.ProductName
                                     ORDER BY SUM(OD.Quantity) DESC";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@TopN", topN);

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        recommended.Add(reader["ProductName"].ToString());
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error while getting recommended products: " + ex.Message);
            }
            return recommended;
        }

    }
}
