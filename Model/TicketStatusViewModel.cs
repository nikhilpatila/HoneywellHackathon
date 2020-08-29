using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HoneywellHackathon.Model
{
    public class TicketStatusViewModel
    {
        public class TicketStatus
        {
            public int IncidentID { get; set; }

            public string Remark { get; set; }

            public string ReporterName { get; set; }

            public DateTime LastUpdated { get; set; }
        }
    }
}
