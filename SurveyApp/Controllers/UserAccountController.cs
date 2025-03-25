using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SurveyApp.Data;
using SurveyApp.Handlers;
using SurveyApp.Models.Entities;

namespace SurveyApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAccountController : ControllerBase
    {
        private readonly AppDbContext _context;
        public UserAccountController(AppDbContext dbcontext) => _context = dbcontext;

        [HttpGet]
        public async Task<List<UserAccount>> Get()
        {
            return await _context.UserAccounts.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserAccount>> GetById(int id)
        {
            var userAccount = await _context.UserAccounts.FirstOrDefaultAsync(x => x.Id == id);
            if (userAccount == null)
            {
                return NotFound();
            }
            return Ok(userAccount);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] UserAccount userAccount)
        {
           if(string.IsNullOrEmpty(userAccount.FullName) || string.IsNullOrEmpty(userAccount.UserName) || string.IsNullOrEmpty(userAccount.Password))
            {
                return BadRequest("Please provide all the required fields");
            }
            userAccount.Password = PasswordHashHandler.HashPassword(userAccount.Password);
            await _context.UserAccounts.AddAsync(userAccount);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = userAccount.Id }, userAccount);
        }

        [HttpPut]
        public async Task<ActionResult> Update([FromBody] UserAccount userAccount)
        {
            if (string.IsNullOrEmpty(userAccount.FullName) || string.IsNullOrEmpty(userAccount.UserName) || string.IsNullOrEmpty(userAccount.Password))
            {
                return BadRequest("Please provide all the required fields");
            }
            var existingUserAccount = await _context.UserAccounts.FirstOrDefaultAsync(x => x.Id == userAccount.Id);
            if (existingUserAccount == null)
            {
                return NotFound("User account not found");
            }
            existingUserAccount.FullName = userAccount.FullName;
            existingUserAccount.UserName = userAccount.UserName;
            existingUserAccount.Password = PasswordHashHandler.HashPassword(userAccount.Password);
            await _context.SaveChangesAsync();
            return Ok(existingUserAccount);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existingUserAccount = await _context.UserAccounts.FirstOrDefaultAsync(x => x.Id == id);
            if (existingUserAccount == null)
            {
                return NotFound("User account not found");
            }
            _context.UserAccounts.Remove(existingUserAccount);
            await _context.SaveChangesAsync();
            return Ok();
        }

    }
}
 