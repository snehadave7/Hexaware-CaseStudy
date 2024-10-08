using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrimeReportingSystem.Exceptions {
    internal class IDNotFoundException : ApplicationException {
        public IDNotFoundException(string message): base (message) { }
        
            
        
    }
}


