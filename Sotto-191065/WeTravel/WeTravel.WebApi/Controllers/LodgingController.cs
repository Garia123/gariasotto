using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using WeTravel.ServiceInterface;
using WeTravel.Filter;
using WeTravel.Model;

namespace WeTravel.WebApi.Controllers
{
    [Route("/api/lodgings")]
    public class LodgingController : ControllerBase
    {
        private readonly ILodgingService _lodgingService;
        private readonly IReviewService _reviewService;

        public LodgingController(ILodgingService lodgingService,IReviewService reviewService)
        {
            _lodgingService = lodgingService;
            _reviewService = reviewService;
        }

        /// <summary>
        /// Crear un nuevo Hospedaje.
        /// </summary>
        /// <param name="lodging">Modelo del Hospedaje a crear</param>
        /// <response code="200">Se ha agregado correctamente</response>
        /// <response code="400">El modelo previsto es invalido o ya existe el Hospedaje</response>
        /// <response code="401">El usuario no esta autenticado</response>
        /// <response code="500">Ocrrio un error fatal en el servidor</response>
        [HttpPost]
        [ServiceFilter(typeof(WeTravelAuthFilter))]
        public IActionResult Create([FromBody] LodgingModelIn lodging)
        {
            _lodgingService.Create(lodging);
            return Ok(lodging);
        }

        /// <summary>
        /// Borrar un nuevo Hospedaje.
        /// </summary>
        /// <param name="id">Id del Hospedaje</param>
        /// <response code="200">Se ha borrado correctamente correctamente</response>
        /// <response code="400">No existe un Hospedaje con la Id prevista</response>
        /// <response code="401">El usuario no esta autenticado</response>
        /// <response code="500">Ocrrio un error fatal en el servidor</response>
        [HttpDelete("{id}")]
        [ServiceFilter(typeof(WeTravelAuthFilter))]
        public IActionResult Delete(Guid id)
        {
            _lodgingService.Delete(id);
            return Ok();
        }

        /// <summary>
        /// Buscar Hospejes por filtro.
        /// </summary>
        /// <param name="filter">Id del Hospedaje</param>
        /// <response code="200">Se ha encontrado los hospedajes</response>
        /// <response code="400">El modelo previsto es invalido</response>
        /// <response code="500">Ocrrio un error fatal en el servidor</response>
        [HttpGet]
        public IActionResult Get([FromQuery] LodgingModelFilter filter)
        {
            return Ok(_lodgingService.Get(filter).ToList());
        }

        [HttpGet("{id}/reviews")]
        public IActionResult GetReviews(Guid id)
        {
            return Ok(_reviewService.GetReviews(id).ToList());
        }

        /// <summary>
        /// Actualiza un Hospedaje.
        /// </summary>
        /// <param name="id">Id del Hospedaje a actualizar</param>
        /// <response code="200">Se ha actualizado correctamente</response>
        /// <response code="400">No existe un Hospedaje con la Id prevista o el model previsto es invalido</response>
        /// <response code="401">El usuario no esta autenticado</response>
        /// <response code="500">Ocrrio un error fatal en el servidor</response>
        [HttpPut("{id}")]
        [ServiceFilter(typeof(WeTravelAuthFilter))]
        public IActionResult UpdateAvailable(Guid id)
        {
            _lodgingService.UpdateAvailable(id);
            return Ok();
        }

        /// <summary>
        /// Obtener un Hospedaje.
        /// </summary>
        /// <param name="id">Id del Hospedaje a buscar</param>
        /// <response code="200">Se ha encontrado correctamente</response>
        /// <response code="400">No existe un Hospedaje con la Id prevista</response>
        /// <response code="500">Ocrrio un error fatal en el servidor</response>
        [HttpGet("{id}")]
        public object GetFromId(Guid id)
        {
            var lodging = _lodgingService.GetFromId(id);
            return Ok(lodging);
        }
    }
}
