using CrimeReportingSystem.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrimeReportingSystem.Repository {
    internal interface ICaseRepository {

        public bool CreateCase(Cases cases,int incidentId);

        public List<Cases> GetAllCases();
        public bool GetAllIncidentForACase(int caseId);
        public Cases GetCaseDetailsForACase(int caseId); 
        public bool UpdateCase(int caseId,string description);


        

    }
}
