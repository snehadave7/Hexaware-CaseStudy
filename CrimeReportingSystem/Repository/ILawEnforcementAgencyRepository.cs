using CrimeReportingSystem.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrimeReportingSystem.Repository {
    internal interface ILawEnforcementAgencyRepository {

        public bool AddNewAgency(LawEnforcementAgencies agency); 

        public List<LawEnforcementAgencies> GetAllAgencies();

        public bool DeleteAgency(int AgencyId);

        public bool UpdateAgencyName(int agencyId,string newValue); 
        public bool UpdateAgencyJurisdiction(int agencyId, string newValue);
        public bool UpdateAgencyContact(int agencyId, string newValue);

    }
}
