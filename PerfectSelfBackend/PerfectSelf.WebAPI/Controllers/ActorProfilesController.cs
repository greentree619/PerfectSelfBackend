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
        // GET: api/ActorProfiles/5
        [HttpGet("ByUid/{uid}")]
        public async Task<ActionResult<ActorProfile>> GetActorProfileByUid(string uid)
        {
            var actorProfile = await _context.ActorProfiles.FirstOrDefaultAsync(p => p.ActorUid.ToString() == uid);

            if (actorProfile == null)
            {
                return NotFound();
            }

            return actorProfile;
        }

        // PUT: api/ActorProfiles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("ByUid/{uid}")]
        public async Task<IActionResult> PutActorProfileByUid(string uid, ActorProfile actorProfile)
        {
            var actor = await _context.ActorProfiles.FirstOrDefaultAsync(p => p.ActorUid.ToString() == uid);
            if (actorProfile == null)
            {
                return NotFound();
            }

            if ( actorProfile.Title != null || actorProfile.Title.Length != 0 ) actor.Title = actorProfile.Title;
            if ( actorProfile.AgeRange != null || actorProfile.AgeRange.Length != 0) actor.AgeRange = actorProfile.AgeRange;
            if ( actorProfile.Height > 0) actor.Height = actorProfile.Height;
            if ( actorProfile.Weight > 0) actor.Weight = actorProfile.Weight;
            if ( actorProfile.Country != null || actorProfile.Country.Length != 0) actor.Country = actorProfile.Country;
            if ( actorProfile.State != null || actorProfile.State.Length != 0) actor.State = actorProfile.State;
            if ( actorProfile.City != null || actorProfile.City.Length != 0) actor.City = actorProfile.City;
            if ( actorProfile.ReviewCount > 0) actor.ReviewCount = actorProfile.ReviewCount;
            if ( actorProfile.Score > 0) actor.Score = actorProfile.Score;
            if ( actorProfile.VaccinationStatus != null) actor.VaccinationStatus = actorProfile.VaccinationStatus;
            _context.Entry(actor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return NoContent();
        }

        // DELETE: api/ActorProfiles/5
        [HttpDelete("ByUid/{uid}")]
        public async Task<IActionResult> DeleteActorProfileByUid(string uid)
        {
            var actor = await _context.ActorProfiles.FirstOrDefaultAsync(p => p.ActorUid.ToString() == uid);
            
            if (actor == null)
            {
                return NotFound();
            }

            _context.ActorProfiles.Remove(actor);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        private bool ActorProfileExists(int id)
        {
            return _context.ActorProfiles.Any(e => e.Id == id);
        }
    }
}
