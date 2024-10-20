import React, { useState } from 'react';
import api from '../../services/api';

const AddBeverage = () => {
  const [name, setName] = useState('');
  const [description, setDescription] = useState('');
  const [price, setPrice] = useState('');
  const [size, setSize] = useState('SmallBottle');  // Default size

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      // Make POST request to the API to add a new beverage
      await api.post('/beverages', {
        name,
        description,
        price: parseFloat(price),
        size
      });
      alert('Beverage added successfully!');
      setName('');  // Clear form fields after successful submission
      setDescription('');
      setPrice('');
      setSize('SmallBottle');
    } catch (error) {
      console.error('Error adding beverage:', error);
    }
  };

  return (
    <div>
      <h2>Add New Beverage</h2>
      <form onSubmit={handleSubmit}>
        <input
          type="text"
          placeholder="Beverage Name"
          value={name}
          onChange={(e) => setName(e.target.value)}
          required
        />
        <textarea
          placeholder="Beverage Description"
          value={description}
          onChange={(e) => setDescription(e.target.value)}
          required
        />
        <input
          type="number"
          placeholder="Price"
          value={price}
          onChange={(e) => setPrice(e.target.value)}
          required
        />
        <select value={size} onChange={(e) => setSize(e.target.value)}>
          <option value="SmallBottle">Small Bottle</option>
          <option value="MediumBottle">Medium Bottle</option>
          <option value="LargeBottle">Large Bottle</option>
          <option value="SmallCan">Small Can</option>
          <option value="MediumCan">Medium Can</option>
        </select>
        <button type="submit">Add Beverage</button>
      </form>
    </div>
  );
};

export default AddBeverage;
