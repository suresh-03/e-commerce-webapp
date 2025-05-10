import { getStoredData } from "../utils/localStorageUtil.js";
import { getCategoryFromURL } from "../utils/commonUtil.js";


function renderProductsPage(){

  const category = getCategoryFromURL();
  const product = getStoredData(category);


  product.forEach((value,key) => {
    createElements(value,category);
  });

}


function createElements(product,category){

  const productImage = document.createElement("div");
  productImage.classList.add("product-image");

  const img = document.createElement("img");
  img.src = product.imageURL;

  productImage.appendChild(img);

  //-----------------------------------------------------------------

  const productInformation = document.createElement("div");
  productInformation.classList.add("product-information");

  const h4 = document.createElement("h4");
  h4.textContent = product.name;

  const p1 = document.createElement("p");
  p1.textContent = product.description;

  const p2 = document.createElement("p");
  p2.classList.add("product-price");
  p2.textContent = "$"+product.price;

  const ratings = document.createElement("div");
  ratings.classList.add("ratings");

  const rating = document.createElement("span");
  rating.classList.add("rating");
  const avgRating = Math.round(product.rating / product.ratingCount);
  rating.textContent = avgRating+"★";

  setBackgroundForRating(avgRating,rating);

  const ratingCount = document.createElement("span");
  ratingCount.classList.add("rating-count");
  ratingCount.textContent = `${product.ratingCount} ratings`;

  ratings.appendChild(rating)
  ratings.appendChild(ratingCount);

  productInformation.appendChild(h4);
  productInformation.appendChild(p1);
  productInformation.appendChild(p2);
  productInformation.appendChild(ratings);

  // ------------------------------------------------------------------

  const a = document.createElement("a");
  a.href = `product/details?category=${category}&id=${product.id}`;
  a.appendChild(productImage);
  a.appendChild(productInformation);

  // ------------------------------------------------------------------

  const productGrid = document.createElement("div");
  productGrid.classList.add("product-grid");

  productGrid.appendChild(a);

  // -------------------------------------------------------------------

  const productInnerContainer = document.getElementById("product-inner-container");

  productInnerContainer.appendChild(productGrid);


  // -------------------------------------------------------------------

}



function setBackgroundForRating(rating,ratingElement){
  switch(rating){
    case 1:
      ratingElement.style.backgroundColor = "tomato";
      ratingElement.style.color = "white";
      break;
    case 2:
      ratingElement.style.backgroundColor = "orange";
      ratingElement.style.color = "white";
      break;
    case 3:
      ratingElement.style.backgroundColor = "yellow";
      break;
    case 4:
      ratingElement.style.backgroundColor = "blue";
      ratingElement.style.color = "white";
      break;
    case 5:
      ratingElement.style.backgroundColor = "green";
      ratingElement.style.color = "white";
      break;
    default:
      ratingElement.style.backgroundColor = "green";
      ratingElement.style.color = "white";
      ratingElement.textContent = "5★";

  }
}


renderProductsPage();