using CrimeReportingSystem.Model;
using CrimeReportingSystem.Util;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrimeReportingSystem.Repository {

    internal class OfficerRepository : IOfficerRepository {

        SqlCommand command = null;
        public OfficerRepository() {
            command = new SqlCommand();
        }

        public bool CreateOfficer(Officers officer) {
            string query = "INSERT INTO Officers(FirstName,LastName,BadgeNumber,Rank,MobileContact,AgencyId)" +
                            "VALUES(@FirstName,@LastName,@BadgeNumber,@Rank,@MobileContact,@AgencyId)";

            using (SqlConnection connection = new SqlConnection(DBConnUtil.GetConnectionString())) {
                command.CommandText = query;
                command.Connection = connection;
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@FirstName", officer.FirstName);
                command.Parameters.AddWithValue("@LastName", officer.LastName);
                command.Parameters.AddWithValue("@BadgeNumber", officer.BadgeNumber);
                command.Parameters.AddWithValue("@Rank", officer.Rank);
                command.Parameters.AddWithValue("@MobileContact", officer.Contact);
                command.Parameters.AddWithValue("@AgencyId", officer.AgencyId);


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

        public bool DeleteOfficer(int officerId) {
            using (SqlConnection connection = new SqlConnection(DBConnUtil.GetConnectionString())) {
                command.CommandText = "DELETE FROM Officers WHERE OfficerId=@OfficerId";
                command.Connection = connection;
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@OfficerId", officerId);
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

        public List<Officers> GetAllOfficers() {
            List<Officers> NewOfficer = new List<Officers>();

            var table = new Table();
            table.AddColumn("Officer ID");
            table.AddColumn("FirstName");
            table.AddColumn("LastName");
            table.AddColumn("BadgeNumber");
            table.AddColumn("Rank");
            table.AddColumn("MobileContact");
            


            using (SqlConnection connection = new SqlConnection(DBConnUtil.GetConnectionString())) {
                command.CommandText = "SELECT * FROM Officers";
                command.Connection = connection;
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader()) {
                    while (reader.Read()) {
                        Officers officer = new Officers();
                        officer.OfficerId = (int)reader["OfficerId"];
                        officer.FirstName = (string)reader["FirstName"];
                        officer.LastName = (string)reader["LastName"];
                        officer.BadgeNumber = (int)reader["BadgeNumber"];
                        officer.Rank = (string)reader["Rank"];
                        officer.Contact = (string)reader["MobileContact"];
                        if (reader["AgencyID"] != DBNull.Value) officer.AgencyId = (int)reader["AgencyId"];
                        else officer.AgencyId = 0;
             

                        table.AddRow(
                                officer.OfficerId.ToString(),
                                officer.FirstName,
                                officer.LastName,
                                officer.BadgeNumber.ToString(),
                                officer.Rank,
                                
                                officer.Contact
                        );
                    }
                }
                AnsiConsole.Write(table);
                return null;
            }
        }

        public bool UpdateOfficerBadge(int badgeNum, int officersId) {
            using (SqlConnection connection = new SqlConnection(DBConnUtil.GetConnectionString())) {
                command.CommandText = $"UPDATE Officers SET BadgeNumber='{badgeNum}' WHERE OfficerId={officersId}";
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

        public bool UpdateOfficerContact(string contact, int officersId) {
            using (SqlConnection connection = new SqlConnection(DBConnUtil.GetConnectionString())) {
                command.CommandText = $"UPDATE Officers SET MobileContact='{contact}' WHERE OfficerId={officersId}";
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

        public bool UpdateOfficerFirstName(string firstName, int officersId) {
            using (SqlConnection connection = new SqlConnection(DBConnUtil.GetConnectionString())) {
                command.CommandText = $"UPDATE Officers SET firstName='{firstName}' WHERE OfficerId={officersId}";
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

        public bool UpdateOfficerLastName(string lastName, int officersId) {
            using (SqlConnection connection = new SqlConnection(DBConnUtil.GetConnectionString())) {
                command.CommandText = $"UPDATE Officers SET lastName='{lastName}' WHERE OfficerId={officersId}";
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

        public bool UpdateOfficerRank(string rank, int officersId) {
            using (SqlConnection connection = new SqlConnection(DBConnUtil.GetConnectionString())) {
                command.CommandText = $"UPDATE Officers SET Rank='{rank}' WHERE OfficerId={officersId}";
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
        public bool GetOfficerById(int officerId) {
           
            using (SqlConnection connection = new SqlConnection(DBConnUtil.GetConnectionString())) {
                command.CommandText = $"SELECT * FROM Officers WHERE OfficerId={officerId}";
                command.Connection = connection;

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader()) {

                    if (reader.Read()) return true;
                }
                return false;
            }

        }
    }
}
