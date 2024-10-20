import React, { useState, useEffect } from 'react';
import api from '../../services/api';

const Orders = () => {
  const [orders, setOrders] = useState([]);

  useEffect(() => {
    const fetchOrders = async () => {
      try {
        const response = await api.get('/orders');  // Fetch orders from API
        setOrders(response.data);
      } catch (error) {
        console.error('Error fetching orders:', error);
      }
    };

    fetchOrders();
  }, []);

  return (
    <div>
      <h2>Customer Orders</h2>
      <ul>
        {orders.map(order => (
          <li key={order.id}>
            Order #{order.id}, Total: ${order.totalAmount}, Status: {order.status}
            <ul>
              {order.orderItems.map(item => (
                <li key={item.id}>
                  {item.beverage.name} - {item.quantity} x ${item.price.toFixed(2)}
                </li>
              ))}
            </ul>
          </li>
        ))}
      </ul>
    </div>
  );
};

export default Orders;
