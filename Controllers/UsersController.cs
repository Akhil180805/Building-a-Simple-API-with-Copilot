
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserManagementAPI.Data;
using UserManagementAPI.Models;
using UserManagementAPI.DTOs;

namespace UserManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UsersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers(
    int page = 1,
    int pageSize = 5,
    CancellationToken cancellationToken = default)
        {
            if (page < 1 || pageSize < 1 || pageSize > 100)
            {
                return BadRequest("Invalid pagination values.");
            }

            var users = await _context.Users
                .AsNoTracking()
                .OrderBy(u => u.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return Ok(users);
        }

        // GET: api/users/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<User>> GetUser(
    int id,
    CancellationToken cancellationToken)
        {
            var user = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(
                    u => u.Id == id,
                    cancellationToken);

            if (user == null)
            {
                return NotFound("User not found");
            }

            return Ok(user);
        }

        // POST: api/users
        [HttpPost]
        public async Task<ActionResult<User>> CreateUser(
    CreateUserRequest request,
    CancellationToken cancellationToken)
        {
            string email = request.Email.Trim().ToLowerInvariant();

            bool emailExists = await _context.Users
                .AnyAsync(u => u.Email == email, cancellationToken);

            if (emailExists)
            {
                return Conflict("Email already exists");
            }

            var user = new User
            {
                Name = request.Name.Trim(),
                Email = email,
                Age = request.Age
            };

            _context.Users.Add(user);

            await _context.SaveChangesAsync(cancellationToken);

            return CreatedAtAction(
                nameof(GetUser),
                new { id = user.Id },
                user
            );
        }

        // PUT: api/users/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateUser(
    int id,
    UpdateUserRequest request,
    CancellationToken cancellationToken)
        {
            var existingUser = await _context.Users
                .FindAsync(new object[] { id }, cancellationToken);

            if (existingUser == null)
            {
                return NotFound("User not found");
            }

            string email = request.Email.Trim().ToLowerInvariant();

            bool emailExists = await _context.Users
                .AnyAsync(u => u.Email == email && u.Id != id, cancellationToken);

            if (emailExists)
            {
                return Conflict("Email already exists");
            }

            existingUser.Name = request.Name.Trim();
            existingUser.Email = email;
            existingUser.Age = request.Age;

            await _context.SaveChangesAsync(cancellationToken);

            return NoContent();
        }

        // DELETE: api/users/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteUser(
    int id,
    CancellationToken cancellationToken)
        {
            var user = await _context.Users
                .FindAsync(new object[] { id }, cancellationToken);

            if (user == null)
            {
                return NotFound("User not found");
            }

            _context.Users.Remove(user);

            await _context.SaveChangesAsync(cancellationToken);

            return NoContent();
        }
    }
}
