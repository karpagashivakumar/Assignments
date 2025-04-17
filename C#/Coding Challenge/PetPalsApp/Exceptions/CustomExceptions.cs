using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetPalsApp.Exceptions
{
    public class CustomExceptions
    {
        // 1. Invalid Pet Age Handling
        public class InvalidPetAgeException : System.Exception
        {
            public InvalidPetAgeException() : base("Invalid pet age. Age must be a positive integer.") { }
            //public InvalidPetAgeException(string message) : base(message) { }
        }

        // 2. Null Reference Exception Handling
        public class PetPropertyNullException : System.Exception
        {
            public PetPropertyNullException() : base("Pet property is null or missing.") { }
            //public PetPropertyNullException(string message) : base(message) { }
        }

        // 3. Insufficient Funds Exception
        public class InsufficientFundsException : System.Exception
        {
            public InsufficientFundsException() : base("Donation amount is below the minimum allowed.") { }
            public InsufficientFundsException(string message) : base(message) { }
        }

        // 4. File Handling Exception
        public class PetFileReadException : IOException
        {
            public PetFileReadException() : base("An error occurred while reading the pet data file.") { }
            //public PetFileReadException(string message) : base(message) { }
        }

        // 5. Custom Exception for Adoption Errors
        public class AdoptionException : System.Exception
        {
            public AdoptionException() : base("An error occurred during the adoption process.") { }
            public AdoptionException(string message) : base(message) { }
        }
    }
}
