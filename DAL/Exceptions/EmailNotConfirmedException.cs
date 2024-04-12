using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Exceptions
{
    public class EmailNotConfirmedException : Exception
    {
        public EmailNotConfirmedException(string message) : base(message)
        {
        }

        public EmailNotConfirmedException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }

}
