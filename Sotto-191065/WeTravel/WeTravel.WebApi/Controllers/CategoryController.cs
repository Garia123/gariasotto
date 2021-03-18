using Microsoft.AspNetCore.Mvc;
using System.Linq;
using WeTravel.ServiceInterface;

namespace WeTravel.WebApi
{
    [Route("/api/categories")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        /// <summary>
        /// Obtener Categories.
        /// </summary>
        /// <returns>Retorna la lista de las categorias</returns>
        /// <response code="200">Se obtienen las categorias correctamente</response>
        /// <response code="500">Ocrrio un error fatal en el servidor</response>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_categoryService.Get().ToList());
        }
    }
}
