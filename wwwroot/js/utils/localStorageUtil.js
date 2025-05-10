// Remove all localStorage data
export function removeAllData() {
  localStorage.clear();
}


export function getStoredData(key) {
  try {
    const data = localStorage.getItem(key);
    console.log(`Data for key: ${key} is retrieved`);
    return data ? JSON.parse(data) : null;
  } catch (e) {
    console.error("Error while getting data from localStorage: ", e);
    return null;
  }
}

// Store object/array as string
export function storeJsonData(key, value) {
  try {
    localStorage.setItem(key, JSON.stringify(value));
    console.log(`Data stored under the key: ${key}`);
  } catch (e) {
    console.error("Error storing data in localStorage:", e);
  }
}

// Remove key from localStorage
export function removeData(key) {
  try {
    localStorage.removeItem(key);
    console.log(`Data removed under the key: ${key}`);
  } catch (e) {
    console.error("Error removing data from localStorage:", e);
  }
}