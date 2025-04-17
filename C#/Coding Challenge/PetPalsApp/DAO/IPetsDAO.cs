using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetPalsApp.Entity;

namespace PetPalsApp.DAO
{
    public interface IPetsDAO
    {
        /// Method to add a new pet
        void AddPet(Pets pet);

        /// Method to get all pets
        List<Pets> GetAllPets();

        /// Method to update pet name
        void UpdatePetName(int petId, string newName);

        /// Method to delete a pet
        void DeletePet(int petId);

        /// Method to add a donation
        bool AdoptPet(int petId);
    }
}
