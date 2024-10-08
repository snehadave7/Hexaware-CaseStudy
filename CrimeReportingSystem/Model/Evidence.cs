using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrimeReportingSystem.Model {
    internal class Evidence {
        public int EvidenceId{ get; set; }
        public string Description { get; set; }
        public string LocationFound { get; set; }
        public int IncidentId { get; set; }

        public Evidence() { }

        public Evidence(string Description,string LocationFound,int IncidentId)
        {
            this.Description = Description;
            this.LocationFound = LocationFound;
            this.IncidentId = IncidentId;
        }

        public override string ToString() {
            return $"EvidenceId: {EvidenceId} Decription: {Description} LocationFound: {LocationFound} IncidentId: {IncidentId}";
        }
    }
}
