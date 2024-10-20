using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BeverageAPI.Controllers;
[Route("api/[controller]")]
[ApiController]
public class CustomersController : ControllerBase
{
    private readonly BeverageContext _context;

    public CustomersController(BeverageContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult GetAllCustomers()
    {
        var customers = _context.Customers.ToList();
        return Ok(customers);
    }

    [HttpGet("{id}")]
    public IActionResult GetCustomer(int id)
    {
        var customer = _context.Customers.Find(id);
        if (customer == null) return NotFound();
        return Ok(customer);
    }

    [HttpPost]
    public IActionResult AddCustomer(Customer customer)
    {
        _context.Customers.Add(customer);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetCustomer), new { id = customer.Id }, customer);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateCustomer(int id, Customer customer)
    {
        if (id != customer.Id) return BadRequest();
        _context.Entry(customer).State = EntityState.Modified;
        _context.SaveChanges();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteCustomer(int id)
    {
        var customer = _context.Customers.Find(id);
        if (customer == null) return NotFound();

        _context.Customers.Remove(customer);
        _context.SaveChanges();
        return NoContent();
    }
}
