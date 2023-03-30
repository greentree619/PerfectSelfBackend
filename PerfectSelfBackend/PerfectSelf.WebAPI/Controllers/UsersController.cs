﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PerfectSelf.WebAPI.Common;
using PerfectSelf.WebAPI.Context;
using PerfectSelf.WebAPI.Models;
using static PerfectSelf.WebAPI.Models.User;

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
        [HttpGet("{uid}")]
        public async Task<ActionResult<User>> GetUser(string uid)
        {
            //var user = await _context.Users.FindAsync(id);
            var user = await _context.Users.FirstOrDefaultAsync(p => p.Uid.ToString() == uid);
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
        [HttpPut("{uid}")]
        public async Task<IActionResult> PutUser(string uid, User user)
        {
            var u = await _context.Users.FirstOrDefaultAsync(p => p.Uid.ToString() == uid);
            if (u == null)
            {
                return NotFound();
            }
            //user.Uid = u.Uid;
            if(user.UserName.Length != 0) u.UserName = user.UserName;
            if (user.Password.Length != 0) u.Password = user.Password;
            if (user.FirstName.Length != 0) u.FirstName = user.FirstName;
            if (user.LastName.Length != 0) u.LastName = user.LastName;
            if (user.Gender >= 0) u.Gender = user.Gender;
            if (user.CurrentAddress.Length != 0) u.CurrentAddress = user.CurrentAddress;
            if (user.PermanentAddress.Length != 0) u.PermanentAddress = user.PermanentAddress;
            if (user.City.Length != 0) u.City = user.City;
            if (user.Nationality.Length != 0) u.Nationality = user.Nationality;
            if (user.PhoneNumber.Length != 0) u.PhoneNumber = user.PhoneNumber;
            if (user.UserType != AccountType.Nothing) u.UserType = user.UserType;

            _context.Entry(u).State = EntityState.Modified;
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
        //// PUT: api/Users/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("name/{uid}")]
        //public async Task<IActionResult> PutUserName(string uid, User user)
        //{
        //    var u = await _context.Users.FirstOrDefaultAsync(p => p.Uid.ToString() == uid);
        //    if (u == null)
        //    {
        //        return NotFound();
        //    }
        //    u.UserName = user.UserName;
        //    _context.Entry(u).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        throw;
        //    }

        //    return NoContent();
        //}
        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok(user);
            //return CreatedAtAction("GetUser", new { id = user.Id }, user);
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
        [HttpDelete("{uid}")]
        public async Task<IActionResult> DeleteUser(string uid)
        {
            var user = await _context.Users.FirstOrDefaultAsync(p => p.Uid.ToString() == uid);

            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(string uid)
        {
            return _context.Users.Any(e => e.Uid.ToString() == uid);
        }
    }
}
