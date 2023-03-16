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
    public class PerfectSelfVersionsController : ControllerBase
    {
        private readonly PerfectSelfContext _context;

        public PerfectSelfVersionsController(PerfectSelfContext context)
        {
            _context = context;
        }

        // GET: api/PerfectSelfVersions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PerfectSelfVersion>>> GetPerfectSelfVersions()
        {
            return await _context.PerfectSelfVersions.ToListAsync();
        }

        // GET: api/PerfectSelfVersions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PerfectSelfVersion>> GetPerfectSelfVersion(int id)
        {
            var perfectSelfVersion = await _context.PerfectSelfVersions.FindAsync(id);

            if (perfectSelfVersion == null)
            {
                return NotFound();
            }

            return perfectSelfVersion;
        }

        // PUT: api/PerfectSelfVersions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPerfectSelfVersion(int id, PerfectSelfVersion perfectSelfVersion)
        {
            if (id != perfectSelfVersion.Id)
            {
                return BadRequest();
            }

            _context.Entry(perfectSelfVersion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PerfectSelfVersionExists(id))
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

        // POST: api/PerfectSelfVersions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PerfectSelfVersion>> PostPerfectSelfVersion(PerfectSelfVersion perfectSelfVersion)
        {
            _context.PerfectSelfVersions.Add(perfectSelfVersion);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPerfectSelfVersion", new { id = perfectSelfVersion.Id }, perfectSelfVersion);
        }

        // DELETE: api/PerfectSelfVersions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePerfectSelfVersion(int id)
        {
            var perfectSelfVersion = await _context.PerfectSelfVersions.FindAsync(id);
            if (perfectSelfVersion == null)
            {
                return NotFound();
            }

            _context.PerfectSelfVersions.Remove(perfectSelfVersion);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PerfectSelfVersionExists(int id)
        {
            return _context.PerfectSelfVersions.Any(e => e.Id == id);
        }
    }
}
