using System;
using Microsoft.AspNetCore.Mvc;
using WeTravel.ServiceInterface;
using WeTravel.Model;

namespace WeTravel.WebApi.Controllers.Controllers
{
    [Route("api/sessions")]
    public class SessionController : ControllerBase
    {
        private readonly ISessionService _sessionService;

        public SessionController(ISessionService sessionService)
        {
            _sessionService = sessionService;
        }

        /// <summary>
        /// Crear un login.
        /// </summary>
        /// <param name="login">Login</param>
        /// <returns>Se retorna el token para usar en la API</returns>
        /// <response code="200">Se crea la sesion correctamente</response>
        /// <response code="400">No se ha encontrado un usuario con email y password prevista</response>
        /// <response code="500">Ocrrio un error fatal en el servidor</response>
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModelIn login)
        {
            Guid token = _sessionService.Login(login);
            return Ok(token);
        }
    }
}
