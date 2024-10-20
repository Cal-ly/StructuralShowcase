import React from 'react';
import { Bar } from 'react-chartjs-2';
import { Chart as ChartJS, CategoryScale, LinearScale, BarElement, Title, Tooltip, Legend } from 'chart.js';

// Register Chart.js components
ChartJS.register(CategoryScale, LinearScale, BarElement, Title, Tooltip, Legend);

const TopCustomerChart = ({ topCustomers }) => {
  const data = {
    labels: topCustomers.map(customer => customer.name), // Labels will be customer names
    datasets: [
      {
        label: 'Total Spent (dkk)',
        data: topCustomers.map(customer => customer.totalSpent), // Data points are the total amounts spent by customers
        backgroundColor: 'rgba(54, 162, 235, 0.6)', // Light blue background for bars
        borderColor: 'rgba(54, 162, 235, 1)', // Darker blue border for bars
        borderWidth: 1,
      },
    ],
  };

  const options = {
    responsive: true,
    plugins: {
      legend: {
        display: true,
        position: 'top',
      },
      title: {
        display: true,
        text: 'Top Customers by Spending',
      },
    },
  };

  return <Bar data={data} options={options} />;
};

export default TopCustomerChart;
