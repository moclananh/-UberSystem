using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UberSystem.Domain.Interfaces.Services;
using UberSystem.Infrastructure;

namespace UberSystem.Api.Customer.Controllers
{
    public class DriversController : BaseApiController
    {
        private readonly UberSystemDbContext _context;
        private readonly IUserService _userService;

        public DriversController(UberSystemDbContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        /// <summary>
        /// Retrieve customers in system
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// 
        /// </remarks>
        [HttpGet("driver")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<Domain.Entities.Driver>>> Getdrivers()
        {
            if (_context.Drivers == null)
            {
                return NotFound();
            }
            return await _context.Drivers.ToListAsync();
        }

        /// <summary>
        /// Retrieve customers in system
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// 
        /// </remarks>
        [HttpGet("driver/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Domain.Entities.Driver>> Getdriver(long id)
        {
            if (_context.Drivers == null)
            {
                return NotFound();
            }
            var item = await _context.Drivers.FindAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            return item;
        }

        /// <summary>
        /// Create a new customer
        /// </summary>
        /// <param name="drivers"></param>
        /// <returns></returns>
        [HttpPost("driver")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Domain.Entities.Driver>> CreateCustomer(Domain.Entities.Driver driver)
        {
            if (driver == null)
            {
                return BadRequest();
            }

            _context.Drivers.Add(driver);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Getdriver), new { id = driver.Id }, driver);
        }



        /// <summary>
        /// Delete a customer
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("driver/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(long id)
        {
            var ca = await _context.Drivers.FindAsync(id);
            if (ca == null)
            {
                return NotFound();
            }

            _context.Drivers.Remove(ca);
            await _context.SaveChangesAsync();

            return NoContent();
        }



        /// <summary>
        /// Update a customer
        /// </summary>
        /// <param name="id"></param>
        /// <param name="driver"></param>
        /// <returns></returns>
        [HttpPut("driver/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Domain.Entities.Driver>> Update(long id, Domain.Entities.Driver driver)
        {
            if (driver == null || id != driver.Id)
            {
                return BadRequest();
            }

            var existingdriver = await _context.Drivers.FindAsync(id);
            if (existingdriver == null)
            {
                return NotFound();
            }

            existingdriver.CabId = driver.CabId;
            existingdriver.Dob = driver.Dob;
            existingdriver.LocationLatitude = driver.LocationLatitude;
            existingdriver.LocationLongitude = driver.LocationLongitude;
            existingdriver.CreateAt = driver.CreateAt;
            existingdriver.UserId = driver.UserId;

            await _context.SaveChangesAsync();

            return Ok(existingdriver);
        }
    }
}
