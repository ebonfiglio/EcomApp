import './App.css';
import 'bootstrap/dist/css/bootstrap.min.css';
import Navbar from './components/navbar/Navbar';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import OrderList from './components/order/OrderList';
import ProductList from './components/product/ProductList';
import Admin from './components/admin/Admin';
import HomePage from './components/home/HomePage';

function App() {
    return (
        <Router>
            <Navbar />
            <div className="container mt-4">
                <Routes>
                    <Route path="/" element={<HomePage />} />
                    <Route path="/products" element={<ProductList />} />
                    <Route path="/orders" element={<OrderList />} />
                    <Route path="/admin/*" element={<Admin />} />
                </Routes>
            </div>
        </Router>
    );
}

export default App;