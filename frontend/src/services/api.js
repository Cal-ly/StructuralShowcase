import axios from 'axios';

const API_URL = 'https://<your-backend-api-url>'; // Replace with your API URL

const api = axios.create({
  baseURL: API_URL,
});

// Add token to Authorization header if it exists
api.interceptors.request.use(
  (config) => {
    const token = localStorage.getItem('token');
    if (token) {
      config.headers['Authorization'] = `Bearer ${token}`;
    }
    return config;
  },
  (error) => {
    return Promise.reject(error);
  }
);

export default api;