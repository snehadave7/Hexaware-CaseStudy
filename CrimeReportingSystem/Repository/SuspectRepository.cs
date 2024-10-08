using CrimeReportingSystem.Model;
using CrimeReportingSystem.Util;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrimeReportingSystem.Repository {
    internal class SuspectRepository : ISuspectRepository {
        SqlCommand command = null;
        public SuspectRepository() {
            command = new SqlCommand();
        }

        public bool CreateNewSuspect(Suspects suspects,int incidentId) {
            string query = "INSERT INTO Suspects(FirstName,LastName,DateOfBirth,Gender,SuspectAddress,SuspectContact) OUTPUT INSERTED.SuspectId " +
                            "VALUES(@FirstName,@LastName,@DateOfBirth,@Gender,@SuspectAddress,@SuspectContact)";


            using (SqlConnection connection = new SqlConnection(DBConnUtil.GetConnectionString())) {
                command.CommandText = query;
                command.Connection = connection;
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@FirstName", suspects.FirstName);
                command.Parameters.AddWithValue("@LastName", suspects.LastName);
                command.Parameters.AddWithValue("DateOfBirth", suspects.DateOfBirth);
                command.Parameters.AddWithValue("Gender", suspects.Gender);
                command.Parameters.AddWithValue("SuspectAddress", suspects.SuspectAddress);
                command.Parameters.AddWithValue("SuspectContact", suspects.SuspectContact);

                connection.Open();

                
                int suspectId=(int)command.ExecuteScalar();
                if (suspectId!=null) {
                    SuspectIncidentMapRepository map = new SuspectIncidentMapRepository();
                    map.CreateSuspectIncidentMap(suspectId,incidentId);
                    return true;
                    
                }
                else {
                    return false;
                }
            }
        }

        public bool DeleteSuspect(int suspectId) {
            using (SqlConnection connection = new SqlConnection(DBConnUtil.GetConnectionString())) {
                command.CommandText = "DELETE FROM Suspects WHERE SuspectId=@SuspectId";
                command.Connection = connection;
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@SuspectId", suspectId);
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

        public List<Suspects> GetAllSuspects() {
            List<Suspects> NewSuspects = new List<Suspects>();

            using (SqlConnection connection = new SqlConnection(DBConnUtil.GetConnectionString())) {
                command.CommandText = $"SELECT * FROM Suspects ";
                command.Connection = connection;
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader()) {
                    while (reader.Read()) {
                        Suspects suspects = new Suspects();
                        suspects.SuspectId = (int)reader["SuspectId"];
                        suspects.FirstName = (string)reader["FirstName"];
                        suspects.LastName = (string)reader["LastName"];
                        suspects.DateOfBirth = (DateTime)reader["DateOfBirth"];
                        suspects.Gender = (string)reader["Gender"];
                        suspects.SuspectAddress = (string)reader["SuspectAddress"];
                        suspects.SuspectContact = (string)reader["SuspectContact"];

                        NewSuspects.Add(suspects);
                    }
                }              
                    return NewSuspects;
            }
        }

        public List<Suspects> GetAllSuspectsRelatedToAnIncident(int incidentId) {
            List<Suspects> NewSuspects = new List<Suspects>();

            using (SqlConnection connection = new SqlConnection(DBConnUtil.GetConnectionString())) {
                command.CommandText = "SELECT S.* FROM SuspectIncidentMap AS SI JOIN Suspects as S ON SI.SuspectId=S.SuspectId " +
                    $"WHERE SI.IncidentId={incidentId}";
                 
                command.Connection = connection;
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader()) {
                    while (reader.Read()) {
                        Suspects suspects = new Suspects();
                        suspects.SuspectId = (int)reader["SuspectId"];
                        suspects.FirstName = (string)reader["FirstName"];
                        suspects.LastName = (string)reader["LastName"];
                        suspects.DateOfBirth = (DateTime)reader["DateOfBirth"];
                        suspects.Gender = (string)reader["Gender"];
                        suspects.SuspectAddress = (string)reader["SuspectAddress"];
                        suspects.SuspectContact = (string)reader["SuspectContact"];

                        NewSuspects.Add(suspects);

                    }
                }
                return NewSuspects;
            }
        }

        public bool UpdateSuspectByAddress(string address, int suspectID) {
            using (SqlConnection connection = new SqlConnection(DBConnUtil.GetConnectionString())) {
                command.CommandText = $"UPDATE Suspects SET SuspectAddress='{address}' WHERE SuspectId={suspectID}";
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

        public bool UpdateSuspectByContact(string contact, int suspectID) {
            using (SqlConnection connection = new SqlConnection(DBConnUtil.GetConnectionString())) {
                command.CommandText = $"UPDATE Suspects SET SuspectContact='{contact}' WHERE SuspectId={suspectID}";
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

        public bool UpdateSuspectByFirstName(string firstName, int suspectID) {
            using (SqlConnection connection = new SqlConnection(DBConnUtil.GetConnectionString())) {
                command.CommandText = $"UPDATE Suspects SET FirstName='{firstName}' WHERE SuspectId={suspectID}";
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

        public bool UpdateSuspectByLastName(string lastName, int suspectID) {
            using (SqlConnection connection = new SqlConnection(DBConnUtil.GetConnectionString())) {
                command.CommandText = $"UPDATE Suspects SET LastName='{lastName}' WHERE SuspectId={suspectID}";
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
