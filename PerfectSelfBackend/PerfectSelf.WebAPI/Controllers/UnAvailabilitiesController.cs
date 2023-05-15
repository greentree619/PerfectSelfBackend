using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PerfectSelf.WebAPI.Context;
using PerfectSelf.WebAPI.Models;

namespace PerfectSelf.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnAvailabilitiesController : ControllerBase
    {
        private readonly PerfectSelfContext _context;

        public UnAvailabilitiesController(PerfectSelfContext context)
        {
            _context = context;
        }

        // GET: api/UnAvailabilities
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UnAvailability>>> GetUnAvailabilities()
        {
            return await _context.UnAvailabilities.ToListAsync();
        }

        // GET: api/UnAvailabilities
        [HttpGet("AllByUid/{uid}")]
        public async Task<ActionResult<IEnumerable<UnAvailability>>> GetUnAvailabilities(String uid)
        {
            return await _context.UnAvailabilities.Where(row => uid == row.ReaderUid.ToString()).ToListAsync();
        }

        [HttpGet("UpcomingByUid/{uid}/{nowDT}")]
        public async Task<ActionResult<IEnumerable<UnAvailability>>> GetUpcomingUnAvailabilities(String uid, String nowDT)
        {
            DateTime dt;
            if( !DateTime.TryParse(nowDT, out dt) ) dt = DateTime.UtcNow;
            return await _context.UnAvailabilities.Where(row => (uid == row.ReaderUid.ToString() && row.Date >= dt)).ToListAsync();
        }

        // GET: api/UnAvailabilities/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UnAvailability>> GetUnAvailability(int id)
        {
            var UnAvailability = await _context.UnAvailabilities.FindAsync(id);

            if (UnAvailability == null)
            {
                return NotFound();
            }

            return UnAvailability;
        }

        // PUT: api/UnAvailabilities/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUnAvailability(int id, UnAvailability UnAvailability)
        {
            if (id != UnAvailability.Id)
            {
                return BadRequest();
            }

            _context.Entry(UnAvailability).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UnAvailabilityExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/UnAvailabilities
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UnAvailability>> PostUnAvailability(UnAvailability UnAvailability)
        {
            _context.UnAvailabilities.Add(UnAvailability);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUnAvailability", new { id = UnAvailability.Id }, UnAvailability);
        }

        // DELETE: api/UnAvailabilities/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUnAvailability(int id)
        {
            var UnAvailability = await _context.UnAvailabilities.FindAsync(id);
            if (UnAvailability == null)
            {
                return NotFound();
            }

            _context.UnAvailabilities.Remove(UnAvailability);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UnAvailabilityExists(int id)
        {
            return _context.UnAvailabilities.Any(e => e.Id == id);
        }
    }
}
