using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetHub.Exceptions
{
    // Exception to which throws Asset Not Found if user gives unmatched asset id
    public class AssetNotFoundException : Exception
    {
        public AssetNotFoundException() : base("Asset not found in the database.")
        {
        }

        public AssetNotFoundException(string message) : base(message)
        {
        }
    }
}
