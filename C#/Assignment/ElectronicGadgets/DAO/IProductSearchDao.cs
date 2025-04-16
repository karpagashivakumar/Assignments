using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicGadgets.DAO
{
    public interface IProductSearchDao
    {
        List<string> SearchProductsByName(string name);
        List<string> SearchProductsByCategory(string category);
        List<string> GetRecommendedProducts(int topN);
    }
}
