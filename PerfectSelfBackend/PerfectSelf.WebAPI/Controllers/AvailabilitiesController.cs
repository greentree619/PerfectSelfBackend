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
    public class AvailabilitiesController : ControllerBase
    {
        private readonly PerfectSelfContext _context;

        public AvailabilitiesController(PerfectSelfContext context)
        {
            _context = context;
        }

        // GET: api/Availabilities
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Availability>>> GetAvailabilities()
        {
            return await _context.Availabilities.ToListAsync();
        }

        // GET: api/Availabilities
        [HttpGet("AllByUid/{uid}")]
        public async Task<ActionResult<IEnumerable<Availability>>> GetAvailabilities(String uid)
        {
            return await _context.Availabilities.Where(row => uid == row.ReaderUid.ToString()).ToListAsync();
        }

        [HttpGet("UpcomingByUid/{uid}/{nowDT}")]
        public async Task<ActionResult<IEnumerable<Availability>>> GetUpcomingAvailabilities(String uid, String nowDT)
        {
            DateTime dt;
            if( !DateTime.TryParse(nowDT, out dt) ) dt = DateTime.UtcNow;
            return await _context.Availabilities.Where(row => (uid == row.ReaderUid.ToString() && row.Date >= dt)).ToListAsync();
        }

        // GET: api/Availabilities/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Availability>> GetAvailability(int id)
        {
            var availability = await _context.Availabilities.FindAsync(id);

            if (availability == null)
            {
                return NotFound();
            }

            return availability;
        }

        // PUT: api/Availabilities/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAvailability(int id, Availability availability)
        {
            if (id != availability.Id)
            {
                return BadRequest();
            }

            _context.Entry(availability).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AvailabilityExists(id))
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

        // POST: api/Availabilities
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Availability>> PostAvailability(Availability availability)
        {
            _context.Availabilities.Add(availability);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAvailability", new { id = availability.Id }, availability);
        }

        [HttpPut("AddBatch")]
        public async Task<ActionResult> AddBatchAvailability(AvailabilityTimeSlotBatch batchSlot)
        {
            // Remove reader's previous availability
            var itemsToRemove = _context.Availabilities.Where(r => batchSlot.ReaderUid.ToString() == r.ReaderUid.ToString());
            _context.RemoveRange(itemsToRemove);

            int addCount = 0;
            foreach (var timeSlot in batchSlot.batchTimeSlot)
            {
                var availability = new Availability{
                    ReaderUid = batchSlot.ReaderUid,
                    IsStandBy = timeSlot.IsStandBy,
                    RepeatFlag = timeSlot.RepeatFlag,
                    Date = timeSlot.Date,
                    FromTime = timeSlot.FromTime,
                    ToTime = timeSlot.ToTime
                };

                _context.Availabilities.Add(availability);
                addCount++;
            }
            await _context.SaveChangesAsync();
            return Ok(new { count = addCount });
        }

        //[HttpPut("UpdateBatch")]
        //public async Task<ActionResult> UpdateBatchAvailability(List<Availability> batchAvailability)
        //{
        //    int addCount = 0;
        //    int updateCount = 0;
        //    foreach (var availInfo in batchAvailability)
        //    {
        //        if (availInfo.Id == 0)
        //        {
        //            var availability = new Availability
        //            {
        //                ReaderUid = availInfo.ReaderUid,
        //                IsStandBy = availInfo.IsStandBy,
        //                RepeatFlag = availInfo.RepeatFlag,
        //                Date = availInfo.Date,
        //                FromTime = availInfo.FromTime,
        //                ToTime = availInfo.ToTime
        //            };

        //            _context.Availabilities.Add(availability);
        //            addCount++;
        //        }
        //        else if (availInfo.Id > 0)
        //        {
        //            _context.Entry(availInfo).State = EntityState.Modified;
        //            updateCount++;
        //        }
        //    }
        //    await _context.SaveChangesAsync();
        //    return Ok(new { add = addCount, updated = updateCount });
        //}

        // DELETE: api/Availabilities/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAvailability(int id)
        {
            var availability = await _context.Availabilities.FindAsync(id);
            if (availability == null)
            {
                return NotFound();
            }

            _context.Availabilities.Remove(availability);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AvailabilityExists(int id)
        {
            return _context.Availabilities.Any(e => e.Id == id);
        }
    }
}
