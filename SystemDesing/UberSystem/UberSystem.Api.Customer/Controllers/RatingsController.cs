using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UberSystem.Domain.Interfaces.Services;
using UberSystem.Infrastructure;

namespace UberSystem.Api.Customer.Controllers
{
  
    public class RatingsController : BaseApiController
    {
        private readonly UberSystemDbContext _context;

        public RatingsController(UberSystemDbContext context)
        {
            _context = context;
        }

        [HttpGet("rating")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<Domain.Entities.Rating>>> GetAllRating()
        {
            if (_context.Ratings == null)
            {
                return NotFound();
            }
            return await _context.Ratings.ToListAsync();
        }

        [HttpGet("rating/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Domain.Entities.Rating>> GetRatingById(long id)
        {
            if (_context.Ratings == null)
            {
                return NotFound();
            }
            var r = await _context.Ratings.FindAsync(id);

            if (r == null)
            {
                return NotFound();
            }

            return r;
        }

        /// <summary>
        /// Create a new customer
        /// </summary>
        /// <param name="rating"></param>
        /// <returns></returns>
        [HttpPost("CreateNewFeedback")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Domain.Entities.Rating>> CreateNewFeedback(Domain.Entities.Rating r)
        {
            if (r == null)
            {
                return BadRequest();
            }

            _context.Ratings.Add(r);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetRatingById), new { id = r.Id }, r);
        }

    }
}
