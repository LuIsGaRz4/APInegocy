using APInegocy.Models;
using Microsoft.AspNetCore.Mvc;

namespace Negocy.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly IServiceManager<Productos> _service;

        public ProductosController(IServiceManager<Productos> service)
        {
            _service = service;
        }

        // 🔹 GET ALL
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAll();
            return Ok(result);
        }

        // 🔹 GET BY ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _service.GetById(id);
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        // 🔹 CREATE
        [HttpPost]
        public async Task<IActionResult> Create(Productos producto)
        {
            var result = await _service.Create(producto);
            return Ok(result);
        }

        // 🔹 UPDATE
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Productos producto)
        {
            if (id != producto.IdProducto)
                return BadRequest();

            var result = await _service.Update(producto);
            return Ok(result);
        }

        // 🔹 DELETE
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.Delete(id);
            if (!result)
                return NotFound();

            return Ok("Eliminado correctamente");
        }
    }
}