using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetPalsApp.OOP
{
    public class AdoptionEvents
    {
        private List<IAdoptable> Participants = new List<IAdoptable>();

        public void RegisterParticipant(IAdoptable participant)
        {
            Participants.Add(participant);
        }

        public void HostEvent()
        {
            Console.WriteLine("Adoption Event Started!");
            foreach (var participant in Participants)
            {
                participant.Adopt();
            }
            Console.WriteLine("Adoption Event Ended!");
        }
    }
}
