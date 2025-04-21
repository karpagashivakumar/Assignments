using AssetHub.DAO;
using AssetHub.Exceptions;
using AssetHub.Entity;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetHub.Main
{
    public class AssetManagementApp
    {
        // Main Class Menu Driven Method
        public static void Main(string[] args)
        {
            // Creating object
            IAssetManagementService assetService = new AssetManagementServiceImpl();

            while (true)
            {
                // Output as Menu
                Console.Clear();
                Console.WriteLine("Asset Management System");
                Console.WriteLine("1. Add Asset");
                Console.WriteLine("2. Update Asset");
                Console.WriteLine("3. Delete Asset");
                Console.WriteLine("4. Allocate Asset");
                Console.WriteLine("5. Deallocate Asset");
                Console.WriteLine("6. Perform Maintenance");
                Console.WriteLine("7. Reserve Asset");
                Console.WriteLine("8. Exit");
                Console.Write("Choose an operation: ");
                string choice = Console.ReadLine();

                try
                {
                    // Switch case to implement each menu
                    switch (choice)
                    {
                        case "1":
                            AddAsset(assetService);
                            break;
                        case "2":
                            UpdateAsset(assetService);
                            break;
                        case "3":
                            DeleteAsset(assetService);
                            break;
                        case "4":
                            AllocateAsset(assetService);
                            break;
                        case "5":
                            DeallocateAsset(assetService);
                            break;
                        case "6":
                            PerformMaintenance(assetService);
                            break;
                        case "7":
                            ReserveAsset(assetService);
                            break;
                        case "8":
                            Console.WriteLine("Exiting the application...");
                            return;
                        default:
                            Console.WriteLine("Invalid choice! Please choose a valid operation.");
                            break;
                    }
                }
                catch (AssetNotFoundException ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
                catch (AssetNotMaintainException ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Unexpected error: {ex.Message}");
                }

                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
        }

        // Method to add asset
        private static void AddAsset(IAssetManagementService assetService)
        {
            Console.WriteLine("\nAdding a new Asset...");

            Console.Write("Enter Asset Name: ");
            string name = Console.ReadLine();

            Console.Write("Enter Asset Type: ");
            string type = Console.ReadLine();

            Console.Write("Enter Asset Serial Number: ");
            string serialNumber = Console.ReadLine();

            Console.Write("Enter Purchase Date (yyyy-mm-dd): ");
            DateTime purchaseDate = DateTime.ParseExact(Console.ReadLine(), "yyyy-MM-dd", CultureInfo.InvariantCulture);

            Console.Write("Enter Asset Location: ");
            string location = Console.ReadLine();

            Console.Write("Enter Asset Status: ");
            string status = Console.ReadLine();

            Console.Write("Enter Owner Employee ID: ");
            int ownerId = int.Parse(Console.ReadLine());

            Assets newAsset = new Assets
            {
                Name = name,
                Type = type,
                SerialNumber = serialNumber,
                PurchaseDate = purchaseDate,
                Location = location,
                Status = status,
                OwnerId = ownerId
            };

            bool result = assetService.AddAsset(newAsset);
            Console.WriteLine(result ? "Asset added successfully!" : "Failed to add asset.");
        }

        // Method to update asset
        private static void UpdateAsset(IAssetManagementService assetService)
        {
            Console.WriteLine("\nUpdating an Asset...");

            Console.Write("Enter Asset ID to update: ");
            int assetId = int.Parse(Console.ReadLine());

            Console.Write("Enter new Asset Name: ");
            string name = Console.ReadLine();

            Console.Write("Enter new Asset Type: ");
            string type = Console.ReadLine();

            Console.Write("Enter new Asset Serial Number: ");
            string serialNumber = Console.ReadLine();

            Console.Write("Enter new Asset Location: ");
            string location = Console.ReadLine();

            Console.Write("Enter new Asset Status: ");
            string status = Console.ReadLine();

            Assets updatedAsset = new Assets
            {
                AssetId = assetId,
                Name = name,
                Type = type,
                SerialNumber = serialNumber,
                Location = location,
                Status = status
            };

            bool result = assetService.UpdateAsset(updatedAsset);
            Console.WriteLine(result ? "Asset updated successfully!" : "Failed to update asset.");
        }

        // Method to delete asset
        private static void DeleteAsset(IAssetManagementService assetService)
        {
            Console.WriteLine("\nDeleting an Asset...");

            Console.Write("Enter Asset ID to delete: ");
            int assetId = int.Parse(Console.ReadLine());

            // Asset not found exception
            try
            {
                bool result = assetService.DeleteAsset(assetId);
                Console.WriteLine(result ? "Asset deleted successfully!" : "Failed to delete asset.");
            }
            catch (AssetNotFoundException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        // Method to allocate asset
        private static void AllocateAsset(IAssetManagementService assetService)
        {
            Console.WriteLine("\nAllocating an Asset...");

            Console.Write("Enter Asset ID: ");
            int assetId = int.Parse(Console.ReadLine());

            Console.Write("Enter Employee ID: ");
            int employeeId = int.Parse(Console.ReadLine());

            Console.Write("Enter Allocation Date (yyyy-mm-dd): ");
            string allocationDate = Console.ReadLine();

            // Asset not found exception
            // Asset Not Maintain exception
            try
            {
                bool result = assetService.AllocateAsset(assetId, employeeId, allocationDate);
                Console.WriteLine(result ? "Asset allocated successfully!" : "Failed to allocate asset.");
            }
            catch (AssetNotFoundException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            catch (AssetNotMaintainException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        // Method to deallocate asset
        private static void DeallocateAsset(IAssetManagementService assetService)
        {
            Console.WriteLine("\nDeallocating an Asset...");

            Console.Write("Enter Asset ID: ");
            int assetId = int.Parse(Console.ReadLine());

            Console.Write("Enter Employee ID: ");
            int employeeId = int.Parse(Console.ReadLine());

            Console.Write("Enter Return Date (yyyy-mm-dd): ");
            string returnDate = Console.ReadLine();

            bool result = assetService.DeallocateAsset(assetId, employeeId, returnDate);
            Console.WriteLine(result ? "Asset deallocated successfully!" : "Failed to deallocate asset.");
        }

        // Method to perform maintenance
        private static void PerformMaintenance(IAssetManagementService assetService)
        {
            Console.WriteLine("\nPerforming Maintenance...");

            Console.Write("Enter Asset ID: ");
            int assetId = int.Parse(Console.ReadLine());

            Console.Write("Enter Maintenance Date (yyyy-mm-dd): ");
            string maintenanceDate = Console.ReadLine();

            Console.Write("Enter Maintenance Description: ");
            string description = Console.ReadLine();

            Console.Write("Enter Maintenance Cost: ");
            double cost = double.Parse(Console.ReadLine());

            bool result = assetService.PerformMaintenance(assetId, maintenanceDate, description, cost);
            Console.WriteLine(result ? "Maintenance recorded successfully!" : "Failed to record maintenance.");
        }
        
        // Method to reserve asset
        private static void ReserveAsset(IAssetManagementService assetService)
        {
            Console.WriteLine("\nReserving an Asset...");

            Console.Write("Enter Asset ID: ");
            int assetId = int.Parse(Console.ReadLine());

            Console.Write("Enter Employee ID: ");
            int employeeId = int.Parse(Console.ReadLine());

            Console.Write("Enter Reservation Date (yyyy-mm-dd): ");
            string reservationDate = Console.ReadLine();

            Console.Write("Enter Start Date (yyyy-mm-dd): ");
            string startDate = Console.ReadLine();

            Console.Write("Enter End Date (yyyy-mm-dd): ");
            string endDate = Console.ReadLine();

            bool result = assetService.ReserveAsset(assetId, employeeId, reservationDate, startDate, endDate);
            Console.WriteLine(result ? "Asset reserved successfully!" : "Failed to reserve asset.");
        }
    }
}
