using NUnit.Framework;
using AssetHub.DAO;
using AssetHub.Entity;
using AssetHub.Exceptions;
using System;

namespace AssetHubTest
{
    public class Tests
    {
        private IAssetManagementService _assetService;
        
        // Setup to declare object
        [SetUp]
        public void Setup()
        {
            _assetService = new AssetManagementServiceImpl();
        }

        // Test Case to whether adding asset is success
        [Test]
        public void Test_AddAsset_Success()
        {
            var asset = new Assets
            {
                Name = "Web Cam",
                Type = "Electronics",
                SerialNumber = "WBCM-001",
                PurchaseDate = DateTime.Now.AddMonths(-2),
                Location = "Room 101",
                Status = "Available",
                OwnerId = 101
            };

            bool result = _assetService.AddAsset(asset);
            Assert.IsTrue(result, "Asset should be added successfully.");
        }

        // Test Case to check Maintenance Perfom is success
        [Test]
        public void Test_PerformMaintenance_Success()
        {
            int assetId = 203;
            string maintenanceDate = DateTime.Now.ToString("yyyy-MM-dd");
            string description = "General Maintenance";
            double cost = 150.00;

            bool result = _assetService.PerformMaintenance(assetId, maintenanceDate, description, cost);
            Assert.IsTrue(result, "Maintenance should be performed successfully.");
        }

        // Test Case to check Reserving Asset is success
        [Test]
        public void Test_ReserveAsset_Success()
        {
            int assetId = 210; 
            int employeeId = 103; 
            string reservationDate = DateTime.Now.ToString("yyyy-MM-dd");
            string startDate = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");
            string endDate = DateTime.Now.AddDays(5).ToString("yyyy-MM-dd");

            bool result = _assetService.ReserveAsset(assetId, employeeId, reservationDate, startDate, endDate);
            Assert.IsTrue(result, "Asset should be reserved successfully.");
        }

        // Test Case to check asset not exception is properly thrown
        [Test]
        public void Test_AssetNotFoundException_When_Asset_Not_Exist()
        {
            int invalidAssetId = 9999; 
            int employeeId = 103; 
            string allocationDate = DateTime.Now.ToString("yyyy-MM-dd");

            var ex = Assert.Throws<AssetNotFoundException>(() =>
            {
                _assetService.AllocateAsset(invalidAssetId, employeeId, allocationDate);
            });

            Assert.That(ex.Message, Does.Contain("Asset ID does not exist."));
        }
    }
}