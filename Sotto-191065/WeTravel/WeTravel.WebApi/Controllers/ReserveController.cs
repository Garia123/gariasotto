using Microsoft.AspNetCore.Mvc;
using System;
using WeTravel.ServiceInterface;
using WeTravel.Filter;
using WeTravel.Model;

namespace WeTravel.WebApi.Controllers
{
    [Route("/api/reserves")]
    public class ReserveController : ControllerBase
    {
        private readonly IReserveService _reserveService;

        public ReserveController(IReserveService reserveService)
        {
            _reserveService = reserveService;
        }

        /// <summary>
        /// Crea una reserva.
        /// </summary>
        /// <param name="model">Model de la reserva a crear</param>
        /// <response code="200">Se ha creado la reserva correctamente</response>
        /// <response code="400">El model provisto es invalido o el Hospedaje no existe</response>
        /// <response code="500">Ocrrio un error fatal en el servidor</response>
        [HttpPost]
        public IActionResult Create([FromBody] ReserveModelIn model)
        {
            return Ok(_reserveService.Create(model));
        }

        /// <summary>
        /// Buscar una reserva por Id.
        /// </summary>
        /// <param name="id">Id de la reserva</param>
        /// <response code="200">Se obtuvo la Reserva correctamente</response>
        /// <response code="400">No existe una Reserva con la Id prevista</response>
        /// <response code="500">Ocrrio un error fatal en el servidor</response>
        [HttpGet("{id}")]
        public object GetFromId(Guid id)
        {
            return Ok(_reserveService.GetById(id));
        }

        /// <summary>
        /// Actualiza el estado de una reserva existente.
        /// </summary>
        /// <param name="reserveDescriptionModel">Model de la reserva a actualizar</param>
        /// <response code="200">Se obtuvo la Reserva correctamente</response>
        /// <response code="400">No existe una Reserva con la Id prevista</response>
        /// <response code="500">Ocrrio un error fatal en el servidor</response>
        [HttpPut]
        [ServiceFilter(typeof(WeTravelAuthFilter))]
        public IActionResult UpdateState([FromBody] ReserveDescriptionModelIn reserveDescriptionModel)
        {
            _reserveService.UpdateState(reserveDescriptionModel);
            return Ok();
        }
    }
}

