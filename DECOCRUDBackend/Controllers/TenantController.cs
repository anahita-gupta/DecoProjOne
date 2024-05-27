using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using UserTenantAPI.Data;
using UserTenantAPI.Models;

namespace UserTenantAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TenantsController : ControllerBase
    {
        private readonly TenantRepository _repository;

        public TenantsController()
        {
            _repository = new TenantRepository();
        }

        // GET: api/tenants
        [HttpGet]
        public ActionResult<IEnumerable<Tenant>> GetAll()
        {
            var tenants = _repository.GetAll();
            return Ok(tenants);
        }

        // GET: api/tenants/{id}
        [HttpGet("{id}")]
        public ActionResult<Tenant> GetById(int id)
        {
            var tenant = _repository.GetById(id);
            if (tenant == null)
            {
                return NotFound();
            }
            return Ok(tenant);
        }

        // POST: api/tenants
        [HttpPost]
        public ActionResult<Tenant> Create([FromBody] Tenant tenant)
        {
            _repository.Add(tenant);
            return CreatedAtAction(nameof(GetById), new { id = tenant.TenantId }, tenant);
        }

        // DELETE: api/tenants/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _repository.Delete(id);
            return NoContent();
        }
        
    

        // PUT: api/tenants/{id}
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Tenant tenant)
        {
            if (id != tenant.TenantId)
            {
                return BadRequest();
            }

            var existingTenant = _repository.GetById(id);
            if (existingTenant == null)
            {
                return NotFound();
            }

            _repository.Update(tenant);
            return NoContent();
        }
    }
}
