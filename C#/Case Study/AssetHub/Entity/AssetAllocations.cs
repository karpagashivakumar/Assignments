using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetHub.Entity
{ 
    public class AssetAllocations
    {
        // Entity class for AssetAllocations
        // Attribute declaration
        private int allocationId;
        private int assetId;
        private int employeeId;
        private DateTime allocationDate;
        private DateTime? returnDate;

        // Constructor
        public AssetAllocations() { }

        // Constructor with parameters
        public AssetAllocations(int allocationId, int assetId, int employeeId, DateTime allocationDate, DateTime? returnDate)
        {
            this.allocationId = allocationId;
            this.assetId = assetId;
            this.employeeId = employeeId;
            this.allocationDate = allocationDate;
            this.returnDate = returnDate;
        }

        // Properties
        public int AllocationId { get => allocationId; set => allocationId = value; }
        public int AssetId { get => assetId; set => assetId = value; }
        public int EmployeeId { get => employeeId; set => employeeId = value; }
        public DateTime AllocationDate { get => allocationDate; set => allocationDate = value; }
        public DateTime? ReturnDate { get => returnDate; set => returnDate = value; }
    }
}
