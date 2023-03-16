using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PerfectSelf.WebAPI.Common;
using PerfectSelf.WebAPI.Context;
using PerfectSelf.WebAPI.Models;

namespace PerfectSelf.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly PerfectSelfContext _context;

        public UsersController(PerfectSelfContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            List<User> userList = await _context.Users.ToListAsync();
            foreach (var usr in userList)
            {
                usr.Password = "";
            }
            return userList;
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }
            else
            {
                user.Password = "";
            }

            return user;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }

        [HttpPost("Login")]
        public async Task<ActionResult<User>> LoginUser(LoginUser user)
        {
            var userdetails = await _context.Users
            .SingleOrDefaultAsync(m => m.Email == user.Email && m.Password == user.Password);
            if (userdetails == null)
            {
                return Ok(new { result = false, user= new { } });
            }

            userdetails.IsLogin = true;
            userdetails.Token = Common.Global.GenToken();
            _context.Users.Update(userdetails);
            _context.SaveChanges();
            Global.onlineAllUsers[userdetails.Token] = userdetails.Id;

            userdetails.Password = "";
            return Ok(new { result=true, user=userdetails });
        }

        [HttpPost("Logout")]
        public async Task<ActionResult<User>> LogoutUser(String tokenParam)
        {
            if (Global.onlineAllUsers[tokenParam] != null)
            {
                var userId = Global.onlineAllUsers[tokenParam].ToString();
                var userdetails = await _context.Users
                .SingleOrDefaultAsync( m => m.Id == Convert.ToInt32(userId) );
                if (userdetails != null)
                {
                    userdetails.IsLogin = false;
                    _context.Users.Update(userdetails);
                    _context.SaveChanges();
                    Global.onlineAllUsers[userdetails.Token] = null;
                    return Ok(new { result = true });
                }
            }
            return Ok(new { result = false });
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
