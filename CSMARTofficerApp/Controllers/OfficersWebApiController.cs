using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CSMARTofficerApp.Models;

namespace CSMARTofficerApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OfficersWebApiController : ControllerBase
    {
        private readonly csmartContext _context;

        public OfficersWebApiController(csmartContext context)
        {
            _context = context;
        }

        // GET: api/OfficersWebApi
        [HttpGet("GetOfficers")]
        public async Task<ActionResult<IEnumerable<Officer>>> GetOfficers()
        {
            return await _context.Officers.ToListAsync();
        }

        // GET: api/OfficersWebApi/5
        [HttpGet("GetOfficerById")]
        public async Task<ActionResult<Officer>> GetOfficer(string id)
        {
            var officer = await _context.Officers.FindAsync(id);

            if (officer == null)
            {
                return NotFound();
            }

            return officer;
        }

        // PUT: api/OfficersWebApi/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("UpdateOfficer")]
        public async Task<IActionResult> PutOfficer(string id, Officer officer)
        {
            if (id != officer.OfficerKey)
            {
                return BadRequest();
            }

            _context.Entry(officer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OfficerExists(id))
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

        // POST: api/OfficersWebApi
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost("AddOfficer")]
        public async Task<ActionResult<Officer>> PostOfficer(Officer officer)
        {
            _context.Officers.Add(officer);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (OfficerExists(officer.OfficerKey))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetOfficer", new { id = officer.OfficerKey }, officer);
        }

        // DELETE: api/OfficersWebApi/5
        [HttpDelete("DeleteOfficerById")]
        public async Task<ActionResult<Officer>> DeleteOfficer(string id)
        {
            var officer = await _context.Officers.FindAsync(id);
            if (officer == null)
            {
                return NotFound();
            }

            _context.Officers.Remove(officer);
            await _context.SaveChangesAsync();

            return officer;
        }

        private bool OfficerExists(string id)
        {
            return _context.Officers.Any(e => e.OfficerKey == id);
        }
    }
}
