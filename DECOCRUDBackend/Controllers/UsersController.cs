// Controllers/UsersController.cs
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using UserTenantAPI.Data;
using UserTenantAPI.Models;

namespace UserTenantAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly UserRepository _repository;

        public UsersController()
        {
            _repository = new UserRepository();
        }

        // GET: api/users
        [HttpGet]
        public ActionResult<IEnumerable<User>> GetAll()
        {
            var users = _repository.GetAll();
            return Ok(users);
        }

        // GET: api/users/{tenantKey}
        [HttpGet("{tenantKey}")]
        public ActionResult<User> GetByTenantKey(int tenantKey)
        {
            var user = _repository.GetByTenantKey(tenantKey);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        // POST: api/users
        [HttpPost]
        public ActionResult<User> Create([FromBody] User user)
        {
            _repository.Add(user);
            Console.WriteLine($"Id: {user.Id}, Name: {user.Name}, TenantKey: {user.TenantKey}");
            Console.WriteLine($"Id: {user.Id}, Name:{user.Name}");
            
            return CreatedAtAction(nameof(GetByTenantKey), new { tenantKey = user.TenantKey }, user);
        }

        // DELETE: api/users/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _repository.Delete(id);
            return NoContent();
        }
    

        // PUT: api/users/{id}
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            var existingUser = _repository.GetById(id);
            if (existingUser == null)
            {
                return NotFound();
            }

            _repository.Update(user);
            return NoContent();
        }

        
    }
}
