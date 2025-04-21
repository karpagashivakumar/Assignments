using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetHub.Entity
{
    public class Reservations
    {
        // Entity class for Reservations
        // Attribute declaration
        private int reservationId;
        private int assetId;
        private int employeeId;
        private DateTime reservationDate;
        private DateTime startDate;
        private DateTime endDate;
        private string status;

        // Constructor
        public Reservations() { }

        // Constructor with parameter
        public Reservations(int reservationId, int assetId, int employeeId, DateTime reservationDate, DateTime startDate, DateTime endDate, string status)
        {
            this.reservationId = reservationId;
            this.assetId = assetId;
            this.employeeId = employeeId;
            this.reservationDate = reservationDate;
            this.startDate = startDate;
            this.endDate = endDate;
            this.status = status;
        }

        // Properties
        public int ReservationId { get => reservationId; set => reservationId = value; }
        public int AssetId { get => assetId; set => assetId = value; }
        public int EmployeeId { get => employeeId; set => employeeId = value; }
        public DateTime ReservationDate { get => reservationDate; set => reservationDate = value; }
        public DateTime StartDate { get => startDate; set => startDate = value; }
        public DateTime EndDate { get => endDate; set => endDate = value; }
        public string Status { get => status; set => status = value; }
    }
}
