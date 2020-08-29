using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HoneywellHackathon.Model;
using HoneywellHackathon.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Swashbuckle.Swagger;
using Swashbuckle.Swagger.Annotations;

namespace HoneywellHackathon.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IncidentController : Controller
    {
        private readonly IIncidentRepository _incidentRepository;

        private readonly ILogger<IncidentController> _logger;

        public IncidentController(ILogger<IncidentController> logger, IIncidentRepository incidentRepository)
        {
            _logger = logger;
            _incidentRepository = incidentRepository;
        }

        [Microsoft.AspNetCore.Mvc.HttpPost]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(IncidentRequestViewModel))]
        public IActionResult AddIncident(IncidentRequestViewModel request)
        {
            try
            {
                var incident = new Incident()
                {
                    BusID = request.BusID,
                    CrewName = request.CrewName,
                    Description = request.Description,
                    IncidentDateTime = request.IncidentDateTime,
                    IncidentType = request.IncidentType,
                    IsUrgent = request.IsUrgent,
                    Subject = request.Subject
                };
                _incidentRepository.InsertCustomerOrderDetails(incident);

                return new AcceptedResult();
            }
            catch (Exception e)
            {
                return new ObjectResult(HttpStatusCode.InternalServerError);
            }
            
        }

        [Microsoft.AspNetCore.Mvc.HttpGet]
        [Route("getall")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(IncidentsViewModel))]
        public IActionResult GetIncidents()
        {
            var rng = new Random();
            try
            {
                var result = _incidentRepository.GetAllIncidents();
                return new ObjectResult(result);
            }
            catch (Exception e)
            {
                return new ObjectResult(HttpStatusCode.InternalServerError);
            }
        }


        [Microsoft.AspNetCore.Mvc.HttpPost]
        [Route("assign")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(AssignTicketViewModel))]
        public IActionResult AssignTicket(AssignTicketViewModel request)
        {
            try
            {
                var assignTicket = new AssignTicket()
                {
                    IncidentID = request.IncidentID,
                    ExecutiveID= request.ExecutiveID,
                }; 
                _incidentRepository.AssignTicket(assignTicket);
                return new OkResult();
            }
            catch (Exception e)
            {
                return new ObjectResult(HttpStatusCode.InternalServerError);
            }
        }
    }
}
