using CrimeReportingSystem.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrimeReportingSystem.Repository {
    internal interface IVictimRepository {

        public List<Victims> GetAllVictimsRelatedToAnIncident(int incidentId);
        public List<Victims> GetAllVictims();
        public bool CreateNewVictim(Victims victims,int incidentId);
        public bool DeleteVictim(int victimId );
        public bool UpdateVictimByFirstName(string firstName,int victimID);
        public bool UpdateVictimByLastName(string lastName,int victimID);
        public bool UpdateVictimByAddress(string address,int victimID);
        public bool UpdateVictimByContact(string contact,int victimID);
        

    }
}
