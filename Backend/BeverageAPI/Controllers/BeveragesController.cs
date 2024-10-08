namespace BeverageAPI.Controllers;

[Route("[controller]")]
[ApiController]
public class BeveragesController(BeverageContext context, IMapper autoMapper) : ControllerBase
{
    // GET: /Beverages
    [HttpGet]
    public async Task<ActionResult<IEnumerable<BeverageDTO>>> GetBeverages()
    {
        var beverages = await context.Beverages.ToListAsync();
        return Ok(autoMapper.Map<IEnumerable<BeverageDTO>>(beverages));
    }

    // GET: /Beverages/5
    [HttpGet("{id}")]
    public async Task<ActionResult<BeverageDTO>> GetBeverage(int id)
    {
        var beverage = await context.Beverages.FindAsync(id);

        if (beverage == null)
        {
            return NotFound();
        }

        return Ok(autoMapper.Map<BeverageDTO>(beverage));
    }

    // POST: /Beverages
    [HttpPost]
    public async Task<ActionResult<BeverageDTO>> PostBeverage(BeverageDTO beverageDto)
    {
        var beverage = autoMapper.Map<Beverage>(beverageDto);
        context.Beverages.Add(beverage);
        await context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetBeverage), new { id = beverage.Id }, autoMapper.Map<BeverageDTO>(beverage));
    }

    // PUT: /Beverages/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutBeverage(int id, BeverageDTO beverageDto)
    {
        var beverage = await context.Beverages.FindAsync(id);

        if (beverage == null)
        {
            return NotFound();
        }

        autoMapper.Map(beverageDto, beverage);
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

    // DELETE: /Beverages/5
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