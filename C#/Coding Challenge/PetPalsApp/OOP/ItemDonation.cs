using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetPalsApp.OOP
{
    public class ItemDonation : Donations
    {
        public string ItemType { get; set; }

        public ItemDonation(string donorName, decimal amount, string itemType)
            : base(donorName, amount)
        {
            ItemType = itemType;
        }

        public override void RecordDonation()
        {
            Console.WriteLine($"Item Donation Recorded: {DonorName}, Item: {ItemType}, Value: {Amount}");
        }
    }
}
