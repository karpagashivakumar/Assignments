using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetHub.Exceptions
{
    // Exception to which throws Asset Not Maintain if the asset has not been maintained for the past 2 years
    public class AssetNotMaintainException : Exception
    {
        public AssetNotMaintainException() : base("The asset has not been maintained for the past 2 years.")
        {
        }

        public AssetNotMaintainException(string message) : base(message)
        {
        }
    }
}
