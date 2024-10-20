using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BeverageAPI.Controllers;
[Route("api/[controller]")]
[ApiController]
public class OrdersController : ControllerBase
{
    private readonly BeverageContext _context;

    public OrdersController(BeverageContext context)
    {
        _context = context;
    }

    [HttpPost]
    public IActionResult PlaceOrder(Order order)
    {
        _context.Orders.Add(order);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetOrder), new { id = order.Id }, order);
    }

    [HttpGet("{id}")]
    public IActionResult GetOrder(int id)
    {
        var order = _context.Orders.Include(o => o.OrderItems).FirstOrDefault(o => o.Id == id);
        if (order == null) return NotFound();
        return Ok(order);
    }

    [HttpPut("{id}/status")]
    public IActionResult UpdateOrderStatus(int id, string status)
    {
        var order = _context.Orders.Find(id);
        if (order == null) return NotFound();

        order.Status = status;
        _context.SaveChanges();
        return NoContent();
    }
}