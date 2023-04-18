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
    public class BooksController : ControllerBase
    {
        private readonly PerfectSelfContext _context;

        public BooksController(PerfectSelfContext context)
        {
            _context = context;
        }

        // GET: api/Books
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
        {
            return await _context.Books.ToListAsync();
        }

        [HttpGet("ByUid/{uid}")]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooksByUid(String uid)
        {
            var books = await _context.Books.Where(row => (uid == row.ActorUid.ToString() || uid == row.ReaderUid.ToString())).ToListAsync();

            if (books == null)
            {
                return NotFound();
            }

            return books;
        }

        [HttpGet("DetailList")]
        public IActionResult GetReaderList()
        {
            var items = _context.BookLists.ToList();
            return Ok(items);
        }

        public enum BookType
        {
            Past,
            Upcoming,
            Pending,
            Nothing = -1
        }
        [HttpGet("DetailList/ByUid/{uid}")]
        public IActionResult GetReaderListByUid(String uid, BookType? bookType)
        {
            IQueryable<BookList> items = _context.BookLists.Where(row => (uid == row.ActorUid.ToString() 
                                || uid == row.ReaderUid.ToString()));

            if (bookType != null)
            {
                switch ( bookType )
                {
                    case BookType.Past:
                        items = items.Where(row=> (row.BookStartTime != null && ((DateTime)row.BookStartTime) <= DateTime.Now));
                        break;
                    case BookType.Upcoming:
                        items = items.Where(row => (row.BookStartTime != null
                                                    && ((DateTime)row.BookStartTime) >= DateTime.Now)
                                                    && row.IsAccept);
                        break;
                    case BookType.Pending:
                        items = items.Where(row => (row.BookStartTime != null
                                                    && ((DateTime)row.BookStartTime) >= DateTime.Now)
                                                    && row.IsAccept == false);
                        break;
                }
            }

            var result = items.OrderBy(r => r.BookStartTime).ToList();
            return Ok(result);
        }

        [HttpGet("GetReaderBookCount/{uid}")]
        public IActionResult GetReaderBookCount(String uid)
        {
            var parameterReturn = new SqlParameter
            {
                ParameterName = "ReturnValue",
                SqlDbType = System.Data.SqlDbType.Int,
                Direction = System.Data.ParameterDirection.Output,
            };

            var result = _context.Database.ExecuteSqlRaw($"EXEC @returnValue = [dbo].[GetBookCount] @uid = N'{uid}'", parameterReturn);
            int returnValue = (int)parameterReturn.Value;

            return Ok(new { count = returnValue });
        }

        // GET: api/Books/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBook(int id)
        {
            var book = await _context.Books.FindAsync(id);

            if (book == null)
            {
                return NotFound();
            }

            return book;
        }

        [HttpGet("GetFeedback/ByUid/{uid}")]
        public IActionResult GetFeedbackByUid(String uid)
        {
            IQueryable<BookList> items = _context.BookLists.Where(row => (uid == row.ActorUid.ToString()
                                || uid == row.ReaderUid.ToString())).Where(row => row.ReaderReview != null 
                                                                                && row.ReaderReview.Length > 0);
            var result = items.OrderByDescending(r => r.BookStartTime).ToList();
            return Ok(result);
        }

        [HttpPut("GiveFeedbackToUid/{id}")]
        public async Task<IActionResult> GiveFeedbackToId(Int32 id, float score, String review)
        {
            var bookInfo = await _context.Books.FirstOrDefaultAsync(p => p.Id == id);
            if (bookInfo == null)
            {
                return NotFound();
            }
            _context.Entry(bookInfo).Property(u => u.ReaderScore).CurrentValue = score;
            _context.Entry(bookInfo).Property(u => u.ReaderReview).CurrentValue = review;
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

        // PUT: api/Books/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBook(int id, Book book)
        {
            if (id != book.Id)
            {
                return BadRequest();
            }

            _context.Entry(book).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(id))
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

        // POST: api/Books
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Book>> PostBook(Book book)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBook", new { id = book.Id }, book);
        }

        // DELETE: api/Books/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        // DELETE: api/Books/5
        [HttpDelete("ByRoomUid/{uid}")]
        public async Task<IActionResult> DeleteBookByRoomId(string uid)
        {
            var book = await _context.Books.FirstOrDefaultAsync(p => p.RoomUid.ToString() == uid);
            if (book == null)
            {
                return NotFound();
            }

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        // Accept Booking: api/Books/5
        [HttpPost("Accept/{id}")]
        public async Task<IActionResult> AcceptBooking(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            _context.Entry(book).Property(u => u.IsAccept).CurrentValue = true;
            await _context.SaveChangesAsync();

            return NoContent();
        }
        // Reschedule Booking: api/Books/5
        [HttpPost("Reschedule/{id}")]
        public async Task<IActionResult> RescheduleBooking(int id, Book newBook)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            _context.Entry(book).Property(u => u.BookStartTime).CurrentValue = newBook.BookStartTime;
            _context.Entry(book).Property(u => u.BookEndTime).CurrentValue = newBook.BookEndTime;
            await _context.SaveChangesAsync();

            return NoContent();
        }
        private bool BookExists(int id)
        {
            return _context.Books.Any(e => e.Id == id);
        }
    }
}
