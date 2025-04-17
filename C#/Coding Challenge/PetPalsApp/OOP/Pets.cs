using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetPalsApp.OOP
{
    public class Pets
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Breed { get; set; }

        public Pets(string name, int age, string breed)
        {
            Name = name;
            Age = age;
            Breed = breed;
        }

        public override string ToString()
        {
            return $"Pet Name: {Name}, Age: {Age}, Breed: {Breed}";
        }
    }
}
