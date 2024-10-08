using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrimeReportingSystem.Model {
    internal class Incidents {

        public int IncidentId { get; set; }
        public string IncidentType { get; set; }
        public DateTime IncidentDate { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public int AgencyId { get; set; }
        public string Location { get; set; }


        public Incidents() { }

        public Incidents(string IncidentType, DateTime IncidentDate, string Description,int AgencyId,string Location) {
            this.IncidentType = IncidentType;
            this.IncidentDate = IncidentDate;
            this.Description = Description;
            this.AgencyId = AgencyId;
            this.Location = Location;
        }

        public override string ToString() {
            return $"IncidentId: {IncidentId} IncidentType: {IncidentType} IncidentDate: {IncidentDate} Description: {Description} Status: {Status} AgencyId: {AgencyId} Location: {Location}";
        }


    }
}
