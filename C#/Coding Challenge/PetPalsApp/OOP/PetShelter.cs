using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetPalsApp.OOP
{
    public class PetShelter
    {
        private List<Pets> availablePets = new List<Pets>();

        public void AddPet(Pets pet)
        {
            availablePets.Add(pet);
        }

        public void RemovePet(Pets pet)
        {
            availablePets.Remove(pet);
        }

        public void ListAvailablePets()
        {
            foreach (var pet in availablePets)
            {
                Console.WriteLine(pet);
            }
        }
    }
}
