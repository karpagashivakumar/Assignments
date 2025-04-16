using ElectronicGadgets.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicGadgets.DAO
{
    public interface IInventoryDao
    {
        void AddToInventory(int productId, int quantity);
        void AddNewProductToInventory(Products product, int quantity);
        void UpdateProductQuantity(int productId, int newQuantity);
        void RemoveDiscontinuedProduct(int productId);
        List<Inventory> GetAllInventoryItems();
    }
}
