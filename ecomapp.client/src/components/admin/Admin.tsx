// Admin.js

import { Route, Routes, Link } from 'react-router-dom';
import ManageOrders from './ManageOrders';
import ManageProducts from './ManageProducts';

function Admin() {
    return (
        <div className="container-fluid">
            <div className="row">
                <nav className="col-md-2 d-none d-md-block bg-light sidebar">
                    <ul className="nav flex-column">
                        <li className="nav-item">
                            <Link className="nav-link"  to="/admin/manage-orders">
                                Manage Orders
                            </Link>
                        </li>
                        <li className="nav-item">
                            <Link className="nav-link" to="/admin/manage-products">
                                Manage Products
                            </Link>
                        </li>
                    </ul>
                </nav>
                <main role="main" className="col-md-9 ml-sm-auto col-lg-10 px-md-4">
                    <Routes>
                    <Route path="/admin/manage-orders" element={<ManageOrders />} />
                     <Route path="/admin/manage-products" element={<ManageProducts />} />
                    </Routes>
                </main>
            </div>
        </div>
    );
}

export default Admin;
