import { storeJsonData, getStoredData } from './localStorageUtil.js';

export function getCategoryFromURL() {
  const urlParams = new URLSearchParams(window.location.search);
  const category = urlParams.get('category');
  console.log(category);
  return category;
}

export function getIdFromURL() {
  const urlParams = new URLSearchParams(window.location.search);
  const id = urlParams.get('id');
  console.log(id);
  return id;
}

// --- Product Factory Function ---
function createProduct(id, name, description, price, rating, ratingCount, isFavorite, isAddedToCart, imageURL) {
    return {
        id,
        name,
        description,
        price,
        rating,
        ratingCount,
        isFavorite,
        isAddedToCart,
        imageURL
    };
}

// --- Insert a New Category ---
export function InsertNewCategory(category) {
    const existing = getStoredData(category);
    if (existing.length === 0) {
        storeJsonData(category, []);
    }
}

// --- Insert a New Product ---
export function InsertNewProduct(category, name, description, price, rating, ratingCount, isFavorite, isAddedToCart, imageURL) {
    const products = getStoredData(category);

    const newId = products.length > 0 ? products[products.length - 1].id + 1 : 1;

    const product = createProduct(newId, name, description, price, rating, ratingCount, isFavorite, isAddedToCart, imageURL);

    products.push(product);
    storeJsonData(category, products);
}

// --- Update an Existing Product by ID ---
export function UpdateProduct(category, id, name, description, price, rating, ratingCount, isFavorite, isAddedToCart, imageURL) {
    const products = getStoredData(category);
    const index = products.findIndex(p => p.id === id);

    if (index === -1) {
        throw new Error(`Product with ID ${id} not found in category '${category}'`);
    }

    products[index] = createProduct(id, name, description, price, rating, ratingCount, isFavorite, isAddedToCart, imageURL);
    storeJsonData(category, products);
}