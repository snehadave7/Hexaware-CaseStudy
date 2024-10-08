using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrimeReportingSystem.Model {
    internal class CaseIncidentMap {
        public int MappingId { get; set; }

        public int IncidentId { get; set; }
        public int CaseId { get; set; }
        public CaseIncidentMap() { }
        
            
        
        public CaseIncidentMap(int CaseId, int IncidentId) {
            this.CaseId = CaseId;
            this.IncidentId = IncidentId;
        }

        public override string ToString() {
            return $"MappingId: {MappingId} CaseId: {CaseId} IncidentID: {IncidentId}";
        }
    }
}
