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
        [HttpPut("{uid}")]
        public async Task<IActionResult> PutReaderProfile(String uid, ReaderProfile readerProfile)
        {
            var reader = await _context.ReaderProfiles.FirstOrDefaultAsync(p => p.ReaderUid.ToString() == uid);

            if (reader == null)
            {
                return NotFound();
            }
            // don't update if the field is null
            if (readerProfile.About == null) readerProfile.About = reader.About;
            if (readerProfile.HourlyPrice == null) readerProfile.HourlyPrice = reader.HourlyPrice;
            if (readerProfile.Skills == null) readerProfile.Skills = reader.Skills;
            if (readerProfile.Title == null) readerProfile.Title = reader.Title;

            //_context.Entry(reader).State = EntityState.Modified;
            _context.Entry(reader).CurrentValues.SetValues(readerProfile);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReaderProfileExists(uid))
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

        private bool ReaderProfileExists(String uid)
        {
            return _context.ReaderProfiles.Any(e => e.ReaderUid.ToString() == uid);
        }
    }
}
