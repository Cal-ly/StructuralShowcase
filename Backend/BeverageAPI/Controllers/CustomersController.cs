using Microsoft.AspNetCore.Authorization;

namespace BeverageAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]  // Only authenticated users can access these endpoints
    public class CustomersController : ControllerBase
    {
        private readonly BeverageContext _context;

        public CustomersController(BeverageContext context)
        {
            _context = context;
        }

        // Get customer profile for the authenticated user
        [HttpGet("me")]
        public IActionResult GetMyProfile()
        {
            var userId = int.Parse(User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value);
            var user = _context.Users.Include(u => u.Customer).FirstOrDefault(u => u.Id == userId);

            if (user == null)
            {
                return NotFound("User not found.");
            }

            var customer = user.Customer;
            if (customer == null)
            {
                return NotFound("Customer profile not found.");
            }

            var customerDTO = new CustomerDTO
            {
                Id = customer.Id,
                Name = customer.Name,
                Email = customer.Email,
                Orders = customer.Orders.Select(o => new OrderDTO
                {
                    OrderDate = o.OrderDate,
                    Status = o.Status.HasValue ? (StatusEnum)o.Status.Value : StatusEnum.Pending, // Default to Pending if null
                    TotalAmount = o.TotalAmount
                }).ToList()
            };

            return Ok(customerDTO);
        }

        // Update authenticated customer's profile
        [HttpPut("me")]
        public IActionResult UpdateMyProfile([FromBody] UpdateCustomerDTO customerDTO)
        {
            var userId = int.Parse(User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value);
            var user = _context.Users.Include(u => u.Customer).FirstOrDefault(u => u.Id == userId);

            if (user == null)
            {
                return NotFound("User not found.");
            }

            var customer = user.Customer;
            if (customer == null)
            {
                return NotFound("Customer profile not found.");
            }

            customer.Name = customerDTO.Name;
            customer.Email = customerDTO.Email;

            _context.SaveChanges();
            return Ok("Profile updated successfully.");
        }

        // For admin users to retrieve all customers
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult GetAllCustomers()
        {
            var customers = _context.Customers.Include(c => c.Orders).ToList();
            var customerDTOs = customers.Select(customer => new CustomerDTO
            {
                Id = customer.Id,
                Name = customer.Name,
                Email = customer.Email,
                Orders = customer.Orders.Select(o => new OrderDTO
                {
                    OrderDate = o.OrderDate,
                    Status = o.Status.HasValue ? (StatusEnum)o.Status.Value : StatusEnum.Pending,
                    TotalAmount = o.TotalAmount
                }).ToList()
            });

            return Ok(customerDTOs);
        }

        // For admin users to get a specific customer by ID
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetCustomer(int id)
        {
            var customer = _context.Customers.Include(c => c.Orders).FirstOrDefault(c => c.Id == id);
            if (customer == null) return NotFound();

            var customerDTO = new CustomerDTO
            {
                Id = customer.Id,
                Name = customer.Name,
                Email = customer.Email,
                Orders = customer.Orders.Select(o => new OrderDTO
                {
                    OrderDate = o.OrderDate,
                    Status = o.Status.HasValue ? (StatusEnum)o.Status.Value : StatusEnum.Pending,
                    TotalAmount = o.TotalAmount
                }).ToList()
            };

            return Ok(customerDTO);
        }

        // For admin users to add a new customer
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult AddCustomer([FromBody] Customer customer)
        {
            _context.Customers.Add(customer);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetCustomer), new { id = customer.Id }, customer);
        }

        // For admin users to update a specific customer by ID
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult UpdateCustomer(int id, [FromBody] Customer customer)
        {
            if (id != customer.Id) return BadRequest();
            _context.Entry(customer).State = EntityState.Modified;
            _context.SaveChanges();
            return NoContent();
        }

        // For admin users to delete a customer by ID
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteCustomer(int id)
        {
            var customer = _context.Customers.Find(id);
            if (customer == null) return NotFound();

            _context.Customers.Remove(customer);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
