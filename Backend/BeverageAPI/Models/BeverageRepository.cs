namespace BeverageAPI.Models;

public class BeverageRepository(BeverageContext context)
{

    // Add a new Beverage to the repository
    public void Add(Beverage beverage)
    {
        if (beverage == null)
        {
            throw new ArgumentNullException(nameof(beverage), "Beverage cannot be null.");
        }

        beverage.Validate(); // Validate the beverage before adding
        context.Beverages.Add(beverage);
        context.SaveChanges();
    }

    // Get all Beverages from the repository
    public List<Beverage> GetAll()
    {
        return context.Beverages.ToList();
    }

    // Get a specific Beverage by Id
    public Beverage? GetById(int id)
    {
        return context.Beverages.FirstOrDefault(b => b.Id == id);
    }

    // Update an existing Beverage in the repository
    public void Update(Beverage beverage)
    {
        if (beverage == null)
        {
            throw new ArgumentNullException(nameof(beverage), "Beverage cannot be null.");
        }

        var existingBeverage = GetById(beverage.Id);
        if (existingBeverage == null)
        {
            throw new ArgumentException("Beverage with the given Id does not exist.");
        }

        beverage.Validate(); // Validate the beverage before updating

        // Update properties
        existingBeverage.Brand = beverage.Brand;
        existingBeverage.Description = beverage.Description;
        existingBeverage.Price = beverage.Price;
        existingBeverage.Size = beverage.Size;

        context.Beverages.Update(existingBeverage);
        context.SaveChanges();
    }

    // Remove a Beverage from the repository by Id
    public void Remove(int id)
    {
        var beverageToRemove = GetById(id);
        if (beverageToRemove == null)
        {
            throw new ArgumentException("Beverage with the given Id does not exist.");
        }

        context.Beverages.Remove(beverageToRemove);
        context.SaveChanges();
    }

    // Check if a Beverage exists by Id
    public bool Exists(int id)
    {
        return context.Beverages.Any(b => b.Id == id);
    }
}
