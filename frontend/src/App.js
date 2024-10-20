import React from 'react';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import Navbar from './components/Navbar';
import Home from './pages/Home';
import Beverages from './pages/beverages/Beverages';
import AddBeverage from './pages/beverages/AddBeverage';
import Orders from './pages/orders/Orders';
import Analytics from './pages/analytics/Analytics';

function App() {
  return (
    <Router>
      <Navbar /> {/* Navbar displayed on all pages */}
      <Routes>
        <Route path="/" element={<Home />} />
        <Route path="/beverages" element={<Beverages />} />
        <Route path="/add-beverage" element={<AddBeverage />} />
        <Route path="/orders" element={<Orders />} />
        <Route path="/analytics" element={<Analytics />} />
      </Routes>
    </Router>
  );
}

export default App;
