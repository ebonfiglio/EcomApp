// components/ManageProducts.tsx
import React, { useState, useEffect } from 'react';
import { Product } from '../../types/Product';
import { getAllProducts, createProduct, updateProduct, deleteProduct } from '../../services/ProductService';

const ManageProducts: React.FC = () => {
    const [products, setProducts] = useState<Product[]>([]);
    const [showModal, setShowModal] = useState(false);
    const [currentProduct, setCurrentProduct] = useState<Product | null>(null);

    useEffect(() => {
        fetchProducts();
    }, []);

    const fetchProducts = async () => {
        const data = await getAllProducts();
        setProducts(data);
    };

    const handleSave = async (e: React.FormEvent) => {
        e.preventDefault();
        if (currentProduct) {
            if (currentProduct.id) {
                await updateProduct(currentProduct.id, currentProduct);
            } else {
                await createProduct(currentProduct);
            }
            fetchProducts();
            setShowModal(false);
        }
    };

    const handleDelete = async (id: string) => {
        if (window.confirm('Are you sure you want to delete this product?')) {
            await deleteProduct(id);
            fetchProducts();
        }
    };

    const showModalForEdit = (product: Product) => {
        setCurrentProduct(product);
        setShowModal(true);
    };

    const showModalForAdd = () => {
        setCurrentProduct({ id: '', name: '', description: '', price: 0 });
        setShowModal(true);
    };

    return (
        <div>
            <h1>Manage Products</h1>
            <button className="btn btn-primary" onClick={() => showModalForAdd()}>
                Add Product
            </button>
            <table className="table table-striped">
                <thead>
                    <tr>
                        <th>Name</th>
                        <th>Description</th>
                        <th>Price</th>
                        <th>Actions</th>
                    </tr>
                </thead>
                <tbody>
                    {products.map((product) => (
                        <tr key={product.id}>
                            <td>{product.name}</td>
                            <td>{product.description}</td>
                            <td>${product.price}</td>
                            <td>
                                <button className="btn btn-info" onClick={() => showModalForEdit(product)}>Edit</button>
                                {' '}
                                <button className="btn btn-danger" onClick={() => handleDelete(product.id)}>Delete</button>
                            </td>
                        </tr>
                    ))}
                </tbody>
            </table>

            {showModal && (
                <div className="modal show" style={{ display: 'block' }} role="dialog">
                    <div className="modal-dialog">
                        <div className="modal-content">
                            <div className="modal-header">
                                <h5 className="modal-title">{currentProduct?.id ? 'Edit Product' : 'Add Product'}</h5>
                                <button type="button" className="close" aria-label="Close" onClick={() => setShowModal(false)}>
                                    <span aria-hidden="true">&times;</span>
                                </button>
                            </div>
                            <form onSubmit={handleSave}>
                                <div className="modal-body">
                                    <div className="form-group">
                                        <label>Name</label>
                                        <input
                                            type="text"
                                            className="form-control"
                                            value={currentProduct?.name || ''}
                                            onChange={(e: React.ChangeEvent<HTMLInputElement>) => setCurrentProduct(currentProduct ? { ...currentProduct, name: e.target.value } : null)}
                                            required
                                        />
                                    </div>
                                    <div className="form-group">
                                        <label>Description</label>
                                        <input
                                            type="text"
                                            className="form-control"
                                            value={currentProduct?.description || ''}
                                            onChange={(e: React.ChangeEvent<HTMLInputElement>) => setCurrentProduct(currentProduct ? { ...currentProduct, description: e.target.value } : null)}
                                            required
                                        />
                                    </div>
                                    <div className="form-group">
                                        <label>Price</label>
                                        <input
                                            type="number"
                                            className="form-control"
                                            value={currentProduct?.price || 0}
                                            onChange={(e: React.ChangeEvent<HTMLInputElement>) => setCurrentProduct(currentProduct ? { ...currentProduct, price: parseFloat(e.target.value) } : null)}
                                            required
                                        />
                                    </div>
                                </div>
                                <div className="modal-footer">
                                    <button type="button" className="btn btn-secondary" onClick={() => setShowModal(false)}>Close</button>
                                    <button type="submit" className="btn btn-primary">Save Changes</button>
                                </div>
                            </form>
                        </div>
                    </div>
                </div>
            )}
            {showModal && <div className="modal-backdrop show"></div>}
        </div>
    );
};

export default ManageProducts;
