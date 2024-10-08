using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrimeReportingSystem.Exceptions {
    internal class InvalidDateFormat:ApplicationException {
        public InvalidDateFormat() {}
        public InvalidDateFormat(string message) : base(message) {}
            
            
        


    }
}
