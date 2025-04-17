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
    public class PetsDAO : IPetsDAO
    {
        private readonly SqlConnection conn = DBConnUtil.GetConnection("appsettings.json");

        // Method to add a new pet
        public void AddPet(Pets pet)
        {
            string query = "INSERT INTO Pets (Name, Age, Breed, Type, AvailableForAdoption, OwnerID, ShelterID) VALUES (@Name, @Age, @Breed, @Type, @AvailableForAdoption, @OwnerID, @ShelterID)";
            using SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Name", pet.Name);
            cmd.Parameters.AddWithValue("@Age", pet.Age);
            cmd.Parameters.AddWithValue("@Breed", pet.Breed);
            cmd.Parameters.AddWithValue("@Type", pet.Type);
            cmd.Parameters.AddWithValue("@AvailableForAdoption", pet.AvailableForAdoption);
            if (pet.OwnerID.HasValue)
                cmd.Parameters.AddWithValue("@OwnerID", pet.OwnerID.Value);
            else
                cmd.Parameters.AddWithValue("@OwnerID", DBNull.Value);
            cmd.Parameters.AddWithValue("@ShelterID", pet.ShelterID);

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        // Method to get all pets
        public List<Pets> GetAllPets()
        {
            List<Pets> pets = new List<Pets>();
            string query = "SELECT * FROM Pets";
            SqlCommand cmd = new SqlCommand(query, conn);
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                pets.Add(new Pets
                {
                    PetID = (int)reader["PetID"],
                    Name = reader["Name"].ToString(),
                    Age = (int)reader["Age"],
                    Breed = reader["Breed"].ToString(),
                    Type = reader["Type"].ToString(),
                    AvailableForAdoption = (bool)reader["AvailableForAdoption"],
                    OwnerID = (int)reader["OwnerID"],
                    ShelterID = (int)reader["ShelterID"]
                });
            }
            conn.Close();
            return pets;
        }

        // Method to update pet name
        public void UpdatePetName(int petId, string newName)
        {
            string query = "UPDATE Pets SET Name = @Name WHERE PetID = @PetID";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Name", newName);
            cmd.Parameters.AddWithValue("@PetID", petId);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        // Method to delete a pet
        public void DeletePet(int petId)
        {
            string query = "DELETE FROM Pets WHERE PetID = @PetID";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@PetID", petId);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        // Method to adopt a pet
        public bool AdoptPet(int petId)
        {
            using (SqlConnection conn = DBConnUtil.GetConnection("appsettings.json"))
            {
                conn.Open();

                // Check if pet exists and is available
                string checkQuery = "SELECT AvailableForAdoption FROM Pets WHERE PetID = @PetID";
                using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn))
                {
                    checkCmd.Parameters.AddWithValue("@PetID", petId);
                    var result = checkCmd.ExecuteScalar();

                    if (result == null)
                    {
                        throw new AdoptionException("Pet with given ID does not exist.");
                    }

                    bool isAvailable = Convert.ToBoolean(result);
                    if (!isAvailable)
                    {
                        throw new AdoptionException("Pet is already adopted or not available.");
                    }
                }

                // Update the adoption status
                string updateQuery = "UPDATE Pets SET AvailableForAdoption = 0 WHERE PetID = @PetID";
                using (SqlCommand cmd = new SqlCommand(updateQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@PetID", petId);
                    int rows = cmd.ExecuteNonQuery();
                    return rows > 0;
                }
            }
        }

    }
}
