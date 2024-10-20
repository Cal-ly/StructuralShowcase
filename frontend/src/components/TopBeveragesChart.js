import React from 'react';
import { Bar } from 'react-chartjs-2';
import { Chart as ChartJS, CategoryScale, LinearScale, BarElement, Title, Tooltip, Legend } from 'chart.js';

// Register Chart.js components
ChartJS.register(CategoryScale, LinearScale, BarElement, Title, Tooltip, Legend);

const TopBeveragesChart = ({ topBeverages }) => {
  const data = {
    labels: topBeverages.map(beverage => beverage.name),
    datasets: [
      {
        label: 'Units Sold',
        data: topBeverages.map(beverage => beverage.totalQuantity),
        backgroundColor: 'rgba(255, 159, 64, 0.6)',
        borderColor: 'rgba(255, 159, 64, 1)',
        borderWidth: 1,
      },
    ],
  };

  const options = {
    responsive: true,
    plugins: {
      legend: {
        display: false,
      },
      title: {
        display: true,
        text: 'Top Beverages by Sales',
      },
    },
  };

  return <Bar data={data} options={options} />;
};

export default TopBeveragesChart;
