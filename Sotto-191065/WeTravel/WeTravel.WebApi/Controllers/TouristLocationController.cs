using WeTravel.Filter;
using WeTravel.Model;
using System;
using WeTravel.ServiceInterface;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace WeTravel.WebApi.Controllers
{
    [Route("/api/touristLocations")]
    public class TouristLocationController : ControllerBase
    {
        private readonly ITouristLocationService _touristLocationService;

        public TouristLocationController(ITouristLocationService touristLocationService)
        {
            _touristLocationService = touristLocationService;
        }

        /// <summary>
        /// Crear una nueva TouristLocation.
        /// </summary>
        /// <param name="model">Modelo de TouristLocation a crear</param>
        /// <response code="200">Se ha agregado correctamente</response>
        /// <response code="400">El modelo previsto es invalido</response>
        /// <response code="401">El usuario no esta autenticado</response>
        /// <response code="500">Ocrrio un error fatal en el servidor</response>
        [HttpPost]
        [ServiceFilter(typeof(WeTravelAuthFilter))]
        public IActionResult Create([FromBody] TouristLocationModelIn model)
        {
            _touristLocationService.Create(model);
            return Ok(model);
        }

        /// <summary>
        /// Buscar TouristLocations por filtros.
        /// </summary>
        /// <param name="filters">Filtro de busqueda</param>
        /// <response code="200">Se ha encontrado las TouristLocation filtradas</response>
        /// <response code="400">El filtro es invalido</response>
        /// <response code="500">Ocrrio un error fatal en el servidor</response>
        [HttpGet]
        public IActionResult Get([FromQuery] TouristLocationModelFilter filters)
        {
            IEnumerable<TouristLocationModelOut> touristLocations = _touristLocationService.GetTouristLocations(filters);
            return Ok(touristLocations);
        }

        [HttpGet("/reports")]
        public IActionResult GetReport([FromQuery] TouristLocationReportFilter filter)
        {
            IEnumerable<ReportLineOut> reports = _touristLocationService.GetReport(filter);
            return Ok(reports);
        }

        /// <summary>
        /// Buscar TouristLocation por Id.
        /// </summary>
        /// <param name="id">Id de TouristLocation a buscar</param>
        /// <response code="200">Se ha encontrado al TouristLocation correctamente</response>
        /// <response code="400">No exise un TouristLocation con la ID prevista</response>
        /// <response code="500">Ocrrio un error fatal en el servidor</response>
        [HttpGet("{id}")]
        public IActionResult GetFromId(Guid id)
        {
            var touristLocation = _touristLocationService.GetById(id);
            return Ok(touristLocation);
        }
    }
}
