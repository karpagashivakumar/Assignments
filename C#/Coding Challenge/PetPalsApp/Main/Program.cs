using PetPalsApp.Entity;
using PetPalsApp.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetPalsApp.DAO;
using static PetPalsApp.Exceptions.CustomExceptions;
//using PetPalsApp.OOP;

namespace PetPalsApp.Main
{
    public class Program
    {
        // Main method to run the application
        static void Main(string[] args)
        {
            int choice = 0;
            do
            {
                // Displaying the menu options
                Console.WriteLine("Welcome to PetPals Platform!");
                Console.WriteLine("\n--- PetPals Platform Menu ---");
                Console.WriteLine("1. Add Pet");
                Console.WriteLine("2. View All Pets");
                Console.WriteLine("3. Update Pet Name");
                Console.WriteLine("4. Delete Pet");
                Console.WriteLine("5. Add Shelter");
                Console.WriteLine("6. View All Shelters");
                Console.WriteLine("7. Add Donation");
                Console.WriteLine("8. Delete Donation");
                Console.WriteLine("9. Adopt a Pet");
                Console.WriteLine("10. Exit");

                Console.Write("Enter your choice: ");
                choice = int.Parse(Console.ReadLine());

                // Handling user input and performing actions based on the choice
                switch (choice)
                {
                    // Adding a new pet
                    case 1:
                        Console.Write("Enter pet name: ");
                        string petName = Console.ReadLine();

                        Console.Write("Enter age: ");
                        int age = int.Parse(Console.ReadLine());
                        try
                        {
                            Pets pet = new Pets { Age = age }; // This will trigger exception if age <= 0
                        }
                        catch (InvalidPetAgeException ex)
                        {
                            Console.WriteLine($"Error: {ex.Message}");
                            break;
                        }

                        Console.Write("Enter breed: ");
                        string breed = Console.ReadLine();

                        Console.Write("Enter type (Dog/Cat/etc.): ");
                        string type = Console.ReadLine();

                        Console.Write("Enter owner ID: ");
                        int ownerId = int.Parse(Console.ReadLine());

                        Console.Write("Enter shelter ID: ");
                        int shelterId = int.Parse(Console.ReadLine());

                        Pets newPet = new Pets { Name = petName, Age = age, Breed = breed, Type = type, AvailableForAdoption = true, OwnerID = ownerId, ShelterID = shelterId };
                        new PetsDAO().AddPet(newPet);
                        Console.WriteLine("Pet added successfully.");
                        break;

                    // Viewing all pets
                    case 2:
                        var pets = new PetsDAO().GetAllPets();
                        foreach (var pet in pets)
                            Console.WriteLine($"{pet.PetID} - {pet.Name}, Age: {pet.Age}, Breed: {pet.Breed}");
                        break;

                    // Updating pet name
                    case 3:
                        Console.Write("Enter Pet ID to update: ");
                        int petIdToUpdate = int.Parse(Console.ReadLine());
                        Console.Write("Enter new pet name: ");
                        string updatedName = Console.ReadLine();
                        new PetsDAO().UpdatePetName(petIdToUpdate, updatedName);
                        Console.WriteLine("Pet name updated.");
                        break;

                    // Deleting a pet
                    case 4:
                        Console.Write("Enter Pet ID to delete: ");
                        int petIdToDelete = int.Parse(Console.ReadLine());
                        new PetsDAO().DeletePet(petIdToDelete);
                        Console.WriteLine("Pet deleted successfully.");
                        break;

                    // Adding a new shelter
                    case 5:
                        Console.Write("Enter shelter name: ");
                        string shelterName = Console.ReadLine();
                        Console.Write("Enter shelter location: ");
                        string location = Console.ReadLine();
                        Shelters newShelter = new Shelters { Name = shelterName, Location = location };
                        new SheltersDAO().AddShelter(newShelter);
                        Console.WriteLine("Shelter added.");
                        break;

                    // Viewing all shelters
                    case 6:
                        var shelters = new SheltersDAO().GetAllShelters();
                        foreach (var s in shelters)
                            Console.WriteLine($"{s.ShelterID} - {s.Name}, Location: {s.Location}");
                        break;

                    // Adding a donation
                    case 7:
                        Console.Write("Enter donor name: ");
                        string donor = Console.ReadLine();
                        Console.Write("Enter donation amount: ");
                        decimal amount = decimal.Parse(Console.ReadLine());
                        try
                        {
                            if (amount < 10)
                                throw new InsufficientFundsException();
                        }
                        catch (InsufficientFundsException ex)
                        {
                            Console.WriteLine($"Error: {ex.Message}");
                        }

                        Donations donation = new Donations { DonorName = donor, DonationType = "Cash", DonationAmount = amount, DonationDate = DateTime.Now };
                        new DonationsDAO().AddDonation(donation);
                        Console.WriteLine("Donation recorded.");
                        break;

                    // Deleting a donation
                    case 8:
                        Console.Write("Enter Donation ID to delete: ");
                        int donationId = int.Parse(Console.ReadLine());
                        new DonationsDAO().DeleteDonation(donationId);
                        Console.WriteLine("Donation deleted.");
                        break;

                    // Adopting a pet
                    case 9:
                        try
                        {
                            Console.Write("Enter the Pet ID to adopt: ");
                            int petId = int.Parse(Console.ReadLine());

                            PetsDAO petsDAO = new PetsDAO();
                            bool success = petsDAO.AdoptPet(petId);

                            if (success)
                            {
                                Console.WriteLine("Pet adopted successfully!");
                            }
                            else
                            {
                                Console.WriteLine("Adoption failed. Pet not found or already adopted.");
                            }
                        }
                        catch (AdoptionException ex)
                        {
                            Console.WriteLine($"Adoption Error: {ex.Message}");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Unexpected Error: {ex.Message}");
                        }
                        break;

                    // Exiting the application
                    case 10:
                        Console.WriteLine("Exiting...");
                        break;

                    // Handling invalid choice
                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
            } while (choice != 10);
        }
    }
}
