using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetPalsApp.Entity;

namespace PetPalsApp.DAO
{
    public interface IDonationsDAO
    {
        // Interface for Donations Data Access Object
        void AddDonation(Donations donation);

        // Method to delete a donation by its ID
        void DeleteDonation(int donationId);

        // Method to retrieve all donations
        List<Donations> GetAllDonations();
    }
}
