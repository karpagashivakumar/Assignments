using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetPalsApp.OOP
{
    public class CashDonation : Donations
    {
        public DateTime DonationDate { get; set; }

        public CashDonation(string donorName, decimal amount, DateTime donationDate)
            : base(donorName, amount)
        {
            DonationDate = donationDate;
        }

        public override void RecordDonation()
        {
            Console.WriteLine($"Cash Donation Recorded: {DonorName}, Amount: {Amount}, Date: {DonationDate}");
        }
    }
}
