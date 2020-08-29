using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using HoneywellHackathon.Model;
using Microsoft.ApplicationBlocks.Data;

namespace HoneywellHackathon.Repository
{
    public class IncidentRepository : IIncidentRepository
    {
        private string _connectionString =
            "Data Source=(LocalDb)\\MSSQLLocalDB;Initial Catalog=Hack;Integrated Security=SSPI;";


        public void InsertCustomerOrderDetails(Incident incident)
        {

            DataSet dsCartDetails = new DataSet();
            try
            {
                SqlParameter[] sqlparams = new SqlParameter[8];
                sqlparams[0] = new SqlParameter("@BusID", incident.BusID);
                sqlparams[1] = new SqlParameter("@Subject", incident.Subject);
                sqlparams[2] = new SqlParameter("@Description", incident.Description);
                sqlparams[3] = new SqlParameter("@IsUrgent", incident.IsUrgent);
                sqlparams[4] = new SqlParameter("@IncidentType", incident.IncidentType);
                sqlparams[5] = new SqlParameter("@CrewName", incident.CrewName);
                sqlparams[6] = new SqlParameter("@IncidentDateTime", incident.IncidentDateTime);
                dsCartDetails = SqlHelper.ExecuteDataset(_connectionString, CommandType.StoredProcedure,
                    "usp_InsertIncident", sqlparams);
            }
            catch (Exception ex)
            {
            }

            //return dsCartDetails;
        }

        public List<Incident> GetAllIncidents()
        {
            var incidents = new List<Incident>();
            DataSet dsIncidents = new DataSet();
            try
            {
                dsIncidents = SqlHelper.ExecuteDataset(_connectionString, CommandType.StoredProcedure,
                    "usp_GetAllActiveIncidents");

                if (dsIncidents != null && dsIncidents.Tables.Count > 0 && dsIncidents.Tables[0].Rows.Count > 0)
                {
                    var dt = dsIncidents.Tables[0];
                    var rowsCount = dt.Rows.Count;
                    for (var i = 0; i < rowsCount; i++)
                    {
                        incidents.Add(new Incident()
                        {
                            BusID = dt.Rows[i]["BusID"].ToString(),
                            Subject = dt.Rows[i]["Subject"].ToString(),
                            Description = dt.Rows[i]["Description"].ToString(),
                            IncidentType = dt.Rows[i]["IncidentType"].ToString(),
                            CrewName = dt.Rows[i]["CrewName"].ToString(),
                            IncidentDateTime = dt.Rows[i]["IncidentDateTime"].ToString(),
                        });
                    }
                }
                return incidents;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public void AssignTicket(AssignTicket assignTicket)
        {
            DataSet dsCartDetails = new DataSet();
            try
            {
                SqlParameter[] sqlparams = new SqlParameter[8];
                sqlparams[0] = new SqlParameter("@IncidentID", assignTicket.IncidentID);
                sqlparams[1] = new SqlParameter("@ExecutiveID", assignTicket.ExecutiveID);
                dsCartDetails = SqlHelper.ExecuteDataset(_connectionString, CommandType.StoredProcedure,
                    "usp_AssignTicket", sqlparams);
            }
            catch (Exception ex)
            {
            }
        }
    }
}
