import React from 'react';
import { Link } from 'react-router-dom';

const Navbar = () => {
return (
    <nav className="navbar navbar-expand-lg navbar-light bg-light">
        <div className="container-fluid">
            <Link className="navbar-brand" to="/">Kongebryg Online</Link>
            <button className="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarNav" aria-controls="navbarNav" aria-expanded="false" aria-label="Toggle navigation">
                <span className="navbar-toggler-icon"></span>
            </button>
            <div className="collapse navbar-collapse" id="navbarNav">
                <ul className="navbar-nav">
                    <li className="nav-item">
                        <Link className="nav-link" to="/">Home</Link>
                    </li>
                    <li className="nav-item">
                        <Link className="nav-link" to="/beverages">View Beverages</Link>
                    </li>
                    <li className="nav-item">
                        <Link className="nav-link" to="/add-beverage">Add Beverage</Link>
                    </li>
                    <li className="nav-item">
                        <Link className="nav-link" to="/orders">View Orders</Link>
                    </li>
                    <li className="nav-item">
                        <Link className="nav-link" to="/analytics">View Analytics</Link>
                    </li>
                </ul>
            </div>
        </div>
    </nav>
);
};

export default Navbar;
