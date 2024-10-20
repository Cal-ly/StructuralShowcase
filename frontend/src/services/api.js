import axios from 'axios';

// Set the base URL for the API
const api = axios.create({
  baseURL: 'https://beverageapi2024.azurewebsites.net/api',
});

export const getTopBeverages = () => api.get('/analytics/top-beverages');
export const getTotalSales = () => api.get('/analytics/total-sales');
export const getTopCustomers = () => api.get('/analytics/top-customers');
export const getMonthlyRevenue = (year) => api.get(`/analytics/monthly-revenue?year=${year}`);
export const getSalesBySize = () => api.get('/analytics/sales-by-size');

export default api;