using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrimeReportingSystem.Model {
    internal class Cases {
        public List<Incidents> incidents=new List<Incidents>();
        public int CaseId { get; set; }
        public string CaseDescription { get; set; }
        public DateTime Date { get; set; }

        public Cases(string CaseDescription,DateTime Date)
        {
            this.CaseDescription = CaseDescription;
            this.Date = Date;
        }
        public Cases() { }

        public Cases(int caseId, string CaseDescription, DateTime Date,List<Incidents> incidents) {
            this.CaseId = caseId;
            this.CaseDescription = CaseDescription;
            this.Date = Date;
            this.incidents = incidents;

        }
        

        public override string ToString() {
            return $"CaseId: {CaseId} CaseDescription: {CaseDescription} DateOfCase: {Date}";
        }

    }
}
