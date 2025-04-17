using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetPalsApp.OOP
{
    public abstract class Donations
    {
        public string DonorName { get; set; }
        public decimal Amount { get; set; }

        public Donations(string donorName, decimal amount)
        {
            DonorName = donorName;
            Amount = amount;
        }

        public abstract void RecordDonation();
    }
}
