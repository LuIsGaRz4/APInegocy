using APInegocy.DTOs;
using APInegocy.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace APInegocy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly NegocyDbContext _context;

        public UsersController(NegocyDbContext context)
        {
            _context = context;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            // Hasheamos la contraseña que envía el usuario
            var hashedPassword = HashPassword(dto.Password);

            // Buscamos un usuario que coincida con username y password
            var userExists = await _context.Users
                .AnyAsync(u => u.Username == dto.Username && u.PasswordHash == hashedPassword);

            if (!userExists)
                return Unauthorized("Acceso denegado"); // 401

            return Ok("Acceso concedido"); // 200
        }
        // 🔹 GET ALL
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _context.Users.ToListAsync();
            return Ok(users);
        }

        // 🔹 GET BY ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
                return NotFound();

            return Ok(user);
        }

        // 🔹 POST (REGISTER USER)
        [HttpPost]
        public async Task<IActionResult> CreateUser(User dto)
        {
            var user = new User
            {
                Username = dto.Username,
                PasswordHash = HashPassword(dto.PasswordHash),
                Role = dto.Role
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok(user);
        }

        // 🔹 UPDATE
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, User dto)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
                return NotFound();

            user.Username = dto.Username;
            user.Role = dto.Role;

            await _context.SaveChangesAsync();

            return Ok(user);
        }

        // 🔹 DELETE
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
                return NotFound();

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return Ok("Usuario eliminado");
        }

        // 🔐 HASH METHOD
        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(bytes);
            }
        }
    }
}