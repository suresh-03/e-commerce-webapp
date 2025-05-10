import { getCategoryFromURL, getIdFromURL} from "../utils/commonUtil.js";
import { getStoredData } from "../utils/localStorageUtil.js";


function renderProductPage(){
	const category = getCategoryFromURL();
	const id = getIdFromURL();

	const products = getStoredData(category);

	createElements(id,products);

}


function createElements(id,products){

	const productId = parseInt(id);

	const product = products[productId-1];
	console.log(product);

	
	document.getElementById("product-img").src = product.imageURL;
	document.getElementById("product-name").textContent = product.name;
	document.getElementById("product-details").textContent = product.description;
	document.getElementById("product-price").textContent = `$${product.price}`;
	document.getElementById("star-ratings").textContent = `${product.ratingCount} ratings`;

}


renderProductPage();

