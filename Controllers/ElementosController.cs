using aplicacionExamen.Models;
using aplicacionExamen.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace aplicacionExamen.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ElementosController : ControllerBase
    {
        private readonly ElementoService _elementoService;

        public ElementosController(ElementoService elementoService)
        {
            _elementoService = elementoService ?? throw new ArgumentNullException(nameof(elementoService));
        }

        [HttpGet]
        public async Task<ActionResult<List<Elemento>>> GetAllElements()
        {
            return await _elementoService.GetAllElementsAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Elemento>> AddElement(Elemento element)
        {
            await _elementoService.AddElementAsync(element);
            return CreatedAtAction(nameof(AddElement), new { id = element.Id }, element);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditElement(int id, Elemento element)
        {
            if (id != element.Id)
            {
                return BadRequest();
            }

            await _elementoService.EditarElementoAsync(element);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Elemento>> DeleteElement(int id)
        {
            var element = await _elementoService.GetElementByIdAsync(id);
            if (element == null)
            {
                return NotFound();
            }

            await _elementoService.EliminarElementoAsync(id);

            return element;
        }
    }
}






































//using aplicacionExamen.Models;
//using aplicacionExamen.Services;
//using Microsoft.AspNetCore.Mvc;
//using System;
//using System.Collections.Generic;
//using System.Threading.Tasks;

//namespace aplicacionExamen.Controllers
//{
//    [ApiController] // Indica que este controlador responde a solicitudes HTTP API
//    [Route("api/[controller]")] // Define la ruta base para las solicitudes HTTP que llegan a este controlador
//    public class ElementosController : ControllerBase
//    {
//        private readonly ElementoService _elementoService;

//        // Constructor que recibe ElementoService
//        public ElementosController(ElementoService elementoService)
//        {
//            _elementoService = elementoService ?? throw new ArgumentNullException(nameof(elementoService));
//        }

//        // Método para manejar solicitudes HTTP GET y obtener todos los elementos
//        [HttpGet] // Indica que este método maneja solicitudes HTTP GET
//        public async Task<ActionResult<List<Elemento>>> GetAllElements()
//        {
//            return await _elementoService.GetAllElementsAsync(); // Retorna una lista de elementos
//        }

//        // Método para manejar solicitudes HTTP POST y agregar un nuevo elemento
//        [HttpPost] // Indica que este método maneja solicitudes HTTP POST
//        public async Task<ActionResult<Elemento>> AddElement(Elemento element)
//        {
//            await _elementoService.AddElementAsync(element); // Agrega un nuevo elemento a la base de datos
//            // Retorna una respuesta HTTP 201 Created con el nuevo elemento agregado
//            return CreatedAtAction(nameof(AddElement), new { id = element.Id }, element);
//        }
//    }
//}
