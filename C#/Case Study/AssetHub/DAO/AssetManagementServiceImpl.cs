using AssetHub.Entity;
using AssetHub.Exceptions;
using AssetHub.Util;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetHub.DAO
{
    // Service Class implements interface class
    public class AssetManagementServiceImpl : IAssetManagementService
    {
        private readonly string configFile = "appsettings.json";

        // Service method for adding asset
        public bool AddAsset(Assets asset)
        {
            using (SqlConnection con = DBConnUtil.GetConnection(configFile))
            {
                con.Open();
                string query = "INSERT INTO Assets (Name, Type, Serial_Number, Purchase_Date, Location, Status, Owner_id) " +
                               "VALUES (@Name, @Type, @SerialNumber, @PurchaseDate, @Location, @Status, @OwnerId)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Name", asset.Name);
                cmd.Parameters.AddWithValue("@Type", asset.Type);
                cmd.Parameters.AddWithValue("@SerialNumber", asset.SerialNumber);
                cmd.Parameters.AddWithValue("@PurchaseDate", asset.PurchaseDate);
                cmd.Parameters.AddWithValue("@Location", asset.Location);
                cmd.Parameters.AddWithValue("@Status", asset.Status);
                cmd.Parameters.AddWithValue("@OwnerId", asset.OwnerId);

                return cmd.ExecuteNonQuery() > 0;
            }
        }

        // Service method to update asset
        public bool UpdateAsset(Assets asset)
        {
            using (SqlConnection con = DBConnUtil.GetConnection(configFile))
            {
                con.Open();
                string query = "UPDATE Assets SET Name=@Name, Type=@Type, Serial_Number=@SerialNumber, Purchase_Date=@PurchaseDate, " +
                               "Location=@Location, Status=@Status, Owner_id=@OwnerId WHERE Asset_id=@AssetId";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Name", asset.Name);
                cmd.Parameters.AddWithValue("@Type", asset.Type);
                cmd.Parameters.AddWithValue("@SerialNumber", asset.SerialNumber);
                cmd.Parameters.AddWithValue("@PurchaseDate", asset.PurchaseDate);
                cmd.Parameters.AddWithValue("@Location", asset.Location);
                cmd.Parameters.AddWithValue("@Status", asset.Status);
                cmd.Parameters.AddWithValue("@OwnerId", asset.OwnerId);
                cmd.Parameters.AddWithValue("@AssetId", asset.AssetId);

                int rows = cmd.ExecuteNonQuery();
                if (rows == 0)
                    throw new AssetNotFoundException("Asset not found for update.");
                return true;
            }
        }

        // Service method to delete asset
        public bool DeleteAsset(int assetId)
        {
            ValidateAssetExistence(assetId);
            using (SqlConnection con = DBConnUtil.GetConnection(configFile))
            {
                con.Open();
                string query = "DELETE FROM Assets WHERE Asset_id=@AssetId";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@AssetId", assetId);

                int rows = cmd.ExecuteNonQuery();
                if (rows == 0)
                    throw new AssetNotFoundException("Asset not found to delete.");
                return true;
            }
        }

        // Service method to allocate asset
        public bool AllocateAsset(int assetId, int employeeId, string allocationDate)
        {
            ValidateAssetExistence(assetId);
            ValidateAssetMaintenance(assetId);

            using (SqlConnection con = DBConnUtil.GetConnection(configFile))
            {
                con.Open();
                string query = "INSERT INTO Asset_Allocations (Asset_id, Employee_id, Allocation_Date) " +
                               "VALUES (@AssetId, @EmployeeId, @AllocationDate)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@AssetId", assetId);
                cmd.Parameters.AddWithValue("@EmployeeId", employeeId);
                cmd.Parameters.AddWithValue("@AllocationDate", DateTime.Parse(allocationDate));

                return cmd.ExecuteNonQuery() > 0;
            }
        }

        // Service method to deallocate asset
        public bool DeallocateAsset(int assetId, int employeeId, string returnDate)
        {
            using (SqlConnection con = DBConnUtil.GetConnection(configFile))
            {
                con.Open();
                string query = "UPDATE Asset_Allocations SET Return_Date=@ReturnDate " +
                               "WHERE Asset_id=@AssetId AND Employee_id=@EmployeeId";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@ReturnDate", DateTime.Parse(returnDate));
                cmd.Parameters.AddWithValue("@AssetId", assetId);
                cmd.Parameters.AddWithValue("@EmployeeId", employeeId);

                int rows = cmd.ExecuteNonQuery();
                if (rows == 0)
                    throw new AssetNotFoundException("Asset not found for deallocation.");
                return true;
            }
        }

        // Service method to perform maintenance
        public bool PerformMaintenance(int assetId, string maintenanceDate, string description, double cost)
        {
            ValidateAssetExistence(assetId);

            using (SqlConnection con = DBConnUtil.GetConnection(configFile))
            {
                con.Open();
                string query = "INSERT INTO Maintenance_Records (Asset_id, Maintenance_Date, Description, Cost) " +
                               "VALUES (@AssetId, @MaintenanceDate, @Description, @Cost)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@AssetId", assetId);
                cmd.Parameters.AddWithValue("@MaintenanceDate", DateTime.Parse(maintenanceDate));
                cmd.Parameters.AddWithValue("@Description", description);
                cmd.Parameters.AddWithValue("@Cost", cost);

                return cmd.ExecuteNonQuery() > 0;
            }
        }

        // Service method to reserve asset
        public bool ReserveAsset(int assetId, int employeeId, string reservationDate, string startDate, string endDate)
        {
            ValidateAssetExistence(assetId);

            using (SqlConnection con = DBConnUtil.GetConnection(configFile))
            {
                con.Open();
                string query = "INSERT INTO Reservations (Asset_id, Employee_id, Reservation_Date, Start_Date, End_Date, Status) " +
                               "VALUES (@AssetId, @EmployeeId, @ReservationDate, @StartDate, @EndDate, 'Pending')";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@AssetId", assetId);
                cmd.Parameters.AddWithValue("@EmployeeId", employeeId);
                cmd.Parameters.AddWithValue("@ReservationDate", DateTime.Parse(reservationDate));
                cmd.Parameters.AddWithValue("@StartDate", DateTime.Parse(startDate));
                cmd.Parameters.AddWithValue("@EndDate", DateTime.Parse(endDate));

                return cmd.ExecuteNonQuery() > 0;
            }
        }

        // Service mrthod to withdraw reservation
        public bool WithdrawReservation(int reservationId)
        {
            using (SqlConnection con = DBConnUtil.GetConnection(configFile))
            {
                con.Open();
                string query = "UPDATE Reservations SET Status='Canceled' WHERE Reservation_id=@ReservationId";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@ReservationId", reservationId);

                int rows = cmd.ExecuteNonQuery();
                if (rows == 0)
                    throw new AssetNotFoundException("Reservation ID not found.");
                return true;
            }
        }

        // Method to check asset existence and throws the exception if not found
        private void ValidateAssetExistence(int assetId)
        {
            using (SqlConnection con = DBConnUtil.GetConnection(configFile))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Assets WHERE Asset_id=@AssetId", con);
                cmd.Parameters.AddWithValue("@AssetId", assetId);
                int count = (int)cmd.ExecuteScalar();
                if (count == 0)
                    throw new AssetNotFoundException("Asset ID does not exist.");
            }
        }

        // Method to check asset maintenance and throws exception if not maintained
        private void ValidateAssetMaintenance(int assetId)
        {
            using (SqlConnection con = DBConnUtil.GetConnection(configFile))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT MAX(Maintenance_Date) FROM Maintenance_Records WHERE Asset_id=@AssetId", con);
                cmd.Parameters.AddWithValue("@AssetId", assetId);
                object result = cmd.ExecuteScalar();

                if (result == DBNull.Value)
                    throw new AssetNotMaintainException("Asset has never been maintained.");

                DateTime lastMaintenance = Convert.ToDateTime(result);
                if ((DateTime.Now - lastMaintenance).TotalDays > 730)
                    throw new AssetNotMaintainException("Asset hasn't been maintained for over 2 years.");
            }
        }
    }
}
