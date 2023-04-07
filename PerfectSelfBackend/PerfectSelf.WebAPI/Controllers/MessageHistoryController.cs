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
    public class MessageHistoryController : ControllerBase
    {
        private readonly PerfectSelfContext _context;

        public MessageHistoryController(PerfectSelfContext context)
        {
            _context = context;
        }

        // GET: api/MessageHistorys
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MessageHistory>>> GetMessageHistorys()
        {
            return await _context.MessageHistorys.ToListAsync();
        }

        // GET: api/MessageHistorys/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MessageHistory>> GetMessageHistory(int id)
        {
            var MessageHistory = await _context.MessageHistorys.FindAsync(id);

            if (MessageHistory == null)
            {
                return NotFound();
            }

            return MessageHistory;
        }

        // PUT: api/MessageHistorys/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMessageHistory(int id, MessageHistory MessageHistory)
        {
            if (id != MessageHistory.Id)
            {
                return BadRequest();
            }

            _context.Entry(MessageHistory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MessageHistoryExists(id))
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

        // POST: api/MessageHistorys
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MessageHistory>> PostMessageHistory(MessageHistory MessageHistory)
        {
            _context.MessageHistorys.Add(MessageHistory);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMessageHistory", new { id = MessageHistory.Id }, MessageHistory);
        }

        // DELETE: api/MessageHistorys/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMessageHistory(int id)
        {
            var MessageHistory = await _context.MessageHistorys.FindAsync(id);
            if (MessageHistory == null)
            {
                return NotFound();
            }

            _context.MessageHistorys.Remove(MessageHistory);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MessageHistoryExists(int id)
        {
            return _context.MessageHistorys.Any(e => e.Id == id);
        }
    }
}
