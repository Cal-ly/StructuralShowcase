import React, { useState, useEffect } from 'react';
import { getTopBeverages, getTotalSales, getTopCustomers, getMonthlyRevenue, getSalesBySize } from '../../services/api';
import MonthlyRevenueChart from '../../components/MonthlyRevenueChart';
import SalesBySizeChart from '../../components/SalesBySizeChart';
import TopBeveragesChart from '../../components/TopBeveragesChart';
import TopCustomerChart from '../../components/TopCustomerChart';

const Analytics = () => {
  const [topBeverages, setTopBeverages] = useState([]);
  const [totalSales, setTotalSales] = useState(0);
  const [topCustomers, setTopCustomers] = useState([]);
  const [monthlyRevenue, setMonthlyRevenue] = useState([]);
  const [salesBySize, setSalesBySize] = useState([]);

  useEffect(() => {
    // Fetch top beverages
    const fetchTopBeverages = async () => {
      const response = await getTopBeverages();
      setTopBeverages(response.data);
    };

    // Fetch total sales
    const fetchTotalSales = async () => {
      const response = await getTotalSales();
      setTotalSales(response.data);
    };

    // Fetch top customers
    const fetchTopCustomers = async () => {
      const response = await getTopCustomers();
      setTopCustomers(response.data);
    };

    // Fetch monthly revenue (current year)
    const fetchMonthlyRevenue = async () => {
      const year = new Date().getFullYear();
      const response = await getMonthlyRevenue(year);
      setMonthlyRevenue(response.data);
    };

    // Fetch sales by size
    const fetchSalesBySize = async () => {
      const response = await getSalesBySize();
      setSalesBySize(response.data);
    };

    fetchTopBeverages();
    fetchTotalSales();
    fetchTopCustomers();
    fetchMonthlyRevenue();
    fetchSalesBySize();
  }, []);

  return (
    <div className="container mt-5">
      <h2 className="mb-4">Analytics Dashboard</h2>

      {/* Total Sales */}
      <section className="mb-5">
        <h3>Total Sales</h3>
        <p className="lead">${totalSales.toFixed(2)}</p>
      </section>

      {/* Top Beverages Chart */}
      <section className="mb-5">
        <h3>Top Beverages</h3>
        <div className="card">
          <div className="card-body">
            <TopBeveragesChart topBeverages={topBeverages} />
          </div>
        </div>
      </section>

      {/* Top Customers */}
      <section className="mb-5">
        <h3>Top Customers</h3>
        <div className="card">
          <div className="card-body">
            <TopCustomerChart topCustomers={topCustomers} />
          </div>
        </div>
      </section>

      {/* Monthly Revenue Chart */}
      <section className="mb-5">
        <h3>Monthly Revenue</h3>
        <div className="card">
          <div className="card-body">
            <MonthlyRevenueChart monthlyRevenue={monthlyRevenue} />
          </div>
        </div>
      </section>

      {/* Sales by Size Chart */}
      <section className="mb-5">
        <h3>Sales by Size</h3>
        <div className="card">
          <div className="card-body">
            <SalesBySizeChart salesBySize={salesBySize} />
          </div>
        </div>
      </section>
    </div>
  );
};

export default Analytics;
