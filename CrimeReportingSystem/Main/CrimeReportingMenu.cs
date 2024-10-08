using CrimeReportingSystem.Model;
using CrimeReportingSystem.Repository;
using CrimeReportingSystem.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace CrimeReportingSystem.Main {
    internal class CrimeReportingMenu {
        CrimeAnalysisService crime = new CrimeAnalysisService();
        public void Run() {

            Console.WriteLine("Welcome to Police Chauki! How may I help you?");
        MainMenu:
            Console.WriteLine("Type 1: If you are a Victim");
            Console.WriteLine("Type 2: If you are the head of Law Agencies");
            Console.WriteLine("Type 3: If you are a Senior Officer");
            Console.WriteLine("Type 4: If you are a Reporting Officer");
            Console.WriteLine("Type 5: To Exist");
            int personType = int.Parse(Console.ReadLine());
            switch (personType) {
                case 1:
                    Console.WriteLine("Sorry for your condition! We are here to help you in all possible ways!");
                    Console.WriteLine("Feel free to ask anything! We have these services available at out Chauki");
                IncidentMenu:
                    Console.WriteLine("Type 1: To create a new incident");
                    Console.WriteLine("Type 2: To delete an incident");
                    Console.WriteLine("Type 3: To update an incident");
                    Console.WriteLine("Type 4: To view status of incident");
                    Console.WriteLine("Type 5: To Update Victim Details");
                    Console.WriteLine("Type 6: To register Suspect");
                    Console.WriteLine("Type 7: To go back to Main Menu");
                    int victimServices = int.Parse(Console.ReadLine());
                    switch (victimServices) {
                        case 1:
                            crime.RegisterIncident();
                            crime.GetIncidents();// get all incidents
                            crime.RegisterVictim();

                            Console.WriteLine("Type 1: To go back to Incidents Menu");
                            Console.WriteLine("Type 2: To go back to Main Menu");
                            Console.WriteLine("Type 3: To exit");
                            int option1 = int.Parse(Console.ReadLine());
                            switch (option1) {
                                case 1:
                                    goto IncidentMenu;
                                    break;
                                case 2:
                                    goto MainMenu;
                                    break;
                                case 3:
                                Exist:
                                    return;
                            }
                            break;

                        case 2:
                            crime.GetIncidents();
                            crime.DeleteIncident();
                            Console.WriteLine("We are deleting your incident! I hope justice was prevailed to you");

                            Console.WriteLine("Type 1: To go back to Incidents Menu");
                            Console.WriteLine("Type 2: To go back to Main Menu");
                            Console.WriteLine("Type 3: To exit");
                            int option2 = int.Parse(Console.ReadLine());
                            switch (option2) {
                                case 1:
                                    goto IncidentMenu;
                                    break;
                                case 2:
                                    goto MainMenu;
                                    break;
                                case 3:
                                    return;
                            }
                            break;
                        case 3:

                        UpdateIncidentMenu:
                            Console.WriteLine("Type 1: To Update Incident Type");
                            Console.WriteLine("Type 2: To Update Incident Date");
                            Console.WriteLine("Type 3: To Update Incident Description");
                            Console.WriteLine("Type 4: To Update Incident Location");
                            int updateIncident = int.Parse(Console.ReadLine());

                            switch (updateIncident) {
                                case 1:
                                    crime.GetIncidents();
                                    crime.UpdateIncidentType();
                                    break;
                                case 2:
                                    crime.GetIncidents();
                                    crime.UpdateIncidentDate();
                                    break;
                                case 3:
                                    crime.GetIncidents();
                                    crime.UpdateIncidentDescription();
                                    break;
                                case 4:
                                    crime.GetIncidents();
                                    crime.UpdateIncidentLocation();
                                    break;
                                default:
                                    Console.BackgroundColor = ConsoleColor.Red;
                                    Console.ForegroundColor = ConsoleColor.White;
                                    Console.WriteLine("Wrong value entered");
                                    Console.ResetColor();
                                    goto UpdateIncidentMenu;
                                    break;
                            }
                            Console.WriteLine("Type 1: To go back to Update Incident Menu");
                            Console.WriteLine("Type 2: To go back to Incidents Menu");
                            Console.WriteLine("Type 3: To go back to Main Menu");
                            Console.WriteLine("Type 4: To exit");
                            int option3 = int.Parse(Console.ReadLine());
                            switch (option3) {
                                case 1:
                                    goto UpdateIncidentMenu;
                                    break;
                                case 2:
                                    goto IncidentMenu;
                                    break;
                                case 3:
                                    goto MainMenu;
                                    break;
                                case 4:
                                    return;
                            }
                            break;

                        case 4:
                            crime.GetIncidents();
                            crime.ViewStatus();
                            Console.WriteLine("Type 1: To go back to Incidents Menu");
                            Console.WriteLine("Type 2: To go back to Main Menu");
                            Console.WriteLine("Type 3: To exit");
                            int option4 = int.Parse(Console.ReadLine());
                            switch (option4) {
                                case 1:
                                    goto IncidentMenu;
                                    break;
                                case 2:
                                    goto MainMenu;
                                    break;
                                case 3:
                                    return;
                            }
                            break;
                        case 5:
                        UpdateVictimMenu:
                            Console.WriteLine("Type 1: To update Victim By FirstName");
                            Console.WriteLine("Type 2: To update Victim By LastName");
                            Console.WriteLine("Type 3: To update Victim By Address");
                            Console.WriteLine("Type 4: To update Victim By Contact");
                            int updateVictim = int.Parse(Console.ReadLine());
                            switch (updateVictim) {
                                case 1:

                                    crime.UpdateVictimFirstName();
                                    break;
                                case 2:
                                    crime.UpdateVictimLastName();
                                    break;
                                case 3:
                                    crime.UpdateVictimAddress();
                                    break;
                                case 4:
                                    crime.UpdateVictimContact();
                                    break;
                                default:
                                    Console.BackgroundColor = ConsoleColor.Red;
                                    Console.ForegroundColor = ConsoleColor.White;
                                    Console.WriteLine("Wrong value entered");
                                    Console.ResetColor();
                                    goto UpdateVictimMenu;
                                    break;
                            }

                            Console.WriteLine("Type 1: To go back to Update Victim Menu");
                            Console.WriteLine("Type 2: To go back to Incidents Menu");
                            Console.WriteLine("Type 3: To go back to Main Menu");
                            Console.WriteLine("Type 4: To exit");
                            int option5 = int.Parse(Console.ReadLine());
                            switch (option5) {
                                case 1:
                                    goto UpdateVictimMenu;
                                    break;
                                case 2:
                                    goto IncidentMenu;
                                    break;
                                case 3:
                                    goto MainMenu;
                                    break;
                                case 4:
                                    return;
                            }
                            break;
                        case 6:
                            crime.GetIncidents();
                            crime.RegisterSuspect();
                            Console.WriteLine("Type 1: To go back to Incidents Menu");
                            Console.WriteLine("Type 2: To go back to Main Menu");
                            Console.WriteLine("Type 3: To exit");
                            int option6 = int.Parse(Console.ReadLine());
                            switch (option6) {
                                case 1:
                                    goto IncidentMenu;
                                    break;
                                case 2:
                                    goto MainMenu;
                                    break;
                                case 3:
                                    return;
                            }

                            break;
                        case 7:
                            goto MainMenu;
                            break;
                        default:
                            Console.BackgroundColor = ConsoleColor.Red;
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine("Wrong value entered");
                            Console.ResetColor();
                            goto IncidentMenu;
                            break;


                    }

                    break;
                case 2:
                LawAgencyMenu:
                    Console.WriteLine("Respected Sir! We have these services available at out Chauki");
                    Console.WriteLine("Type 1: If you want to register a new law agency");
                    Console.WriteLine("Type 2: If you want to see all law agencies registered");
                    Console.WriteLine("Type 3: If you want to delete a law agency");
                    Console.WriteLine("Type 4: If you want to  update a law agency");
                    Console.WriteLine("Type 5: To go back to Main Menu");
                    int headServices = int.Parse(Console.ReadLine());
                    switch (headServices) {
                        case 1:
                            crime.CreateAgency();
                            Console.WriteLine("Type 1: To go back to LawAgency Menu");
                            Console.WriteLine("Type 2: To go back to Main Menu");
                            Console.WriteLine("Type 3: To exit");
                            int option1 = int.Parse(Console.ReadLine());
                            switch (option1) {
                                case 1:
                                    goto LawAgencyMenu;
                                    break;
                                case 2:
                                    goto MainMenu;
                                    break;
                                case 3:
                                    return;
                            }
                            break;
                        case 2:
                            crime.GetAgency();
                            Console.WriteLine("Type 1: To go back to LawAgency Menu");
                            Console.WriteLine("Type 2: To go back to Main Menu");
                            Console.WriteLine("Type 3: To exit");
                            int option2 = int.Parse(Console.ReadLine());
                            switch (option2) {
                                case 1:
                                    goto LawAgencyMenu;
                                    break;
                                case 2:
                                    goto MainMenu;
                                    break;
                                case 3:
                                    return;
                            }
                            break;
                        case 3:
                            crime.GetAgency();
                            crime.DeleteAgency();
                            Console.WriteLine("Type 1: To go back to LawAgency Menu");
                            Console.WriteLine("Type 2: To go back to Main Menu");
                            Console.WriteLine("Type 3: To exit");
                            int option3 = int.Parse(Console.ReadLine());
                            switch (option3) {
                                case 1:
                                    goto LawAgencyMenu;
                                    break;
                                case 2:
                                    goto MainMenu;
                                    break;
                                case 3:
                                    return;
                            }
                            break;
                        case 4:
                        UpdateAgencyMenu:
                            Console.WriteLine("Type 1: If you want to update by Name");
                            Console.WriteLine("Type 2: If you want to update by Juridiction");
                            Console.WriteLine("Type 3: If you want to update by Contact Details");
                            int updateAgency = int.Parse(Console.ReadLine());
                            crime.GetAgency();
                            switch (updateAgency) {
                                case 1:
                                    crime.UpdateAgencyName();
                                    break;
                                case 2:
                                    crime.UpdateAgencyJurisdiction();
                                    break;
                                case 3:
                                    crime.UpdateAgencyContact();
                                    break;
                                default:
                                    Console.BackgroundColor = ConsoleColor.Red;
                                    Console.ForegroundColor = ConsoleColor.White;
                                    Console.WriteLine("Wrong value entered");
                                    Console.ResetColor();
                                    goto UpdateAgencyMenu;
                                    break;

                            }
                            Console.WriteLine("Type 1: To go back to Update Agency Menu");
                            Console.WriteLine("Type 2: To go back to LawAgency Menu");
                            Console.WriteLine("Type 3: To go back to Main Menu");
                            Console.WriteLine("Type 4: To exit");
                            int option4 = int.Parse(Console.ReadLine());
                            switch (option4) {
                                case 1:
                                    goto UpdateAgencyMenu;
                                    break;
                                case 2:
                                    goto LawAgencyMenu;
                                    break;
                                case 3:
                                    goto MainMenu;
                                    break;
                                case 4:
                                    return;

                            }
                            break;
                        case 5:
                            goto MainMenu;
                            return;
                        default:
                            Console.BackgroundColor = ConsoleColor.Red;
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine("Wrong value entered");
                            Console.ResetColor();
                            goto LawAgencyMenu;
                            break;
                    }
                    break;
                case 3:
                SeniorOfficersMenu:
                    Console.WriteLine("Jai Hind Sir! How may i help you? We have these services available at out Chauki!!");
                    Console.WriteLine("Type 1: To add a new Officer");
                    Console.WriteLine("Type 2: To view all reports");
                    Console.WriteLine("Type 3: To get a specific report of an incident");
                    Console.WriteLine("Type 4: To delete an Officer");
                    Console.WriteLine("Type 5: To view all  Officers");
                    Console.WriteLine("Type 6: To view all incidents");
                    Console.WriteLine("Type 7: To view Incidents In a DateRange");
                    Console.WriteLine("Type 8: To view incidents of same type");
                    Console.WriteLine("Type 9: To view incident in a location");
                    Console.WriteLine("Type 10: To go back to Main Menu");
                    int seniorServices = int.Parse(Console.ReadLine());
                    switch (seniorServices) {
                        case 1:
                            crime.AddOfficer();
                            Console.WriteLine("Type 1: To go back to SeniorOfficers Menu");
                            Console.WriteLine("Type 2: To go back to Main Menu");
                            Console.WriteLine("Type 3: To exit");
                            int option1 = int.Parse(Console.ReadLine());
                            switch (option1) {
                                case 1:
                                    goto SeniorOfficersMenu;
                                    break;
                                case 2:
                                    goto MainMenu;
                                    break;
                                case 3:
                                    return;
                            }
                            break;
                        case 2:
                            crime.ViewReports();
                            
                            Console.WriteLine("Type 1: To go back to SeniorOfficers Menu");
                            Console.WriteLine("Type 2: To go back to Main Menu");
                            Console.WriteLine("Type 3: To exit");
                            int option2 = int.Parse(Console.ReadLine());
                            switch (option2) {
                                case 1:
                                    goto SeniorOfficersMenu;
                                    break;
                                case 2:
                                    goto MainMenu;
                                    break;
                                case 3:
                                    return;
                            }
                            break;
                        case 3:

                            crime.GetSpecificReport();
                            
                            Console.WriteLine("Type 1: To go back to SeniorOfficers Menu");
                            Console.WriteLine("Type 2: To go back to Main Menu");
                            Console.WriteLine("Type 3: To exit");
                            int option3 = int.Parse(Console.ReadLine());
                            switch (option3) {
                                case 1:
                                    goto SeniorOfficersMenu;
                                    break;
                                case 2:
                                    goto MainMenu;
                                    break;
                                case 3:
                                    return;
                            }
                            break;
                        case 4:
                            crime.ViewOfficers();
                            crime.DeleteOfficer();
                            Console.WriteLine("Type 1: To go back to SeniorOfficers Menu");
                            Console.WriteLine("Type 2: To go back to Main Menu");
                            Console.WriteLine("Type 3: To exit");
                            int option4 = int.Parse(Console.ReadLine());
                            switch (option4) {
                                case 1:
                                    goto SeniorOfficersMenu;
                                    break;
                                case 2:
                                    goto MainMenu;
                                    break;
                                case 3:
                                    return;
                            }
                            break;
                        case 5:
                            crime.ViewOfficers();

                            Console.WriteLine("Type 1: To go back to SeniorOfficers Menu");
                            Console.WriteLine("Type 2: To go back to Main Menu");
                            Console.WriteLine("Type 3: To exit");
                            int option5 = int.Parse(Console.ReadLine());
                            switch (option5) {
                                case 1:
                                    goto SeniorOfficersMenu;
                                    break;
                                case 2:
                                    goto MainMenu;
                                    break;
                                case 3:
                                    return;
                            }
                            break;
                        case 6:
                            crime.GetIncidents();
                            Console.WriteLine("Type 1: To go back to SeniorOfficers Menu");
                            Console.WriteLine("Type 2: To go back to Main Menu");
                            Console.WriteLine("Type 3: To exit");
                            int option6 = int.Parse(Console.ReadLine());
                            switch (option6) {
                                case 1:
                                    goto SeniorOfficersMenu;
                                    break;
                                case 2:
                                    goto MainMenu;
                                    break;
                                case 3:
                                    return;
                            }
                            break;
                        case 7:

                            crime.ViewIncidentInADateRange();
                            Console.WriteLine("Type 1: To go back to SeniorOfficers Menu");
                            Console.WriteLine("Type 2: To go back to Main Menu");
                            Console.WriteLine("Type 3: To exit");
                            int option7 = int.Parse(Console.ReadLine());
                            switch (option7) {
                                case 1:
                                    goto SeniorOfficersMenu;
                                    break;
                                case 2:
                                    goto MainMenu;
                                    break;
                                case 3:
                                    return;
                            }
                            break;
                        case 8:

                            crime.ViewIncidentOfSameType();
                            Console.WriteLine("Type 1: To go back to SeniorOfficers Menu");
                            Console.WriteLine("Type 2: To go back to Main Menu");
                            Console.WriteLine("Type 3: To exit");
                            int option8 = int.Parse(Console.ReadLine());
                            switch (option8) {
                                case 1:
                                    goto SeniorOfficersMenu;
                                    break;
                                case 2:
                                    goto MainMenu;
                                    break;
                                case 3:
                                    return;
                            }
                            break;
                        case 9:
                            crime.GetIncidentByLocation();
                            Console.WriteLine("Type 1: To go back to SeniorOfficers Menu");
                            Console.WriteLine("Type 2: To go back to Main Menu");
                            Console.WriteLine("Type 3: To exit");
                            int option9 = int.Parse(Console.ReadLine());
                            switch (option9) {
                                case 1:
                                    goto SeniorOfficersMenu;
                                    break;
                                case 2:
                                    goto MainMenu;
                                    break;
                                case 3:
                                    return;
                            }
                            break;
                        case 10:
                            goto MainMenu;
                            break;
                        default:
                            Console.WriteLine("Wrong value entered");
                            goto SeniorOfficersMenu;
                            break;
                    }
                    break;
                case 4:
                ReportingOfficersMenu:
                    Console.WriteLine("Jai Hind Officer! We have these services available at out Chauki");

                    Console.WriteLine("Type 1: To create a report for an incident");
                    Console.WriteLine("Type 2: To delete a report for an incident");
                    Console.WriteLine("Type 3: To update a report");
                    Console.WriteLine("Type 4: Register an evidence");
                    Console.WriteLine("Type 5: Delete an evidence");
                    Console.WriteLine("Type 6: Update an evidence");
                    Console.WriteLine("Type 7: Register a suspect");
                    Console.WriteLine("Type 8: Delete a suspect");
                    Console.WriteLine("Type 9: Update a suspect");
                    Console.WriteLine("Type 10: Update Officers Information");
                    Console.WriteLine("Type 11: Get all evidences related to an incident");
                    Console.WriteLine("Type 12: Get all suspects related to an incident");
                    Console.WriteLine("Type 13: Get all victims related to an incident");
                    Console.WriteLine("Type 14: Create case");
                    Console.WriteLine("Type 15: Add to exsiting case");
                    Console.WriteLine("Type 16: To see all incidents associated to a specific case");
                    Console.WriteLine("Type 17: Update status of an incident");
                    Console.WriteLine("Type 18: To check details of specific case");
                    Console.WriteLine("Type 19: To view all cases");
                    Console.WriteLine("Type 20: To update case details");
                    Console.WriteLine("Type 21: To go back to Main Menu");
                    int officerServices = int.Parse(Console.ReadLine());
                    switch (officerServices) {
                        case 1:

                            crime.CreateReport();
                            Console.WriteLine("Type 1: To go back to ReportingOfficers Menu");
                            Console.WriteLine("Type 2: To go back to Main Menu");
                            Console.WriteLine("Type 3: To exit");
                            int option1 = int.Parse(Console.ReadLine());
                            switch (option1) {
                                case 1:
                                    goto ReportingOfficersMenu;
                                    break;
                                case 2:
                                    goto MainMenu;
                                    break;
                                case 3:
                                    return;
                            }
                            break;
                        case 2:
                            crime.ViewReports();
                            crime.DeleteReport();
                            Console.WriteLine("Type 1: To go back to ReportingOfficers Menu");
                            Console.WriteLine("Type 2: To go back to Main Menu");
                            Console.WriteLine("Type 3: To exit");
                            int option2 = int.Parse(Console.ReadLine());
                            switch (option2) {
                                case 1:
                                    goto ReportingOfficersMenu;
                                    break;
                                case 2:
                                    goto MainMenu;
                                    break;
                                case 3:
                                    return;
                            }
                            break;
                        case 3:
                        UpdateReportMenu:
                            Console.WriteLine("Type 1: To update by Date");
                            Console.WriteLine("Type 2: To update by Status");
                            int updateReport = int.Parse(Console.ReadLine());
                            crime.ViewReports();
                            switch (updateReport) {
                                case 1:
                                    crime.ViewReports();
                                    crime.UpdateReportDate();
                                    break;
                                case 2:
                                    crime.ViewReports();
                                    crime.UpdateReportStatus();
                                    break;
                                default:
                                    Console.WriteLine("Wrong value entered");
                                    goto UpdateReportMenu;
                                    break;
                            }
                            Console.WriteLine("Type 1: To go back to Update Report Menu");
                            Console.WriteLine("Type 2: To go back to ReportingOfficers Menu");
                            Console.WriteLine("Type 3: To go back to Main Menu");
                            Console.WriteLine("Type 4: To exit");
                            int option3 = int.Parse(Console.ReadLine());

                            switch (option3) {
                                case 1:
                                    goto UpdateReportMenu;
                                    break;
                                case 2:
                                    goto ReportingOfficersMenu;
                                    break;
                                case 3:
                                    goto MainMenu;
                                    break;
                                case 4:
                                    return;
                            }
                            break;
                        case 4:
                            crime.GetIncidents();
                            crime.RegisterEvidence();
                            Console.WriteLine("Type 1: To go back to ReportingOfficers Menu");
                            Console.WriteLine("Type 2: To go back to Main Menu");
                            Console.WriteLine("Type 3: To exit");
                            int option4 = int.Parse(Console.ReadLine());
                            switch (option4) {
                                case 1:
                                    goto ReportingOfficersMenu;
                                    break;
                                case 2:
                                    goto MainMenu;
                                    break;
                                case 3:
                                    return;
                            }
                            break;
                        case 5:
                            
                            crime.DeleteEvidence();
                            Console.WriteLine("Type 1: To go back to ReportingOfficers Menu");
                            Console.WriteLine("Type 2: To go back to Main Menu");
                            Console.WriteLine("Type 3: To exit");
                            int option5 = int.Parse(Console.ReadLine());
                            switch (option5) {
                                case 1:
                                    goto ReportingOfficersMenu;
                                    break;
                                case 2:
                                    goto MainMenu;
                                    break;
                                case 3:
                                    return;
                            }
                            break;
                        case 6:
                        UpdateEvidenceMenu:
                            Console.WriteLine("Type 1: To update by Location");
                            Console.WriteLine("Type 2: To update by Description");
                            int updateEvidence = int.Parse(Console.ReadLine());
                            switch (updateEvidence) {
                                case 1:
                                    crime.UpdateEvidenceLocation();
                                    break;
                                case 2:
                                    crime.UpdateEvidenceDescription();
                                    break;
                                default:
                                    Console.WriteLine("Wrong value entered");
                                    goto UpdateEvidenceMenu;
                                    break;
                            }
                            Console.WriteLine("Type 1: To go back to Update Evidence Menu");
                            Console.WriteLine("Type 2: To go back to ReportingOfficers Menu");
                            Console.WriteLine("Type 3: To go back to Main Menu");
                            Console.WriteLine("Type 4: To exit");
                            int option6 = int.Parse(Console.ReadLine());
                            switch (option6) {
                                case 1:
                                    goto UpdateEvidenceMenu;
                                    break;
                                case 2:
                                    goto ReportingOfficersMenu;
                                    break;
                                case 3:
                                    goto MainMenu;
                                    break;
                                case 4:
                                    return;
                            }
                            break;
                        case 7:
                            crime.GetIncidents();
                            crime.RegisterSuspect();
                            Console.WriteLine("Type 1: To go back to ReportingOfficers Menu");
                            Console.WriteLine("Type 2: To go back to Main Menu");
                            Console.WriteLine("Type 3: To exit");
                            int option7 = int.Parse(Console.ReadLine());
                            switch (option7) {
                                case 1:
                                    goto ReportingOfficersMenu;
                                    break;
                                case 2:
                                    goto MainMenu;
                                    break;
                                case 3:
                                    return;
                            }
                            break;
                        case 8:
                            crime.DeleteSuspect();
                            Console.WriteLine("Type 1: To go back to ReportingOfficers Menu");
                            Console.WriteLine("Type 2: To go back to Main Menu");
                            Console.WriteLine("Type 3: To exit");
                            int option8 = int.Parse(Console.ReadLine());
                            switch (option8) {
                                case 1:
                                    goto ReportingOfficersMenu;
                                    break;
                                case 2:
                                    goto MainMenu;
                                    break;
                                case 3:
                                    return;
                            }
                            break;
                        case 9:
                        UpdateSuspectMenu:
                            Console.WriteLine("Type 1: To update Suspect By FirstName");
                            Console.WriteLine("Type 2: To update Suspect By LastName");
                            Console.WriteLine("Type 3: To update Suspect By Address");
                            Console.WriteLine("Type 4: To update Suspect By Contact");
                            int updateSuspect = int.Parse(Console.ReadLine());

                            switch (updateSuspect) {
                                case 1:
                                    crime.UpdateSuspectFirstName();
                                    break;
                                case 2:
                                    crime.UpdateSuspectLastName();
                                    break;
                                case 3:
                                    crime.UpdateSuspectAddress();
                                    break;
                                case 4:
                                    crime.UpdateSuspectContact();
                                    break;
                                default:
                                    break;
                            }
                            Console.WriteLine("Type 1: To go back to Update Suspect Menu");
                            Console.WriteLine("Type 2: To go back to ReportingOfficers Menu");
                            Console.WriteLine("Type 3: To go back to Main Menu");
                            Console.WriteLine("Type 4: To exit");
                            int option9 = int.Parse(Console.ReadLine());
                            switch (option9) {
                                case 1:
                                    goto UpdateSuspectMenu;
                                    break;
                                case 2:
                                    goto ReportingOfficersMenu;
                                    break;
                                case 3:
                                    goto MainMenu;
                                    break;
                                case 4:
                                    return;
                            }
                            break;
                        case 10:
                        UpdateOfficersMenu:
                            Console.WriteLine("Type 1: To update Officer FirstName");
                            Console.WriteLine("Type 2: To update Officer LastName");
                            Console.WriteLine("Type 3: To update Officer Badge");
                            Console.WriteLine("Type 4: To update Officer Rank");
                            Console.WriteLine("Type 5: To update Officers Contact");
                            int updateOfficer = int.Parse(Console.ReadLine());
                            crime.ViewOfficers();
                            switch (updateOfficer) {
                                case 1:
                                    crime.UpdateOfficerFirstName();
                                    break;
                                case 2:
                                    crime.UpdateOfficerLastName();
                                    break;
                                case 3:
                                    crime.UpdateOfficerBadge();
                                    break;
                                case 4:
                                    crime.UpdateOfficerRank();
                                    break;
                                case 5:
                                    crime.UpdateOfficerContact();
                                    break;
                                default:
                                    break;

                            }
                            Console.WriteLine("Type 1: To go back to Update Officers Menu");
                            Console.WriteLine("Type 2: To go back to ReportingOfficers Menu");
                            Console.WriteLine("Type 3: To go back to Main Menu");
                            Console.WriteLine("Type 4: To exit");
                            int option10 = int.Parse(Console.ReadLine());
                            switch (option10) {
                                case 1:
                                    goto UpdateOfficersMenu;
                                    break;
                                case 2:
                                    goto ReportingOfficersMenu;
                                    break;
                                case 3:
                                    goto MainMenu;
                                    break;
                                case 4:
                                    return;
                            }
                            break;
                        case 11:
                            crime.GetIncidents();
                            crime.GetEvidenceRelatedToIncident();
                            Console.WriteLine("Type 1: To go back to ReportingOfficers Menu");
                            Console.WriteLine("Type 2: To go back to Main Menu");
                            Console.WriteLine("Type 3: To exit");
                            int option11 = int.Parse(Console.ReadLine());
                            switch (option11) {
                                case 1:
                                    goto ReportingOfficersMenu;
                                    break;
                                case 2:
                                    goto MainMenu;
                                    break;
                                case 3:
                                    return;
                            }
                            break;
                        case 12:
                            crime.GetIncidents();
                            crime.GetSuspectsRelatedToIncident();
                            Console.WriteLine("Type 1: To go back to ReportingOfficers Menu");
                            Console.WriteLine("Type 2: To go back to Main Menu");
                            Console.WriteLine("Type 3: To exit");
                            int option12 = int.Parse(Console.ReadLine());
                            switch (option12) {
                                case 1:
                                    goto ReportingOfficersMenu;
                                    break;
                                case 2:
                                    goto MainMenu;
                                    break;
                                case 3:
                                    return;
                            }
                            break;
                        case 13:
                            crime.GetIncidents();
                            crime.GetVictimsRelatedToIncident();
                            Console.WriteLine("Type 1: To go back to ReportingOfficers Menu");
                            Console.WriteLine("Type 2: To go back to Main Menu");
                            Console.WriteLine("Type 3: To exit");
                            int option13 = int.Parse(Console.ReadLine());
                            switch (option13) {
                                case 1:
                                    goto ReportingOfficersMenu;
                                    break;
                                case 2:
                                    goto MainMenu;
                                    break;
                                case 3:
                                    return;
                            }
                            break;

                            break;
                        case 14:
                            crime.CreateCase();
                            Console.WriteLine("Type 1: To go back to ReportingOfficers Menu");
                            Console.WriteLine("Type 2: To go back to Main Menu");
                            Console.WriteLine("Type 3: To exit");
                            int option14 = int.Parse(Console.ReadLine());
                            switch (option14) {
                                case 1:
                                    goto ReportingOfficersMenu;
                                    break;
                                case 2:
                                    goto MainMenu;
                                    break;
                                case 3:
                                    return;
                            }
                            break;
                        case 15:
                            crime.AddToExsitingCase();
                            Console.WriteLine("Type 1: To go back to ReportingOfficers Menu");
                            Console.WriteLine("Type 2: To go back to Main Menu");
                            Console.WriteLine("Type 3: To exit");
                            int option15 = int.Parse(Console.ReadLine());
                            switch (option15) {
                                case 1:
                                    goto ReportingOfficersMenu;
                                    break;
                                case 2:
                                    goto MainMenu;
                                    break;
                                case 3:
                                    return;
                            }
                            break;
                        case 16:
                            crime.GetAllCases();
                            crime.GetAllIncidentsToASpecificCase();
                            Console.WriteLine("Type 1: To go back to ReportingOfficers Menu");
                            Console.WriteLine("Type 2: To go back to Main Menu");
                            Console.WriteLine("Type 3: To exit");
                            int option16 = int.Parse(Console.ReadLine());
                            switch (option16) {
                                case 1:
                                    goto ReportingOfficersMenu;
                                    break;
                                case 2:
                                    goto MainMenu;
                                    break;
                                case 3:
                                    return;
                            }
                            break;
                        case 17:
                            crime.GetIncidents();
                            crime.UpdateStatusOfIncident();
                            Console.WriteLine("Type 1: To go back to ReportingOfficers Menu");
                            Console.WriteLine("Type 2: To go back to Main Menu");

                            Console.WriteLine("Type 3: To exit");
                            int option17 = int.Parse(Console.ReadLine());
                            switch (option17) {
                                case 1:
                                    goto ReportingOfficersMenu;
                                    break;
                                case 2:
                                    goto MainMenu;
                                    break;
                                case 3:
                                    return;
                            }
                            break;
                        case 18:
                            crime.GetAllCases();
                            crime.GetCaseDetails();
                            Console.WriteLine("Type 1: To go back to ReportingOfficers Menu");
                            Console.WriteLine("Type 2: To go back to Main Menu");

                            Console.WriteLine("Type 3: To exit");
                            int option18 = int.Parse(Console.ReadLine());
                            switch (option18) {
                                case 1:
                                    goto ReportingOfficersMenu;
                                    break;
                                case 2:
                                    goto MainMenu;
                                    break;
                                case 3:
                                    return;
                                    break;
                            }
                            break;
                        case 19:
                            crime.GetAllCases();
                            Console.WriteLine("Type 1: To go back to ReportingOfficers Menu");
                            Console.WriteLine("Type 2: To go back to Main Menu");
                            Console.WriteLine("Type 3: To exit");
                            int option19 = int.Parse(Console.ReadLine());
                            switch (option19) {
                                case 1:
                                    goto ReportingOfficersMenu;
                                    break;
                                case 2:
                                    goto MainMenu;
                                    break;
                                case 3:
                                    return;
                                    break;
                            }
                            break;
                        case 20:
                            crime.GetAllCases();
                            crime.UpdateCaseDetails();
                            Console.WriteLine("Type 1: To go back to ReportingOfficers Menu");
                            Console.WriteLine("Type 2: To go back to Main Menu");
                            Console.WriteLine("Type 3: To exit");
                            int option20 = int.Parse(Console.ReadLine());
                            switch (option20) {
                                case 1:
                                    goto ReportingOfficersMenu;
                                    break;
                                case 2:
                                    goto MainMenu;
                                    break;
                                case 3:
                                    return;
                                    break;
                            }
                            break;
                        case 21:
                            goto MainMenu;
                        default:
                            Console.WriteLine("Wrong value entered");
                            goto ReportingOfficersMenu;
                            break;

                    }

                    break;

                case 5:

                    return;
                default:

                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.ForegroundColor = ConsoleColor.White;

                    Console.WriteLine("Wrong value entered");
                    Console.ResetColor();
                    goto MainMenu;
                    break;
            }

        }
        public void Exit() {
            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("Hope you query is resolved!");
            Console.WriteLine("Thank you for visiting!");
            Console.ResetColor();
        }
    }

}

