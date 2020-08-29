using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HoneywellHackathon.Model;

namespace HoneywellHackathon.Repository
{
    public interface IIncidentRepository
    {
        void InsertCustomerOrderDetails(Incident incident);

        List<Incident> GetAllIncidents();

        void AssignTicket(AssignTicket assignTicket);

        TicketStatus GetTicketStatus(int incidentID);
    }
}
