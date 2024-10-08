using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrimeReportingSystem.Model {
    internal class VictimIncidentMap {
        public int MappingId { get; set; }
        public int VictimId { get; set; }
        public int IncidentId { get; set; }

        public VictimIncidentMap() { }

        public VictimIncidentMap(int VictimId, int IncidentId) {
            this.VictimId = VictimId;
            this.IncidentId = IncidentId;
        }

        public override string ToString() {
            return $"MappingId: {MappingId} VictimId: {VictimId} IncidentID: {IncidentId}";
        }
    }
}
