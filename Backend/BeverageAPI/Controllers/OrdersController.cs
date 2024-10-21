using BeverageAPI.Models.Enums;

namespace BeverageAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]  // Require authentication for all actions
public class OrdersController : ControllerBase
{
    private readonly BeverageContext _context;

    public OrdersController(BeverageContext context)
    {
        _context = context;
    }

    // Get all orders for the authenticated customer
    [HttpGet("my-orders")]
    public IActionResult GetMyOrders()
    {
        var userId = int.Parse(User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value);
        var user = _context.Users.Include(u => u.Customer).FirstOrDefault(u => u.Id == userId);
        if (user == null || user.Customer == null) return NotFound("Customer not found.");

        var orders = _context.Orders
            .Where(o => o.CustomerId == user.Customer.Id)
            .Include(o => o.OrderItems)
            .ToList();

        var orderDTOs = orders.Select(order => new OrderDTO
        {
            OrderDate = order.OrderDate,
            Status = order.Status.HasValue ? order.Status.Value : StatusEnum.Pending,
            TotalAmount = order.TotalAmount,
            OrderItems = order.OrderItems.Select(oi => new OrderItemDTO
            {
                BeverageId = oi.BeverageId,
                Quantity = oi.Quantity,
                Price = oi.Price
            }).ToList()
        }).ToList();

        return Ok(orderDTOs);
    }

    // Place a new order for the authenticated customer
    [HttpPost("place-order")]
    public IActionResult PlaceOrder([FromBody] List<OrderItemDTO> orderItemsDTO)
    {
        var userId = int.Parse(User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value);
        var user = _context.Users.Include(u => u.Customer).FirstOrDefault(u => u.Id == userId);
        if (user == null || user.Customer == null) return NotFound("Customer not found.");

        var order = new Order
        {
            CustomerId = user.Customer.Id,
            OrderDate = DateTime.UtcNow,
            Status = StatusEnum.Pending,
            OrderItems = orderItemsDTO.Select(oi => new OrderItem
            {
                BeverageId = oi.BeverageId,
                Quantity = oi.Quantity,
                Price = oi.Price * oi.Quantity
            }).ToList()
        };

        order.TotalAmount = order.OrderItems.Sum(oi => oi.Price);
        _context.Orders.Add(order);
        _context.SaveChanges();

        return CreatedAtAction(nameof(GetOrder), new { id = order.Id }, order);
    }

    // Get all orders
    [HttpGet]
    [Authorize]
    public IActionResult GetOrders()
    {
        var orders = _context.Orders.Include(o => o.OrderItems).ToList();
        return Ok(orders);
    }

    // Get a specific order by ID
    [HttpGet("{id}")]
    public IActionResult GetOrder(int id)
    {
        var order = _context.Orders.Include(o => o.OrderItems).FirstOrDefault(o => o.Id == id);
        if (order == null) return NotFound();
        return Ok(order);
    }

    // Update order status (for admin users)
    [HttpPut("{id}/status")]
    [Authorize(Roles = nameof(UserRole.Admin))]
    public IActionResult UpdateOrderStatus(int id, [FromBody] StatusEnum newStatus)
    {
        var order = _context.Orders.Find(id);
        if (order == null) return NotFound();

        order.Status = newStatus;
        _context.SaveChanges();
        return NoContent();
    }
}
