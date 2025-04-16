using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicGadgets.Exceptions
{
    public class CustomException
    {
        public class InvalidDataException : Exception
        {
            public InvalidDataException(string message) : base(message) { }
        }

        public class InsufficientStockException : Exception
        {
            public InsufficientStockException(string message) : base(message) { }
        }

        public class IncompleteOrderException : Exception
        {
            public IncompleteOrderException(string message) : base(message) { }
        }

        public class PaymentFailedException : Exception
        {
            public PaymentFailedException(string message) : base(message) { }
        }

        public class ConcurrencyException : Exception
        {
            public ConcurrencyException(string message) : base(message) { }
        }

        public class AuthenticationException : Exception
        {
            public AuthenticationException(string message) : base(message) { }
        }

        public class AuthorizationException : Exception
        {
            public AuthorizationException(string message) : base(message) { }
        }
    }
}
