
using CrimeReportingSystem.Exceptions;
using CrimeReportingSystem.Model;
using CrimeReportingSystem.Repository;
using NUnit.Framework;

namespace CrimeReportingSystem.Tests {
    public class Test {
        [Test]
        public void TestToAddIncidents() {
            IIncidentRepository incidentRepository = new IncidentRepository();
            Incidents incidents = new Incidents();
            {

                incidents.IncidentType = "Fire";
                incidents.IncidentDate = DateTime.Now;
                incidents.Description = "Description";
                incidents.Status = "Open";
                incidents.AgencyId = 1;
                incidents.Location = "delhi";
            }
            int incidentId=incidentRepository.CreateIncident(incidents);
            bool addIsCreatedStatus = true;
            if(incidentId==0) addIsCreatedStatus = false;
            Assert.That(true,Is.EqualTo(addIsCreatedStatus));

            Incidents createdIncident=incidentRepository.GetIncidentById(incidentId);
            Assert.That(createdIncident, Is.Not.Null);
            Assert.That(createdIncident.IncidentType, Is.EqualTo("Fire"));
            Assert.That(createdIncident.Description, Is.EqualTo("Description"));
            Assert.That(createdIncident.Status, Is.EqualTo("Open"));
            Assert.That(createdIncident.AgencyId, Is.EqualTo(1));
            Assert.That(createdIncident.Location, Is.EqualTo("delhi"));

        }
        
        [Test]
        public void TestToUpdateIncidentStatus() {
            IIncidentRepository incidentRepository= new IncidentRepository();
            
            string status = "Closed";
            int incidentId = 1;
            bool updateStatus=incidentRepository.UpdateIncidentStatus(status, incidentId);
            Assert.That(true, Is.EqualTo(updateStatus));
        }


        [Test]
        public void TestInvalidStatusUpdate_Throw_Exception() {
            ReportRepository reportRepository = new ReportRepository();
            string status = "wrongValue";
            int reportId = 1;
            var ex = Assert.Throws<InvalidStatusException>(() => reportRepository.UpdateReportByStatus(status, reportId));
            Assert.That(ex.Message, Is.EqualTo("You have entered wrong value for status!"));
        }

       
        
    }
}
