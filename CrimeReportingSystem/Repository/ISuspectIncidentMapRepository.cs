using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrimeReportingSystem.Repository {
    internal interface ISuspectIncidentMapRepository{
        public bool CreateSuspectIncidentMap(int suspectid,int incidentid);
        
    }
}

