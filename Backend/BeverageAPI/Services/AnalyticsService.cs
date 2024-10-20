namespace BeverageAPI.Services;

public class AnalyticsService
{
    private readonly BeverageContext _context;

    public AnalyticsService(BeverageContext context)
    {
        _context = context;
    }

    // Get top beverages by sales quantity
    public IEnumerable<Beverage> GetTopBeverages(int topN)
    {
        var topBeverages = _context.OrderItems
            .GroupBy(oi => oi.BeverageId)
            .Select(g => new
            {
                BeverageId = g.Key,
                TotalQuantity = g.Sum(oi => oi.Quantity)
            })
            .OrderByDescending(g => g.TotalQuantity)
            .Take(topN)
            .Join(_context.Beverages, oi => oi.BeverageId, b => b.Id, (oi, b) => new Beverage
            {
                Id = b.Id,
                Name = b.Name,
                Description = b.Description,
                Price = b.Price,
                Size = b.Size
            })
            .ToList();

        return topBeverages;
    }

    // Get total sales (sum of all orders)
    public decimal GetTotalSales()
    {
        return _context.Orders.Sum(o => o.TotalAmount);
    }

    // Get top customers by total spending
    public IEnumerable<CustomerDTO> GetTopCustomers(int topN)
    {
        var topCustomers = _context.Orders
            .GroupBy(o => o.CustomerId)
            .Select(g => new
            {
                CustomerId = g.Key,
                TotalSpent = g.Sum(o => o.TotalAmount)
            })
            .OrderByDescending(g => g.TotalSpent)
            .Take(topN)
            .Join(_context.Customers, o => o.CustomerId, c => c.Id, (o, c) => new CustomerDTO
            {
                Id = c.Id,
                Name = c.Name,
                Email = c.Email
            })
            .ToList();

        return topCustomers;
    }

    // Get monthly revenue trends
    public IEnumerable<MonthlyRevenueDTO> GetMonthlyRevenue(int year)
    {
        var revenueTrends = _context.Orders
            .Where(o => o.OrderDate.Year == year)
            .GroupBy(o => o.OrderDate.Month)
            .Select(g => new MonthlyRevenueDTO
            {
                Month = g.Key,
                TotalRevenue = g.Sum(o => o.TotalAmount)
            })
            .OrderBy(r => r.Month)
            .ToList();

        return revenueTrends;
    }

    // Get sales by product size
    public IEnumerable<SalesBySizeDTO> GetSalesBySize()
    {
        var salesBySize = _context.OrderItems
            .GroupBy(oi => oi.Beverage.Size)
            .Select(g => new SalesBySizeDTO
            {
                Size = g.Key,
                TotalQuantity = g.Sum(oi => oi.Quantity),
                TotalRevenue = g.Sum(oi => oi.Price * oi.Quantity)
            })
            .ToList();

        return salesBySize;
    }
}
