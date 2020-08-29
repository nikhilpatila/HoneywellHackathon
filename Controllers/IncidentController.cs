using System;
using System.Net;
using HoneywellHackathon.Model;
using HoneywellHackathon.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.Swagger.Annotations;

namespace HoneywellHackathon.Controllers
{
    //TODO : Add unit tests
    //TODO : Add authentication, authorization
    //TODO : Move logic to service layers
    //TODO : Add custom response messages
    //TODO : Verify edge case scenarios
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

        /// <summary>
        /// API to add Incidents on vehicles part of the zone
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
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
                _logger.LogError(e, e.Message);
                return new ObjectResult(HttpStatusCode.InternalServerError);
            }
            
        }

        /// <summary>
        /// API to get all active incidents pertaining to zonal vehicles
        /// </summary>
        /// <returns></returns>
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
                _logger.LogError(e, e.Message);
                return new ObjectResult(HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// API to assign open tickets to a Support Executive
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
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
                _logger.LogError(e, e.Message);
                return new ObjectResult(HttpStatusCode.InternalServerError);
            }
        }

        /// <remarks>
        /// API to fetch details about the ticket
        /// </remarks>
        /// <param name="incidentID"></param>
        /// <returns></returns>
        [Microsoft.AspNetCore.Mvc.HttpGet]
        [Route("status/{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(TicketStatusViewModel))]
        public IActionResult GetTicketStatus(string id)
        {
            var rng = new Random();
            try
            {
                var result = _incidentRepository.GetTicketStatus(Convert.ToInt32(id));
                return new ObjectResult(result);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return new ObjectResult(HttpStatusCode.InternalServerError);
            }
        }
    }
}
