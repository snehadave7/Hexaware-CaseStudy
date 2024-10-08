using CrimeReportingSystem.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrimeReportingSystem.Repository {
    internal interface IIncidentRepository {
        public List<Incidents> GetAllIncidents();
        public List<Incidents> GetIncidentsInDateRange(DateTime startDate,DateTime endDate);
        public List<Incidents> SearchIncidents(string incidentType);
        public void GetIncidentByLocation(string location);
        public  int CreateIncident(Incidents incident);
        public bool DeleteIncident(int incidentId);
        public bool UpdateIncidentType(string incidentType,int incidentId);
        public List<Incidents> GetIncidentsOfSameType(string type);
        public bool UpdateIncidentDate(DateTime incidentDate,int incidentId);
        public bool UpdateIncidentDescription(string incidentDescription,int incidentId);
        public bool UpdateIncidentLocation(string incidentLocation,int incidentId);
        public bool UpdateIncidentStatus(string status,int incidentId);
        public bool CheckIncidentStatus(int incidentId);
        public Incidents GetIncidentById(int incidentId);
    }
}
