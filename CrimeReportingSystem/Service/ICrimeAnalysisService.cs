using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrimeReportingSystem.Service {
    internal interface ICrimeAnalysisService {
        public void RegisterIncident();
        public void RegisterVictim();
        public void GetIncidents();
        public void DeleteIncident();
        public void UpdateIncidentType();
        public void UpdateIncidentDate();
        public void UpdateIncidentDescription();
        public void RegisterSuspect();
        public void ViewStatus();
        public void CreateAgency();
        public void GetAgency();
        public void DeleteAgency();
        public void AddOfficer();
        public void ViewReports();
        public void GetSpecificReport();
        public void DeleteOfficer();
        public void ViewIncidentInADateRange();
        public void ViewIncidentOfSameType();
        public void CreateReport();
        public void DeleteReport();
        public void RegisterEvidence();
        public void DeleteEvidence();
        public void GetEvidenceRelatedToIncident();
        public void GetSuspectsRelatedToIncident();
        public void GetVictimsRelatedToIncident();
        public void CreateCase();
        public void GetAllIncidentsToASpecificCase();
        public void GetAllCases();
        public void GetCaseDetails();


    }
}
