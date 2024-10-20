import React, { useState } from 'react';
import api from '../services/api';

const OrderStatusForm = ({ order, onSave, onCancel }) => {
  const [status, setStatus] = useState(order.status);

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      await api.put(`/orders/${order.id}/status`, { status });
      onSave();  // Refresh the order list after saving
    } catch (err) {
      console.error('Error updating order status', err);
    }
  };

  return (
    <form onSubmit={handleSubmit}>
      <select value={status} onChange={(e) => setStatus(e.target.value)}>
        <option value="Pending">Pending</option>
        <option value="Shipped">Shipped</option>
        <option value="Delivered">Delivered</option>
      </select>
      <button type="submit">Save Status</button>
      <button type="button" onClick={onCancel}>Cancel</button>
    </form>
  );
};

export default OrderStatusForm;
