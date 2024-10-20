import React, { useState, useEffect } from 'react';
import api from '../services/api';
import BeverageForm from '../components/BeverageForm';
import OrderStatusForm from '../components/OrderStatusForm';

const AdminDashboard = () => {
  // States for beverages management
  const [beverages, setBeverages] = useState([]);
  const [selectedBeverage, setSelectedBeverage] = useState(null);
  const [isEditingBeverage, setIsEditingBeverage] = useState(false);

  // States for orders management
  const [orders, setOrders] = useState([]);
  const [selectedOrder, setSelectedOrder] = useState(null);
  const [isEditingOrder, setIsEditingOrder] = useState(false);

  // Fetch beverages and orders when the component mounts
  useEffect(() => {
    fetchBeverages();
    fetchOrders();
  }, []);

  // Fetch the list of beverages from the API
  const fetchBeverages = async () => {
    try {
      const response = await api.get('/beverages');
      setBeverages(response.data);
    } catch (err) {
      console.error('Error fetching beverages', err);
    }
  };

  // Fetch the list of orders from the API
  const fetchOrders = async () => {
    try {
      const response = await api.get('/orders');
      setOrders(response.data);
    } catch (err) {
      console.error('Error fetching orders', err);
    }
  };

  // Handle deleting a beverage
  const handleDeleteBeverage = async (id) => {
    try {
      await api.delete(`/beverages/${id}`);
      fetchBeverages();  // Refresh the list after deletion
    } catch (err) {
      console.error('Error deleting beverage', err);
    }
  };

  // Handle editing a beverage
  const handleEditBeverage = (beverage) => {
    setSelectedBeverage(beverage);
    setIsEditingBeverage(true);
  };

  // Handle adding a new beverage
  const handleAddBeverage = () => {
    setSelectedBeverage(null);  // Clear selection for new beverage
    setIsEditingBeverage(true);
  };

  // Handle updating order status
  const handleEditOrderStatus = (order) => {
    setSelectedOrder(order);
    setIsEditingOrder(true);
  };

  return (
    <div>
      <h2>Admin Dashboard</h2>

      {/* Beverage Management */}
      <div>
        <h3>Beverage Management</h3>
        <button onClick={handleAddBeverage}>Add Beverage</button>

        {isEditingBeverage ? (
          <BeverageForm
            beverage={selectedBeverage}
            onSave={() => {
              setIsEditingBeverage(false);
              fetchBeverages();  // Refresh the list after saving
            }}
            onCancel={() => setIsEditingBeverage(false)}
          />
        ) : (
          <ul>
            {beverages.map(beverage => (
              <li key={beverage.id}>
                {beverage.name} - ${beverage.price}
                <button onClick={() => handleEditBeverage(beverage)}>Edit</button>
                <button onClick={() => handleDeleteBeverage(beverage.id)}>Delete</button>
              </li>
            ))}
          </ul>
        )}
      </div>

      {/* Order Management */}
      <div>
        <h3>Order Management</h3>
        <ul>
          {orders.map(order => (
            <li key={order.id}>
              Order #{order.id}, Total: ${order.totalAmount}, Status: {order.status}
              <button onClick={() => handleEditOrderStatus(order)}>Update Status</button>
            </li>
          ))}
        </ul>

        {isEditingOrder && (
          <OrderStatusForm
            order={selectedOrder}
            onSave={() => {
              setIsEditingOrder(false);
              fetchOrders();  // Refresh the orders after saving
            }}
            onCancel={() => setIsEditingOrder(false)}
          />
        )}
      </div>
    </div>
  );
};

export default AdminDashboard;
