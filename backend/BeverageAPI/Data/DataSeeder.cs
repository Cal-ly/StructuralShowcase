namespace BeverageAPI.Data;

public class DataSeeder
{
    private readonly BeverageContext _context;

    public DataSeeder(BeverageContext context)
    {
        _context = context;
    }

    public void SeedData()
    {
        if (!_context.Customers.Any())
        {
            // Seed Customers
            var customers = new List<Customer>
            {
                new Customer { Name = "Soren Sorensen", Email = "sorensorensen@test.dk" },
                new Customer { Name = "Jorn Jorgensen", Email = "jornjorgensen@test.dk" },
                new Customer { Name = "Hans Hansen", Email = "hanshansen@test.dk" },
                new Customer { Name = "Lars Larsen", Email = "larslarsen@test.dk" },
                new Customer { Name = "Peter Petersen", Email = "peterpetersen@test.dk" },
                new Customer { Name = "Ole Olsen", Email = "oleolsen@test.dk" },
                new Customer { Name = "Karl Karlsen", Email = "karlkarlsen@test.dk" },
                new Customer { Name = "Morten Mortensen", Email = "mortenmortensen@test.dk" },
                new Customer { Name = "Frederik Frederiksen", Email = "frederikfrederiksen@test.dk" },
                new Customer { Name = "Niels Nielsen", Email = "nielsnielsen@test.dk" },
                new Customer { Name = "Anders Andersen", Email = "andersandersen@test.dk" },
                new Customer { Name = "Poul Poulsen", Email = "poulpoulsen@test.dk" },
                new Customer { Name = "Jens Jensen", Email = "jensjensen@test.dk" },
                new Customer { Name = "Erik Eriksen", Email = "erikeriksen@test.dk" },
                new Customer { Name = "Mads Madsen", Email = "madsmadsen@test.dk" },
                new Customer { Name = "Kristian Kristiansen", Email = "kristiankristiansen@test.dk" },
                new Customer { Name = "Michael Michaelsen", Email = "michaelmichaelsen@test.dk" },
                new Customer { Name = "Henrik Henriksen", Email = "henrikhenriksen@test.dk" },
                new Customer { Name = "Thomas Thomassen", Email = "thomasthomassen@test.dk" },
                new Customer { Name = "Martin Martinsen", Email = "martinmartinsen@test.dk" },

            };
            _context.Customers.AddRange(customers);
            _context.SaveChanges();

            // Seed Users (linked to customers)
            var users = new List<User>
            {
                new User { Email = "sorensorensen@test.dk", PasswordHash = BCrypt.Net.BCrypt.HashPassword("password"), CustomerId = customers[0].Id },
                new User { Email = "jornjorgensen@test.dk", PasswordHash = BCrypt.Net.BCrypt.HashPassword("password"), CustomerId = customers[1].Id },
                new User { Email = "hanshansen@test.dk", PasswordHash = BCrypt.Net.BCrypt.HashPassword("password"), CustomerId = customers[2].Id },
                new User { Email = "larslarsen@test.dk", PasswordHash = BCrypt.Net.BCrypt.HashPassword("password"), CustomerId = customers[3].Id },
                new User { Email = "peterpetersen@test.dk", PasswordHash = BCrypt.Net.BCrypt.HashPassword("password"), CustomerId = customers[4].Id },
                new User { Email = "oleolsen@test.dk", PasswordHash = BCrypt.Net.BCrypt.HashPassword("password"), CustomerId = customers[5].Id },
                new User { Email = "karlkarlsen@test.dk", PasswordHash = BCrypt.Net.BCrypt.HashPassword("password"), CustomerId = customers[6].Id },
                new User { Email = "mortenmortensen@test.dk", PasswordHash = BCrypt.Net.BCrypt.HashPassword("password"), CustomerId = customers[7].Id },
                new User { Email = "frederikfrederiksen@test.dk", PasswordHash = BCrypt.Net.BCrypt.HashPassword("password"), CustomerId = customers[8].Id },
                new User { Email = "nielsnielsen@test.dk", PasswordHash = BCrypt.Net.BCrypt.HashPassword("password"), CustomerId = customers[9].Id },
                new User { Email = "andersandersen@test.dk", PasswordHash = BCrypt.Net.BCrypt.HashPassword("password"), CustomerId = customers[10].Id },
                new User { Email = "poulpoulsen@test.dk", PasswordHash = BCrypt.Net.BCrypt.HashPassword("password"), CustomerId = customers[11].Id },
                new User { Email = "jensjensen@test.dk", PasswordHash = BCrypt.Net.BCrypt.HashPassword("password"), CustomerId = customers[12].Id },
                new User { Email = "erikeriksen@test.dk", PasswordHash = BCrypt.Net.BCrypt.HashPassword("password"), CustomerId = customers[13].Id },
                new User { Email = "madsmadsen@test.dk", PasswordHash = BCrypt.Net.BCrypt.HashPassword("password"), CustomerId = customers[14].Id },
                new User { Email = "kristiankristiansen@test.dk", PasswordHash = BCrypt.Net.BCrypt.HashPassword("password"), CustomerId = customers[15].Id },
                new User { Email = "michaelmichaelsen@test.dk", PasswordHash = BCrypt.Net.BCrypt.HashPassword("password"), CustomerId = customers[16].Id },
                new User { Email = "henrikhenriksen@test.dk", PasswordHash = BCrypt.Net.BCrypt.HashPassword("password"), CustomerId = customers[17].Id },
                new User { Email = "thomasthomassen@test.dk", PasswordHash = BCrypt.Net.BCrypt.HashPassword("password"), CustomerId = customers[18].Id },
                new User { Email = "martinmartinsen@test.dk", PasswordHash = BCrypt.Net.BCrypt.HashPassword("password"), CustomerId = customers[19].Id }
            };
            _context.Users.AddRange(users);
            _context.SaveChanges();
        }

        if (!_context.Beverages.Any())
        {
            var beverages = new List<Beverage>
            {
                new Beverage { Name = "Faxe Kondi", Description = "Refreshing lemon-lime soda", Price = 15.99m, Size = SizeEnum.MediumCan },
                new Beverage { Name = "Ceres Top", Description = "Classic Danish pilsner beer", Price = 20.50m, Size = SizeEnum.MediumBottle },
                new Beverage { Name = "Royal Pilsner", Description = "Light and crisp pilsner beer", Price = 18.00m, Size = SizeEnum.MediumBottle },
                new Beverage { Name = "Royal Export", Description = "Strong lager with a rich taste", Price = 19.50m, Size = SizeEnum.LargeBottle },
                new Beverage { Name = "Royal Classic", Description = "Smooth, amber lager", Price = 17.50m, Size = SizeEnum.MediumBottle },
                new Beverage { Name = "Royal Organic", Description = "Organic pilsner with a mild taste", Price = 22.00m, Size = SizeEnum.SmallBottle },
                new Beverage { Name = "Faxe Kondi Booster", Description = "Energy drink with a lemon-lime flavor", Price = 12.99m, Size = SizeEnum.SmallCan },
                new Beverage { Name = "Egekilde Citrus", Description = "Sparkling water with a hint of citrus", Price = 10.00m, Size = SizeEnum.SmallBottle },
                new Beverage { Name = "Egekilde Elderflower", Description = "Sparkling water with elderflower taste", Price = 10.00m, Size = SizeEnum.SmallBottle },
                new Beverage { Name = "Albani Odense Classic", Description = "Full-bodied dark lager", Price = 18.50m, Size = SizeEnum.MediumBottle },
                new Beverage { Name = "Albani Giraf Beer", Description = "Strong beer with a distinct flavor", Price = 24.00m, Size = SizeEnum.XLargeBottle },
                new Beverage { Name = "Albani Mosaic IPA", Description = "Hoppy IPA with tropical notes", Price = 25.00m, Size = SizeEnum.MediumBottle },
                new Beverage { Name = "Faxe Kondi Free", Description = "Sugar-free version of the classic soda", Price = 15.99m, Size = SizeEnum.SmallCan },
                new Beverage { Name = "Pepsi Max", Description = "Sugar-free cola", Price = 16.50m, Size = SizeEnum.MediumCan },
                new Beverage { Name = "Royal Beer", Description = "Rich, full-bodied lager", Price = 18.99m, Size = SizeEnum.LargeBottle }
            };
            _context.Beverages.AddRange(beverages);
            _context.SaveChanges();
        }

        if (!_context.Orders.Any())
        {
            var random = new Random();
            var oneYearAgo = DateTime.Now.AddYears(-1);
            var statuses = Enum.GetValues(typeof(StatusEnum));

            var orders = new List<Order>();
            var orderItems = new List<OrderItem>();

            // Generating 100 synthetic orders
            for (int i = 0; i < 100; i++)
            {
                // Select random customer
                var customer = _context.Customers
                    .AsEnumerable() // Switch to client-side evaluation
                    .OrderBy(c => random.Next())
                    .First();

                // Random order date between last year and now
                var orderDate = oneYearAgo.AddDays(random.Next(0, 365));

                // 95% chance for Completed, 5% for other statuses closer to DateTime.Now
                StatusEnum status;
                if (random.NextDouble() < 0.95)
                {
                    status = StatusEnum.Completed;
                }
                else
                {
                    status = (StatusEnum)(statuses.GetValue(random.Next(0, 4)) ?? StatusEnum.Pending); // Pending to Shipped
                    orderDate = DateTime.Now.AddDays(-random.Next(1, 30)); // Closer to now
                }

                // Update the order creation to use the StatusEnum directly
                var order = new Order
                {
                    CustomerId = customer.Id,
                    OrderDate = orderDate,
                    Status = status, // Use the StatusEnum directly
                    TotalAmount = 0m // To be calculated based on order items
                };

                _context.Orders.Add(order);
                _context.SaveChanges(); // Save to generate the OrderId

                // Generate between 1 and 5 order items per order
                int numOrderItems = random.Next(1, 6);
                decimal totalAmount = 0;

                for (int j = 0; j < numOrderItems; j++)
                {
                    // Select random beverage
                    var beverage = _context.Beverages
                        .AsEnumerable() // Switch to client-side evaluation
                        .OrderBy(b => random.Next())
                        .First();
                    var quantity = random.Next(1, 21); // Quantity between 1 and 20

                    var orderItemPrice = beverage.Price * quantity;

                    var orderItem = new OrderItem
                    {
                        OrderId = order.Id,
                        BeverageId = beverage.Id,
                        Quantity = quantity,
                        Price = orderItemPrice // Calculate the price based on quantity and beverage price
                    };

                    orderItems.Add(orderItem);

                    // Accumulate total amount for the order
                    totalAmount += orderItemPrice;
                }

                // Update the order total amount and save it
                order.TotalAmount = totalAmount;
                _context.Orders.Update(order);
            }

            _context.OrderItems.AddRange(orderItems);
            _context.SaveChanges();
        }
    }
}
