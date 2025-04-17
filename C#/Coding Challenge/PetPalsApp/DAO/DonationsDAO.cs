using Microsoft.Data.SqlClient;
using PetPalsApp.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetPalsApp.Entity;
using static PetPalsApp.Exceptions.CustomExceptions;

namespace PetPalsApp.DAO
{
    public class DonationsDAO : IDonationsDAO
    {
        private readonly SqlConnection conn = DBConnUtil.GetConnection("appsettings.json");

        // Method to add a donation to the database
        public void AddDonation(Donations donation)
        {

            if (donation.DonationAmount < 10)
            {
                throw new InsufficientFundsException("Minimum donation amount must be $10.");
            }

            string query = "INSERT INTO Donations (DonorName, DonationType, DonationAmount, DonationItem, DonationDate) VALUES (@DonorName, @DonationType, @DonationAmount, @DonationItem, @DonationDate)";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@DonorName", donation.DonorName);
            cmd.Parameters.AddWithValue("@DonationType", donation.DonationType);
            cmd.Parameters.AddWithValue("@DonationAmount", donation.DonationAmount);
            cmd.Parameters.AddWithValue("@DonationItem", donation.DonationItem);
            cmd.Parameters.AddWithValue("@DonationDate", donation.DonationDate);

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        // Method to delete a donation by its ID
        public void DeleteDonation(int donationId)
        {
            string query = "DELETE FROM Donations WHERE DonationID = @DonationID";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@DonationID", donationId);

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        // Method to retrieve all donations from the database
        public List<Donations> GetAllDonations()
        {
            List<Donations> donations = new List<Donations>();
            string query = "SELECT * FROM Donations";
            SqlCommand cmd = new SqlCommand(query, conn);
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                donations.Add(new Donations
                {
                    DonationID = (int)reader["DonationID"],
                    DonorName = reader["DonorName"].ToString(),
                    DonationType = reader["DonationType"].ToString(),
                    DonationAmount = (decimal)reader["DonationAmount"],
                    DonationItem = reader["DonationItem"].ToString(),
                    DonationDate = (DateTime)reader["DonationDate"]
                });
            }
            conn.Close();
            return donations;
        }
    }
}
