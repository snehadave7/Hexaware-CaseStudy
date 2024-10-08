using CrimeReportingSystem.Exceptions;
using CrimeReportingSystem.Model;
using CrimeReportingSystem.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ConsoleTableExt;
using Spectre.Console;
using System.Diagnostics.Metrics;
namespace CrimeReportingSystem.Service {
    internal class CrimeAnalysisService:ICrimeAnalysisService {
        readonly ILawEnforcementAgencyRepository _lawEnforcementAgencyRepository;
        readonly IIncidentRepository _incidentRepository;
        readonly IVictimRepository _victimRepository;
        readonly ISuspectRepository _suspectRepository;
        readonly IOfficerRepository _officerRepository;
        readonly IReportRepository _reportRepository;
        readonly IEvidenceRepository _evidenceRepository;
        readonly ICaseRepository _caseRepository;
        public string[] incidentStatus = new string[] { "Open", "Closed", "UnderInvestigation" ,"open","closed", "underInvestigation" };
        public string[] reportStatus = new string[] { "Pending", "Draft", "Finalized", "pending", "draft", "finalized" };
        public string[] genderstatus = new string[] { "Male","male","Female","female","Other","other" };

        public CrimeAnalysisService() {
            _lawEnforcementAgencyRepository = new LawEnforcementAgencyRepository();
            _incidentRepository = new IncidentRepository();
            _victimRepository = new VictimRepository();
            _suspectRepository = new SuspectRepository();
            _officerRepository = new OfficerRepository();
            _reportRepository = new ReportRepository();
            _evidenceRepository = new EvidenceRepository();
            _caseRepository=new CaseRepository();
        }
        public void RegisterIncident() {
            Console.WriteLine("Enter the type of incident");
            string incidentType = Console.ReadLine();
        date: try {
                
                Console.WriteLine("Enter the date of incident (YYYY-MM-DD)");
                DateTime dates = DateTime.Parse(Console.ReadLine());
                if (dates > DateTime.Now) throw new FormatException("Date cannot be greater than current date");
                Console.WriteLine("Enter the description of the incident");
                string description = Console.ReadLine();
                _lawEnforcementAgencyRepository.GetAllAgencies();
                
                
                Console.WriteLine("Enter the agencyId");
                int agencyId = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter the incident location");
                string location = Console.ReadLine();
                Incidents incidents = new Incidents(incidentType, dates, description, agencyId, location);
                if (_incidentRepository.CreateIncident(incidents)>0) {
                    Console.BackgroundColor = ConsoleColor.Green;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.WriteLine("Incident created successfully!");
                }
                else {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("Incident not created!");
                }
                Console.ResetColor();
            }
            catch (FormatException e) {
                Console.WriteLine(e.Message + " Enter again"); goto date;
            }
            

        }
            
        public void RegisterVictim() {
            Console.WriteLine("Enter  firstname of the victim");
            string firstname = Console.ReadLine();
            Console.WriteLine("Enter lastname of the victim");
            string lastname = Console.ReadLine();
            date: try {
                Console.WriteLine("Enter DateOfBirth of the victim");
                DateTime dob = DateTime.Parse(Console.ReadLine());
                if (dob > DateTime.Now) throw new FormatException("Date cannot be greater than current date");
                string gender = "";
            gender: try {
                    Console.WriteLine("Enter gender of the victim: (Male,Female,Other)");
                    gender = Console.ReadLine();
                    if (!genderstatus.Contains(gender) ){
                        throw new InvalidStatusException("Wrong choice entered for gender! Enter again");
                    }
                }
                catch (InvalidStatusException e) {
                    Console.WriteLine(e.Message); goto gender;
                }

                Console.WriteLine("Enter address of the victim");
                string address = Console.ReadLine();
                Console.WriteLine("Enter contact of the victim");
                string contact = Console.ReadLine();
                Console.WriteLine("What is ur incident id");
                int incidentId = int.Parse(Console.ReadLine());


                Victims victim = new Victims(firstname, lastname, dob, gender, address, contact);
                if (_victimRepository.CreateNewVictim(victim, incidentId)) {
                    Console.BackgroundColor = ConsoleColor.Green;
                    Console.ForegroundColor = ConsoleColor.Black;

                    Console.WriteLine("Victim details added successfully!");
                }
                else {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("Victim not created!");
                }
                Console.ResetColor();
            }
            catch (FormatException e) {
                Console.WriteLine(e.Message + " Enter again"); goto date;
            }
            
            
        }

        public void GetIncidents() {
             _incidentRepository.GetAllIncidents();
            
        }
        public void DeleteIncident() {
            id:
            Console.WriteLine("Enter the incidentId for the incident you want to delete");
            int incidentId = int.Parse(Console.ReadLine());
            if (_incidentRepository.DeleteIncident(incidentId)) {
                Console.BackgroundColor = ConsoleColor.Green;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine("Incident deleted successfully");
            }
            else {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Incident number not found! Try again"); goto id;
            }
            Console.ResetColor();
        }
        
        public void UpdateIncidentType() {
        enterAgain: try {

                Console.WriteLine("Enter the incidentId");
                int incidentTypeId = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter the incidentType u wish to change for the given incident id");
                string incidentType = Console.ReadLine();
                bool updateStatus = _incidentRepository.UpdateIncidentType(incidentType, incidentTypeId);
                if (updateStatus) Console.WriteLine("Incident updated successfully");
                else throw new IDNotFoundException("Incident Id was not found. Check the number again and enter !");
            }

            catch (Exception e) {
                Console.WriteLine(e.Message); goto enterAgain;
            }
        }
        public void UpdateIncidentDate() {
            
            date: try {
                Console.WriteLine("Enter the incidentDate u wish to change for the given incident id (YYYY-MM-DD)");
                DateTime incidentDate = DateTime.Parse(Console.ReadLine());
                
                id: try {
                    Console.WriteLine("Enter the incidentId");
                    int incidentId = int.Parse(Console.ReadLine());
                    if (_incidentRepository.UpdateIncidentDate(incidentDate, incidentId)) Console.WriteLine("Incident updated successfully");
                    else throw new IDNotFoundException("Incident number not found! Enter again");
                }
                catch (IDNotFoundException e) {
                    Console.WriteLine(e.Message); goto id;
                }
            }
            catch (FormatException) {
                Console.WriteLine("Wrong format of date! Enter again"); goto date;
            }
            
        }
        public void UpdateIncidentDescription() {
            id:
            Console.WriteLine("Enter the incidentId");
            int incidentTypeId = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter the incident description u wish to change for the given incident id");
            string incidentDescription = Console.ReadLine();
            if (_incidentRepository.UpdateIncidentDescription(incidentDescription, incidentTypeId)) Console.WriteLine("Incident updated successfully");
            else Console.WriteLine("IncidentId not found! Not updated. Try again "); goto id;
        }
        public void UpdateIncidentLocation() {
            id:
            Console.WriteLine("Enter the incidentId");
            int incidentTypeId = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter the incident location u wish to change for the given incident id");
            string incidentLocation = Console.ReadLine();
            if (_incidentRepository.UpdateIncidentLocation(incidentLocation, incidentTypeId)) Console.WriteLine("Incident updated successfully");
            else Console.WriteLine("IncidentId not found! Not updated. Try again "); goto id;
        }

        public void RegisterSuspect() {
            Console.WriteLine("Enter  firstname of the Suspect");
            string firstname = Console.ReadLine();
            Console.WriteLine("Enter lastname of the Suspect");
            string lastname = Console.ReadLine();
            date: try {
                Console.WriteLine("Enter DateOfBirth of the Suspect");
                DateTime dob = DateTime.Parse(Console.ReadLine());
                Console.WriteLine("Enter gender of the Suspect: (Male,Female,Other)");
                string gender = Console.ReadLine();
                Console.WriteLine("Enter address of the Suspect");
                string address = Console.ReadLine();
                Console.WriteLine("Enter contact of the Suspect");
                string contact = Console.ReadLine();
                Console.WriteLine("What is ur incident id");
                int incidentId = int.Parse(Console.ReadLine());


                Suspects suspect = new Suspects(firstname, lastname, dob, gender, address, contact);
                if (_suspectRepository.CreateNewSuspect(suspect, incidentId)) {
                    Console.WriteLine("Suspect details added successfully!");
                }
                else Console.WriteLine("Suspect not created!");
            }
            catch (FormatException e) { Console.WriteLine(e.Message + " Enter again"); goto date; }

        }
        public void ViewStatus() {
            ID:
            try {
                Console.WriteLine("Enter the incidentId");
                int incidentId = int.Parse(Console.ReadLine());
                if (_incidentRepository.CheckIncidentStatus(incidentId)) { }
                else throw new IDNotFoundException("Id not found! enter again");
            }
            catch(IDNotFoundException e) { Console.WriteLine(e.Message); goto ID; }
            catch (FormatException e) { Console.WriteLine(e.Message); goto ID; }

        }

        public void UpdateVictimFirstName() {
            Console.WriteLine("Enter victim id");
            int victimId = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter new FirstName");
            string firstName = Console.ReadLine();
            if (_victimRepository.UpdateVictimByFirstName(firstName, victimId)) Console.WriteLine("Victim updated successfully!");
            else Console.WriteLine("Victim not updated");
        }
        public void UpdateVictimLastName() {
            Console.WriteLine("Enter victim id");
            int victimId = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter new LastName");
            string LastName = Console.ReadLine();
            if (_victimRepository.UpdateVictimByLastName(LastName, victimId)) Console.WriteLine("Victim updated successfully!");
            else Console.WriteLine("Victim not updated");

        }
        public void UpdateVictimAddress() {
            Console.WriteLine("Enter victim id");
            int victimId = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter new Address");
            string Address = Console.ReadLine();
            if (_victimRepository.UpdateVictimByAddress(Address, victimId)) Console.WriteLine("Victim updated successfully!");
            else Console.WriteLine("Victim not updated");

        }
        public void UpdateVictimContact() {
            Console.WriteLine("Enter victim id");
            int victimId = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter new contact");
            string contact = Console.ReadLine();
            if (_victimRepository.UpdateVictimByContact(contact, victimId)) Console.WriteLine("Victim updated successfully!");
            else Console.WriteLine("Victim not updated");

        }

        public void CreateAgency() {
            Console.WriteLine("Enter Agency Name");
            string agencyName = Console.ReadLine();
            Console.WriteLine("Enter juridiction for the agency");
            string jurisdiction = Console.ReadLine();
            Console.WriteLine("Enter contact details");
            string contact = Console.ReadLine();
            LawEnforcementAgencies agency = new LawEnforcementAgencies(agencyName, jurisdiction, contact);
            if (_lawEnforcementAgencyRepository.AddNewAgency(agency)) {
                Console.BackgroundColor = ConsoleColor.Green;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine("Agency added successfully");
            }
            else {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Agnecy not added");
            }
            Console.ResetColor();
        }

        public void GetAgency() {
            
            _lawEnforcementAgencyRepository.GetAllAgencies();
            
        }

        public void DeleteAgency() {
            id:
            Console.WriteLine("Enter the agencyid");
            int agencyId = int.Parse(Console.ReadLine());
            if (_lawEnforcementAgencyRepository.DeleteAgency(agencyId)) {
                Console.BackgroundColor = ConsoleColor.Green;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine("Agency deleted successfully");
            }
            else {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Id not found! enter again");
                Console.ResetColor(); goto id;
            }
            Console.ResetColor();
        }

        public void UpdateAgencyName() {
            id:
            Console.WriteLine("Enter the agencyid");
            int agencyId = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter newName");
            string name = Console.ReadLine();
            if (_lawEnforcementAgencyRepository.UpdateAgencyName(agencyId, name)) Console.WriteLine("Agency updated successfully");
            else {
                Console.WriteLine("Agency id not found Enter again"); goto id;
            }
        }
        public void UpdateAgencyJurisdiction() {
            id:
            Console.WriteLine("Enter the agencyid");
            int agencyId = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter new Jurisdiction");
            string jurisdiction = Console.ReadLine();
            if (_lawEnforcementAgencyRepository.UpdateAgencyJurisdiction(agencyId, jurisdiction)) Console.WriteLine("Agency updated successfully");
            else {
                Console.WriteLine("Agency id not found Enter again"); goto id;
            }
        }
        public void UpdateAgencyContact() {
            id:
            Console.WriteLine("Enter the agencyid");
            int agencyId = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter new Contact");
            string contact = Console.ReadLine();
            if (_lawEnforcementAgencyRepository.UpdateAgencyContact(agencyId, contact)) Console.WriteLine("Agency updated successfully");
            else {
                Console.WriteLine("Agency id not found Enter again"); goto id;
            }
        }

        public void AddOfficer() {
            Console.WriteLine("Enter firstName of officer");
            string firstName = Console.ReadLine();
            Console.WriteLine("Enter lastName of officer");
            string lastName = Console.ReadLine();
            Console.WriteLine("enter badge number of officer");
            int badge = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter rank of officer");
            string rank = Console.ReadLine();
            Console.WriteLine("Enter contact of officer");
            string contact = Console.ReadLine();
            _lawEnforcementAgencyRepository.GetAllAgencies();
            Console.WriteLine("Enter agencyid of officer");
            int agenyid = int.Parse(Console.ReadLine());
            Officers officer = new Officers(firstName, lastName, badge, rank, contact, agenyid);
            if (_officerRepository.CreateOfficer(officer)) {
                Console.BackgroundColor = ConsoleColor.Green;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine("Officer added successfully");
            }
            else {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("");
            }
            Console.ResetColor();
        }

        public void ViewReports() {
          
                _reportRepository.GetAllReports();
            

        }
        public void GetSpecificReport() {
            _incidentRepository.GetAllIncidents();
            id: try {
                Console.WriteLine("Enter IncidentId");
                int incidentId;
               id1: try {
                     incidentId = int.Parse(Console.ReadLine());
                }
                catch(Exception e) {
                    Console.WriteLine("Invalid input! Enter again"); goto id1;
                }
                bool status=_reportRepository.GenerateReportForAnIncident(incidentId);
                if (!status) throw new IDNotFoundException($"No report exists for incidentNum: {incidentId} ! Enter again"); 
            }
            catch(IDNotFoundException e) {
                Console.WriteLine(e.Message); goto id;
            }
            catch (FormatException e) { Console.WriteLine(e.Message); goto id; }


        }
        public void DeleteOfficer() {
            Console.WriteLine("Enter officerId");
            int officerId = int.Parse(Console.ReadLine());
            if (_officerRepository.DeleteOfficer(officerId)) {
                Console.BackgroundColor = ConsoleColor.Green;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine("Deleted successfully");
            }
            else {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Exception");
            }
            Console.ResetColor ();
        }
        public void ViewOfficers() {
            
                _officerRepository.GetAllOfficers();
            
        }

        public void ViewIncidentInADateRange() {
            date: try {
                Console.WriteLine("Enter start date");
                DateTime start = DateTime.Parse(Console.ReadLine());
                Console.WriteLine("Enter end date");
                DateTime end = DateTime.Parse(Console.ReadLine());
                _incidentRepository.GetIncidentsInDateRange(start, end);
                
            }
            catch (FormatException) {
                Console.WriteLine("Wrong Date format! enter again"); goto date;
            }
        }
        public void ViewIncidentOfSameType() {
            Console.WriteLine("Enter incident type");
            string type = Console.ReadLine();
            _incidentRepository.GetIncidentsOfSameType(type);
        }

        public void CreateReport() {
            _incidentRepository.GetAllIncidents();
            int id;
            int Rid;
        iid: try {
                Console.WriteLine("Enter IncidentId for which this report is being created");
                id = int.Parse(Console.ReadLine());
                Incidents incident = _incidentRepository.GetIncidentById(id); 
                if (incident.IncidentId == 0) {
                    throw new IDNotFoundException("ID not found! Enter again");
                }
            }
            catch (IDNotFoundException e) { Console.WriteLine(e.Message); goto iid; }
            catch(Exception e) {
                Console.WriteLine("Id format incorrect! Enter again"); goto iid;
            }
            _officerRepository.GetAllOfficers();

        rid: try {
                Console.WriteLine("Enter Reporting officers id ");
                Rid = int.Parse(Console.ReadLine());
                if (_officerRepository.GetOfficerById(Rid) == false) { throw new IDNotFoundException("ID not found! Enter again"); }
            }
            catch (IDNotFoundException e) { Console.WriteLine(e.Message); goto rid; }
            catch (Exception e) {
                Console.WriteLine("Id format incorrect! Enter again"); goto iid;
            }

            DateTime date = DateTime.Now;
            Console.WriteLine("Enter Report details");
            string detail=Console.ReadLine();
        status: try {
                Console.WriteLine("Enter status of the Report: Draft,Finalized,Pending");
                string status = Console.ReadLine();      
                if (!reportStatus.Contains(status)) {
                    throw new InvalidStatusException("You have entered wrong value for status! Enter again");
                }
                Reports reports = new Reports(id, Rid, date, detail, status);
                if (_reportRepository.CreateReport(reports)) {
                    Console.BackgroundColor = ConsoleColor.Green;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.WriteLine("Report created successfully");
                    Console.ResetColor();
                }
                else {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("Exc");
                    Console.ResetColor();
                }
            }
            catch (Exception e) {
                Console.WriteLine(e.Message); goto status;
            }
            Console.ResetColor();

        }
        public void DeleteReport() {
            Console.WriteLine("Enter reportId");
            int reportId=int.Parse(Console.ReadLine());

            if (_reportRepository.DeleteReport(reportId)) {
                Console.BackgroundColor = ConsoleColor.Green;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine("Deleted successfully");
            }

            else {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("exc");
            }
            Console.ResetColor();
        }
        public void UpdateReportDate() {
            dates: try {
                Console.WriteLine("Enter new Report date");
                DateTime date = DateTime.Parse(Console.ReadLine());
                Console.WriteLine("Enter reportId");
                int reportId = int.Parse(Console.ReadLine());
                if (_reportRepository.UpdateReportByDate(date, reportId)) Console.WriteLine("Updated Successfully");
                else Console.WriteLine("exc");
            }
            catch (FormatException) {
                Console.WriteLine("Wrong dateFormat! Enter again"); goto dates;
            }
        }
        public void UpdateReportStatus() {
            status: try {
                Console.WriteLine("Enter new report Status: Draft,Finalized,Pending");
                string status = Console.ReadLine();
                if (!reportStatus.Contains(status)) {
                    throw new InvalidStatusException("You have entered wrong value for status! Enter again");
                }
                Console.WriteLine("Enter reportId");
                int reportId = int.Parse(Console.ReadLine());
                if (_reportRepository.UpdateReportByStatus(status, reportId)) Console.WriteLine("Updated Successfully");
                else Console.WriteLine("exc");
            }

            catch (Exception e) {
                Console.WriteLine(e.Message); goto status;
            }
            

        }
        public void RegisterEvidence() {
            Console.WriteLine("Enter incident id for which this evidence is found");
            int id = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter evidence description");
            string des=Console.ReadLine();
            Console.WriteLine("Enter Location found");
            string location=Console.ReadLine();
            Evidence evidence = new Evidence(des,location,id);
            if (_evidenceRepository.CreateEvidence(evidence)) {
                Console.BackgroundColor = ConsoleColor.Green;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine("Evidence created successfully");
            }
            else {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("exc");
            }
            Console.ResetColor();

        }
        public void DeleteEvidence() {
            Console.WriteLine("Enter EvidenceId");
            int evidenceId = int.Parse(Console.ReadLine());
            if (_evidenceRepository.DeleteEvidence(evidenceId)) Console.WriteLine("Deleted successfully");
            else Console.WriteLine("exc");
        }
        public void UpdateEvidenceLocation() {
            Console.WriteLine("Enter new location");
            string location = Console.ReadLine();
            Console.WriteLine("Enter EvidenceId");
            int evidenceId = int.Parse(Console.ReadLine());
            if (_evidenceRepository.UpdateEvidenceOnLocation(location,evidenceId)) Console.WriteLine("Deleted successfully");
            else Console.WriteLine("exc");
        }

        public void UpdateEvidenceDescription() {
            Console.WriteLine("Enter new description");
            string description = Console.ReadLine();
            Console.WriteLine("Enter EvidenceId");
            int evidenceId = int.Parse(Console.ReadLine());
            if (_evidenceRepository.UpdateEvidenceOnDescription(description, evidenceId)) Console.WriteLine("Deleted successfully");
            else Console.WriteLine("exc");
        }

        public void DeleteSuspect() {
            Console.WriteLine("Enter suspectId");
            int suspectId = int.Parse(Console.ReadLine());
            if(_suspectRepository.DeleteSuspect(suspectId)) Console.WriteLine("Deleted successfully");
            else Console.WriteLine("exc");
        }
        public void UpdateSuspectFirstName() {
            Console.WriteLine("Enter new firstName");
            string firstName = Console.ReadLine();
            Console.WriteLine("Enter suspectId");
            int suspectId = int.Parse(Console.ReadLine());
            if(_suspectRepository.UpdateSuspectByFirstName(firstName, suspectId))  Console.WriteLine("Updated successfully");
            else Console.WriteLine("Exc");
        }
        public void UpdateSuspectLastName() {
            Console.WriteLine("Enter new LastName");
            string lastName = Console.ReadLine();
            Console.WriteLine("Enter suspectId");
            int suspectId = int.Parse(Console.ReadLine());
            if (_suspectRepository.UpdateSuspectByLastName(lastName,suspectId)) Console.WriteLine("Updated successfully");
            else Console.WriteLine("Exc");
        }
        public void UpdateSuspectAddress() {
            Console.WriteLine("Enter new Address");
            string address = Console.ReadLine();
            Console.WriteLine("Enter suspectId");
            int suspectId = int.Parse(Console.ReadLine());
            if (_suspectRepository.UpdateSuspectByAddress(address, suspectId)) Console.WriteLine("Updated successfully");
            else Console.WriteLine("Exc");
        }
        public void UpdateSuspectContact() {
            Console.WriteLine("Enter new contact");
            string contact = Console.ReadLine();
            Console.WriteLine("Enter suspectId");
            int suspectId = int.Parse(Console.ReadLine());
            if (_suspectRepository.UpdateSuspectByContact(contact, suspectId)) Console.WriteLine("Updated successfully");
            else Console.WriteLine("Exc");
        }
        public void UpdateOfficerFirstName() {
            Console.WriteLine("Enter new firstName");
            string firstName = Console.ReadLine();
            Console.WriteLine("Enter OfficerId");
            int officerId=int.Parse(Console.ReadLine());
            if(_officerRepository.UpdateOfficerFirstName(firstName, officerId)) Console.WriteLine("Updated successfully");
            else Console.WriteLine("Exc");

        }
        public void UpdateOfficerLastName() {
            Console.WriteLine("Enter new LastName");
            string lastName = Console.ReadLine();
            Console.WriteLine("Enter OfficerId");
            int officerId = int.Parse(Console.ReadLine());
            if (_officerRepository.UpdateOfficerLastName(lastName, officerId)) Console.WriteLine("Updated successfully");
            else Console.WriteLine("Exc");
        }
        public void UpdateOfficerBadge() {
            Console.WriteLine("Enter new BadgeNum");
            int badgeNum = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter OfficerId");
            int officerId = int.Parse(Console.ReadLine());
            if (_officerRepository.UpdateOfficerBadge(badgeNum, officerId)) Console.WriteLine("Updated successfully");
            else Console.WriteLine("Exc");
        }
        public void UpdateOfficerRank() {
            Console.WriteLine("Enter new Rank");
            string rank = Console.ReadLine();
            Console.WriteLine("Enter OfficerId");
            int officerId = int.Parse(Console.ReadLine());
            if (_officerRepository.UpdateOfficerRank(rank, officerId)) Console.WriteLine("Updated successfully");
            else Console.WriteLine("Exc");
        }
        public void UpdateOfficerContact() {
            Console.WriteLine("Enter new contact");
            string contact = Console.ReadLine();
            Console.WriteLine("Enter OfficerId");
            int officerId = int.Parse(Console.ReadLine());
            if (_officerRepository.UpdateOfficerContact(contact, officerId)) Console.WriteLine("Updated successfully");
            else Console.WriteLine("Exc");
        }
        public void GetEvidenceRelatedToIncident() {
            Console.WriteLine("Enter the incident id");
            int incidentId = int.Parse(Console.ReadLine());
            List<Evidence> list = _evidenceRepository.GetEvidenceRelatedToAnIncident(incidentId);
            foreach (Evidence v in list) {
                Console.WriteLine(v);
            }
        }

        public void GetSuspectsRelatedToIncident() {
            Console.WriteLine("Enter the incident id");
            int incidentId = int.Parse(Console.ReadLine());
            List<Suspects> list = _suspectRepository.GetAllSuspectsRelatedToAnIncident(incidentId);
            foreach (Suspects v in list) {
                Console.WriteLine(v);
            }
        }

        public void GetVictimsRelatedToIncident() {
            Console.WriteLine("Enter the incident id");
            int incidentId=int.Parse(Console.ReadLine());
            List<Victims> list = _victimRepository.GetAllVictimsRelatedToAnIncident(incidentId);
            foreach (Victims v in list) {
                Console.WriteLine(v);
            }
        }

        public void CreateCase() {  
            int incidentId;
            id: try {
                Console.WriteLine("Choose the incidenId for the case u want to create");
                _incidentRepository.GetAllIncidents();
                incidentId = int.Parse(Console.ReadLine());
                Incidents incident = _incidentRepository.GetIncidentById(incidentId);
                if (incident.IncidentId == 0) {
                    throw new IDNotFoundException("ID not found! Enter again");
                }
            }
            catch (IDNotFoundException e) { Console.WriteLine(e.Message); goto id; }
            catch(FormatException e) { Console.WriteLine(e.Message); goto id; }

            Console.WriteLine("enter case description");
            string des = Console.ReadLine();
            DateTime date=DateTime.Now;
            Cases cases= new Cases(des,date);
            if (_caseRepository.CreateCase(cases, incidentId)) {
                Console.BackgroundColor = ConsoleColor.Green;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine("Case created successfully");

            }
            else {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.White;
            }
            Console.ResetColor();
        }

        public void AddToExsitingCase() {
            int incidentId;
            int caseId;
            _caseRepository.GetAllCases();
            cid: try {
                Console.WriteLine("Enter caseId");
                 caseId = int.Parse(Console.ReadLine());
                _incidentRepository.GetAllIncidents();
            iid: try {
                    Console.WriteLine("Enter incidentId");
                     incidentId = int.Parse(Console.ReadLine());
                    Incidents incident = _incidentRepository.GetIncidentById(incidentId);
                    if (incident.IncidentId == 0) {
                        throw new IDNotFoundException("Incident ID not found! Enter again");
                    }
                }
                catch (IDNotFoundException e) { Console.WriteLine(e.Message); goto iid; }
                catch (FormatException e) { Console.WriteLine(e.Message); goto iid; }
            }
            catch (IDNotFoundException e) { Console.WriteLine(e.Message); goto cid; }
            catch (FormatException e) { Console.WriteLine(e.Message); goto cid; }

            CaseIncidentMapRepo caseIncidentMapRepo = new CaseIncidentMapRepo();
            if (caseIncidentMapRepo.CreateCaseIncidentMap(caseId, incidentId))  Console.WriteLine("Incident added in the case");
            else Console.WriteLine("Incident not added");
            
        }
        public void GetAllIncidentsToASpecificCase() {
            id: try {
                Console.WriteLine("Enter caseId");
                int caseId = int.Parse(Console.ReadLine());
                if (!_caseRepository.GetAllIncidentForACase(caseId)) throw new IDNotFoundException("Case Id not found! Enter again");
            }
            catch(IDNotFoundException e) {
                Console.WriteLine(e.Message); goto id;
            }
            catch (FormatException e) { Console.WriteLine(e.Message); goto id; }


        }


        public void GetAllCases() {
            
                _caseRepository.GetAllCases();
            
        }
        public void UpdateCaseDetails() {
            Console.WriteLine("Enter case id");
            int caseId = int.Parse(Console.ReadLine());
            Console.WriteLine("enter new detail");
            string description= Console.ReadLine();
            if(_caseRepository.UpdateCase(caseId, description)) {
                Console.WriteLine("Updated successfully");
            }
            else {
                Console.WriteLine("Not updated");
            }
        }

        public void UpdateStatusOfIncident() {
            
            Console.WriteLine("Enter incidentId for changingStatus");
            int incidentId = int.Parse(Console.ReadLine());
            status: try {
                Console.WriteLine("Enter new status: 'Open','Closed','UnderInvestigation'");
                string status = Console.ReadLine();
                if (!incidentStatus.Contains(status)) {
                    throw new InvalidStatusException("You have entered wrong value for status! Enter again");
                }
                if (_incidentRepository.UpdateIncidentStatus(status, incidentId)) Console.WriteLine("Status updated successfully");
                else Console.WriteLine("Status not updated");
                
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message); goto status;
            }                     
        }
        public void GetCaseDetails() {
            id: try {
                Console.WriteLine("Enter case id");
                int caseId = int.Parse(Console.ReadLine());
                
                Cases cases = _caseRepository.GetCaseDetailsForACase(caseId);
                
                if (cases.CaseId == 0) throw new IDNotFoundException("ID not found! Enter again");
                Console.WriteLine(cases);
                List<Incidents> incident = cases.incidents;
                foreach (Incidents inc in incident) {
                    Console.WriteLine(inc);
                }
            }

            catch (IDNotFoundException e) {
                Console.WriteLine(e.Message); goto id;
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
                Console.WriteLine("Invalid Id format! enter again"); goto id;
            }
            
            
        }
        public void GetIncidentByLocation() {
            Console.WriteLine("Enter location: Bhopal,Indore");
            string location = Console.ReadLine();
            _incidentRepository.GetIncidentByLocation(location);

        }
    }

}

