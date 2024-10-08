namespace BeverageAPI.Models;

public class BeverageRepository(BeverageContext context)
{
    public List<Beverage> GetAll()
    {
        return [.. context.Beverages];
    }

    public Beverage? GetById(int id)
    {
        return context.Beverages.FirstOrDefault(b => b.Id == id);
    }

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

        beverage.Validate();

        existingBeverage.Brand = beverage.Brand;
        existingBeverage.Description = beverage.Description;
        existingBeverage.Price = beverage.Price;
        existingBeverage.Size = beverage.Size;

        context.Beverages.Update(existingBeverage);
        context.SaveChanges();
    }

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

    public bool Exists(int id)
    {
        return context.Beverages.Any(b => b.Id == id);
    }
}
