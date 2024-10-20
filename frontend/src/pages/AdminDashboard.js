import React, { useState, useEffect } from 'react';
import api from '../services/api';

const AdminDashboard = () => {
  const [orders, setOrders] = useState([]);
  const [analytics, setAnalytics] = useState({ totalSales: 0, topBeverages: [] });

  useEffect(() => {
    const fetchAdminData = async () => {
      try {
        const ordersResponse = await api.get('/orders');
        setOrders(ordersResponse.data);

        const salesResponse = await api.get('/analytics/total-sales');
        const topBeveragesResponse = await api.get('/analytics/top-beverages');

        setAnalytics({
          totalSales: salesResponse.data,
          topBeverages: topBeveragesResponse.data
        });
      } catch (err) {
        console.error('Error fetching admin data', err);
      }
    };

    fetchAdminData();
  }, []);

  return (
    <div>
      <h2>Admin Dashboard</h2>
      <h3>Total Sales: ${analytics.totalSales}</h3>

      <h3>Top Beverages</h3>
      <ul>
        {analytics.topBeverages.map(beverage => (
          <li key={beverage.id}>{beverage.name} - Sold: {beverage.totalQuantity}</li>
        ))}
      </ul>

      <h3>All Orders</h3>
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

export default AdminDashboard;
