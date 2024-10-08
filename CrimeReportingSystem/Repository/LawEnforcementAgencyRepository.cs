using CrimeReportingSystem.Model;
using CrimeReportingSystem.Util;
using Microsoft.VisualBasic;
using Spectre.Console;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrimeReportingSystem.Repository {
    internal class LawEnforcementAgencyRepository:ILawEnforcementAgencyRepository {
        SqlCommand command = null;

        public LawEnforcementAgencyRepository() {
            command = new SqlCommand();
        }

        public bool AddNewAgency(LawEnforcementAgencies agency) {

            string query = "INSERT INTO LawEnforcementAgencies(AgencyName, Jurisdiction, Contact)" +
                            "VALUES(@AgencyName,@Jurisdiction,@Contact)";


            using(SqlConnection connection=new SqlConnection(DBConnUtil.GetConnectionString())) { 
                command.CommandText = query;
                command.Connection = connection;
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@AgencyName", agency.AgencyName);
                command.Parameters.AddWithValue("@Jurisdiction", agency.Jurisdiction);
                command.Parameters.AddWithValue("@Contact", agency.AgencyContact);
                connection.Open();

                
                int input=command.ExecuteNonQuery();
                if (input > 0) {
                    return true;
                }
                else {
                    return false;
                }
                
                
            }
        }

        public bool DeleteAgency(int AgencyId) {
            using (SqlConnection connection = new SqlConnection(DBConnUtil.GetConnectionString())) {
                command.CommandText = "DELETE FROM LawEnforcementAgencies WHERE AgencyId=@AgencyId";
                command.Connection= connection;
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@AgencyId", AgencyId); // This adds value to the sql query that we write above in place of @AgencyId
                connection.Open();
                
                int input=command.ExecuteNonQuery();
                if (input > 0) {
                    return true;
                }
                else {
                    return false;
                }

            }
           
        }

        public List<LawEnforcementAgencies> GetAllAgencies() {
            List<LawEnforcementAgencies> NewAgency= new List<LawEnforcementAgencies>();
            var table = new Table();
            table.AddColumn("Agency ID");
            table.AddColumn("Agency Name");
            table.AddColumn("Jurisdiction");
            table.AddColumn("Contact");
            

            using (SqlConnection connection = new SqlConnection(DBConnUtil.GetConnectionString())) {
                command.CommandText = "SELECT * FROM LawEnforcementAgencies";
                command.Connection = connection;
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader()) {
                    while (reader.Read()) {
                        LawEnforcementAgencies agency = new LawEnforcementAgencies();
                        agency.AgencyId = (int)reader["AgencyId"];
                        agency.AgencyName = (string)reader["AgencyName"];
                        agency.Jurisdiction = (string)reader["Jurisdiction"];
                        agency.AgencyContact = (string)reader["Contact"];

                        table.AddRow(
                                agency.AgencyId.ToString(),
                                agency.AgencyName,
                                agency.Jurisdiction,
                                agency.AgencyContact
                        );
                                
                    }
                }
                AnsiConsole.Write(table);
                return null;
            }                        
        }

        public bool UpdateAgencyName(int agencyId,string newName) {
            using (SqlConnection connection = new SqlConnection(DBConnUtil.GetConnectionString())) {
                command.CommandText = $"UPDATE LawEnforcementAgencies SET AgencyName='{newName}' WHERE AgencyId={agencyId}";
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
        public bool UpdateAgencyJurisdiction(int agencyId, string newName) {
            using (SqlConnection connection = new SqlConnection(DBConnUtil.GetConnectionString())) {
                command.CommandText = $"UPDATE LawEnforcementAgencies SET Jurisdiction='{newName}' WHERE AgencyId={agencyId}";
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
        public bool UpdateAgencyContact(int agencyId, string newName) {
            using (SqlConnection connection = new SqlConnection(DBConnUtil.GetConnectionString())) {
                command.CommandText = $"UPDATE LawEnforcementAgencies SET Contact='{newName}' WHERE AgencyId={agencyId}";
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


