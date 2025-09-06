using Cabio.Dashboard.Api.Models;
using Cabio.Dashboard.Domain.Entities;
using Cabio.Dashboard.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cabio.Dashboard.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DriverController : ControllerBase
    {
        private readonly AppDbContext _db;

        public DriverController(AppDbContext db)
        {
            _db = db;
        }

        // GET: api/driver
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DriverDto>>> GetAll()
        {
            var drivers = await _db.Drivers.ToListAsync();

            return Ok(drivers.Select(d => new DriverDto(
                d.Id, d.Name, d.Address, d.DateOfBirth, d.LicenseNumber, d.Contact,
                d.SafeGuarding, d.DisabilityAwareness
            )));
        }

        // GET: api/driver/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<DriverDto>> GetById(int id)
        {
            var driver = await _db.Drivers.FindAsync(id);
            if (driver == null) return NotFound();

            return new DriverDto(
                driver.Id, driver.Name, driver.Address, driver.DateOfBirth,
                driver.LicenseNumber, driver.Contact, driver.SafeGuarding, driver.DisabilityAwareness
            );
        }

        // POST: api/driver
        [HttpPost]
        public async Task<ActionResult<DriverDto>> Create(CreateDriverRequest request)
        {
            var driver = new Driver
            {
                Name = request.Name,
                Address = request.Address,
                DateOfBirth = request.DateOfBirth,
                LicenseNumber = request.LicenseNumber,
                Contact = request.Contact,
                SafeGuarding = request.SafeGuarding,
                DisabilityAwareness = request.DisabilityAwareness
            };

            _db.Drivers.Add(driver);
            await _db.SaveChangesAsync();

            var dto = new DriverDto(
                driver.Id, driver.Name, driver.Address, driver.DateOfBirth,
                driver.LicenseNumber, driver.Contact, driver.SafeGuarding, driver.DisabilityAwareness
            );

            return CreatedAtAction(nameof(GetById), new { id = driver.Id }, dto);
        }

        // PUT: api/driver/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CreateDriverRequest request)
        {
            var driver = await _db.Drivers.FindAsync(id);
            if (driver == null) return NotFound();

            driver.Name = request.Name;
            driver.Address = request.Address;
            driver.DateOfBirth = request.DateOfBirth;
            driver.LicenseNumber = request.LicenseNumber;
            driver.Contact = request.Contact;
            driver.SafeGuarding = request.SafeGuarding;
            driver.DisabilityAwareness = request.DisabilityAwareness;

            await _db.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/driver/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var driver = await _db.Drivers.FindAsync(id);
            if (driver == null) return NotFound();

            _db.Drivers.Remove(driver);
            await _db.SaveChangesAsync();

            return NoContent();
        }
    }
}
