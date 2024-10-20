import React from 'react';
import { Link } from 'react-router-dom';

const Home = () => {
return (
    <div className="container mt-5">
        <h1 className="text-center mb-4">Welcome to the Brewery Management System</h1>
        <nav>
            <ul className="nav flex-column align-items-center">
                <li className="nav-item mb-2">
                    <Link className="nav-link btn btn-primary w-100" to="/beverages">View Beverages</Link>
                </li>
                <li className="nav-item mb-2">
                    <Link className="nav-link btn btn-secondary w-100" to="/add-beverage">Add Beverage</Link>
                </li>
                <li className="nav-item mb-2">
                    <Link className="nav-link btn btn-success w-100" to="/orders">View Orders</Link>
                </li>
                <li className="nav-item mb-2">
                    <Link className="nav-link btn btn-info w-100" to="/analytics">View Analytics</Link>
                </li>
            </ul>
        </nav>
    </div>
);
};

export default Home;
