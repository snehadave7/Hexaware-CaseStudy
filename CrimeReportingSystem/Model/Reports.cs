using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrimeReportingSystem.Model {
    internal class Reports {
        public int ReportId  { get; set; }
        public int IncidentId { get; set; }
        public int ReportingOfficerID { get; set; }
        public DateTime ReportDate { get; set; }
        public string ReportDetails { get; set; }
        public string Status { get; set; }

        public Reports() { }

        public Reports(int IncidentId, int ReportingOfficerID, DateTime ReportDate, string ReportDetails, string Status) {
            this.IncidentId = IncidentId;
            this.ReportingOfficerID = ReportingOfficerID;
            this.ReportDate = ReportDate;
            this.ReportDetails = ReportDetails;
            this.Status = Status;
        }


        public override string ToString() {
            return $"ReportId: {ReportId} IncidentId:{IncidentId} ReportingOfficerID:{ReportingOfficerID}  ReportDate: {ReportDate} ReportDetails: {ReportDetails} Status: {Status}";
        }






    }
}
