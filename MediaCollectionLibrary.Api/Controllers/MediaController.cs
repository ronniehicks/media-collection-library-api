using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediaCollectionLibrary.Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MediaCollectionLibrary.Api.Controllers
{
    [Route("api/[controller]")]
    public class MediaController : Controller
    {
        private readonly MclContext _context;

        public MediaController(MclContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_context.Media.ToList());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var entry = _context.Media.Find(id);
            if (entry == null) return NotFound();

            return Ok(entry);
        }

        [HttpPost]
        public IActionResult Post([FromBody]Medium medium)
        {
            if (medium == null || !ModelState.IsValid)
                return BadRequest();

            if (_context.Media.Find(medium.MediumId) != null)
                return StatusCode(StatusCodes.Status409Conflict);

            _context.Media.Add(medium);
            _context.SaveChanges();

            return CreatedAtAction("Get", new { id = medium.MediumId }, medium);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Medium value)
        {
            if (value == null || value.MediumId != id || !ModelState.IsValid)
                return BadRequest();

            var entry = _context.Media.Find(id);
            if (entry == null) return NotFound();

            //TODO: Map values

            _context.Media.Update(entry);
            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (id <= 0) return BadRequest();

            var entry = _context.Media.Find(id);
            if (entry == null) return NotFound();

            _context.Media.Remove(entry);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
