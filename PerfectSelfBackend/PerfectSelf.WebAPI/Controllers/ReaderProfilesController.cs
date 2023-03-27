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
    public class ReaderProfilesController : ControllerBase
    {
        private readonly PerfectSelfContext _context;

        public ReaderProfilesController(PerfectSelfContext context)
        {
            _context = context;
        }

        // GET: api/ReaderProfiles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReaderProfile>>> GetReaderProfiles()
        {
            return await _context.ReaderProfiles.ToListAsync();
        }

        [HttpGet("ReaderList")]
        public IActionResult GetReaderList()
        {
            var items = _context.ReaderLists.ToList();
            return Ok(items);
        }

        // GET: api/ReaderProfiles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ReaderProfile>> GetReaderProfile(int id)
        {
            var readerProfile = await _context.ReaderProfiles.FindAsync(id);

            if (readerProfile == null)
            {
                return NotFound();
            }

            return readerProfile;
        }

        [HttpGet("Detail/{uid}")]
        public async Task<ActionResult> GetReaderDetailProfile(String uid)
        {
            var ReaderDetailProfile = (from users in _context.Users
                                   join profiles in _context.ReaderProfiles
                                   on users.Uid equals profiles.ReaderUid
                                   where users.Uid.ToString() == uid
                                   select new
                                   {
                                       users.Uid,
                                       users.UserName,
                                       profiles.Title,
                                       profiles.HourlyPrice,
                                       profiles.Others,
                                       profiles.VoiceType,
                                       profiles.About,
                                       profiles.Skills
                                    }).Single();
            return Ok(ReaderDetailProfile);
        }

        // PUT: api/ReaderProfiles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReaderProfile(int id, ReaderProfile readerProfile)
        {
            if (id != readerProfile.Id)
            {
                return BadRequest();
            }

            _context.Entry(readerProfile).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReaderProfileExists(id))
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

        // POST: api/ReaderProfiles
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ReaderProfile>> PostReaderProfile(ReaderProfile readerProfile)
        {
            _context.ReaderProfiles.Add(readerProfile);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReaderProfile", new { id = readerProfile.Id }, readerProfile);
        }

        // DELETE: api/ReaderProfiles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReaderProfile(int id)
        {
            var readerProfile = await _context.ReaderProfiles.FindAsync(id);
            if (readerProfile == null)
            {
                return NotFound();
            }

            _context.ReaderProfiles.Remove(readerProfile);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("ByUid/{uid}")]
        public async Task<IActionResult> DeleteReaderProfileBy(String uid)
        {
            var readerProfile = await _context.ReaderProfiles.FirstOrDefaultAsync(r => r.ReaderUid.ToString() == uid);
            if (readerProfile == null)
            {
                return NotFound();
            }

            _context.ReaderProfiles.Remove(readerProfile);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ReaderProfileExists(int id)
        {
            return _context.ReaderProfiles.Any(e => e.Id == id);
        }
    }
}
