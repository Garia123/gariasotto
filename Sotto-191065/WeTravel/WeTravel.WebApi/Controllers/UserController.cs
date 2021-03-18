using Microsoft.AspNetCore.Mvc;
using WeTravel.ServiceInterface;
using WeTravel.Filter;
using WeTravel.Model;

namespace WeTravel.WebApi.Controllers
{
    [Route("/api/users")]
    //[ServiceFilter(typeof(WeTravelAuthFilter))]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Crea un usuario.
        /// </summary>
        /// <param name="model">Modelo de usuario a crear</param>
        /// <response code="200">Ha sido exitoso</response>
        /// <response code="400">El modelo preveido es invalido o el usuario existe previamente</response>
        /// <response code="401">El usuario no esta autenticado</response>
        /// <response code="500">Ocrrio un error fatal en el servidor</response>
        [HttpPost]
        public IActionResult Create([FromBody] UserModelIn model)
        {
            _userService.Create(model);
            return Ok();
        }

        /// <summary>
        /// Borra un usuario.
        /// </summary>
        /// <param name="email">Email de usuario</param>
        /// <response code="200">El borrado a sido exitoso</response>
        /// <response code="400">No existe ningun usuario con ese mail</response>
        /// <response code="401">El usuario no esta autenticado</response>
        /// <response code="500">Ocrrio un error fatal en el servidor</response>
        [HttpDelete("{email}")]
        public IActionResult Delete(string email)
        {
            _userService.Delete(email);
            return Ok();
        }

        /// <summary>
        /// Actualiza un usuario existente.
        /// </summary>
        /// <param name="updatedUserData">Modelo de usuario a actualizar</param>
        /// <response code="200">El borrado a sido exitoso</response>
        /// <response code="400">No existe ningun usuario con ese mail</response>
        /// <response code="401">El usuario no esta autenticado</response>
        /// <response code="500">Ocrrio un error fatal en el servidor</response>
        [HttpPut]
        public IActionResult UpdateAvailable([FromBody] UserModelIn updatedUserData)
        {
            _userService.UpdateUser(updatedUserData);
            return Ok();
        }

        /// <summary>
        /// Actualiza un usuario existente.
        /// </summary>
        /// <response code="200">Se han conseguido los usuarios exitosamente</response>
        /// <response code="401">El usuario no esta autenticado</response>
        /// <response code="500">Ocrrio un error fatal en el servidor</response>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_userService.Get());
        }
    }
}
