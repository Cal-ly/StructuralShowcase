namespace BeverageAPI.Controllers;

[Route("[controller]")]
[ApiController]
public class BeveragesController : ControllerBase
{
    private readonly BeverageContext _context;
    private readonly IMapper _autoMapper;

    public BeveragesController(BeverageContext context, IMapper autoMapper)
    {
        _context = context;
        _autoMapper = autoMapper;
    }

    // GET: /Beverages
    [HttpGet]
    public async Task<ActionResult<IEnumerable<BeverageDTO>>> GetBeverages()
    {
        var beverages = await _context.Beverages.ToListAsync();
        return Ok(_autoMapper.Map<IEnumerable<BeverageDTO>>(beverages));
    }

    // GET: /Beverages/5
    [HttpGet("{id}")]
    public async Task<ActionResult<BeverageDTO>> GetBeverage(int id)
    {
        var beverage = await _context.Beverages.FindAsync(id);

        if (beverage == null)
        {
            return NotFound();
        }

        return Ok(_autoMapper.Map<BeverageDTO>(beverage));
    }

    // POST: /Beverages
    [HttpPost]
    public async Task<ActionResult<BeverageDTO>> PostBeverage(BeverageDTO beverageDto)
    {
        var beverage = _autoMapper.Map<Beverage>(beverageDto);
        _context.Beverages.Add(beverage);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetBeverage), new { id = beverage.Id }, _autoMapper.Map<BeverageDTO>(beverage));
    }

    // PUT: /Beverages/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutBeverage(int id, BeverageDTO beverageDto)
    {
        var beverage = await _context.Beverages.FindAsync(id);

        if (beverage == null)
        {
            return NotFound();
        }

        _autoMapper.Map(beverageDto, beverage);
        _context.Entry(beverage).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
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
        var beverage = await _context.Beverages.FindAsync(id);
        if (beverage == null)
        {
            return NotFound();
        }

        _context.Beverages.Remove(beverage);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool BeverageExists(int id)
    {
        return _context.Beverages.Any(e => e.Id == id);
    }
}
