using ElectronicGadgets.Util;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicGadgets.DAO
{
    public class SalesReportDao : ISalesReportDao
    {
        private readonly string configFile = "appsettings.json";

        public decimal GetTotalSales()
        {
            decimal totalSales = 0;
            try
            {
                using (SqlConnection conn = DBConnUtil.GetConnection(configFile))
                {
                    conn.Open();
                    string query = "SELECT SUM(TotalAmount) FROM Orders";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    object result = cmd.ExecuteScalar();
                    if (result != DBNull.Value)
                    {
                        totalSales = Convert.ToDecimal(result);
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error while fetching total sales: " + ex.Message);
            }
            return totalSales;
        }

        public List<(DateTime Date, decimal TotalSales)> GetDailySalesReport()
        {
            List<(DateTime, decimal)> report = new List<(DateTime, decimal)>();
            try
            {
                using (SqlConnection conn = DBConnUtil.GetConnection(configFile))
                {
                    conn.Open();
                    string query = "SELECT CAST(OrderDate AS DATE) AS OrderDay, SUM(TotalAmount) AS DailyTotal FROM Orders GROUP BY CAST(OrderDate AS DATE)";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        DateTime date = Convert.ToDateTime(reader["OrderDay"]);
                        decimal total = Convert.ToDecimal(reader["DailyTotal"]);
                        report.Add((date, total));
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error while fetching daily sales report: " + ex.Message);
            }
            return report;
        }

        public List<(string ProductName, int QuantitySold)> GetTopSellingProducts(int topN)
        {
            List<(string, int)> topProducts = new List<(string, int)>();
            try
            {
                using (SqlConnection conn = DBConnUtil.GetConnection(configFile))
                {
                    conn.Open();
                    string query = @"SELECT TOP (@TopN) P.ProductName, SUM(OD.Quantity) AS QuantitySold
                                     FROM OrderDetails OD
                                     JOIN Products P ON OD.ProductID = P.ProductID
                                     GROUP BY P.ProductName
                                     ORDER BY QuantitySold DESC";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@TopN", topN);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        string name = reader["ProductName"].ToString();
                        int qty = Convert.ToInt32(reader["QuantitySold"]);
                        topProducts.Add((name, qty));
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error while fetching top selling products: " + ex.Message);
            }
            return topProducts;
        }
        public void GenerateReport()
        {
            Console.WriteLine("Total Sales: ₹" + GetTotalSales());

            Console.WriteLine("\n--- Daily Sales Report ---");
            foreach (var entry in GetDailySalesReport())
            {
                Console.WriteLine($"{entry.Date:yyyy-MM-dd} - ₹{entry.TotalSales}");
            }

            Console.WriteLine("\n--- Top Selling Products ---");
            foreach (var product in GetTopSellingProducts(5))
            {
                Console.WriteLine($"{product.ProductName} - {product.QuantitySold} units");
            }
        }
    }
}
