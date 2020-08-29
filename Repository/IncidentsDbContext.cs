using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace HoneywellHackathon.Repository
{
    public class IncidentsDbContext : DbContext
    {
        public DbSet<Incident> Incidents { get; set; }
        public IncidentsDbContext(DbContextOptions<IncidentsDbContext> options)
            : base(options)
        { }
    }
}
