using Microsoft.AspNetCore.Mvc;
using System.Linq;
using WeTravel.ServiceInterface;

namespace WeTravel.WebApi
{
    [Route("/api/regions")]
    public class RegionController : ControllerBase
    {
        private readonly IRegionService _regionService;

        public RegionController(IRegionService regionService)
        {
            _regionService = regionService;
        }

        /// <summary>
        /// Obtener regiones.
        /// </summary>
        /// <returns>Retorna la lista de las regiones en el sistema</returns>
        /// <response code="200">Las regiones se obtienen correctamente</response>
        /// <response code="500">Ocrrio un error fatal en el servidor</response>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_regionService.Get());
        }
    }
}
