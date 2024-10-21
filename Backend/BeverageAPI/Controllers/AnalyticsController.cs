namespace BeverageAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[AllowAnonymous]
public class AnalyticsController : ControllerBase
{
    private readonly AnalyticsService _analyticsService;

    public AnalyticsController(AnalyticsService analyticsService)
    {
        _analyticsService = analyticsService;
    }

    // Get top beverages by sales quantity
    [HttpGet("top-beverages")]
    public IActionResult GetTopBeverages(int topN = 5)
    {
        var beverages = _analyticsService.GetTopBeverages(topN);
        return Ok(beverages);
    }

    // Get total sales (sum of all orders)
    [HttpGet("total-sales")]
    public IActionResult GetTotalSales()
    {
        var totalSales = _analyticsService.GetTotalSales();
        return Ok(totalSales);
    }

    // Get top customers by total spending
    [HttpGet("top-customers")]
    public IActionResult GetTopCustomers(int topN = 5)
    {
        var customers = _analyticsService.GetTopCustomers(topN);
        return Ok(customers);
    }

    // Get monthly revenue trends
    [HttpGet("monthly-revenue")]
    public IActionResult GetMonthlyRevenue(int year)
    {
        var revenue = _analyticsService.GetMonthlyRevenue(year);
        return Ok(revenue);
    }

    // Get sales by product size
    [HttpGet("sales-by-size")]
    public IActionResult GetSalesBySize()
    {
        var sales = _analyticsService.GetSalesBySize();
        return Ok(sales);
    }
}
