using System;

namespace HoneywellHackathon.Repository
{
    public class TicketStatus
    {
        public int IncidentID { get; set; }

        public string Remark { get; set; }

        public string ReporterName { get; set; }

        public DateTime LastUpdated { get; set; }
    }
}
