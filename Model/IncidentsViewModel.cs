using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HoneywellHackathon.Model
{
    public class IncidentsViewModel
    {
        public string BusID { get; set; }

        public string Subject { get; set; }

        public string Description { get; set; }

        public bool IsUrgent { get; set; }

        public string IncidentType { get; set; }

        public string CrewName { get; set; }

        public string IncidentDateTime { get; set; }

    }
}
