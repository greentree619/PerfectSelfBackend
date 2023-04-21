using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
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

        // GET: api/MessageHistorys/5
        [HttpGet("GetChannelHistory/{uid}")]
        public async Task<ActionResult<IEnumerable<MessageChannelView>>> GetChannelHistory(String uid)
        {
            List<MessageChannelView> messageChannelHistories = _context.MessageChannelViews.Where(row => (row.SenderUid != null && row.ReceiverUid != null  && (row.SenderUid.ToString().CompareTo(uid) == 0 
                                                                                                                      || row.ReceiverUid.ToString().CompareTo(uid) == 0))).ToList();

            if (messageChannelHistories == null)
            {
                return NotFound();
            }

            return messageChannelHistories;
        }

        [HttpGet("GetChatHistory/{roomUid}")]
        public async Task<ActionResult<IEnumerable<MessageHistory>>> GetChatHistory(String roomUid)
        {
            List<MessageHistory> messageHistories = _context.MessageHistorys.Where(row => (row.RoomUid.ToString() == roomUid))
                                                                                                .OrderByDescending(row => row.SendTime).Take(10).OrderBy(row => row.Id).ToList();
            if (messageHistories == null)
            {
                return NotFound();
            }

            return messageHistories;
        }

        [HttpGet("GetChatHistoryEx/{senderUid}/{receiverUid}")]
        //[HttpGet("GetChatHistoryEx/{roomUid}")]
        public async Task<ActionResult<IEnumerable<MessageHistory>>> GetChatHistoryEx(String senderUid, String receiverUid)
        {
            List<MessageHistory> messageHistories = _context.MessageHistorys.FromSqlRaw<MessageHistory>($"EXEC [dbo].[GetChatHistoryEx] @SenderUid = N'{senderUid}', @ReceiverUid = N'{receiverUid}'").ToList();
            return messageHistories;
        }

        [HttpGet("GetUnreadCount/{roomUid}")]
        public async Task<IActionResult> GetUnreadCount(String roomUid)
        {
            var parameterReturn = new SqlParameter
            {
                ParameterName = "ReturnValue",
                SqlDbType = System.Data.SqlDbType.Int,
                Direction = System.Data.ParameterDirection.Output,
            };

            var result = _context.Database.ExecuteSqlRaw($"EXEC @returnValue = [dbo].[GetUnreadMessageCount] @roomUid = N'{roomUid}'", parameterReturn);
            int returnValue = (int)parameterReturn.Value;

            return Ok(new { count = returnValue });
        }

        [HttpGet("GetUnreadCountEx/{uid}")]
        public async Task<IActionResult> GetUnreadCountEx(String uid)
        {
            Int32 unreadCount = _context.MessageHistorys
                .Where(row => ((row.SenderUid.ToString() == uid || row.ReceiverUid.ToString() == uid)
                               && row.HadRead == false)).ToList().Count;
            return Ok(new { uid = uid, unreadCount = unreadCount });
        }

        [HttpGet("GetRoomId/{senderUid}/{receiverUid}")]
        public async Task<IActionResult> GetRoomId(String senderUid, String receiverUid)
        {
            List<MessageHistory> messageChatHistories = _context.MessageHistorys
                                                                        .Where(row => ((row.SenderUid.ToString() == senderUid && row.ReceiverUid.ToString() == receiverUid)
                                                                                      || (row.SenderUid.ToString() == receiverUid && row.ReceiverUid.ToString() == senderUid)))
                                                                        .Take(1).ToList();

            String roomUid = Guid.NewGuid().ToString();
            if (messageChatHistories != null && messageChatHistories.Count > 0)
            {
                roomUid = messageChatHistories[0].RoomUid.ToString();
            }

            return Ok(new { roomUid = roomUid });
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

        [HttpPut("SetReadMessage/{id}/{flag}")]
        public async Task<IActionResult> SetReadMessage(int id, bool flag)
        {
            var message = await _context.MessageHistorys.FirstOrDefaultAsync(m => m.Id == id);
            if (message == null)
            {
                return NotFound();
            }

            var messageVal = new MessageHistory();
            messageVal = message;
            messageVal.HadRead = flag;
            //_context.Entry(reader).State = EntityState.Modified;
            _context.Entry(message).CurrentValues.SetValues(messageVal);
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

        [HttpPut("SetAllReadMessage/{reciverUid}/{senderUid}")]
        public async Task<IActionResult> SetAllReadMessage(String reciverUid, String senderUid)
        {
            //FIXME _context.Database.ExecuteSqlRaw($"EXEC [dbo].[SetAllReadMessage] @receiverUid = N'{reciverUid}', @senderUid = N'{senderUid}'");
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
