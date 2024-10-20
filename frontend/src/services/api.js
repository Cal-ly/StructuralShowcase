import axios from 'axios';

// Set the base URL for the API
const api = axios.create({
  baseURL: 'https://beverageapi2024.azurewebsites.net/api',
});

// Interceptor to add the Authorization header with the JWT token
api.interceptors.request.use(config => {
  const token = localStorage.getItem('token');
  if (token) {
    config.headers.Authorization = `Bearer ${token}`;
  }
  return config;
});

export default api;
