using CrimeReportingSystem.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrimeReportingSystem.Repository {
    internal interface IEvidenceRepository {

        public List<Evidence> GetEvidenceRelatedToAnIncident(int incidentId);

        public bool DeleteEvidence(int evidenceId);
        public bool UpdateEvidenceOnLocation(string location,int evidenceId);
        public bool UpdateEvidenceOnDescription(string description,int evidenceId);

        public bool CreateEvidence(Evidence evidence);
        
    }
}
