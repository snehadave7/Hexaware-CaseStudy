using CrimeReportingSystem.Model;
using CrimeReportingSystem.Util;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace CrimeReportingSystem.Repository {
    internal class VictimRepository : IVictimRepository {

        SqlCommand command = null;
        public VictimRepository() {
            command = new SqlCommand();
        }

        public bool CreateNewVictim(Victims victims,int incidentId) {
            string query = "INSERT INTO Victims(FirstName,LastName,DateOfBirth,Gender,VictimAddress,VictimContact) OUTPUT INSERTED.VictimId " +
                            "VALUES(@FirstName,@LastName,@DateOfBirth,@Gender,@VictimAddress,@VictimContact)";


            using (SqlConnection connection = new SqlConnection(DBConnUtil.GetConnectionString())) {
                command.CommandText = query;
                command.Connection = connection;
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@FirstName", victims.FirstName);
                command.Parameters.AddWithValue("@LastName", victims.LastName);
                command.Parameters.AddWithValue("DateOfBirth", victims.DateOfBirth);
                command.Parameters.AddWithValue("Gender", victims.Gender);
                command.Parameters.AddWithValue("VictimAddress", victims.VictimAddress);
                command.Parameters.AddWithValue("VictimContact", victims.VictimContact);

                connection.Open();


                int victimId = (int)command.ExecuteScalar();
                if (victimId != null) {
                    // Call the create mapping function
                    VictimIncidentMapRepository map = new VictimIncidentMapRepository();
                    map.CreateVictimIncidentMap(victimId,incidentId);
                    return true;

                }
                else {
                    return false;
                }
            }
        }

        public bool DeleteVictim(int victimId) {
            using (SqlConnection connection = new SqlConnection(DBConnUtil.GetConnectionString())) {
                command.CommandText = "DELETE FROM Victims WHERE VictimId=@VictimId";
                command.Connection = connection;
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@VictimId", victimId);
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

        public List<Victims> GetAllVictims() {
            List<Victims> NewVictims = new List<Victims>();

            using (SqlConnection connection = new SqlConnection(DBConnUtil.GetConnectionString())) {
                command.CommandText = $"SELECT * FROM Victims ";
                command.Connection = connection;
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader()) {
                    while (reader.Read()) {
                        Victims victims = new Victims();
                        victims.VictimId = (int)reader["VictimId"];
                        victims.FirstName = (string)reader["FirstName"];
                        victims.LastName = (string)reader["LastName"];
                        victims.DateOfBirth = (DateTime)reader["DateOfBirth"];
                        victims.Gender = (string)reader["Gender"];
                        victims.VictimAddress = (string)reader["VictimAddress"];
                        victims.VictimContact = (string)reader["VictimContact"];

                        NewVictims.Add(victims);

                    }
                    
                }
                return NewVictims;

            }
        }

        public List<Victims> GetAllVictimsRelatedToAnIncident(int incidentId) {
            List<Victims> NewVictims = new List<Victims>();

            using (SqlConnection connection = new SqlConnection(DBConnUtil.GetConnectionString())) {
                command.CommandText = $"SELECT V.* FROM VictimIncidentMap AS VI JOIN Victims as V ON VI.VictimId=V.VictimId WHERE VI.IncidentId={incidentId}";
                command.Connection = connection;
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader()) {
                    while (reader.Read()) {
                        Victims victims = new Victims();
                        victims.VictimId = (int)reader["VictimId"];
                        victims.FirstName = (string)reader["FirstName"];
                        victims.LastName = (string)reader["LastName"];
                        victims.DateOfBirth = (DateTime)reader["DateOfBirth"];
                        victims.Gender = (string)reader["Gender"];
                        victims.VictimAddress = (string)reader["VictimAddress"];
                        victims.VictimContact = (string)reader["VictimContact"];

                        NewVictims.Add(victims);

                    }
                }
                return NewVictims;   
            }

        }

        public bool UpdateVictimByAddress(string address, int victimID) {

            using (SqlConnection connection = new SqlConnection(DBConnUtil.GetConnectionString())) {
                command.CommandText = $"UPDATE Victims SET VictimAddress='{address}' WHERE VictimId={victimID}";
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

        public bool UpdateVictimByContact(string contact, int victimID) {
            using (SqlConnection connection = new SqlConnection(DBConnUtil.GetConnectionString())) {
                command.CommandText = $"UPDATE Victims SET VictimContact='{contact}' WHERE VictimId={victimID}";
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

        public bool UpdateVictimByFirstName(string firstName, int victimID) {
            using (SqlConnection connection = new SqlConnection(DBConnUtil.GetConnectionString())) {
                command.CommandText = $"UPDATE Victims SET FirstName='{firstName}' WHERE VictimId={victimID}";
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

        public bool UpdateVictimByLastName(string lastName, int victimID) {
            using (SqlConnection connection = new SqlConnection(DBConnUtil.GetConnectionString())) {
                command.CommandText = $"UPDATE Victims SET LastName='{lastName}' WHERE VictimId={victimID}";
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
