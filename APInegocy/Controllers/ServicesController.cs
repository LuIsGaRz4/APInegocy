using APInegocy.Models;
using Microsoft.AspNetCore.Mvc;

namespace Negocy.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicesController : ControllerBase
    {
        private readonly IServiceManager<Service> _service;

        public ServicesController(IServiceManager<Service> service)
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
        public async Task<IActionResult> Create(Service service)
        {
            var result = await _service.Create(service);
            return Ok(result);
        }

        // 🔹 UPDATE
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Service service)
        {
            if (id != service.ID)
                return BadRequest();

            var result = await _service.Update(service);
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