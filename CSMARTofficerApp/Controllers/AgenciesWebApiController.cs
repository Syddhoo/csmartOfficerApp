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
    public class AgenciesWebApiController : ControllerBase
    {
        private readonly csmartContext _context;

        public AgenciesWebApiController(csmartContext context)
        {
            _context = context;
        }

        // GET: api/AgenciesWebApi
        [HttpGet ("GetAgencies")]
        public async Task<ActionResult<IEnumerable<OfficerAgency>>> GetOfficerAgencies()
        {
            return await _context.OfficerAgencies.ToListAsync();
        }

        // GET: api/AgenciesWebApi/5
        [HttpGet("GetAgencyById")]
        public async Task<ActionResult<OfficerAgency>> GetOfficerAgency(string id)
        {
            var officerAgency = await _context.OfficerAgencies.FindAsync(id);

            if (officerAgency == null)
            {
                return NotFound();
            }

            return officerAgency;
        }

        // PUT: api/AgenciesWebApi/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("UpdateAgency")]
        public async Task<IActionResult> PutOfficerAgency(string id, OfficerAgency officerAgency)
        {
            if (id != officerAgency.AgencyCode)
            {
                return BadRequest();
            }

            _context.Entry(officerAgency).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OfficerAgencyExists(id))
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

        // POST: api/AgenciesWebApi
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost("AddAgency")]
        public async Task<ActionResult<OfficerAgency>> PostOfficerAgency(OfficerAgency officerAgency)
        {
            _context.OfficerAgencies.Add(officerAgency);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (OfficerAgencyExists(officerAgency.AgencyCode))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetOfficerAgency", new { id = officerAgency.AgencyCode }, officerAgency);
        }

        // DELETE: api/AgenciesWebApi/5
        [HttpDelete("DeleteAgencyById")]
        public async Task<ActionResult<OfficerAgency>> DeleteOfficerAgency(string id)
        {
            var officerAgency = await _context.OfficerAgencies.FindAsync(id);
            if (officerAgency == null)
            {
                return NotFound();
            }

            _context.OfficerAgencies.Remove(officerAgency);
            await _context.SaveChangesAsync();

            return officerAgency;
        }

        private bool OfficerAgencyExists(string id)
        {
            return _context.OfficerAgencies.Any(e => e.AgencyCode == id);
        }
    }
}
