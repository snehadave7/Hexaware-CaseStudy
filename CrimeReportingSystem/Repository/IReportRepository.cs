using CrimeReportingSystem.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrimeReportingSystem.Repository {
    internal interface IReportRepository {
        public bool CreateReport(Reports report);
        public bool DeleteReport(int reportId);
        public List<Reports> GetAllReports();
        public bool GenerateReportForAnIncident(int incidentId);
        public bool UpdateReportByDate(DateTime date,int reportId);
        public bool UpdateReportByStatus(string status, int reportId);
        public List<Reports> GetReportById(int reportId);



    }
}
