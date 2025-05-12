import { getCategoryFromURL, getIdFromURL} from "../utils/commonUtil.js";
import { getStoredData, storeJsonData } from "../utils/localStorageUtil.js";


function renderProductPage(){
	const category = getCategoryFromURL();
	const id = getIdFromURL();

	const products = getStoredData(category);

	createElements(category,id,products);

}


function createElements(category,id,products){

	const productId = parseInt(id);

	const product = products[productId-1];
	console.log(product);

	
	document.getElementById("product-img").src = product.imageURL;
	document.getElementById("product-name").textContent = product.name;
	document.getElementById("product-details").textContent = product.description;
	document.getElementById("product-price").textContent = `$${product.price}`;
	document.getElementById("ratings-count").textContent = `${product.ratingCount} ratings`;

	const avgRating = Math.round(product.rating / product.ratingCount);

	renderStarRatings(avgRating);

	const cartBtn = document.getElementById("cart-btn");
	cartBtn.dataset.id = productId;
	cartBtn.dataset.category = category;

	renderCartButton(product);

	const favoriteBtn = document.getElementById("favorite-btn");
	favoriteBtn.dataset.id = productId;
	favoriteBtn.dataset.category = category;

  	renderFavoriteButton(product);

	cartBtn.addEventListener("click", toggleCart);
 	favoriteBtn.addEventListener("click", toggleFavorite);

}

function renderCartButton(product){
	const span = document.getElementById("cart-icon");
	if(product.isAddedToCart){
		span.classList.remove("add-to-cart");
		span.classList.add("remove-from-cart");
		span.textContent = "remove_shopping_cart";

		document.getElementById("cart-content").textContent = "Remove from Cart";
	}
	else{
		span.classList.remove("remove-from-cart");
		span.classList.add("add-to-cart");
		span.textContent = "add_shopping_cart";

		document.getElementById("cart-content").textContent = "Add to Cart";
	}
}

function renderFavoriteButton(product){
	const span = document.getElementById("favorite-icon");
	const favoriteContainer = document.getElementById("favorite-container");
	if(product.isFavorite){
		favoriteContainer.classList.remove("not-favorite-container");
		favoriteContainer.classList.add("favorite-container");
		span.classList.add("favorite-icon");

		document.getElementById("favorite-content").textContent = "Favorite";
	}
	else{
		favoriteContainer.classList.remove("favorite-container");
		favoriteContainer.classList.add("not-favorite-container");
		span.classList.remove("favorite-icon");

		document.getElementById("favorite-content").textContent = "Add to Favorites";
	}
}

function renderStarRatings(avgRating){
	const star1 = document.getElementById("star-1");
	const star2 = document.getElementById("star-2");
	const star3 = document.getElementById("star-3");
	const star4 = document.getElementById("star-4");
	const star5 = document.getElementById("star-5");

	if(avgRating >= 1){
		star1.classList.add("filled-star");
		star1.classList.remove("not-filled-star");
	}
	if(avgRating >= 2){
		star2.classList.add("filled-star");
		star2.classList.remove("not-filled-star");
	}
	if(avgRating >= 3){
		star3.classList.add("filled-star");
		star3.classList.remove("not-filled-star");
	}
	if(avgRating >= 4){
		star4.classList.add("filled-star");
		star4.classList.remove("not-filled-star");
	}
	if(avgRating >= 5){
		star5.classList.add("filled-star");
		star5.classList.remove("not-filled-star");
	}

}


function toggleCart(event) {
  const dataset = event.target.closest('a').dataset;
  const products = getStoredData(dataset.category);
  const product = products[dataset.id-1];

  const isAddedToCart = product.isAddedToCart;

  if(isAddedToCart){
  	product.isAddedToCart = false;
  }
  else{
  	product.isAddedToCart = true;
  }
  storeJsonData(dataset.category,products);
  renderCartButton(product);
}

function toggleFavorite(event) {
  const dataset = event.target.closest('a').dataset;
  const products = getStoredData(dataset.category);
  const product = products[dataset.id-1];

  const isFavorite = product.isFavorite;

  if(isFavorite){
  	product.isFavorite = false;
  }
  else{
  	product.isFavorite = true;
  }
  storeJsonData(dataset.category,products);
  renderFavoriteButton(product);
}





renderProductPage();

