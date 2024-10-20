import React, { useState, useEffect } from 'react';
import api from '../services/api';

const Dashboard = () => {
  const [beverages, setBeverages] = useState([]);
  const [orders, setOrders] = useState([]);

  useEffect(() => {
    const fetchData = async () => {
      try {
        const beverageResponse = await api.get('/beverages');
        setBeverages(beverageResponse.data);

        const orderResponse = await api.get('/orders');
        setOrders(orderResponse.data);
      } catch (err) {
        console.error('Error fetching data', err);
      }
    };

    fetchData();
  }, []);

  const placeOrder = async (beverageId) => {
    try {
      await api.post('/orders', {
        orderItems: [{ beverageId, quantity: 1 }]
      });
      alert('Order placed successfully!');
    } catch (err) {
      console.error('Error placing order', err);
    }
  };

  return (
    <div>
      <h2>Available Beverages</h2>
      <ul>
        {beverages.map(beverage => (
          <li key={beverage.id}>
            {beverage.name} - ${beverage.price}
            <button onClick={() => placeOrder(beverage.id)}>Order</button>
          </li>
        ))}
      </ul>

      <h2>Your Orders</h2>
      <ul>
        {orders.map(order => (
          <li key={order.id}>
            Order #{order.id}, Total: ${order.totalAmount}, Status: {order.status}
          </li>
        ))}
      </ul>
    </div>
  );
};

export default Dashboard;
