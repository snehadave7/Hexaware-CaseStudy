using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrimeReportingSystem.Model {
    internal class SuspectIncidentMap {
        public int MappingId { get; set; }
        public int SuspectId { get; set; }
        public int IncidentId { get; set; }

        public SuspectIncidentMap() { }

        public SuspectIncidentMap(int SuspectId,int IncidentId)
        {
            this.SuspectId = SuspectId;
            this.IncidentId = IncidentId;
        }

        public override string ToString() {
            return $"MappingId: {MappingId} SuspectId: {SuspectId} IncidentID: {IncidentId}";
        }
    }
}
