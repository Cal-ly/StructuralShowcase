using BeverageAPI.Services;

namespace BeverageAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AnalyticsController : ControllerBase
{
    private readonly AnalyticsService _analyticsService;

    public AnalyticsController(AnalyticsService analyticsService)
    {
        _analyticsService = analyticsService;
    }

    [HttpGet("top-beverages")]
    public IActionResult GetTopBeverages(int topN = 5)
    {
        var beverages = _analyticsService.GetTopBeverages(topN);
        return Ok(beverages);
    }

    [HttpGet("total-sales")]
    public IActionResult GetTotalSales()
    {
        var totalSales = _analyticsService.GetTotalSales();
        return Ok(totalSales);
    }

    [HttpGet("top-customers")]
    public IActionResult GetTopCustomers(int topN = 5)
    {
        var customers = _analyticsService.GetTopCustomers(topN);
        return Ok(customers);
    }

    [HttpGet("monthly-revenue")]
    public IActionResult GetMonthlyRevenue(int year)
    {
        var revenue = _analyticsService.GetMonthlyRevenue(year);
        return Ok(revenue);
    }

    [HttpGet("sales-by-size")]
    public IActionResult GetSalesBySize()
    {
        var sales = _analyticsService.GetSalesBySize();
        return Ok(sales);
    }
}