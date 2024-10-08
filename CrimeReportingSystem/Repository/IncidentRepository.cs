using CrimeReportingSystem.Model;
using CrimeReportingSystem.Util;
using Spectre.Console;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spectre.Console;
namespace CrimeReportingSystem.Repository {
    internal class IncidentRepository : IIncidentRepository {
        SqlCommand command = null;
        
        public IncidentRepository() {
            command = new SqlCommand();
        }

        public bool CheckIncidentStatus(int incidentId) {
            string query = "SELECT Status from Incidents WHERE IncidentId=@incidentId";
            using (SqlConnection connection = new SqlConnection(DBConnUtil.GetConnectionString())) {
                command.CommandText = query;
                command.Connection = connection;
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@incidentId", incidentId);
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader()) {
                    if (reader.Read()) {
                        string status = (string)reader["Status"];
                        if (status != null) {
                            Console.WriteLine("Your status is " + status);
                            return true;
                        }
                    }
                    return false;
                }

            }
        }

        public  int CreateIncident(Incidents incident) {
            string query = "INSERT INTO Incidents (IncidentType, IncidentDate, Description, Status, AgencyId, Location) " +
               "OUTPUT INSERTED.IncidentId " +
               "VALUES (@IncidentType, @IncidentDate, @Description, @Status, @AgencyId, @Location)";

            string status = "Open";
            using (SqlConnection connection = new SqlConnection(DBConnUtil.GetConnectionString())) {
                command.CommandText = query;
                command.Connection = connection;
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@IncidentType", incident.IncidentType);
                command.Parameters.AddWithValue("@IncidentDate", incident.IncidentDate);
                command.Parameters.AddWithValue("@Description", incident.Description);
                command.Parameters.AddWithValue("@Status", status);
                command.Parameters.AddWithValue("@AgencyId", incident.AgencyId);
                command.Parameters.AddWithValue("@Location", incident.Location);


                connection.Open();

                
                int input =(int)command.ExecuteScalar(); // incidentId
                return input;
                
            }
        }

        public bool DeleteIncident(int incidentId) {
            using (SqlConnection connection = new SqlConnection(DBConnUtil.GetConnectionString())) {
                command.CommandText = "DELETE FROM incidents WHERE incidentId=@incidentId";
                command.Connection = connection;
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@incidentId", incidentId);
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

        public List<Incidents> GetAllIncidents() {
            List<Incidents> NewIncident = new List<Incidents>();

            var table = new Table();
            table.AddColumn("Incident ID");
            table.AddColumn("Incident Type");
            table.AddColumn("Incident Date");
            table.AddColumn("Description");
            table.AddColumn("Status");
            table.AddColumn("AgencyId");
            table.AddColumn("Location");

            using (SqlConnection connection = new SqlConnection(DBConnUtil.GetConnectionString())) {
                command.CommandText = "SELECT * FROM Incidents";
                command.Connection = connection;
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader()) {
                    while (reader.Read()) {
                        Incidents incident = new Incidents();
                        incident.IncidentId = (int)reader["IncidentId"];
                        incident.IncidentType = (string)reader["IncidentType"];
                        incident.IncidentDate = (DateTime)reader["IncidentDate"];
                        incident.Description = (string)reader["Description"];
                        incident.Status = (string)reader["Status"];
                        
                        if (reader["AgencyID"] != DBNull.Value) incident.AgencyId = (int)reader["AgencyId"];
                        else incident.AgencyId = 0;
                        incident.Location = (string)reader["Location"];

                        table.AddRow(
                        incident.IncidentId.ToString(),
                        incident.IncidentType,
                        incident.IncidentDate.ToString("yyyy-MM-dd"),
                        incident.Description,
                        incident.Status,
                        incident.AgencyId.ToString(),
                        incident.Location);
                    }
                }
                AnsiConsole.Write(table);
                return null;
            }

        }


        public List<Incidents> GetIncidentsInDateRange(DateTime startDate, DateTime endDate) {
            List<Incidents> GetIncident = new List<Incidents>();
            var table = new Table();
            table.AddColumn("Incident ID");
            table.AddColumn("Incident Type");
            table.AddColumn("Incident Date");
            table.AddColumn("Description");
            table.AddColumn("Status");
            table.AddColumn("AgencyId");
            table.AddColumn("Location");

            using (SqlConnection connection = new SqlConnection(DBConnUtil.GetConnectionString())) {
                command.CommandText = $"SELECT * FROM Incidents where IncidentDate BETWEEN '{startDate}' AND '{endDate}'";
                command.Connection = connection;
                connection.Open();
                

                using (SqlDataReader reader = command.ExecuteReader()) {
                    while (reader.Read()) {
                        Incidents incident = new Incidents();
                        incident.IncidentId = (int)reader["IncidentId"];
                        incident.IncidentType = (string)reader["IncidentType"];
                        incident.IncidentDate = (DateTime)reader["IncidentDate"];
                        incident.Description = (string)reader["Description"];
                        incident.Status = (string)reader["Status"];
                        incident.AgencyId = (int)reader["AgencyId"];
                        incident.Location = (string)reader["Location"];

                        GetIncident.Add(incident);
                        table.AddRow(
                        incident.IncidentId.ToString(),
                        incident.IncidentType,
                        incident.IncidentDate.ToString("yyyy-MM-dd"),
                        incident.Description,
                        incident.Status,
                        incident.AgencyId.ToString(),
                        incident.Location);

                    }
                }
                AnsiConsole.Write(table);
                return null;
            }
        }
        public List<Incidents> GetIncidentsOfSameType(string type) {
            List<Incidents> GetIncident = new List<Incidents>();
            var table = new Table();
            table.AddColumn("Incident ID");
            table.AddColumn("Incident Type");
            table.AddColumn("Incident Date");
            table.AddColumn("Description");
            table.AddColumn("Status");
            table.AddColumn("AgencyId");
            table.AddColumn("Location");


            using (SqlConnection connection = new SqlConnection(DBConnUtil.GetConnectionString())) {
                command.CommandText = "SELECT * FROM Incidents WHERE IncidentType LIKE @type";
                command.Connection = connection;
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@type", $"%{type}%");
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader()) {
                    while (reader.Read()) {
                        Incidents incident = new Incidents();
                        incident.IncidentId = (int)reader["IncidentId"];
                        incident.IncidentType = (string)reader["IncidentType"];
                        incident.IncidentDate = (DateTime)reader["IncidentDate"];
                        incident.Description = (string)reader["Description"];
                        incident.Status = (string)reader["Status"];
                        incident.AgencyId = (int)reader["AgencyId"];
                        incident.Location = (string)reader["Location"];

                        GetIncident.Add(incident);
                        
                        table.AddRow(
                        incident.IncidentId.ToString(),
                        incident.IncidentType,
                        incident.IncidentDate.ToString("yyyy-MM-dd"),
                        incident.Description,
                        incident.Status,
                        incident.AgencyId.ToString(),
                        incident.Location);
                    }
                }
                AnsiConsole.Write(table);
                return null;

            }
        }
        public List<Incidents> SearchIncidents(string incidentType) {
            List<Incidents> SearchIncident = new List<Incidents>();

            using (SqlConnection connection = new SqlConnection(DBConnUtil.GetConnectionString())) {
                command.CommandText = $"SELECT * FROM Incidents where incidentType={incidentType}";
                command.Connection = connection;
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader()) {
                    while (reader.Read()) {
                        Incidents incident = new Incidents();
                        incident.IncidentId = (int)reader["IncidentId"];
                        incident.IncidentType = (string)reader["IncidentType"];
                        incident.IncidentDate = (DateTime)reader["IncidentDate"];
                        incident.Description = (string)reader["Description"];
                        incident.Status = (string)reader["Status"];
                        incident.AgencyId = (int)reader["AgencyId"];
                        incident.Location = (string)reader["Location"];

                        SearchIncident.Add(incident);

                    }
                }

                return SearchIncident;

            }
        }

        public bool UpdateIncidentDate(DateTime incidentDate, int incidentId) {
            using (SqlConnection connection = new SqlConnection(DBConnUtil.GetConnectionString())) {
                command.CommandText = $"UPDATE incidents SET incidentDate='{incidentDate}' WHERE incidentId={incidentId}";
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

        public bool UpdateIncidentDescription(string incidentDescription, int incidentId) {
            using (SqlConnection connection = new SqlConnection(DBConnUtil.GetConnectionString())) {
                command.CommandText = $"UPDATE incidents SET Description='{incidentDescription}' WHERE incidentId={incidentId}";
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

        public bool UpdateIncidentLocation(string incidentLocation, int incidentId) {
            using (SqlConnection connection = new SqlConnection(DBConnUtil.GetConnectionString())) {
                command.CommandText = $"UPDATE incidents SET Location='{incidentLocation}' WHERE incidentId={incidentId}";
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

        public bool UpdateIncidentStatus(string status, int incidentId) {
            using (SqlConnection connection = new SqlConnection(DBConnUtil.GetConnectionString())) {
                command.CommandText = $"UPDATE incidents SET status ='{status}' WHERE incidentId={incidentId}";
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

        public bool UpdateIncidentType(string incidentType, int incidentId) {
            using (SqlConnection connection = new SqlConnection(DBConnUtil.GetConnectionString())) {
                command.CommandText = $"UPDATE incidents SET incidentType='{incidentType}' WHERE incidentId={incidentId}";
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

        public Incidents GetIncidentById(int incidentId) {
            
            Incidents incident = new Incidents();
            using (SqlConnection connection = new SqlConnection(DBConnUtil.GetConnectionString())) {
                command.CommandText = "SELECT * FROM Incidents WHERE IncidentId= @incidentId";
                command.Connection = connection;
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@incidentId", incidentId);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader()) {

                    while (reader.Read()) {

                        incident.IncidentId = (int)reader["IncidentId"];
                        incident.IncidentType = (string)reader["IncidentType"];
                        incident.IncidentDate = (DateTime)reader["IncidentDate"];
                        incident.Description = (string)reader["Description"];
                        incident.Status = (string)reader["Status"];
                        incident.AgencyId = (int)reader["AgencyId"];
                        incident.Location = (string)reader["Location"];

                    }

                }
            }
            return incident;
        }

        public void GetIncidentByLocation(string location) {
            
            var table = new Table();
            table.AddColumn("Incident ID");
            table.AddColumn("Incident Type");
            table.AddColumn("Incident Date");
            table.AddColumn("Description");
            table.AddColumn("Status");
            table.AddColumn("AgencyId");
            table.AddColumn("Location");


            using (SqlConnection connection = new SqlConnection(DBConnUtil.GetConnectionString())) {
                command.CommandText = "SELECT * FROM Incidents WHERE Location LIKE @location";
                command.Connection = connection;
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@location", $"%{location}%");
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader()) {
                    while (reader.Read()) {
                        Incidents incident = new Incidents();
                        incident.IncidentId = (int)reader["IncidentId"];
                        incident.IncidentType = (string)reader["IncidentType"];
                        incident.IncidentDate = (DateTime)reader["IncidentDate"];
                        incident.Description = (string)reader["Description"];
                        incident.Status = (string)reader["Status"];
                        incident.AgencyId = (int)reader["AgencyId"];
                        incident.Location = (string)reader["Location"];

                        table.AddRow(
                        incident.IncidentId.ToString(),
                        incident.IncidentType,
                        incident.IncidentDate.ToString("yyyy-MM-dd"),
                        incident.Description,
                        incident.Status,
                        incident.AgencyId.ToString(),
                        incident.Location);
                    }
                }
                AnsiConsole.Write(table);
            }
            }
    }
}

