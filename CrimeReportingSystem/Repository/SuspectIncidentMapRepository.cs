using CrimeReportingSystem.Model;
using CrimeReportingSystem.Util;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrimeReportingSystem.Repository {
    internal class SuspectIncidentMapRepository : ISuspectIncidentMapRepository {

        SqlCommand command = null;
        public SuspectIncidentMapRepository() {
            command = new SqlCommand();
        }
        public bool CreateSuspectIncidentMap(int suspectId,int incidentId) {
            string query = "INSERT INTO SuspectIncidentMap(SuspectId,IncidentId)" +
                            "VALUES(@SuspectId,@IncidentId)";


            using (SqlConnection connection = new SqlConnection(DBConnUtil.GetConnectionString())) {
                command.CommandText = query;
                command.Connection = connection;
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@SuspectId", suspectId);             
                command.Parameters.AddWithValue("IncidentId", incidentId);

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
