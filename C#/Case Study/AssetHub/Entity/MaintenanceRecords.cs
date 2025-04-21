using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetHub.Entity
{
    public class MaintenanceRecords
    {
        // Entity class for MaintenanceRecords
        // Attribute declaration
        private int maintenanceId;
        private int assetId;
        private DateTime maintenanceDate;
        private string description;
        private decimal cost;

        // Constructor
        public MaintenanceRecords() { }

        // Constructor with parameters
        public MaintenanceRecords(int maintenanceId, int assetId, DateTime maintenanceDate, string description, decimal cost)
        {
            this.maintenanceId = maintenanceId;
            this.assetId = assetId;
            this.maintenanceDate = maintenanceDate;
            this.description = description;
            this.cost = cost;
        }

        // Properties
        public int MaintenanceId { get => maintenanceId; set => maintenanceId = value; }
        public int AssetId { get => assetId; set => assetId = value; }
        public DateTime MaintenanceDate { get => maintenanceDate; set => maintenanceDate = value; }
        public string Description { get => description; set => description = value; }
        public decimal Cost { get => cost; set => cost = value; }
    }
}
