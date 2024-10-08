using CrimeReportingSystem.Model;
using CrimeReportingSystem.Util;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrimeReportingSystem.Repository {
    internal class CaseIncidentMapRepo {
        
        SqlCommand command = null;

        public CaseIncidentMapRepo() {
            command = new SqlCommand();
        }

        

        public bool CreateCaseIncidentMap(int caseId,int incidentId) {
            
            string query = "INSERT INTO CaseIncidentMap(CaseId,IncidentId)" +
                            "VALUES(@CaseId,@IncidentId)";


            using (SqlConnection connection = new SqlConnection(DBConnUtil.GetConnectionString())) {
                command.CommandText = query;
                command.Connection = connection;
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@CaseId", caseId);
                command.Parameters.AddWithValue("@IncidentId", incidentId);

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

