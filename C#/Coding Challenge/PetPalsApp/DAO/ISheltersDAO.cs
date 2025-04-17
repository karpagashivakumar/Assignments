using PetPalsApp.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetPalsApp.DAO
{
    public interface ISheltersDAO
    {
        // Method signatures for shelter-related operations
        void AddShelter(Shelters shelter);

        // Method to get a shelter by its ID
        List<Shelters> GetAllShelters();

        // Method to get a shelter by its ID
        void UpdateShelterLocation(int shelterId, string newLocation);

        // Method to get a shelter by its ID
        void DeleteShelter(int shelterId);
    }
}
