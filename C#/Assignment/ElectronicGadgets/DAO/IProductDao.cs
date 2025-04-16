using ElectronicGadgets.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicGadgets.DAO
{
    public interface IProductDao
    {
        void AddProduct(Products product);
        void UpdateProduct(Products product);
        void DeleteProduct(int productId);
        Products GetProductById(int productId);
        List<Products> GetAllProducts();
        List<Products> SearchProducts(string keyword);
    }
}
