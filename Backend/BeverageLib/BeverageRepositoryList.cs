using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeverageLib;
public class BeverageRepositoryList
{
    private readonly List<Beverage> _beverages;

    public BeverageRepositoryList()
    {
        _beverages = new List<Beverage>();
    }

    // Add a new Beverage to the repository
    public void Add(Beverage beverage)
    {
        if (beverage == null)
        {
            throw new ArgumentNullException(nameof(beverage), "Beverage cannot be null.");
        }

        beverage.Validate(); // Validate the beverage before adding
        _beverages.Add(beverage);
    }

    // Get all Beverages from the repository
    public List<Beverage> GetAll()
    {
        return _beverages;
    }

    // Get a specific Beverage by Id
    public Beverage? GetById(int id)
    {
        return _beverages.FirstOrDefault(b => b.Id == id);
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
    }

    // Remove a Beverage from the repository by Id
    public void Remove(int id)
    {
        var beverageToRemove = GetById(id);
        if (beverageToRemove == null)
        {
            throw new ArgumentException("Beverage with the given Id does not exist.");
        }

        _beverages.Remove(beverageToRemove);
    }

    // Check if a Beverage exists by Id
    public bool Exists(int id)
    {
        return _beverages.Any(b => b.Id == id);
    }
}
