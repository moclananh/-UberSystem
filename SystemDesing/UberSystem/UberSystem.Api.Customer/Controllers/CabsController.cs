using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UberSystem.Domain.Interfaces.Services;
using UberSystem.Infrastructure;

namespace UberSystem.Api.Customer.Controllers
{
    public class CabsController : BaseApiController
    {
        private readonly UberSystemDbContext _context;
        private readonly IUserService _userService;

        public CabsController(UberSystemDbContext context, IUserService userService)
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
        [HttpGet("cab")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<Domain.Entities.Cab>>> GetCabs()
        {
            if (_context.Cabs == null)
            {
                return NotFound();
            }
            return await _context.Cabs.ToListAsync();
        }

        /// <summary>
        /// Retrieve customers in system
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// 
        /// </remarks>
        [HttpGet("cab/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Domain.Entities.Cab>> GetCab(long id)
        {
            if (_context.Cabs == null)
            {
                return NotFound();
            }
            var item = await _context.Cabs.FindAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            return item;
        }

        /// <summary>
        /// Create a new customer
        /// </summary>
        /// <param name="cabs"></param>
        /// <returns></returns>
        [HttpPost("cab")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Domain.Entities.Cab>> CreateCustomer(Domain.Entities.Cab cab)
        {
            if (cab == null)
            {
                return BadRequest();
            }

            _context.Cabs.Add(cab);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetCab), new { id = cab.Id }, cab);
        }



        /// <summary>
        /// Delete a customer
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("cab/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(long id)
        {
            var ca = await _context.Cabs.FindAsync(id);
            if (ca == null)
            {
                return NotFound();
            }

            _context.Cabs.Remove(ca);
            await _context.SaveChangesAsync();

            return NoContent();
        }



        /// <summary>
        /// Update a customer
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cab"></param>
        /// <returns></returns>
        [HttpPut("cab/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Domain.Entities.Cab>> Update(long id, Domain.Entities.Cab cab)
        {
            if (cab == null || id != cab.Id)
            {
                return BadRequest();
            }

            var existingCab = await _context.Cabs.FindAsync(id);
            if (existingCab == null)
            {
                return NotFound();
            }

            existingCab.Type = cab.Type;
            existingCab.DriverId = cab.DriverId;
            existingCab.RegNo = cab.RegNo;

            await _context.SaveChangesAsync();

            return Ok(existingCab);
        }
    }
}
