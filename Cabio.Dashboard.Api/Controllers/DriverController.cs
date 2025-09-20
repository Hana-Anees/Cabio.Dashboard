using Cabio.Dashboard.Application.Drivers.Commands.CreateDriver;
using Cabio.Dashboard.Application.Drivers.Commands.DeleteDriver;
using Cabio.Dashboard.Application.Drivers.Commands.UpdateDriver;
using Cabio.Dashboard.Application.Drivers.Queries;
using Cabio.Dashboard.Application.Drivers.Queries.GetAllDrivers;
using Cabio.Dashboard.Application.Dtos;
using Cabio.Dashboard.Application.Dtos.Drivers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Cabio.Dashboard.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DriverController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DriverController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/Driver
        [HttpGet]
        public async Task<IActionResult> GetAllDrivers()
        {
            var drivers = await _mediator.Send(new GetAllDriversQuery());
            return Ok(drivers);
        }

        //// GET: api/Driver/{id}
        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetDriverById(int id)
        //{
        //    var driver = await _mediator.Send(new GetDriverByIdQuery(id));
        //    if (driver == null) return NotFound();
        //    return Ok(driver);
        //}

        //// POST: api/Driver
        //[HttpPost]
        //public async Task<IActionResult> CreateDriver([FromBody] CreateDriverDto driverDto)
        //{
        //    var command = new CreateDriverCommand(driverDto);
        //    var createdDriver = await _mediator.Send(command);
        //    return CreatedAtAction(nameof(GetDriverById), new { id = createdDriver.Id }, createdDriver);
        //}

        //// PUT: api/Driver/{id}
        //[HttpPut("{id}")]
        //public async Task<IActionResult> UpdateDriver(int id, [FromBody] UpdateDriverDto driverDto)
        //{
        //    var command = new UpdateDriverCommand(id, driverDto);
        //    var updatedDriver = await _mediator.Send(command);
        //    if (updatedDriver == null) return NotFound();
        //    return Ok(updatedDriver);
        //}

        //// DELETE: api/Driver/{id}
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteDriver(int id)
        //{
        //    var command = new DeleteDriverCommand(id);
        //    var result = await _mediator.Send(command);
        //    if (!result) return NotFound();
        //    return NoContent();
        //}
    }
}
