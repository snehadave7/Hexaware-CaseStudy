using CrimeReportingSystem.Model;
using CrimeReportingSystem.Util;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CrimeReportingSystem.Model;
using Spectre.Console;
namespace CrimeReportingSystem.Repository {
    internal class CaseRepository : ICaseRepository {
        SqlCommand command = null;
        public CaseRepository() {
            command = new SqlCommand();
        }

        public bool CreateCase(Cases cases, int incidentId) {
            string query = "INSERT INTO Cases(CaseDescription,CaseDate) OUTPUT INSERTED.CaseId " +
                            "VALUES(@CaseDescription,@CaseDate)";


            using (SqlConnection connection = new SqlConnection(DBConnUtil.GetConnectionString())) {
                command.CommandText = query;
                command.Connection = connection;
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@CaseDescription", cases.CaseDescription);
                command.Parameters.AddWithValue("@CaseDate", cases.Date);


                connection.Open();


                int caseId = (int)command.ExecuteScalar();

                if (caseId != null) {
                    CaseIncidentMapRepo map = new CaseIncidentMapRepo();

                    map.CreateCaseIncidentMap(caseId, incidentId);
                    return true;
                }
                else {
                    
                    return false;
                }
            }
        }


        public List<Cases> GetAllCases() {
            List<Cases> cases = new List<Cases>();

            var table = new Table();
            table.AddColumn("Case ID");
            table.AddColumn("Case Description");
            table.AddColumn("Case Date");
            using (SqlConnection connection = new SqlConnection(DBConnUtil.GetConnectionString())) {
                command.CommandText = "SELECT * FROM Cases";
                command.Connection = connection;
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader()) {
                    while (reader.Read()) {
                        Cases case1 = new Cases();
                        case1.CaseId = (int)reader["CaseId"];
                        case1.CaseDescription = (string)reader["CaseDescription"];
                        case1.Date = (DateTime)reader["CaseDate"];


                        table.AddRow(
                            case1.CaseId.ToString(),
                            case1.CaseDescription,
                            case1.Date.ToString("yyyy-MM-dd")
                        );
                    }
                }
                AnsiConsole.Write(table);
                
                return null;
            }

        }

        public bool GetAllIncidentForACase(int caseId) {
         
            var table = new Table();
            table.AddColumn("Incident ID");
            table.AddColumn("Incident Type");
            table.AddColumn("Incident Date");
            table.AddColumn("Location");
            table.AddColumn("Description");
            table.AddColumn("Status");


            using (SqlConnection connection = new SqlConnection(DBConnUtil.GetConnectionString())) {
                command.CommandText = "SELECT I.* FROM Incidents I " +
                    "INNER JOIN CaseIncidentMap M ON I.IncidentId = M.IncidentId " +
                    "INNER JOIN Cases C ON M.CaseId = C.CaseId " +
                    "WHERE C.CaseId = @CaseId ";
                command.Connection = connection;
                connection.Open();
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@CaseId", caseId);
                using (SqlDataReader reader = command.ExecuteReader()) {
                    bool hasRows = false;

                    while (reader.Read()) {
                        hasRows = true;
                        Incidents i = new Incidents();
                        i.IncidentId = (int)reader["IncidentId"];
                        i.IncidentType = (string)reader["IncidentType"];
                        i.IncidentDate = (DateTime)reader["IncidentDate"];
                        i.Location = (string)reader["Location"];
                        i.Description = (string)reader["Description"];
                        i.Status = (string)reader["status"];

                        //incident.Add(i);
                        table.AddRow(
                                    i.IncidentId.ToString(),
                                    i.IncidentType,
                                    i.IncidentDate.ToString("yyyy-MM-dd"),
                                    i.Location,
                                    i.Description,
                                    i.Status
                        );

                    }
                    if (hasRows) {
                        AnsiConsole.Write(table);
                        return true;
                    }
                    else return false;
                    

                }

            }

        }

        public Cases GetCaseDetailsForACase(int caseId) {
            
            Cases case1 = new Cases();
            List<Incidents> incidents = new List<Incidents>();
            using (SqlConnection connection = new SqlConnection(DBConnUtil.GetConnectionString())) {
                

                command.CommandText = "SELECT * FROM Cases where caseId=@caseId";
                command.Connection = connection;
                connection.Open();
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@caseId", caseId);
                using (SqlDataReader reader = command.ExecuteReader()) {
                    if (reader.Read()) {

                        case1.CaseId = (int)reader["CaseId"];
                        case1.CaseDescription = (string)reader["CaseDescription"];
                        case1.Date = (DateTime)reader["CaseDate"];

                    }
                }

                command.CommandText = "SELECT I.* FROM Incidents I " +
                    "INNER JOIN CaseIncidentMap M ON I.IncidentId = M.IncidentId " +
                    "WHERE M.CaseId = @CaseId ";
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@CaseId", caseId);

                using (SqlDataReader reader = command.ExecuteReader()) {
                    while (reader.Read()) {

                        Incidents i = new Incidents();
                        i.IncidentId = (int)reader["IncidentId"];
                        i.IncidentType = (string)reader["IncidentType"];
                        i.IncidentDate = (DateTime)reader["IncidentDate"];
                        i.Location = (string)reader["Location"];
                        i.Description = (string)reader["Description"];
                        i.Status = (string)reader["status"];
                        i.AgencyId = (int)reader["AgencyId"];
                        incidents.Add(i);
                    }
                    case1.incidents = incidents;
                }
                return case1;
            }
            
        }
    

        public bool UpdateCase(int caseId, string description) {
            using (SqlConnection connection = new SqlConnection(DBConnUtil.GetConnectionString())) {
                command.CommandText = $"UPDATE cases SET CaseDescription='{description}' WHERE caseId={caseId}";
                command.Connection = connection;

                connection.Open();

                int input = command.ExecuteNonQuery();
                if (input > 0) {
                    return true;
                    
                }
                else {
                    return false;
                   
                }
            }
        }
    }
}
