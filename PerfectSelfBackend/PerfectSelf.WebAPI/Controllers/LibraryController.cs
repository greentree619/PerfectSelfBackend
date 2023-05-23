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
    public class LibraryController : ControllerBase
    {
        private readonly PerfectSelfContext _context;

        public LibraryController(PerfectSelfContext context)
        {
            _context = context;
        }

        // GET: api/Library
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ActorReaderTapMap>>> GetTapes()
        {
            return await _context.ActorReaderTapMaps.OrderByDescending(row=>row.CreatedTime).ToListAsync();
        }

        [HttpGet("ByUid/{uid}")]
        public async Task<ActionResult<IEnumerable<ActorReaderTapMap>>> GetTapesByUid(String uid)
        {
            var tapes = await _context.ActorReaderTapMaps.Where(row => uid == row.ActorUid.ToString()).OrderByDescending(row=>row.CreatedTime).ToListAsync();

            if (tapes == null)
            {
                return NotFound();
            }

            return tapes;
        }

        // GET: api/Library/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tape>> GetTape(int id)
        {
            var tape = await _context.Tapes.FindAsync(id);

            if (tape == null)
            {
                return NotFound();
            }

            return tape;
        }        

        // PUT: api/Library/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTape(int id, Tape tape)
        {
            if (id != tape.Id)
            {
                return BadRequest();
            }

            _context.Entry(tape).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TapeExists(id))
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

        // POST: api/Library
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Tape>> PostTape(Tape tape)
        {
            _context.Tapes.Add(tape);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTape", new { id = tape.Id }, tape);
        }

        // DELETE: api/Library/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTape(int id)
        {
            var tape = await _context.Tapes.FindAsync(id);
            if (tape == null)
            {
                return NotFound();
            }

            _context.Tapes.Remove(tape);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TapeExists(int id)
        {
            return _context.Tapes.Any(e => e.Id == id);
        }
    }
}
