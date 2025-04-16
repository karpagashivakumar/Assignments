using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicGadgets.DAO
{
    public interface ISalesReportDao
    {
        decimal GetTotalSales();
        List<(DateTime Date, decimal TotalSales)> GetDailySalesReport();
        List<(string ProductName, int QuantitySold)> GetTopSellingProducts(int topN);
        void GenerateReport();
    }
}
