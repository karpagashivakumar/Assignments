using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetPalsApp.OOP
{
    public class Cat : Pets
    {
        public string CatColor { get; set; }

        public Cat(string name, int age, string breed, string catColor) : base(name, age, breed)
        {
            CatColor = catColor;
        }

        public override string ToString()
        {
            return base.ToString() + $", Cat Color: {CatColor}";
        }
    }
}
