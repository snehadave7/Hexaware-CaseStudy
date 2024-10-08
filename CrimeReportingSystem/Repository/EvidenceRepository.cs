using CrimeReportingSystem.Model;
using CrimeReportingSystem.Util;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrimeReportingSystem.Repository {
    internal class EvidenceRepository : IEvidenceRepository {

        SqlCommand command = null;
        public EvidenceRepository() {
            command = new SqlCommand();
        }

        public bool CreateEvidence(Evidence evidence) {
            string query = "INSERT INTO Evidence(Description,LocationFound,IncidentId)" +
                            "VALUES(@Description,@LocationFound,@IncidentId)";

            
            using (SqlConnection connection = new SqlConnection(DBConnUtil.GetConnectionString())) {
                command.CommandText = query;
                command.Connection = connection;
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@Description", evidence.Description);
                command.Parameters.AddWithValue("@LocationFound", evidence.LocationFound);
                command.Parameters.AddWithValue("IncidentId", evidence.IncidentId);
    
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

        public bool DeleteEvidence(int evidenceId) {
            using (SqlConnection connection = new SqlConnection(DBConnUtil.GetConnectionString())) {
                command.CommandText = "DELETE FROM Evidence WHERE EvidenceId=@EvidenceId";
                command.Connection = connection;
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@EvidenceId", evidenceId);
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

        

        public List<Evidence> GetEvidenceRelatedToAnIncident(int incidentId) {
            List<Evidence> NewEvidence = new List<Evidence>();

            using (SqlConnection connection = new SqlConnection(DBConnUtil.GetConnectionString())) {
                command.CommandText = $"SELECT * FROM Evidence where incidentId={incidentId}";
                command.Connection = connection;
                connection.Open();
                
                using (SqlDataReader reader = command.ExecuteReader()) {
                    while (reader.Read()) {
                        Evidence evidence = new Evidence();
                        evidence.EvidenceId = (int)reader["EvidenceId"];
                        evidence.Description = (string)reader["Description"];
                        evidence.LocationFound = (string)reader["LocationFound"];
                        
                        evidence.IncidentId = (int)reader["IncidentId"];

                        NewEvidence.Add(evidence);

                    }
                }
                    return NewEvidence;         
            }
        }

        public bool UpdateEvidenceOnDescription(string description, int evidenceId) {
            using (SqlConnection connection = new SqlConnection(DBConnUtil.GetConnectionString())) {
                command.CommandText = $"UPDATE Evidence SET Description='{description}' WHERE EvidenceId={evidenceId}";
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

        public bool UpdateEvidenceOnLocation(string location, int evidenceId) {
            using (SqlConnection connection = new SqlConnection(DBConnUtil.GetConnectionString())) {
                command.CommandText = $"UPDATE Evidence SET Location='{location}' WHERE EvidenceId={evidenceId}";
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
