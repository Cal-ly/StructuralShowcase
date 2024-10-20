import React, { useState, useEffect } from 'react';
import api from '../../services/api';

const Beverages = () => {
  const [beverages, setBeverages] = useState([]);

  // Fetch beverages from the API when the component loads
  useEffect(() => {
    const fetchBeverages = async () => {
      try {
        const response = await api.get('/beverages');  // Make API call to fetch beverages
        setBeverages(response.data);  // Set the fetched data to the state
      } catch (error) {
        console.error('Error fetching beverages:', error);
      }
    };

    fetchBeverages();
  }, []);  // Empty dependency array means this runs once when the component mounts

  return (
    <div>
      <h2>Available Beverages</h2>
      <ul>
        {beverages.map(beverage => (
          <li key={beverage.id}>
            {beverage.name} - ${beverage.price.toFixed(2)}
            <p>{beverage.description}</p>
          </li>
        ))}
      </ul>
    </div>
  );
};

export default Beverages;
