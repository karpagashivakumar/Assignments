using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PetPalsApp.Exceptions.CustomExceptions;

namespace PetPalsApp.Entity
{

    // 1. Pets Class Properties
    public class Pets
    {
        public int PetID { get; set; }         
        public string Name { get; set; }

        private int age;
        public int Age
        {
            get { return age; }
            set
            {
                if (value <= 0)
                {
                    throw new InvalidPetAgeException();
                }
                age = value;
            }
        }
        public string Breed { get; set; }         
        public string Type { get; set; }         
        public bool AvailableForAdoption { get; set; }         
        public int? OwnerID { get; set; }         
        public int ShelterID { get; set; }
    }
}
