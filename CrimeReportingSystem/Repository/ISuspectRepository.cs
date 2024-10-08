using CrimeReportingSystem.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrimeReportingSystem.Repository {
    internal interface ISuspectRepository {
        public List<Suspects> GetAllSuspectsRelatedToAnIncident(int incidentId);
        public List<Suspects> GetAllSuspects();
        public bool CreateNewSuspect(Suspects suspects,int incidentId);
        public bool DeleteSuspect(int suspectId);
        public bool UpdateSuspectByFirstName(string firstName, int suspectID);
        public bool UpdateSuspectByLastName(string lastName, int suspectID);
        public bool UpdateSuspectByAddress(string address, int suspectID);
        public bool UpdateSuspectByContact(string contact, int suspectID);
    }
}
