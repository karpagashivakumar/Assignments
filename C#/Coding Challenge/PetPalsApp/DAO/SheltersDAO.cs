using Microsoft.Data.SqlClient;
using PetPalsApp.Entity;
using PetPalsApp.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetPalsApp.DAO
{
    public class SheltersDAO : ISheltersDAO
    {
        private readonly SqlConnection conn = DBConnUtil.GetConnection("appsettings.json");

        // Method to add a new shelter
        public void AddShelter(Shelters shelter)
        {
            string query = "INSERT INTO Shelters (Name, Location) VALUES (@Name, @Location)";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Name", shelter.Name);
            cmd.Parameters.AddWithValue("@Location", shelter.Location);

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        // Method to get a shelter by its ID
        public List<Shelters> GetAllShelters()
        {
            List<Shelters> shelters = new List<Shelters>();
            string query = "SELECT * FROM Shelters";
            SqlCommand cmd = new SqlCommand(query, conn);
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                shelters.Add(new Shelters
                {
                    ShelterID = (int)reader["ShelterID"],
                    Name = reader["Name"].ToString(),
                    Location = reader["Location"].ToString()
                });
            }
            conn.Close();
            return shelters;
        }

        // Method to update a shelter's location
        public void UpdateShelterLocation(int shelterId, string newLocation)
        {
            string query = "UPDATE Shelters SET Location = @Location WHERE ShelterID = @ShelterID";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Location", newLocation);
            cmd.Parameters.AddWithValue("@ShelterID", shelterId);

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        // Method to delete a shelter
        public void DeleteShelter(int shelterId)
        {
            string query = "DELETE FROM Shelters WHERE ShelterID = @ShelterID";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@ShelterID", shelterId);

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }
    }
}
