using CrimeReportingSystem.Model;
using CrimeReportingSystem.Util;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spectre.Console;
using CrimeReportingSystem.Exceptions;
using System.Diagnostics.Metrics;
using System.Runtime.InteropServices;
using System.Net;
namespace CrimeReportingSystem.Repository {
    internal class ReportRepository : IReportRepository {
        SqlCommand command = null;

        public ReportRepository() {
            command = new SqlCommand();
        }

        public bool CreateReport(Reports report) {
            string query = "INSERT INTO Reports(IncidentId,ReportingOfficerId,ReportDate,ReportDetails,Status)" +
                            "VALUES(@IncidentId,@ReportingOfficerId,@ReportDate,@ReportDetails,@Status)";

            using (SqlConnection connection = new SqlConnection(DBConnUtil.GetConnectionString())) {
                command.CommandText = query;
                command.Connection = connection;
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@IncidentId", report.IncidentId);
                command.Parameters.AddWithValue("@ReportingOfficerId", report.ReportingOfficerID);
                command.Parameters.AddWithValue("@ReportDate", report.ReportDate);
                command.Parameters.AddWithValue("@ReportDetails", report.ReportDetails);
                command.Parameters.AddWithValue("@Status", report.Status);


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

        public bool DeleteReport(int reportId) {
            using (SqlConnection connection = new SqlConnection(DBConnUtil.GetConnectionString())) {
                command.CommandText = "DELETE FROM Reports WHERE ReportId=@ReportId";
                command.Connection = connection;
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@ReportId", reportId);
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

        public List<Reports> GetAllReports() {
            List<Reports> NewReport = new List<Reports>();

            var table = new Table();
            table.AddColumn("Report ID");
            table.AddColumn("Incident Id");
            table.AddColumn("ReportingOfficer ID");
            table.AddColumn("Report Date");
            table.AddColumn("Report Details");
            table.AddColumn("Status");

            using (SqlConnection connection = new SqlConnection(DBConnUtil.GetConnectionString())) {
                command.CommandText = "SELECT * FROM Reports";
                command.Connection = connection;
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader()) {
                    while (reader.Read()) {
                        Reports report = new Reports();
                        report.ReportId = (int)reader["ReportId"];
                        report.IncidentId = (int)reader["IncidentId"];
                        if (reader["ReportingOfficerId"] != DBNull.Value)
                            report.ReportingOfficerID = (int)reader["ReportingOfficerID"];
                        else report.ReportingOfficerID = 0;

                        report.ReportDate = (DateTime)reader["ReportDate"];
                        report.ReportDetails = (string)reader["ReportDetails"];
                        report.Status = (string)reader["Status"];

                        table.AddRow(
                                report.ReportId.ToString(),
                                report.IncidentId.ToString(),
                                report.ReportingOfficerID.ToString(),
                                report.ReportDate.ToString(),
                                report.ReportDetails.ToString(),
                                report.Status
                        );
                    }
                }
                AnsiConsole.Write(table);
                return null;
            }
        }

        public bool GenerateReportForAnIncident(int incidentId) {
            List<Reports> NewReport = new List<Reports>();

            using (SqlConnection connection = new SqlConnection(DBConnUtil.GetConnectionString())) {
                command.CommandText = "SELECT " +
                                    "O.FirstName AS FirstName, " + // Officer's first name
                                    "O.LastName AS LastName, " + // Officer's last name
                                    "R.ReportDate, R.ReportDetails, " +
                                    "I.IncidentType, I.Description, I.Status, I.Location, I.IncidentDate, " +
                                    "V.FirstName AS VictimFirstName, V.LastName AS VictimLastName, " + // Victim's names
                                    "V.Gender, V.VictimAddress " +
                                    "FROM Reports AS R " +
                                    "JOIN Incidents AS I ON R.IncidentId = I.IncidentId " +
                                    "JOIN VictimIncidentMap AS VI ON I.IncidentId = VI.IncidentId " +
                                    "JOIN Victims AS V ON VI.VictimId = V.VictimId " +
                                    "JOIN Officers AS O ON R.ReportingOfficerID  = O.OfficerId " +
                                    "WHERE R.IncidentId = @IncidentId"; 



                command.Connection = connection;
                connection.Open();
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@IncidentId", incidentId);
                using (SqlDataReader reader = command.ExecuteReader()) {
                    if (!reader.HasRows) {
                        return false; // Exit if no results
                    }
                    else {

                        var table = new Table();
                        table.AddColumn("Officer Full Name"); // Single column for full name
                        table.AddColumn("Report Date");
                        table.AddColumn("Report Details");
                        table.AddColumn("Incident Type");
                        table.AddColumn("Description");
                        table.AddColumn("Status");
                        table.AddColumn("Location");
                        table.AddColumn("Incident Date");
                        table.AddColumn("Victim Full Name");
                        table.AddColumn("Gender");
                        table.AddColumn("Address");

                        while (reader.Read()) {
                            string officerFirstName = reader["FirstName"].ToString();
                            string officerLastName = reader["LastName"].ToString();

                            StringBuilder officerFullName = new StringBuilder();
                            officerFullName.Append(officerFirstName).Append(" ").Append(officerLastName);

                            DateTime reportDate = (DateTime)reader["ReportDate"];
                            string reportDetails = reader["ReportDetails"].ToString();
                            string incidentType = reader["IncidentType"].ToString();
                            string description = reader["Description"].ToString();
                            string status = reader["Status"].ToString();
                            string location = reader["Location"].ToString();
                            DateTime incidentDate = (DateTime)reader["IncidentDate"];

                            string victimFirstName = reader["VictimFirstName"].ToString();
                            string victimLastName = reader["VictimLastName"].ToString();

                            StringBuilder victimFullName = new StringBuilder();
                            victimFullName.Append(victimFirstName).Append(" ").Append(victimLastName);

                            string victimGender = reader["Gender"].ToString();
                            string Address = reader["VictimAddress"].ToString();

                            table.AddRow(
                            officerFullName.ToString(),
                            reportDate.ToString("yyyy-MM-dd"),
                            reportDetails,
                            incidentType,
                            description,
                            status,
                            location,
                            incidentDate.ToString("yyyy-MM-dd"),
                            victimFullName.ToString(),
                            victimGender,
                            Address
                        );



                        }
                        AnsiConsole.Write(table);
                        return true;
                    }

                }
            }

        }

        public bool UpdateReportByDate(DateTime date, int reportId) {
            using (SqlConnection connection = new SqlConnection(DBConnUtil.GetConnectionString())) {
                command.CommandText = $"UPDATE Reports SET ReportDate='{date}' WHERE ReportId={reportId}";
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

        public bool UpdateReportByStatus(string status, int reportId) {
            using (SqlConnection connection = new SqlConnection(DBConnUtil.GetConnectionString())) {
                command.CommandText = $"UPDATE Reports SET Status='{status}' WHERE ReportId={reportId}";
                command.Connection = connection;

                connection.Open();
                try {
                    int input = command.ExecuteNonQuery();
                    if (input > 0) {
                        return true;

                    }
                    else {

                        throw new InvalidStatusException("No rows were updated. Please check the report ID.");
                    }
                }
                catch (SqlException ex) {
                    throw new InvalidStatusException("You have entered wrong value for status!");
                }


            }
        }
        public List<Reports> GetReportById(int reportId) {
            List<Reports> NewReport = new List<Reports>();
            using (SqlConnection connection = new SqlConnection(DBConnUtil.GetConnectionString())) {
                command.CommandText = $"SELECT * FROM Reports WHERE ReportId={reportId}";
                command.Connection = connection;

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader()) {
                    while (reader.Read()) {
                        Reports report = new Reports();
                        report.ReportId = (int)reader["ReportId"];
                        report.IncidentId = (int)reader["IncidentId"];
                        if (reader["ReportingOfficerId"] != DBNull.Value)
                            report.ReportingOfficerID = (int)reader["ReportingOfficerID"];
                        else report.ReportingOfficerID = 0;

                        report.ReportDate = (DateTime)reader["ReportDate"];
                        report.ReportDetails = (string)reader["ReportDetails"];
                        report.Status = (string)reader["Status"];

                        NewReport.Add(report);
                    }
                }
                return NewReport;
            }

        }
    }
}
