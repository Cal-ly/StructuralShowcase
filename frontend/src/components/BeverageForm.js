import React, { useState, useEffect } from 'react';
import api from '../services/api';

const BeverageForm = ({ beverage, onSave, onCancel }) => {
  const [name, setName] = useState(beverage?.name || '');
  const [description, setDescription] = useState(beverage?.description || '');
  const [price, setPrice] = useState(beverage?.price || 0);
  const [size, setSize] = useState(beverage?.size || 'SmallBottle');  // Default to SmallBottle

  const handleSubmit = async (e) => {
    e.preventDefault();
    const newBeverage = { name, description, price, size };

    try {
      if (beverage) {
        await api.put(`/beverages/${beverage.id}`, newBeverage);  // Update
      } else {
        await api.post('/beverages', newBeverage);  // Create
      }
      onSave();  // Refresh the list after saving
    } catch (err) {
      console.error('Error saving beverage', err);
    }
  };

  return (
    <form onSubmit={handleSubmit}>
      <input
        type="text"
        placeholder="Name"
        value={name}
        onChange={(e) => setName(e.target.value)}
        required
      />
      <textarea
        placeholder="Description"
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
      <button type="submit">Save Beverage</button>
      <button type="button" onClick={onCancel}>Cancel</button>
    </form>
  );
};

export default BeverageForm;
