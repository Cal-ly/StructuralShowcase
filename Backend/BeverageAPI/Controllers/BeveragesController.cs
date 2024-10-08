using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BeverageAPI.Models;

namespace BeverageAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BeveragesController(BeverageContext context) : ControllerBase
    {
        // GET: Beverages
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Beverage>>> GetBeverages()
        {
            return await context.Beverages.ToListAsync();
        }

        // GET: Beverages/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Beverage>> GetBeverage(int id)
        {
            var beverage = await context.Beverages.FindAsync(id);

            return beverage == null ? (ActionResult<Beverage>)NotFound() : (ActionResult<Beverage>)beverage;
        }

        // PUT: Beverages/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBeverage(int id, Beverage beverage)
        {
            if (id != beverage.Id)
            {
                return BadRequest();
            }

            context.Entry(beverage).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BeverageExists(id))
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

        // POST: Beverages
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Beverage>> PostBeverage(Beverage beverage)
        {
            context.Beverages.Add(beverage);
            await context.SaveChangesAsync();

            return CreatedAtAction("GetBeverage", new { id = beverage.Id }, beverage);
        }

        // DELETE: Beverages/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBeverage(int id)
        {
            var beverage = await context.Beverages.FindAsync(id);
            if (beverage == null)
            {
                return NotFound();
            }

            context.Beverages.Remove(beverage);
            await context.SaveChangesAsync();

            return NoContent();
        }

        private bool BeverageExists(int id)
        {
            return context.Beverages.Any(e => e.Id == id);
        }
    }
}
