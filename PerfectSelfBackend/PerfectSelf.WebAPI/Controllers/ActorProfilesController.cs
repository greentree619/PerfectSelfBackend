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
    public class ActorProfilesController : ControllerBase
    {
        private readonly PerfectSelfContext _context;

        public ActorProfilesController(PerfectSelfContext context)
        {
            _context = context;
        }

        // GET: api/ActorProfiles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ActorProfile>>> GetActorProfiles()
        {
            return await _context.ActorProfiles.ToListAsync();
        }

        // GET: api/ActorProfiles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ActorProfile>> GetActorProfile(int id)
        {
            var actorProfile = await _context.ActorProfiles.FindAsync(id);

            if (actorProfile == null)
            {
                return NotFound();
            }

            return actorProfile;
        }

        // PUT: api/ActorProfiles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutActorProfile(int id, ActorProfile actorProfile)
        {
            if (id != actorProfile.Id)
            {
                return BadRequest();
            }

            _context.Entry(actorProfile).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ActorProfileExists(id))
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

        // POST: api/ActorProfiles
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ActorProfile>> PostActorProfile(ActorProfile actorProfile)
        {
            _context.ActorProfiles.Add(actorProfile);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetActorProfile", new { id = actorProfile.Id }, actorProfile);
        }

        // DELETE: api/ActorProfiles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteActorProfile(int id)
        {
            var actorProfile = await _context.ActorProfiles.FindAsync(id);
            if (actorProfile == null)
            {
                return NotFound();
            }

            _context.ActorProfiles.Remove(actorProfile);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ActorProfileExists(int id)
        {
            return _context.ActorProfiles.Any(e => e.Id == id);
        }
    }
}
