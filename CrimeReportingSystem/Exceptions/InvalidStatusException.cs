using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrimeReportingSystem.Exceptions {
    internal class InvalidStatusException:ApplicationException {
        public InvalidStatusException() { }

        public InvalidStatusException(string message) : base(message) { }
        
            
        

    }
}
