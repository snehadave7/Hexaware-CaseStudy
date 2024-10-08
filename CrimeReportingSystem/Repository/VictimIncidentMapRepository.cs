using CrimeReportingSystem.Model;
using CrimeReportingSystem.Util;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrimeReportingSystem.Repository {
    internal class VictimIncidentMapRepository : IVictimIncidentMapRepository {
        SqlCommand command = null;
        public VictimIncidentMapRepository() {
            command = new SqlCommand();
        }
        public bool CreateVictimIncidentMap(int victimId, int incidentId) {
            string query = "INSERT INTO VictimIncidentMap(VictimId,IncidentId)" +
                            "VALUES(@VictimId,@IncidentId)";


            using (SqlConnection connection = new SqlConnection(DBConnUtil.GetConnectionString())) {
                command.CommandText = query;
                command.Connection = connection;
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@VictimId", victimId);
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
