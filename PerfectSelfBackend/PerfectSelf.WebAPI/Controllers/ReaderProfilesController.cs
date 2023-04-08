using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PerfectSelf.WebAPI.Context;
using PerfectSelf.WebAPI.Models;
using static PerfectSelf.WebAPI.Models.ReaderProfile;

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

        public enum ReaderListSortType
        {
            Relevance,
            Rate,
            RateDesc,
            Price,
            PriceDesc,
            Available,
            Nothing = -1
        }

        public enum AvailableTimeSlotType
        {
            Min15,
            Min30,
            Min30More,
            StandBy,
            Nothing = -1
        }

        [HttpGet("ReaderList")]
        public IActionResult GetReaderList( String? readerName,
                                            bool? isSponsored,
                                            bool? availableSoon,
                                            float? topRated,
                                            bool? isOnline,
                                            AvailableTimeSlotType? availableTimeSlotType,
                                            DateTime? availableFrom,
                                            DateTime? availableTo,
                                            float? minPrice,
                                            float? maxPrice,
                                            PerfectSelfBase.Gender? gender,
                                            ReaderListSortType? sortBy )
        {
            //DbSet<ReaderList> readerLists = _context.ReaderLists;
            IQueryable<ReaderList> queryableLists = _context.ReaderLists;
            //String? readerName,
            if (readerName != null && readerName.Length > 0)
            {
                queryableLists = queryableLists.Where(r => 
                                                        (r.UserName.Contains(readerName) 
                                                        || r.FirstName.Contains(readerName)
                                                        || r.LastName.Contains(readerName)));
            }

            //bool? isSponsored,
            if (isSponsored != null)
            {
                queryableLists = queryableLists.Where(r =>
                                                        (r.IsSponsored == isSponsored));
            }

            //bool? availableSoon,
            if (availableSoon != null)
            {
                if (availableSoon == true)
                {
                    queryableLists = queryableLists.Where(r => (r.Date != null && ((DateTime)r.Date).Date == DateTime.Now.Date));
                }
                else
                {
                    queryableLists = queryableLists.Where(r => (r.Date == null || ((DateTime)r.Date).Date != DateTime.Now.Date));
                }
            }

            //float? topRated,
            if (topRated != null)
            {
                queryableLists = queryableLists.Where(r => (r.Score >= topRated));
            }

            //bool? isOnline,
            if (isOnline != null)
            {
                queryableLists = queryableLists.Where(r => (r.IsLogin == isOnline));
            }

            //AvailableTimeSlotType? availableTimeSlotType,
            if (availableTimeSlotType != null)
            {
                TimeSpan slotSpan = new TimeSpan(0, 0, 0);
                switch(availableTimeSlotType) {
                    case AvailableTimeSlotType.Min15:
                        slotSpan.Add(new TimeSpan(0, 15, 0));
                        queryableLists = queryableLists.Where(r => (r.ToTime != null
                                                                && r.FromTime != null
                                                                && ((DateTime)r.ToTime).Subtract(((DateTime)r.FromTime)).CompareTo(slotSpan) >= 0));
                        break;
                    case AvailableTimeSlotType.Min30:
                        slotSpan.Add(new TimeSpan(0, 30, 0));
                        queryableLists = queryableLists.Where(r => (r.ToTime != null
                                                                && r.FromTime != null
                                                                && ((DateTime)r.ToTime).Subtract(((DateTime)r.FromTime)) >= slotSpan));
                        break;
                    case AvailableTimeSlotType.Min30More:
                        slotSpan.Add(new TimeSpan(0, 45, 0));
                        queryableLists = queryableLists.Where(r => (r.ToTime != null
                                                                && r.FromTime != null
                                                                && ((DateTime)r.ToTime).Subtract(((DateTime)r.FromTime)) >= slotSpan));
                        break;
                    case AvailableTimeSlotType.StandBy:
                        queryableLists = queryableLists.Where(r => (r.IsStandBy == true));
                        break;
                    default:
                        break;
                }
            }

            //DateTime? availableFrom,
            if (availableFrom != null)
            {
                queryableLists = queryableLists.Where(r => (r.Date >= availableFrom));
            }

            //DateTime? availableTo,
            if (availableTo != null)
            {
                queryableLists = queryableLists.Where(r => (r.Date <= availableTo));
            }

            //float? minPrice,
            if (minPrice != null)
            {
                queryableLists = queryableLists.Where(r => (r.HourlyPrice >= minPrice));
            }

            //float? maxPrice,
            if (maxPrice != null)
            {
                queryableLists = queryableLists.Where(r => (r.HourlyPrice <= maxPrice));
            }

            //PerfectSelfBase.Gender? gender,
            if (gender != null)
            {
                queryableLists = queryableLists.Where(r => (r.Gender== gender));
            }

            //ReaderListSortType? sortBy
            List<ReaderList> resultList = new List<ReaderList>();
            if (sortBy != null)
            {
                switch (sortBy)
                {
                    case ReaderListSortType.Relevance:
                        resultList = queryableLists.OrderByDescending(r => r.CreatedTime).ToList();
                        break;
                    case ReaderListSortType.Rate:
                        resultList = queryableLists.OrderBy(r => r.Score).ToList();
                        break;
                    case ReaderListSortType.RateDesc:
                        resultList = queryableLists.OrderByDescending(r => r.Score).ToList();
                        break;
                    case ReaderListSortType.Price:
                        resultList = queryableLists.OrderBy(r => r.HourlyPrice).ToList();
                        break;
                    case ReaderListSortType.PriceDesc:
                        resultList = queryableLists.OrderByDescending(r => r.HourlyPrice).ToList();
                        break;
                    case ReaderListSortType.Available:
                        resultList = queryableLists.OrderBy(r => (r.Date == null ? DateTime.MaxValue : r.Date )).ToList();
                        break;
                    default:
                        resultList = queryableLists.OrderByDescending(r => r.CreatedTime).ToList();
                        break;
                }
            }
            else resultList = queryableLists.OrderByDescending(r => r.CreatedTime).ToList();

            return Ok(resultList);
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
            readerProfile.Id = reader.Id;
            if (readerProfile.About == null || readerProfile.About.Length == 0) readerProfile.About = reader.About;
            if (readerProfile.HourlyPrice <= 0) readerProfile.HourlyPrice = reader.HourlyPrice;
            if (readerProfile.Skills == null || readerProfile.Skills.Length == 0) readerProfile.Skills = reader.Skills;
            if (readerProfile.Title == null || readerProfile.Title.Length == 0) readerProfile.Title = reader.Title;
            if (readerProfile.VoiceType == _VoiceType.Nothing) readerProfile.VoiceType = reader.VoiceType;
            if (readerProfile.Others== _Others.Nothing) readerProfile.Others = reader.Others;

            //_context.Entry(reader).State = EntityState.Modified;
            _context.Entry(reader).CurrentValues.SetValues(readerProfile);
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
