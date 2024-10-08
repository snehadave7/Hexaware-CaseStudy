using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrimeReportingSystem.Model {
    internal class LawEnforcementAgencies {

        public int AgencyId { get; set; }
        public string AgencyName{ get; set; }
        public string Jurisdiction { get; set; }
        public string AgencyContact { get; set; }

        public LawEnforcementAgencies(string AgencyName, string Jurisdiction, string AgencyContact) {
            this.AgencyName = AgencyName;
            this.Jurisdiction = Jurisdiction;
            this.AgencyContact = AgencyContact;
        }
        public LawEnforcementAgencies() {}
        
            
        

        public override string ToString() {
            return $"AgencyId: {AgencyId} AgencyName={AgencyName} Jurisdiction={Jurisdiction} AgencyContact={AgencyContact}";
        }
    }
}
