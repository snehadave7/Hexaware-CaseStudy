using CrimeReportingSystem.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrimeReportingSystem.Repository {
    internal interface IOfficerRepository {
        public bool CreateOfficer(Officers officer);
        public bool UpdateOfficerFirstName(string firstName,int officersId);
        public bool UpdateOfficerLastName(string lastName, int officersId);
        public bool UpdateOfficerBadge(int badgeNum , int officersId);
        public bool UpdateOfficerRank(string rank, int officersId);
        public bool UpdateOfficerContact(string contact, int officersId);
        public bool DeleteOfficer(int officerId);
        public List<Officers> GetAllOfficers();
        public bool GetOfficerById(int officerId);
    }
}
