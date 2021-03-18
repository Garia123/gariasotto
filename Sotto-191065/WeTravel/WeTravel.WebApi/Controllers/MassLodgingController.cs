using Microsoft.AspNetCore.Mvc;
using WeTravel.ServiceInterface;
using WeTravel.Filter;

namespace WeTravel.WebApi.Controllers
{
    [Route("/api/massLodgings")]
    [ServiceFilter(typeof(WeTravelAuthFilter))]
    public class MassLodgingController : ControllerBase    {
        
        private readonly IMassLodgingService _massLodgingService;

        public MassLodgingController(IMassLodgingService massLodgingService)
        {
            _massLodgingService = massLodgingService;
        }

        [HttpPost]
        public IActionResult MassCreate([FromQuery] int implementation, string filePath)
        {
            _massLodgingService.MassCreate(implementation,filePath);
            return Ok();
        }

        [HttpGet]
        public IActionResult MassCreateGetImps()
        {
            return Ok(_massLodgingService.GetImplementationsForMassAdd());
        }
    }
}