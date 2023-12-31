// services/ProductService.ts
import axios from 'axios';
import { Product } from '../types/Product';

const API_URL = 'https://localhost:7146/product';

export const getAllProducts = async (): Promise<Product[]> => {
    const response = await axios.get<Product[]>(`${API_URL}`);
    return response.data;
};

export const getProduct = async (id: string): Promise<Product> => {
    const response = await axios.get<Product>(`${API_URL}/${id}`);
    return response.data;
};

export const createProduct = async (product: Product): Promise<Product> => {
    const response = await axios.post<Product>(`${API_URL}`, product);
    return response.data;
};

export const updateProduct = async (id: string, product: Product): Promise<void> => {
    await axios.put(`${API_URL}/${id}`, product);
};

export const deleteProduct = async (id: string): Promise<void> => {
    await axios.delete(`${API_URL}/${id}`);
};
